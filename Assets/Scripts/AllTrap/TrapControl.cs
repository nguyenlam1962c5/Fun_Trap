using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrapControl : MonoBehaviour
{
    public Vector2 sensorPoint1;
    public Vector2 sensorPoint2;
    public GameObject targetTrap; // Thay thế trapTag bằng tham chiếu trực tiếp
    public string playerTag = "Player";
    public float activationDelay = 0f;

    private TrapBase trapScript;
    private bool wasPlayerInArea = false;
    private bool isWaitingToActivate = false;

    private void Start()
    {
        if (targetTrap == null)
        {
            Debug.LogError($"TrapControl trên GameObject {gameObject.name}: Chưa gán trap! Vui lòng kéo trap vào Target Trap trong Inspector.");
            enabled = false;
            return;
        }

        trapScript = targetTrap.GetComponent<TrapBase>();
        if (trapScript == null)
        {
            Debug.LogError($"TrapControl trên GameObject {gameObject.name}: GameObject được gán không có script TrapBase!");
            enabled = false;
            return;
        }

        trapScript.Initialize();
    }

    private void Update()
    {
        bool isPlayerInArea = IsPlayerInArea();

        if (isPlayerInArea && !wasPlayerInArea && !isWaitingToActivate)
        {
            if (activationDelay <= 0)
            {
                trapScript?.Activate();
            }
            else
            {
                isWaitingToActivate = true;
                StartCoroutine(ActivateWithDelay());
            }
        }

        wasPlayerInArea = isPlayerInArea;
    }

    private IEnumerator ActivateWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);
        trapScript?.Activate();
        isWaitingToActivate = false;
    }

    private bool IsPlayerInArea()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
        {
            Debug.LogWarning($"TrapControl trên GameObject {gameObject.name}: Không tìm thấy GameObject nào có tag '{playerTag}'!");
            return false;
        }

        Vector2 playerPos = player.transform.position;
        float minX = Mathf.Min(sensorPoint1.x, sensorPoint2.x);
        float maxX = Mathf.Max(sensorPoint1.x, sensorPoint2.x);
        float minY = Mathf.Min(sensorPoint1.y, sensorPoint2.y);
        float maxY = Mathf.Max(sensorPoint1.y, sensorPoint2.y);

        return playerPos.x >= minX && playerPos.x <= maxX &&
               playerPos.y >= minY && playerPos.y <= maxY;
    }

    public void SetDuration(float duration)
    {
        trapScript?.Activate(duration);
    }

    // Thêm phương thức để đặt trap trong code nếu cần
    public void SetTargetTrap(GameObject trap)
    {
        if (trap == null) return;

        targetTrap = trap;
        trapScript = trap.GetComponent<TrapBase>();
        if (trapScript != null)
        {
            trapScript.Initialize();
        }
    }
}






/*using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrapControl : MonoBehaviour
{
    public Vector2 sensorPoint1;
    public Vector2 sensorPoint2;
    [SerializeField] private string trapTag;
    public string playerTag = "Player";
    public float activationDelay = 0f; // Thêm delay cho kích hoạt

    private GameObject trapObject;
    private TrapBase trapScript;
    private bool wasPlayerInArea = false;
    private bool isWaitingToActivate = false; // Biến kiểm tra đang chờ kích hoạt

    private void Start()
    {
        if (string.IsNullOrEmpty(trapTag))
        {
            Debug.LogError($"TrapControl trên GameObject {gameObject.name}: trapTag chưa được thiết lập! Vui lòng gán tag trong Inspector.");
            enabled = false;
            return;
        }

        trapObject = GameObject.FindGameObjectWithTag(trapTag);
        if (trapObject == null)
        {
            Debug.LogError($"TrapControl trên GameObject {gameObject.name}: Không tìm thấy GameObject nào có tag '{trapTag}'!");
            enabled = false;
            return;
        }

        trapScript = trapObject.GetComponent<TrapBase>();
        if (trapScript == null)
        {
            enabled = false;
            return;
        }

        trapScript.Initialize();
    }

    private void Update()
    {
        bool isPlayerInArea = IsPlayerInArea();

        // Kích hoạt khi player mới bước vào vùng cảm biến và chưa đang chờ kích hoạt
        if (isPlayerInArea && !wasPlayerInArea && !isWaitingToActivate)
        {
            if (activationDelay <= 0)
            {
                // Kích hoạt ngay nếu không có delay
                trapScript?.Activate();
            }
            else
            {
                // Bắt đầu đếm ngược delay
                isWaitingToActivate = true;
                StartCoroutine(ActivateWithDelay());
            }
        }

        wasPlayerInArea = isPlayerInArea;
    }

    private IEnumerator ActivateWithDelay()
    {
        yield return new WaitForSeconds(activationDelay);
        trapScript?.Activate();
        isWaitingToActivate = false;
    }

    private bool IsPlayerInArea()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
        {
            Debug.LogWarning($"TrapControl trên GameObject {gameObject.name}: Không tìm thấy GameObject nào có tag '{playerTag}'!");
            return false;
        }

        Vector2 playerPos = player.transform.position;
        float minX = Mathf.Min(sensorPoint1.x, sensorPoint2.x);
        float maxX = Mathf.Max(sensorPoint1.x, sensorPoint2.x);
        float minY = Mathf.Min(sensorPoint1.y, sensorPoint2.y);
        float maxY = Mathf.Max(sensorPoint1.y, sensorPoint2.y);

        return playerPos.x >= minX && playerPos.x <= maxX &&
               playerPos.y >= minY && playerPos.y <= maxY;
    }

    public void SetDuration(float duration)
    {
        trapScript?.Activate(duration);
    }

    public string TrapTag
    {
        get { return trapTag; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError($"TrapControl trên GameObject {gameObject.name}: Không thể set trapTag thành null hoặc empty!");
                return;
            }
            trapTag = value;
        }
    }
}*/