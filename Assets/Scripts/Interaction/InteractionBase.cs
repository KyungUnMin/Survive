using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �÷��̾�� ��ȣ�ۿ��ϰ� �� ������Ʈ
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
