using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        

        Count
    }

    public enum Sound
    {
        Bgm,
        Effect,

        MaxCount
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
}
