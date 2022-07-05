using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))//GameObject로서 불렀다면
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);//이름추출

            GameObject go = GameManager.Pool.GetOriginal(name);//풀링용 오브젝트인지 확인
            if (go != null)//맞으면 poolDict에서 빼오기
                return go as T;
        }

        T res = Resources.Load<T>(path);
        if (res == null)
            Debug.LogError("잘못된경로 : " + path);

        return res;
    }

    public GameObject Instantiate(string path, Transform parent = null)//자동위치 : Resource/Prefabs/
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"잘못된 경로 : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)//풀링용 오브젝트이면
            return GameManager.Pool.Pop(original, parent).gameObject;//풀에서 꺼내오기

        //풀링용 오브젝트가 아니면 바로 생성하기
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();//풀링용 오브젝트면 파괴하지 않고 비활성화& 풀에 넣기
        if (poolable != null)
        {
            GameManager.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);//일반 오브젝트면 그냥 파괴
    }
}
