using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    /*
     * 유저로 부터 받는 입력은 다 여기서 관리할 생각입니다.
     * 근데 모바일 환경이라 이 매니저를 사용할지는 모르겠네
     * (다 UI로 작동되서 ㅇㅇ)
     */

    //유저로부터 입력을 받는지 결정하는 변수
    private bool useInput = true;
    public void SetOffInput()
    {
        useInput = false;
        inputX = 0f;
        inputZ = 0f;
    }
    public void SetOnInput() { useInput = true; }
    public bool GetInput() { return useInput; }


    //일단은 예시
    public float inputX { get; private set; }
    public float inputZ { get; private set; }
    

    public void OnUpdate()
    {
        if (useInput)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
        }

        
    }

}
