using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoldHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float holdTime = 0f;
    private bool isHolding = false;

    // Khi nhấn nút
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTime = 2f;  // Reset thời gian giữ
    }

    // Khi thả nút
    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        Debug.Log("Hold time: " + holdTime);
    }

    private void Update()
    {
        // Tăng thời gian giữ nút
        if (isHolding)
        {
            holdTime += Time.deltaTime;
        }
    }
}
