using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    /*
     * 오브젝트의 Instantiate, Destroy 및
     * 다른 리소스의 로드는 이 매니저를 통해 관리됨
     */

    public GameObject Instantiate(string path, Transform parent = null)
    {
                                                //자동위치 : Resource/Prefabs/
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"잘못된 경로 : {path}");
            return null;
        }

        //풀링용 오브젝트이면 풀에서 꺼내오기
        if (original.GetComponent<Poolable>() != null)
            return GameManager.Pool.Pop(original, parent).gameObject;

        //풀링용 오브젝트가 아니면 바로 생성하기
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    //풀링용 오브젝트인지 확인하고 아닐시 Resource폴더에서 Load
    public T Load<T>(string path) where T : Object
    {
        //위의 Instantiate함수를 통해서 호출될때
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);//이름추출

            //풀링 Dictionary에 존재했으면 거기서 빼오기
            GameObject go = GameManager.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        T res = Resources.Load<T>(path);
        if (res == null)
            Debug.LogError("잘못된경로 : " + path);

        return res;
    }


    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        //풀링용 오브젝트면 파괴하지 않고 비활성화& 풀에 넣기
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            GameManager.Pool.Push(poolable);
            return;
        }

        //일반 오브젝트면 그냥 파괴
        Object.Destroy(go);
    }
}
