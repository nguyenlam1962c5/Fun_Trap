
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap02 : TrapBase
{
    public bool permanent = false; // Trap có ẩn vĩnh viễn không
    public float revealDelay = 3f; // Thời gian ẩn trước khi hiển thị lại

    public override void HandleTrap(Vector2 playerPosition, TrapControl trapControl, System.Action onComplete)
    {
        if (isVisible && trapControl.IsPlayerInZone(playerPosition))
        {
            if (!permanent)
            {
                StartCoroutine(HideAndRevealTrap(revealDelay, onComplete));
            }
            else
            {
                SetVisibility(false);
                isVisible = false;
                onComplete?.Invoke();
            }
        }
    }

    private IEnumerator HideAndRevealTrap(float delay, System.Action onComplete)
    {
        SetVisibility(false);
        isVisible = false;
        yield return new WaitForSeconds(delay);
        SetVisibility(true);
        isVisible = true;
        onComplete?.Invoke();
    }
}
