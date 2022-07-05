using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        private Stack<Poolable> poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count=5)//기본 풀링 설정
        {
            Original = original;
            Root = new GameObject().transform;//original 오브젝트들만 모아놓을 Transform 생성
            Root.name = original.name + "_Root";

            for (int i = 0; i < count; ++i)//일정 수만큼 풀링
                Push(Create());
        }

        private Poolable Create()//Original 오브젝트를 생성하는 함수
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)//생성한 오브젝트를 비활성화 한 후 stack에 담아놓는 함수
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)//풀에서 하나 빼오기
        {
            Poolable poolable;

            if (poolStack.Count > 0)
                poolable = poolStack.Pop();
            else//풀에서 빼올 오브젝트가 없을때
                poolable = Create();

            poolable.gameObject.SetActive(true);//활성화

            //if (parent == null)
            //    poolable.transform.parent = GameManager.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }

    }


    private Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();
    private Transform root;//모든 풀링오브젝트들을 담아놓을 그릇

    public void Init()//모든 풀링오브젝트 그릇 생성
    {
        if(root==null)
        {
            root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(root);
        }
    }

    public void CreatePool(GameObject original, int count=5)//Dictionary에 저장 및 풀링
    {
        Pool pool = new Pool();
        pool.Init(original, count);//풀링
        pool.Root.parent = root;// "@Pool_Root"의 자식으로 original_root가 들어감

        poolDict.Add(original.name, pool);
    }

    public void Push(Poolable poolable)//삭제하는 경우
    {
        string name = poolable.gameObject.name;
        if(poolDict.ContainsKey(name)==false)//poolDict에 포함된 객체가 아니면 그냥 삭제
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        poolDict[name].Push(poolable);//pool 클래스의 Push함수
    }

    public Poolable Pop(GameObject original, Transform parent=null)//생성하는 경우
    {
        if (poolDict.ContainsKey(original.name) == false)//PoolDict에 등록되지 않았었다면
            CreatePool(original);//풀링오브젝트로 설정

        return poolDict[original.name].Pop(parent);//풀링에서 하나 빼옴
    }
    public GameObject GetOriginal(string name)//poolDIct에 존재한다면 Original뺴오기
    {
        if (poolDict.ContainsKey(name) == false)
            return null;
        return poolDict[name].Original;
    }

    public void Clear()//다음 씬으로 넘어갈때 풀링 삭제 & 씬에 있는 루트 자식 오브젝트 모두 삭제
    {
        foreach (Transform child in root)
            GameObject.Destroy(child.gameObject);

        poolDict.Clear();
    }





}
