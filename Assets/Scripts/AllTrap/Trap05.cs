using UnityEngine;
using System.Collections;

public class Trap05 : TrapBase
{
    [Header("Movement Type")]
    public bool isLoopMovement = true;  // true: di chuyển vòng lặp, false: di chuyển ngược lại

    [Header("Position Settings")]
    public Vector2 pointA = Vector2.zero;  // Điểm A
    public Vector2 pointB = Vector2.one;   // Điểm B

    [Header("Movement Settings")]
    public float moveSpeed = 5f;           // Tốc độ di chuyển
    public float delayAtPoints = 0.5f;     // Thời gian delay tại A và B

    private bool isMoving = false;
    private bool movingToB = true;         // true: đang di chuyển về B, false: đang di chuyển về A
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = new Vector3(pointA.x, pointA.y, transform.position.z);
    }

    public override void Activate(float duration = 0f)
    {
        base.Activate(duration);
        if (isActivated && !isMoving)
        {
            StartCoroutine(MoveObject());
        }
    }

    public override void ResetTrap()
    {
        base.ResetTrap();
        StopAllCoroutines();
        isMoving = false;
        movingToB = true;
        transform.position = new Vector3(pointA.x, pointA.y, transform.position.z);
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    private IEnumerator MoveObject()
    {
        isMoving = true;

        while (isActivated)
        {
            Vector2 currentTarget = movingToB ? pointB : pointA;
            Vector3 targetPosition = new Vector3(currentTarget.x, currentTarget.y, transform.position.z);

            // Di chuyển đến điểm đích
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime
                );
                yield return null;
            }

            // Đã đến điểm đích
            if (isLoopMovement)
            {
                // Nếu là di chuyển vòng lặp
                if (movingToB)
                {
                    yield return new WaitForSeconds(delayAtPoints);
                    if (spriteRenderer != null) spriteRenderer.enabled = false;
                    transform.position = new Vector3(pointA.x, pointA.y, transform.position.z);
                    if (spriteRenderer != null) spriteRenderer.enabled = true;
                }
                // Luôn di chuyển từ A đến B
            }
            else
            {
                // Nếu là di chuyển ngược lại
                yield return new WaitForSeconds(delayAtPoints);
                movingToB = !movingToB; // Đảo chiều di chuyển
            }
        }

        isMoving = false;
    }

    // Phương thức để cập nhật điểm trong code nếu cần
    public void SetPoints(Vector2 newPointA, Vector2 newPointB)
    {
        pointA = newPointA;
        pointB = newPointB;
    }
}