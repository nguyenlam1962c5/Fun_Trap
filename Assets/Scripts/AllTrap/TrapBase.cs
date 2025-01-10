using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TrapBase : MonoBehaviour
{
    public bool isVisible = false; // Trạng thái hiển thị của bẫy

    public abstract void HandleTrap(Vector2 playerPosition, TrapControl trapControl, Action customAction);

    public void SetVisibility(bool visible)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = visible;
        }

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = visible;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (!visible)
            {
                rb.Sleep();
            }
            else
            {
                rb.WakeUp();
            }
        }
    }

    // Phương thức mới di chuyển vật thể
    public void MoveObject(Vector2 direction, float speed, float distance)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * speed;
            // Cần giới hạn di chuyển theo khoảng cách
            // (sử dụng một coroutine để dừng sau khi đi được khoảng cách)
            rb.velocity = direction.normalized * speed;
            StartCoroutine(StopMovementAfterDistance(rb, direction, distance));
        }
    }

    private IEnumerator StopMovementAfterDistance(Rigidbody2D rb, Vector2 direction, float distance)
    {
        float distanceTraveled = 0f;
        while (distanceTraveled < distance)
        {
            distanceTraveled += rb.velocity.magnitude * Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero; // Dừng lại sau khi di chuyển đủ khoảng cách
    }
}
