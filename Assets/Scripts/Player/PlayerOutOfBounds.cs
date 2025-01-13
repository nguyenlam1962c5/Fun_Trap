using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOutOfBounds : MonoBehaviour
{
    // Tham chiếu đến Camera chính
    private Camera mainCamera;
    // Biến để kiểm soát xem Player đã ra khỏi phạm vi chưa
    private bool isOutOfBounds = false;

    // Start is called before the first frame update
    void Start()
    {
        // Lấy Camera chính
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu Player đang ra khỏi phạm vi của Camera
        if (!isOutOfBounds && !IsPlayerInCameraView())
        {
            isOutOfBounds = true;
            // Gọi hàm ReloadScene sau 5 giây
            Invoke("ReloadScene", 2f);
        }
    }

    // Hàm kiểm tra nếu Player đang trong phạm vi của Camera
    bool IsPlayerInCameraView()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Kiểm tra nếu Player nằm ngoài phạm vi 0-1 của Viewport (Camera không thấy)
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            return false;
        }

        return true;
    }

    // Hàm load lại Scene
    void ReloadScene()
    {
        // Load lại Scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
