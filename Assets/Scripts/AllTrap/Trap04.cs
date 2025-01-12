// Trap04.cs
using UnityEngine;
using System.Collections;

public class Trap04 : TrapBase
{
    [Header("Scale Settings")]
    public Vector2 scaleTarget = Vector2.one;  // Tỉ lệ scale đầu tiên (x, y)
    public float scaleSpeed = 1f;              // Tốc độ scale

    [Header("Second Scale (Optional)")]
    public bool useSecondScale = false;        // Bật/tắt scale lần 2
    public Vector2 secondScaleTarget;          // Tỉ lệ scale lần 2 (x, y)
    public float delayBeforeSecondScale = 0f;  // Thời gian chờ trước khi scale lần 2

    public void SetScaleTarget(float x, float y)
    {
        scaleTarget = new Vector2(x, y);
    }

    public void SetSecondScaleTarget(float x, float y, float delay)
    {
        secondScaleTarget = new Vector2(x, y);
        delayBeforeSecondScale = delay;
        useSecondScale = true;
    }

    public void DisableSecondScale()
    {
        useSecondScale = false;
    }

    public override void Activate(float duration = 0f)
    {
        base.Activate(duration);
        StartCoroutine(ScaleObject());
    }

    private IEnumerator ScaleObject()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 target = new Vector3(scaleTarget.x * originalScale.x,
                                   scaleTarget.y * originalScale.y,
                                   originalScale.z);

        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale,
                                                     target,
                                                     scaleSpeed * Time.deltaTime);
            yield return null;
        }

        if (useSecondScale)
        {
            yield return new WaitForSeconds(delayBeforeSecondScale);
            target = new Vector3(secondScaleTarget.x * originalScale.x,
                               secondScaleTarget.y * originalScale.y,
                               originalScale.z);

            while (Vector3.Distance(transform.localScale, target) > 0.01f)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale,
                                                         target,
                                                         scaleSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}