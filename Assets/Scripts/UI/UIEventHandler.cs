using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
	//Action을 이용한 옵저버패턴
	public Action<PointerEventData> OnClickHandler = null;
	public Action<PointerEventData> OnDragHandler = null;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (OnClickHandler != null)
			OnClickHandler.Invoke(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (OnDragHandler != null)
			OnDragHandler.Invoke(eventData);
	}
}
