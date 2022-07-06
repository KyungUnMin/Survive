using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    /*
     *  ResourceManager와 연계됨
     *  PoolDictionary - Pool - Stack - GameObject 구조
     */


    private class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        private Stack<Poolable> poolStack = new Stack<Poolable>();

        //기본 풀링 설정
        public void Init(GameObject original, int count=5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = original.name + "_Root";

            for (int i = 0; i < count; ++i)
                Push(Create());
        }

        //객체 생성
        private Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        //생성한 오브젝트를 비활성화 한 후 stack에 담아놓는 함수
        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            poolStack.Push(poolable);
        }


        //풀에서 하나 빼오기(없으면 더 만들기)
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (poolStack.Count > 0)
                poolable = poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);


            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }

    }

    private Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();
    private Transform root;

    //풀링되는 오브젝트의 최상위 부모 생성
    public void Init()
    {
        if(root==null)
        {
            root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(root);
        }
    }


    //Resource::Load로부터 호출됨
    //poolDiIct에 존재한다면 Original뺴오기
    public GameObject GetOriginal(string name)
    {
        if (poolDict.ContainsKey(name) == false)
            return null;
        return poolDict[name].Original;
    }


    //Resource::Instantiate로부터 호출됨
    public Poolable Pop(GameObject original, Transform parent = null)//생성하는 경우
    {
        //PoolDict에 등록되지 않았었다면 풀링오브젝트로 설정
        if (poolDict.ContainsKey(original.name) == false)
            CreatePool(original);

        //풀링에서 하나 빼옴
        return poolDict[original.name].Pop(parent);
    }


    //Dictionary에 저장 및 풀링
    private void CreatePool(GameObject original, int count=5)
    {
        Pool pool = new Pool();

        //해당 오브젝트 풀링
        pool.Init(original, count);

        // "@Pool_Root"의 자식으로 original_root가 들어감
        pool.Root.parent = root;

        //pool객체를 딕셔너리에 저장
        poolDict.Add(original.name, pool);
    }



    //Resource::Destroy로부터 호출됨
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        //poolDict에 포함된 객체가 아니면 그냥 삭제
        if (poolDict.ContainsKey(name)==false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        //pool 클래스의 Push함수
        poolDict[name].Push(poolable);
    }

    


    

    //다음 씬으로 넘어갈때 풀링 삭제 & 씬에 있는 루트 자식 오브젝트 모두 삭제
    public void Clear()
    {
        //풀 매니저의 Root는 DontDestroy객체이기 때문에 별도로 지워줘야 함
        foreach (Transform child in root)
            GameObject.Destroy(child.gameObject);

        poolDict.Clear();
    }


    


}
