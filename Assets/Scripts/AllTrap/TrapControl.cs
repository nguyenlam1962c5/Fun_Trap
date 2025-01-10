/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour
{
    public Vector2 lowerLeftCorner;   // Góc dưới bên trái của vùng kích hoạt
    public Vector2 upperRightCorner; // Góc trên bên phải của vùng kích hoạt

    private bool isCoroutineRunning = false;

    // Hàm kiểm tra xem Player có nằm trong vùng kích hoạt không
    public bool IsPlayerInZone(Vector2 playerPosition)
    {
        return (playerPosition.x >= lowerLeftCorner.x && playerPosition.x <= upperRightCorner.x) &&
               (playerPosition.y >= lowerLeftCorner.y && playerPosition.y <= upperRightCorner.y);
    }

    // Hàm gọi khi cần điều chỉnh trạng thái bẫy
    public void TriggerTrap(TrapBase trap, Vector2 playerPosition)
    {
        if (!isCoroutineRunning)
        {
            trap.HandleTrap(playerPosition, this, () => { isCoroutineRunning = false; });
            isCoroutineRunning = true;
        }
    }

    public void TriggerTrap02(Trap02 trap02, Vector2 playerPosition)
    {
        if (!isCoroutineRunning)
        {
            trap02.HandleTrap(playerPosition, this, () => { isCoroutineRunning = false; });
            isCoroutineRunning = true;
        }
    }

    // Phương thức mới để kích hoạt Trap03
    public void TriggerTrap03(Trap03 trap03, Vector2 direction, float speed, float distance)
    {
        trap03.MoveObject(direction, speed, distance);
    }
    public Trap04[] trap04Objects;
    public void TriggerTrap04(Trap04 trap04, Vector2 playerPosition)
    {
        if (!isCoroutineRunning)
        {
            Debug.Log($"Đang kích hoạt Trap04: {trap04.gameObject.name}");
            trap04.HandleTrap(playerPosition, this, () => {
                isCoroutineRunning = false;
                Debug.Log("Hoàn thành scale");
            });
            isCoroutineRunning = true;
        }
    }
    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;
            //Debug.Log($"Player Position: {playerPosition}"); // Check vị trí player

            if (IsPlayerInZone(playerPosition))
            {
                Debug.Log("Player is in zone!"); // Check xem player có vào zone không
                if (trap04Objects != null && trap04Objects.Length > 0)
                {
                    Debug.Log($"Found {trap04Objects.Length} Trap04 objects"); // Check số lượng trap
                    foreach (Trap04 trap in trap04Objects)
                    {
                        if (trap != null)
                        {
                            Debug.Log($"Triggering Trap04: {trap.gameObject.name}"); // Check khi kích hoạt trap
                            TriggerTrap04(trap, playerPosition);
                        }
                    }
                }
                else
                {
                    Debug.Log("No Trap04 objects assigned!"); // Check nếu không có trap nào được gán
                }
            }
        }
        else
        {
            Debug.Log("Player not found!"); // Check nếu không tìm thấy player
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour
{
    public Vector2 lowerLeftCorner;   // Góc dưới bên trái của vùng kích hoạt
    public Vector2 upperRightCorner; // Góc trên bên phải của vùng kích hoạt

    private bool isCoroutineRunning = false;

    // Hàm kiểm tra xem Player có nằm trong vùng kích hoạt không
    public bool IsPlayerInZone(Vector2 playerPosition)
    {
        return (playerPosition.x >= lowerLeftCorner.x && playerPosition.x <= upperRightCorner.x) &&
               (playerPosition.y >= lowerLeftCorner.y && playerPosition.y <= upperRightCorner.y);
    }

    // Hàm gọi khi cần điều chỉnh trạng thái bẫy
    public void TriggerTrap(TrapBase trap, Vector2 playerPosition)
    {
        if (!isCoroutineRunning)
        {
            trap.HandleTrap(playerPosition, this, () => { isCoroutineRunning = false; });
            isCoroutineRunning = true;
        }
    }

    // Trigger Trap04 based on the player position
    public void TriggerTrap04(Trap04 trap04, Vector2 playerPosition)
    {
        if (!isCoroutineRunning)
        {
            trap04.HandleTrap(playerPosition, this, () => { isCoroutineRunning = false; });
            isCoroutineRunning = true;
        }
    }

    // In the TrapControl Update
    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;
            //Debug.Log($"Player Position: {playerPosition}"); // Check player position

            if (IsPlayerInZone(playerPosition))
            {
               // Debug.Log("Player is in zone!"); // Check if player is in the zone

                // Find the trap with the tag "trap04" and trigger it
                GameObject trap04Object = GameObject.FindGameObjectWithTag("trap04");
                if (trap04Object != null)
                {
                    Trap04 trap04 = trap04Object.GetComponent<Trap04>();
                    if (trap04 != null)
                    {
                        //Debug.Log($"Triggering Trap04: {trap04.gameObject.name}"); // Log when triggering Trap04
                        TriggerTrap04(trap04, playerPosition);
                    }
                    else
                    {
                        //Debug.Log("No Trap04 script found on trap object.");
                    }
                }
                else
                {
                    //Debug.Log("No Trap04 object found with the 'trap04' tag!");
                }
            }
        }
        else
        {
            //Debug.Log("Player not found!"); // If the player is not found
        }
    }
}
