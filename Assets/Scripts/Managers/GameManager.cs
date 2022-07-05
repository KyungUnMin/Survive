using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static GameManager Instance { get { Init(); return _instance; } }//자기자신(GameManager) 싱글톤 패턴

    #region 매니저들

    private InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }//InputManager연결

    private ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }//ResourceManager 연결

    private PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }//PoolManager연결

    private SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance._scene; } }//SceneManager연결

    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }//UIManager연결

    private SoundManager _sound = new SoundManager();
    public static SoundManager Sound { get { return Instance._sound; } }//SoundManager연결

    private InteractionManager _interaction = new InteractionManager();
    public static InteractionManager Interaction { get { return Instance._interaction; } }

    private InventoryManager _inven = new InventoryManager();
    public static InventoryManager Inven { get { return Instance._inven; } }

    #endregion


    private static void Init()//딱 한번만 실행됨
    {
        if(_instance==null)//아직 게임매니저가 할당되지 않았다면
        {
            //Screen.SetResolution(1280, 720, false);
            GameObject go = GameObject.Find("@GameManager");//씬에서 매니저를 찾고
            if(go==null)//없다면
            {
                go = new GameObject { name = "@GameManager" };//씬에서 새 오브젝트를 만든다.
                go.AddComponent<GameManager>();//새로 만든 오브젝트에 게임매니저 컴포넌트 부착
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<GameManager>();//객체(instance)와 게임매니저 오브젝트(go) 연결

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Screen.SetResolution(1920, 1080, true);

            //다른 매니저들 초기화
            _instance._pool.Init();
            _instance._sound.Init();
            _instance._inven.Init();
            _instance._scene.Init();
        }

    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }


    public static void Clear()//씬이 넘어갈때 다른 매니저 스크립트 초기화(SceneManagerEx만 사용)
    {
        Sound.Clear();
        Scene.Clear();
        Pool.Clear();
        Interaction.Clear();
    }


}
