using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private BaseScene pCurScene;

    //씬이 시작될때마다 호출됨
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

    //씬의 Dictionary에 등록하는 함수
    public void AddObj2Dict(string key, GameObject value)
    {
        if(GetCurrentScene().sceneDict.ContainsKey(key))
        {
            Debug.LogError("Dictionary에 이미 존재하는 Key값");
            return;
        }

        GetCurrentScene().sceneDict.Add(key, value);
    }

    //씬의 Dictionary로부터 받아오는 함수
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

    //다른 씬으로 넘기는 함수(인자는 Define스크립트의 enum값)
    public void LoadScene(Define.Scene type)
    {
        //씬이 넘어가기전 초기화
        GameManager.Clear();

        //다음 씬 호출
        SceneManager.LoadScene(GetSceneName(type));
    }

    //enum -> string
    private string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        //현재 씬도 초기화
        GetCurrentScene().Clear();
    }

}
