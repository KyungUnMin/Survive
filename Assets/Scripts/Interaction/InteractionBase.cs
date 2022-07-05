using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 플레이어와 상호작용하게 될 오브젝트
 */

public abstract class InteractionBase : MonoBehaviour
{
    public bool isAct { get; protected set; } = false;
    protected GameObject player;

    public virtual void Init()
    {
        player = GameManager.Scene.GetObjFromDict("Player");
        
    }

    public virtual void Connect()
    {
        isAct = true;
        
    }

    public virtual void Disconnect()
    {
        isAct = false;
        
    }

    public virtual void Clear() { }

}
