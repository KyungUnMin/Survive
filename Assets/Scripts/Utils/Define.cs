using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Intro1 = 0,
        Intro2,

        Stage1,
        Stage2_Main,
        Stage2_Sokovan,
        Stage2_Maze,
        Stage2_Jump,

        Stage3,
        Ending,
        Ending2,
        Ending3,
        Credit,

        Count
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,//사운드 종류의 총 갯수를 알기위함
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        Click,
    }


    public enum Layer
    {
        Interaction = (1<<10),

    }

    public enum Item
    {
        Mirror,

        SokovanClear,
        MazeClear,
        JumpClear,
        Count
    }

    public enum AnimationKey
    {
        GateOpen,

    }
}
