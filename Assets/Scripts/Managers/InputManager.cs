using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private bool useInput = true;
    public void SetOffInput()
    {
        useInput = false;

        inputX = 0;
        inputZ = 0;
        jump = false;

        axisDownLeft = false;
        axisDownRight = false;
        axisDownUp = false;
        axisDownDown = false;

        mouseLeftDown = false;
        mouseLeftUp = false;

        mouseX = 0f;
        mouseY = 0f;
    }

    public void SetOnInput() { useInput = true; }
    public bool GetInput() { return useInput; }

    public float inputX { get; private set; }
    public float inputZ { get; private set; }
    public bool jump { get; private set; }
    
    public bool escape { get; private set; }
    public bool mouseLeftDown { get; private set; }
    public bool mouseLeftUp { get; private set; }

    public bool axisDownLeft { get; private set; }
    public bool axisDownRight { get; private set; }
    public bool axisDownUp { get; private set; }
    public bool axisDownDown { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }

    public void OnUpdate()
    {
        if (useInput)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
            jump = Input.GetButtonDown("Jump");

            axisDownLeft = Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.A);
            axisDownRight = Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.D);
            axisDownUp = Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W);
            axisDownDown = Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.S);

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }

        mouseLeftDown = Input.GetMouseButtonDown(0);
        mouseLeftUp = Input.GetMouseButtonUp(0);
        escape = Input.GetKeyDown(KeyCode.Escape);
    }
}
