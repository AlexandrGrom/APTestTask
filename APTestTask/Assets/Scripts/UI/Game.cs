﻿using UnityEngine;

public class Game : ScreenElement
{
    protected override void Animation()
    {
    }

    protected override void Reset()
    {
    }

    protected override void OnEnable()
    {
        Debug.Log("Game");
        base.OnEnable();
    }
}
