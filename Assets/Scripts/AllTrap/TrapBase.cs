using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrapBase : MonoBehaviour
{
    protected bool isActivated = false;
    protected float duration = 0f;
    protected bool isPermanent = true;
    protected float timer = 0f;

    public virtual void Initialize() { }

    public virtual void Activate(float duration = 0f)
    {
        // Chỉ kích hoạt nếu trap chưa được kích hoạt
        if (!isActivated)
        {
            isActivated = true;
            this.duration = duration;
            isPermanent = (duration <= 0f);
            timer = 0f;
        }
    }

    protected virtual void Update()
    {
        if (isActivated && !isPermanent)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                isActivated = false;
                timer = 0f;
            }
        }
    }
}