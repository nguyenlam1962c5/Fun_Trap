// Trap01.cs
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Trap01 : TrapBase
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public override void Activate(float duration = 0f)
    {
        base.Activate(duration);
        spriteRenderer.enabled = true;
    }

    protected override void Update()
    {
        base.Update();
        if (!isActivated && !isPermanent)
        {
            spriteRenderer.enabled = false;
        }
    }
}
