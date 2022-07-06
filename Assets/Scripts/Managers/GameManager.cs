using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static GameManager Instance { get { Init(); return _instance; } }

    #region 매니저들

    //Input담당
    private InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    //Resource담당
    private ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    //오브젝트 풀링 담당
    private PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    //씬 전환 담당
    private SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    //UI담당
    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    //Sound담당
    private SoundManager _sound = new SoundManager();
    public static SoundManager Sound { get { return Instance._sound; } }

    //상호작용 오브젝트 담당
    private InteractionManager _interaction = new InteractionManager();
    public static InteractionManager Interaction { get { return Instance._interaction; } }

    #endregion


    //게임 처음 실행시 딱 한번만 호출
    private static void Init()
    {
        if (_instance==null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if(go==null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<GameManager>();

            //다른 매니저들 초기화
            _instance._pool.Init();
            _instance._sound.Init();
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

    //씬이 넘어갈때 다른 매니저 스크립트 초기화(SceneManagerEx가 호출)
    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        Pool.Clear();
        Interaction.Clear();
    }


}
