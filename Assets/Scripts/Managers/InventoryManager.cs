using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    private int[] vecInven = new int[(int)Define.Item.Count];

    public void Init()
    {
        for (int i = 0; i < (int)Define.Item.Count; ++i)
            vecInven[i] = 0;
    }

    public void Push(Define.Item item) { ++vecInven[(int)item]; }

    public void Pop(Define.Item item)
    {
        if (vecInven[(int)item] < 1)
            return;

        --vecInven[(int)item];
    }

    public int Peek(Define.Item item) { return vecInven[(int)item]; }

}
