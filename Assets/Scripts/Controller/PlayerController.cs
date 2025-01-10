
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;

    private void Awake()
    {
        // Lấy tham chiếu đến PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Xử lý input cho di chuyển ngang
        float inputAxis = 0f;
        if (moveLeft) inputAxis = -1f;
        if (moveRight) inputAxis = 1f;

        // Gửi giá trị input trục ngang tới PlayerMovement
        playerMovement.SetHorizontalInput(inputAxis);

        // Xử lý nhảy
        if (jump)
        {
            playerMovement.Jump();
            jump = false; // Reset trạng thái nhảy sau khi thực hiện
        }
    }

    // Các phương thức cho nút di chuyển trái
    public void OnLeftButtonDown() => moveLeft = true;
    public void OnLeftButtonUp() => moveLeft = false;

    // Các phương thức cho nút di chuyển phải
    public void OnRightButtonDown() => moveRight = true;
    public void OnRightButtonUp() => moveRight = false;

    // Các phương thức cho nút nhảy
    public void OnJumpButtonDown() => jump = true; // Bắt đầu nhảy

}

/*
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Di chuyển qua các nút cảm ứng hoặc bàn phím
        HandleMovementInput();

        // Nhảy qua các nút cảm ứng hoặc bàn phím
        HandleJumpInput();

        // Truyền dữ liệu vào PlayerMovement
        playerMovement.SetHorizontalInput(moveLeft ? -1f : moveRight ? 1f : 0f);

        // Thực hiện nhảy
        if (jump)
        {
            playerMovement.Jump();
            jump = false;
        }
    }

    private void HandleMovementInput()
    {
        // Input từ bàn phím
        moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    private void HandleJumpInput()
    {
        // Nhảy với bàn phím
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    // Các phương thức cho nút di chuyển trái
    public void OnLeftButtonDown() => moveLeft = true;
    public void OnLeftButtonUp() => moveLeft = false;

    // Các phương thức cho nút di chuyển phải
    public void OnRightButtonDown() => moveRight = true;
    public void OnRightButtonUp() => moveRight = false;

    // Phương thức cho nút nhảy
    public void OnJumpButtonDown() => jump = true;
}
*/