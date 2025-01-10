/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapVisibilityController : MonoBehaviour
{
    public string trapTag = "trap";       // Tag để xác định các đối tượng bẫy cần ẩn khi vào game
    public string trap02Tag = "trap02";   // Tag để xác định các đối tượng bẫy cần hiện khi vào game

    private void Start()
    {
        // Ẩn tất cả các đối tượng có tag "trap"
        SetTrapsVisibility(trapTag, false);

        // Hiện tất cả các đối tượng có tag "trap02"
        SetTrapsVisibility(trap02Tag, true);
    }

    private void Update()
    {
        Debug.Log("Player Position: " + transform.position.x + ", " + transform.position.y);

        // Kiểm tra và cập nhật trạng thái của các bẫy với tag "trap"
        CheckAndUpdateTrapsVisibility(trapTag, true);

        // Kiểm tra và cập nhật trạng thái của các bẫy với tag "trap02"
        CheckAndUpdateTrapsVisibility(trap02Tag, false);
    }

    // Hàm để kiểm tra và cập nhật trạng thái hiển thị của các bẫy
    private void CheckAndUpdateTrapsVisibility(string tag, bool revealWhenPlayerReaches)
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject trap in traps)
        {
            Trap trapComponent = trap.GetComponent<Trap>();
            if (trapComponent != null)
            {
                trapComponent.CheckVisibility(transform.position, revealWhenPlayerReaches);
            }
        }
    }

    // Hàm để ẩn hoặc hiện tất cả các đối tượng có tag được chỉ định
    private void SetTrapsVisibility(string tag, bool visible)
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject trap in traps)
        {
            SpriteRenderer spriteRenderer = trap.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = visible;
            }

            Collider2D[] colliders = trap.GetComponents<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = visible;
            }

            Rigidbody2D rb = trap.GetComponent<Rigidbody2D>();
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
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapVisibilityController : MonoBehaviour
{
    public string[] trapTags = { "trap", "trap02", "trap03" }; // Danh sách tag các bẫy cần kiểm tra

    private void Start()
    {
        // Đặt trạng thái mặc định cho tất cả các bẫy
        foreach (string tag in trapTags)
        {
            SetTrapsVisibility(tag, tag != "trap"); // Ẩn "trap", hiển thị "trap02" và "trap03" mặc định
        }
    }

    private void Update()
    {
        //Debug.Log("Player Position: " + transform.position.x + ", " + transform.position.y);

        // Kiểm tra và cập nhật trạng thái của các bẫy theo tag
        foreach (string tag in trapTags)
        {
            bool revealWhenPlayerReaches = tag == "trap"; // Logic cho "trap" (hiển khi tới gần)
            CheckAndUpdateTrapsVisibility(tag, revealWhenPlayerReaches);
        }
    }

    // Hàm để kiểm tra và cập nhật trạng thái hiển thị của các bẫy
    private void CheckAndUpdateTrapsVisibility(string tag, bool revealWhenPlayerReaches)
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject trap in traps)
        {
            TrapBase trapComponent = trap.GetComponent<TrapBase>();
            if (trapComponent != null)
            {
                trapComponent.HandleTrap(transform.position, trap.GetComponent<TrapControl>(), null);
            }
        }
    }

    // Hàm để ẩn hoặc hiện tất cả các đối tượng có tag được chỉ định
    private void SetTrapsVisibility(string tag, bool visible)
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject trap in traps)
        {
            TrapBase trapComponent = trap.GetComponent<TrapBase>();
            if (trapComponent != null)
            {
                trapComponent.isVisible = visible;
                trapComponent.SetVisibility(visible); // Không còn lỗi
            }
        }
    }
}
