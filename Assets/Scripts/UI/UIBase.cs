using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
										//<UI Type, ui배열>
	protected Dictionary<Type, UnityEngine.Object[]> uiDict = new Dictionary<Type, UnityEngine.Object[]>();
	public abstract void Init();


	//UI들을 Dictionary에 저장해두는 함수
	protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
		//enum - > string
		string[] names = Enum.GetNames(type);
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

		//배열에 오브젝트 보관
		for (int i = 0; i < names.Length; i++)
		{

			if (typeof(T) == typeof(GameObject))
				objects[i] = Util.FindChild(gameObject, names[i], true);
			else
				objects[i] = Util.FindChild<T>(gameObject, names[i], true);

			if (objects[i] == null)
				Debug.Log($"연결실패({names[i]})");

		}
		uiDict.Add(typeof(T), objects);
	}

	//Dictionary에 저장된 UI들을 return하는 함수
	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (uiDict.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}

	//UI 이벤트처리를 Action을 이용해 등록
	public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
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
