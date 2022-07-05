using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Sample : UIBase
{
    enum Texts//사용할 오브젝트들을 enum으로 선언한다.(주의, 씬에 있는 오브젝트와 enum에 있는 오브젝트 이름이 다르면 안됨)
    {
        testA,
        testB,
        
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));//text들 연결

        Text testA = Get<Text>((int)Texts.testA);//받아오기
        Text testB = Get<Text>((int)Texts.testB);
        testA.text = "testA";
        testB.text = "testB";

        //이벤트 연결
        BindEvent(testA.gameObject, (PointerEventData data) => { testA.color = Random.ColorHSV(); }, Define.UIEvent.Click);//클릭
        BindEvent(testB.gameObject, (PointerEventData data) => { testB.transform.position = data.position; }, Define.UIEvent.Drag);//드래그
    }
}
