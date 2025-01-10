/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Vector2 lowerLeftCorner;  // Tọa độ góc dưới bên trái của vùng (x1, y1)
    public Vector2 upperRightCorner; // Tọa độ góc trên bên phải của vùng (x2, y2)
    private bool isVisible = false;  // Trạng thái hiện tại của bẫy
    private bool isCoroutineRunning = false; // Đảm bảo coroutine chỉ chạy một lần

    private void Start()
    {
        // Đảm bảo isVisible phản ánh đúng trạng thái ban đầu của bẫy
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            isVisible = spriteRenderer.enabled;
        }
    }

    // Kiểm tra xem Player có nằm trong vùng kích hoạt hay không
    public void CheckVisibility(Vector2 playerPosition, bool revealWhenPlayerReaches)
    {
        Debug.Log("Trap Activation Area: (" + lowerLeftCorner.x + ", " + lowerLeftCorner.y + ") to (" + upperRightCorner.x + ", " + upperRightCorner.y + ")");
        Debug.Log("Player Position: " + playerPosition.x + ", " + playerPosition.y);
        Debug.Log("Trap is currently visible: " + isVisible);

        // Kiểm tra xem Player có nằm trong vùng giới hạn hay không
        bool isInActivationZone = (playerPosition.x >= lowerLeftCorner.x && playerPosition.x <= upperRightCorner.x) &&
                                  (playerPosition.y >= lowerLeftCorner.y && playerPosition.y <= upperRightCorner.y);

        // Nếu bẫy thuộc loại "trap" và Player nằm trong vùng kích hoạt, hiển thị bẫy
        if (revealWhenPlayerReaches && !isVisible && isInActivationZone)
        {
            SetVisibility(true);  // Hiển thị bẫy
            isVisible = true;     // Đảm bảo chỉ hiện một lần
            Debug.Log("Trap with tag 'trap' is now visible.");
        }

        // Nếu bẫy thuộc loại "trap02" và Player nằm trong vùng kích hoạt, ẩn bẫy và sau 3 giây hiện lại
        else if (!revealWhenPlayerReaches && isVisible && isInActivationZone && !isCoroutineRunning)
        {
            StartCoroutine(HideAndRevealTrap()); // Bắt đầu Coroutine để ẩn rồi hiện lại
        }
    }

    // Coroutine để ẩn bẫy trong 3 giây rồi hiện lại
    private IEnumerator HideAndRevealTrap()
    {
        isCoroutineRunning = true;

        // Ẩn bẫy
        SetVisibility(false);
        isVisible = false;
        Debug.Log("Trap with tag 'trap02' is now hidden.");

        // Chờ 3 giây
        yield return new WaitForSeconds(3f);

        // Hiển thị lại bẫy
        SetVisibility(true);
        isVisible = true;
        Debug.Log("Trap with tag 'trap02' is now visible again.");

        isCoroutineRunning = false;
    }

    // Hàm để thay đổi trạng thái hiển thị của bẫy
    private void SetVisibility(bool visible)
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
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Vector2 lowerLeftCorner;  // Tọa độ góc dưới bên trái của vùng (x1, y1)
    public Vector2 upperRightCorner; // Tọa độ góc trên bên phải của vùng (x2, y2)

    // Thay đổi trạng thái vĩnh viễn và thời gian
    public bool permanentForTrap = true; // Cho phép trap hiện vĩnh viễn
    public float trapDuration = 0f;      // Thời gian trap hiện (0 = vĩnh viễn)

    public bool permanentForTrap02 = false; // Cho phép trap02 biến mất vĩnh viễn
    public float trap02Duration = 3f;        // Thời gian trap02 hiện lại

    private bool isVisible = false;  // Trạng thái hiện tại của bẫy
    private bool isCoroutineRunning = false; // Đảm bảo coroutine chỉ chạy một lần

    private void Start()
    {
        // Đảm bảo isVisible phản ánh đúng trạng thái ban đầu của bẫy
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            isVisible = spriteRenderer.enabled;
        }
    }

    // Kiểm tra xem Player có nằm trong vùng kích hoạt hay không
    public void CheckVisibility(Vector2 playerPosition, bool revealWhenPlayerReaches)
    {
        Debug.Log("Trap Activation Area: (" + lowerLeftCorner.x + ", " + lowerLeftCorner.y + ") to (" + upperRightCorner.x + ", " + upperRightCorner.y + ")");
        Debug.Log("Player Position: " + playerPosition.x + ", " + playerPosition.y);
        Debug.Log("Trap is currently visible: " + isVisible);

        // Kiểm tra xem Player có nằm trong vùng giới hạn hay không
        bool isInActivationZone = (playerPosition.x >= lowerLeftCorner.x && playerPosition.x <= upperRightCorner.x) &&
                                  (playerPosition.y >= lowerLeftCorner.y && playerPosition.y <= upperRightCorner.y);

        // Nếu bẫy thuộc loại "trap" và Player nằm trong vùng kích hoạt
        if (revealWhenPlayerReaches && !isVisible && isInActivationZone)
        {
            SetVisibility(true);  // Hiển thị bẫy
            isVisible = true;     // Đảm bảo chỉ hiện một lần
            Debug.Log("Trap with tag 'trap' is now visible.");

            // Nếu trap không vĩnh viễn, bắt đầu đếm ngược
            if (!permanentForTrap && trapDuration > 0)
            {
                StartCoroutine(HideTrapAfterDuration(trapDuration));
            }
        }

        // Nếu bẫy thuộc loại "trap02" và Player nằm trong vùng kích hoạt
        else if (!revealWhenPlayerReaches && isVisible && isInActivationZone && !isCoroutineRunning)
        {
            if (!permanentForTrap02)
            {
                StartCoroutine(HideAndRevealTrap(trap02Duration)); // Bắt đầu Coroutine để ẩn rồi hiện lại
            }
            else
            {
                SetVisibility(false); // Ẩn bẫy nếu nó là vĩnh viễn
                isVisible = false;
                Debug.Log("Trap with tag 'trap02' is now hidden permanently.");
            }
        }
    }

    // Coroutine để ẩn bẫy trong một khoảng thời gian nhất định rồi hiện lại
    private IEnumerator HideAndRevealTrap(float duration)
    {
        isCoroutineRunning = true;

        // Ẩn bẫy
        SetVisibility(false);
        isVisible = false;
        Debug.Log("Trap with tag 'trap02' is now hidden.");

        // Chờ theo thời gian đã chỉ định
        yield return new WaitForSeconds(duration);

        // Hiển thị lại bẫy
        SetVisibility(true);
        isVisible = true;
        Debug.Log("Trap with tag 'trap02' is now visible again.");

        isCoroutineRunning = false;
    }

    // Coroutine để ẩn bẫy trap sau một khoảng thời gian
    private IEnumerator HideTrapAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Ẩn bẫy
        SetVisibility(false);
        isVisible = false;
        Debug.Log("Trap with tag 'trap' is now hidden after duration.");
    }

    // Hàm để thay đổi trạng thái hiển thị của bẫy
    private void SetVisibility(bool visible)
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
}
