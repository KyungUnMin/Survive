using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager
{
    /*
     * �÷��̾�� ��ȣ�ۿ��� ������Ʈ���� ���� �����ϴ� �Ŵ���
     */

                                    //<������Ʈ �̸�, �θ� ������>
    private Dictionary<string, InteractionBase> objDict = new Dictionary<string, InteractionBase>();
    private int idx = 0;

    // ��::Init() -> PushAll() -> ��::LateInit() ������ ȣ��
    //���� �ִ� ��ȣ�ۿ� ������Ʈ�� Dictionary�� ����
    public void PushAll()
    {
        InteractionBase[] obs = UnityEngine.GameObject.FindObjectsOfType<InteractionBase>();
        foreach (InteractionBase ob in obs)
        {
            ob.Init();
            ob.name += idx++;       //������Ʈ �̸� �ߺ� ����
            objDict.Add(ob.name, ob);
        }
    }

    //��ȣ�ۿ��� ������Ʈ�� Instantiate�Ҷ��� ResourceManager�� �ƴ� �Ʒ� �Լ��� ���� ����
    public virtual T Instantiate<T>(string path) where T : InteractionBase
    {
        GameObject go = GameManager.Resource.Instantiate(path);
        go.name += idx++;

        T tmp = go.GetOrAddComponent<T>();
        objDict.Add(go.name, tmp);
        return tmp;
    }

    //T�� ȣ���� ��ü�迭�� ��ȯ
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

    //��� ��ü�� �Ҹ��� ȣ��
    public virtual void Clear()
    {
        idx = 0;
        foreach (string key in objDict.Keys)
            objDict[key].Clear();
        objDict.Clear();
    }
}
