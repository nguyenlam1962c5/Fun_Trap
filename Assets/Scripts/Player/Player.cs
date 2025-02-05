using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public PlayerMovement movement { get; private set; }
    public DeathAnimation deathAnimation { get; private set; }

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private PlayerSpriteRenderer activeRenderer;

    public bool big => bigRenderer.enabled; // Kiểm tra xem người chơi có ở trạng thái lớn không
    public bool dead => deathAnimation.enabled; // Kiểm tra xem người chơi đã chết chưa
    public bool starpower { get; private set; } // Kiểm tra xem người chơi có ở trạng thái sao không

    private void Awake()
    {
        // Khởi tạo các component khi đối tượng được tạo
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        movement = GetComponent<PlayerMovement>();
        deathAnimation = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer; // Đặt renderer hoạt động là smallRenderer
    }

    public void Hit()
    {
        // Kiểm tra trạng thái trước khi xử lý va chạm
        if (!dead && !starpower)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }
    }

    // Reset TrapBase()
    public void ResetTraps()
    {
        // Tìm và reset tất cả các đối tượng có script TrapBase
        TrapBase[] traps = FindObjectsOfType<TrapBase>();

        foreach (TrapBase trap in traps)
        {
            trap.ResetTrap();
        }
    }

    public void Death()
    {
        // Xử lý khi người chơi chết
        smallRenderer.enabled = false; // Ẩn smallRenderer
        bigRenderer.enabled = false; // Ẩn bigRenderer
        deathAnimation.enabled = true; // Bật animation chết
        ResetTraps(); // Kích hoạt ResetTrap()
        //GameManager.Instance.ResetLevel(3f); // Gọi reset level (nếu cần)
    }

    public void Grow()
    {
        // Kiểm tra trạng thái script trước khi phóng to
        if (!this.enabled) return;

        smallRenderer.enabled = false; // Ẩn smallRenderer
        bigRenderer.enabled = true; // Bật bigRenderer
        activeRenderer = bigRenderer; // Cập nhật activeRenderer

        capsuleCollider.size = new Vector2(1f, 2f); // Thay đổi kích thước collider
        capsuleCollider.offset = new Vector2(0f, 0.5f); // Thay đổi offset

        StartCoroutine(ScaleAnimation()); // Bắt đầu animation phóng to
    }

    public void Shrink()
    {
        // Kiểm tra trạng thái script trước khi thu nhỏ
        if (!this.enabled) return;

        smallRenderer.enabled = true; // Bật smallRenderer
        bigRenderer.enabled = false; // Ẩn bigRenderer
        activeRenderer = smallRenderer; // Cập nhật activeRenderer

        capsuleCollider.size = new Vector2(1f, 1f); // Thay đổi kích thước collider
        capsuleCollider.offset = new Vector2(0f, 0f); // Thay đổi offset

        StartCoroutine(ScaleAnimation()); // Bắt đầu animation thu nhỏ
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f; // Thời gian cho animation

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                // Chuyển đổi trạng thái của các renderer để tạo hiệu ứng nhấp nháy
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }

            yield return null; // Chờ đến frame tiếp theo
        }

        // Đảm bảo chỉ một renderer được bật vào cuối quá trình
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation()); // Bắt đầu animation sao
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true; // Đặt trạng thái sao thành true

        float elapsed = 0f;
        float duration = 10f; // Thời gian hiệu ứng sao

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0 && this.enabled) // Chỉ thay đổi màu nếu script đang bật
            {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f); // Thay đổi màu sắc
            }

            yield return null; // Chờ đến frame tiếp theo
        }

        activeRenderer.spriteRenderer.color = Color.white; // Đặt lại màu sắc về trắng
        starpower = false; // Đặt trạng thái sao về false
    }

    // Phương thức để bật/tắt script
    public void ToggleScript(bool enable)
    {
        this.enabled = enable; // Thay đổi trạng thái bật/tắt của script
    }
}
