using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager
{
    private Dictionary<string, InteractionBase> objDict = new Dictionary<string, InteractionBase>();
    private int idx = 0;

    public void PushAll()
    {
        InteractionBase[] obs = UnityEngine.GameObject.FindObjectsOfType<InteractionBase>();
        foreach (InteractionBase ob in obs)
        {
            ob.Init();
            ob.name += idx++;
            objDict.Add(ob.name, ob);
        }
    }

    //public virtual T Instantiate(string path)
    //{
    //    GameObject go = GameManager.Resource.Instantiate(path);
    //    go.name += idx++;

    //    T tmp = go.GetOrAddComponent<T>();
    //    objDict.Add(go.name, tmp);
    //    return tmp;
    //}

    public InteractionBase Get(string objectName)
    {
        InteractionBase tmp;
        if (objDict.TryGetValue(objectName, out tmp) == false)
            return null;

        return tmp;
    }

    public T Get<T>(string objectName) where T : InteractionBase
    {
        InteractionBase tmp;
        if (objDict.TryGetValue(objectName, out tmp) == false)
            return null;

        return tmp as T;
    }

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

    public virtual void Clear()
    {
        idx = 0;
        foreach (string key in objDict.Keys)
            objDict[key].Clear();
        objDict.Clear();
    }
}
