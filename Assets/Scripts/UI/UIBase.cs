using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
	protected Dictionary<Type, UnityEngine.Object[]> uiDict = new Dictionary<Type, UnityEngine.Object[]>();//연결할 UI들을 담아놓는 Dictionary
	public abstract void Init();

	protected void Bind<T>(Type type) where T : UnityEngine.Object//UI들을 Dictionary에 저장해두는 함수
	{
		string[] names = Enum.GetNames(type);//enum - > string
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];//enum에 담긴 크기만큼 object배열 생성
		
		for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))//GameObject타입으로 제네릭을 요청한 경우
				objects[i] = Util.FindChild(gameObject, names[i], true);//이 클래스를 지닌 오브젝트의 자식을 이름으로 찾아 배열에 저장
			else
				objects[i] = Util.FindChild<T>(gameObject, names[i], true);//다른 타입인경우도 마찬가지

			if (objects[i] == null)
				Debug.Log($"연결실패({names[i]})");
		}
		uiDict.Add(typeof(T), objects);//object배열을 Dictionary에 저장
	}

	protected T Get<T>(int idx) where T : UnityEngine.Object//Dictionary에 저장된 UI들을 return하는 함수
	{
		UnityEngine.Object[] objects = null;
		if (uiDict.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}

	public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)//UI 이벤트처리를 Action을 이용해 등록
	{
		UIEventHandler evt = Util.GetOrAddComponent<UIEventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action;
				evt.OnDragHandler += action;
				break;
		}
	}
}
