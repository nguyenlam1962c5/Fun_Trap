using UnityEngine;
using System.Collections;

public class Trap03 : TrapBase
{
    [Header("Movement Settings")]
    public Vector2 targetPosition;
    public float moveSpeed = 5f;

    [Header("Second Movement (Optional)")]
    public bool useSecondMovement = false;
    public float delayBeforeSecondMove = 0f;
    public Vector2 secondTargetPosition;

    private Rigidbody2D rb2d;
    private bool isMoving = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            // Khóa rotation của Rigidbody2D
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            // Đặt gravity scale = 0 để không bị rơi
            rb2d.gravityScale = 0f;
        }
    }

    public void SetMovementTarget(Vector2 target)
    {
        targetPosition = target;
    }

    public void SetSecondMovementTarget(Vector2 target, float delay)
    {
        secondTargetPosition = target;
        delayBeforeSecondMove = delay;
        useSecondMovement = true;
    }

    public void DisableSecondMovement()
    {
        useSecondMovement = false;
    }

    public override void Activate(float duration = 0f)
    {
        base.Activate(duration);
        if (isActivated && !isMoving)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;

        // Di chuyển đến vị trí đầu tiên
        Vector3 initialTarget = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        while (Vector3.Distance(transform.position, initialTarget) > 0.01f)
        {
            if (rb2d != null)
            {
                // Sử dụng MovePosition thay vì thay đổi transform trực tiếp
                Vector2 newPos = Vector2.MoveTowards(rb2d.position, initialTarget, moveSpeed * Time.fixedDeltaTime);
                rb2d.MovePosition(newPos);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position,
                                                       initialTarget,
                                                       moveSpeed * Time.deltaTime);
            }
            yield return null;
        }

        if (useSecondMovement)
        {
            // Đảm bảo delay hoạt động đúng
            if (delayBeforeSecondMove > 0)
            {
                yield return new WaitForSeconds(delayBeforeSecondMove);
            }

            // Di chuyển đến vị trí thứ hai
            Vector3 secondTarget = new Vector3(secondTargetPosition.x, secondTargetPosition.y, transform.position.z);
            while (Vector3.Distance(transform.position, secondTarget) > 0.01f)
            {
                if (rb2d != null)
                {
                    Vector2 newPos = Vector2.MoveTowards(rb2d.position, secondTarget, moveSpeed * Time.fixedDeltaTime);
                    rb2d.MovePosition(newPos);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                                                           secondTarget,
                                                           moveSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }

        isMoving = false;
    }
}