
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap01 : TrapBase
{
    public bool permanent = true; // Trap có hiển thị vĩnh viễn không
    public float duration = 0f;   // Thời gian hiển thị (nếu không vĩnh viễn)

    public override void HandleTrap(Vector2 playerPosition, TrapControl trapControl, System.Action onComplete)
    {
        if (!isVisible && trapControl.IsPlayerInZone(playerPosition))
        {
            SetVisibility(true);
            isVisible = true;

            if (!permanent && duration > 0)
            {
                StartCoroutine(HideTrapAfterDuration(duration, onComplete));
            }
            else
            {
                onComplete?.Invoke();
            }
        }
    }

    private IEnumerator HideTrapAfterDuration(float duration, System.Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        SetVisibility(false);
        isVisible = false;
        onComplete?.Invoke();
    }
}
