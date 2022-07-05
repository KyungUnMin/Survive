using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private BaseScene pCurScene;
    public Animation loading { get; private set; }

    public void Init()
    {
        loading = GameManager.Resource.Instantiate("UI/Scene/SceneChange").GetComponent<Animation>();
        GameObject.DontDestroyOnLoad(loading.gameObject);
    }

    public void SetCurrentScene(BaseScene _pScene)
    {
        this.pCurScene = _pScene; 
    }

    public BaseScene GetCurrentScene()
    {
        if (pCurScene == null)
            pCurScene = UnityEngine.Object.FindObjectOfType<BaseScene>();
        return pCurScene;
    }

    public void AddObj2Dict(string key, GameObject value)
    {
        if(GetCurrentScene().sceneDict.ContainsKey(key))
        {
            Debug.LogError("Dictionary에 이미 존재하는 Key값");
            return;
        }

        GetCurrentScene().sceneDict.Add(key, value);
    }

    public GameObject GetObjFromDict(string key)
    {
        GameObject go;
        if(GetCurrentScene().sceneDict.TryGetValue(key, out go)==false)
        {
            Debug.LogError(key + "가 등록되지 않음");
            return null;
        }

        return go;
    }

    public void LoadScene(Define.Scene type)//다른 씬으로 넘기는 함수(인자는 Define스크립트의 enum값)
    {
        GameManager.Clear();//씬이 넘어가기전 초기화
        SceneManager.LoadScene(GetSceneName(type));//다음 씬 호출
    }

    private string GetSceneName(Define.Scene type)//enum -> string
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        GetCurrentScene().Clear();
    }

}
