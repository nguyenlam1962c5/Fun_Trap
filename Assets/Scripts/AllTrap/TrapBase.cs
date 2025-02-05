/*using UnityEngine;
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
}*/

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

    // Lưu trữ trạng thái ban đầu
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private bool initialSpriteVisibility;

    private void Awake()
    {
        // Lưu trữ trạng thái ban đầu khi đối tượng được tạo
        initialPosition = transform.position;
        initialScale = transform.localScale;

        // Kiểm tra và lưu trạng thái ban đầu của Sprite nếu có
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            initialSpriteVisibility = spriteRenderer.enabled;
        }
    }

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

    public virtual void ResetTrap()
    {
        // Reset trạng thái kích hoạt
        isActivated = false;
        timer = 0f;

        // Khôi phục vị trí ban đầu
        transform.position = initialPosition;
        transform.localScale = initialScale;

        // Reset sprite visibility nếu có
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = initialSpriteVisibility;
        }

        // Reset rigidbody nếu có
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
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