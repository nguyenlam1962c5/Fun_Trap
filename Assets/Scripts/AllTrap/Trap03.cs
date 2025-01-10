using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Trap03 : TrapBase
{
    public float speed = 1f;
    public float distance = 5f;
    public string moveDirection = "up";
    public bool startMovingImmediately = true;
    public int numberOfCycles = 2;  // Number of complete movement cycles
    public float delayBetweenCycles = 0.5f;  // Delay between cycles in seconds

    private Vector2 startPosition;
    private Vector2 moveVector;
    private bool isMoving;
    private int currentCycle = 0;
    private Coroutine cycleCoroutine;

    private void Start()
    {
        startPosition = transform.position;
        moveVector = GetMoveDirection();

        if (startMovingImmediately)
        {
            InitiateMovement();
        }
    }

    public override void HandleTrap(Vector2 playerPosition, TrapControl trapControl, System.Action onComplete)
    {
        if (!startMovingImmediately && !isMoving && isVisible && trapControl.IsPlayerInZone(playerPosition))
        {
            InitiateMovement();
            onComplete?.Invoke();
        }
    }

    private void InitiateMovement()
    {
        if (!isMoving)
        {
            isMoving = true;
            currentCycle = 0;
            cycleCoroutine = StartCoroutine(ExecuteMovementCycles());
        }
    }

    private IEnumerator ExecuteMovementCycles()
    {
        while (currentCycle < numberOfCycles)
        {
            // Forward movement
            MoveObject(moveVector, speed, distance);
            yield return new WaitForSeconds(distance / speed + delayBetweenCycles);

            // Return movement
            MoveObject(-moveVector, speed, distance);
            yield return new WaitForSeconds(distance / speed + delayBetweenCycles);

            currentCycle++;
        }

        isMoving = false;
    }

    private Vector2 GetMoveDirection()
    {
        switch (moveDirection.ToLower())
        {
            case "up": return Vector2.up;
            case "down": return Vector2.down;
            case "left": return Vector2.left;
            case "right": return Vector2.right;
            default: return Vector2.zero;
        }
    }

    private void OnDisable()
    {
        if (cycleCoroutine != null)
        {
            StopCoroutine(cycleCoroutine);
        }
    }
}