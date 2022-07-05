using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseScene : MonoBehaviour//모든 씬클래스의 Base
{
    public Dictionary<string, GameObject> sceneDict = new Dictionary<string, GameObject>();
    protected Define.Scene nowScene = Define.Scene.Count;

    void Awake()
    {
        Init();
        GameManager.Interaction.PushAll();  //모든 자식이 호출, Init이 다 완료된 후 호출
        LateInit();
    }

    protected virtual void Init()//씬을 옮길때마다 
    {
        GameManager.Scene.SetCurrentScene(this);

        //Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        //if (obj == null)                            //EventSystem이 없다면 추가함
        //{
        //    GameObject eventSystem = GameManager.Resource.Instantiate("UI/EventSystem");
        //    eventSystem.name = "@EventSystem";
        //    DontDestroyOnLoad(eventSystem);
        //}

    }

    protected virtual void LateInit() { }

    public virtual void Clear()
    {
        foreach (GameObject obj in sceneDict.Values)
        {
            if (obj)
                GameManager.Resource.Destroy(obj);
        }
        sceneDict.Clear();
    }

    

    
}
