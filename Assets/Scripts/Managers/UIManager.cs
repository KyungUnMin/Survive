using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager
{
    public T ShowSceneUI<T>(string name = null) where T : UIBase//UI를 인스턴스하고 UIBase를 상속받은 컴포넌트로 리턴
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resource.Instantiate("UI/Scene/" + name);
        T sceneUI = Util.GetOrAddComponent<T>(go);

        return sceneUI;
    }

}
