using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager
{
    //UI는 이 함수를 통해 Instantiate
    public T ShowSceneUI<T>(string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resource.Instantiate("UI/Scene/" + name);
        T sceneUI = Util.GetOrAddComponent<T>(go);

        return sceneUI;
    }

}
