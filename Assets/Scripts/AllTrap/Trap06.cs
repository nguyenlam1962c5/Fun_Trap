using UnityEngine;
using System.Collections;

public class Trap06 : TrapBase
{
    private Rigidbody2D rb2d;

    private void Start()
    {
        // Lấy component Rigidbody2D
        rb2d = GetComponent<Rigidbody2D>();

        if (rb2d != null)
        {
            // Tắt Rigidbody2D khi bắt đầu
            rb2d.simulated = false;
        }
        else
        {
            Debug.LogError($"Trap06 trên GameObject {gameObject.name}: Không tìm thấy component Rigidbody2D!");
            enabled = false;
        }
    }

    public override void Activate(float duration = 0f)
    {
        base.Activate(duration);

        if (rb2d != null && isActivated)
        {
            // Bật Rigidbody2D khi được kích hoạt
            rb2d.simulated = true;
        }
    }

    public override void ResetTrap()
    {
        base.ResetTrap();

        if (rb2d != null)
        {
            // Reset lại trạng thái Rigidbody2D
            rb2d.simulated = false;
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
        }
    }
}