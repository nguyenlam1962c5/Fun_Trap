using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TrapControl : MonoBehaviour
{
    public Vector2 sensorPoint1;
    public Vector2 sensorPoint2;
    [SerializeField] private string trapTag;
    public string playerTag = "Player";
    private GameObject trapObject;
    private TrapBase trapScript;
    private bool wasPlayerInArea = false; // Thêm biến để theo dõi trạng thái trước đó

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
            //Debug.LogError($"TrapControl trên GameObject {gameObject.name}: GameObject với tag '{trapTag}' không có script TrapBase!");
            enabled = false;
            return;
        }

        trapScript.Initialize();
    }

    private void Update()
    {
        bool isPlayerInArea = IsPlayerInArea();

        // Chỉ kích hoạt khi player mới bước vào vùng cảm biến
        if (isPlayerInArea && !wasPlayerInArea)
        {
            trapScript?.Activate();
        }

        wasPlayerInArea = isPlayerInArea; // Cập nhật trạng thái cho frame tiếp theo
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
}