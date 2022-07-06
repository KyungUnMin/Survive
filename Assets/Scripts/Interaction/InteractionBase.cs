using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �÷��̾�� ��ȣ�ۿ��ϰ� �� ������Ʈ
 */

public abstract class InteractionBase : MonoBehaviour
{

    //��ȣ�ۿ� ���θ� �˸��� ����
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
