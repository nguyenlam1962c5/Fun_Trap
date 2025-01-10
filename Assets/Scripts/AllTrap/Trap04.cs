/*
using UnityEngine;
using System;
using System.Collections;

public class Trap04 : TrapBase
{
    [Header("First Scale Settings")]
    public float scaleX = 2f;
    public float scaleY = 2f;
    public float firstScaleDuration = 1f;

    [Header("Second Scale Settings")]
    public bool useSecondScale = false;
    public float secondScaleX = 0.5f;
    public float secondScaleY = 0.5f;
    public float secondScaleDuration = 1f;
    public float delayBetweenScales = 1f;

    private Vector3 originalScale;
    private bool isScaling = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public override void HandleTrap(Vector2 playerPosition, TrapControl trapControl, Action customAction)
    {
        if (!isScaling)
        {
            StartCoroutine(ScaleCoroutine(customAction));
        }
    }

    private IEnumerator ScaleCoroutine(Action onComplete)
    {
        isScaling = true;

        // First scale animation
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(
            originalScale.x * scaleX,
            originalScale.y * scaleY,
            originalScale.z
        );

        while (elapsedTime < firstScaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / firstScaleDuration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        if (useSecondScale)
        {
            // Delay before second scale
            yield return new WaitForSeconds(delayBetweenScales);

            // Second scale animation
            elapsedTime = 0f;
            startScale = transform.localScale;
            targetScale = new Vector3(
                originalScale.x * secondScaleX,
                originalScale.y * secondScaleY,
                originalScale.z
            );

            while (elapsedTime < secondScaleDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / secondScaleDuration;
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                yield return null;
            }
        }

        isScaling = false;
        onComplete?.Invoke();
    }

    public void ResetScale()
    {
        transform.localScale = originalScale;
        isScaling = false;
    }
}
*/
using UnityEngine;
using System;
using System.Collections;

public class Trap04 : TrapBase
{
    [Header("First Scale Settings")]
    public float scaleX = 2f;
    public float scaleY = 2f;
    public float firstScaleDuration = 1f;

    [Header("Second Scale Settings")]
    public bool useSecondScale = false;
    public float secondScaleX = 0.5f;
    public float secondScaleY = 0.5f;
    public float secondScaleDuration = 1f;
    public float delayBetweenScales = 1f;

    private Vector3 originalScale;
    private bool isScaling = false;
    private bool hasTriggered = false;  // Thêm cờ trạng thái

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public override void HandleTrap(Vector2 playerPosition, TrapControl trapControl, Action customAction)
    {
        if (!isScaling && !hasTriggered)  // Kiểm tra xem bẫy đã được kích hoạt chưa
        {
            StartCoroutine(ScaleCoroutine(customAction));
            hasTriggered = true;  // Đánh dấu là bẫy đã được kích hoạt
        }
    }

    private IEnumerator ScaleCoroutine(Action onComplete)
    {
        isScaling = true;

        // First scale animation
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new Vector3(
            originalScale.x * scaleX,
            originalScale.y * scaleY,
            originalScale.z
        );

        while (elapsedTime < firstScaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / firstScaleDuration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        if (useSecondScale)
        {
            // Delay before second scale
            yield return new WaitForSeconds(delayBetweenScales);

            // Second scale animation
            elapsedTime = 0f;
            startScale = transform.localScale;
            targetScale = new Vector3(
                originalScale.x * secondScaleX,
                originalScale.y * secondScaleY,
                originalScale.z
            );

            while (elapsedTime < secondScaleDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / secondScaleDuration;
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                yield return null;
            }
        }

        isScaling = false;
        onComplete?.Invoke();
    }

    public void ResetScale()
    {
        transform.localScale = originalScale;
        isScaling = false;
        hasTriggered = false;  // Khôi phục lại trạng thái khi bẫy có thể được kích hoạt lại
    }
}
