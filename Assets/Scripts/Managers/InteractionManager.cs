using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager
{
    /*
     * 플레이어와 상호작용할 오브젝트들을 따로 관리하는 매니저
     */

                                    //<오브젝트 이름, 부모 포인터>
    private Dictionary<string, InteractionBase> objDict = new Dictionary<string, InteractionBase>();
    private int idx = 0;

    // 씬::Init() -> PushAll() -> 씬::LateInit() 순서로 호출
    //씬에 있는 상호작용 오브젝트를 Dictionary에 보관
    public void PushAll()
    {
        InteractionBase[] obs = UnityEngine.GameObject.FindObjectsOfType<InteractionBase>();
        foreach (InteractionBase ob in obs)
        {
            ob.Init();
            ob.name += idx++;       //오브젝트 이름 중복 방지
            objDict.Add(ob.name, ob);
        }
    }

    //상호작용할 오브젝트를 Instantiate할때는 ResourceManager가 아닌 아래 함수를 통해 생성
    public virtual T Instantiate<T>(string path) where T : InteractionBase
    {
        GameObject go = GameManager.Resource.Instantiate(path);
        go.name += idx++;

        T tmp = go.GetOrAddComponent<T>();
        objDict.Add(go.name, tmp);
        return tmp;
    }

    //T로 호출한 객체배열을 반환
    public T[] Gets<T>() where T:InteractionBase
    {
        List<T> vecChild = new List<T>();
        
        foreach (InteractionBase ib in objDict.Values)
        {
            T child = ib as T;
            if (child != null)
                vecChild.Add(child);
        }

        return vecChild.ToArray();
    }

    //모든 객체에 소멸자 호출
    public virtual void Clear()
    {
        idx = 0;
        foreach (string key in objDict.Keys)
            objDict[key].Clear();
        objDict.Clear();
    }
}
