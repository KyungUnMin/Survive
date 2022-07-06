using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 플레이어와 상호작용하게 될 오브젝트
 */

public abstract class InteractionBase : MonoBehaviour
{

    //상호작용 여부를 알리는 변수
    public bool isAct { get; protected set; } = false;

    public abstract void Init();

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
