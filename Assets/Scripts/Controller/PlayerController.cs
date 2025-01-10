/*
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;
    private bool holdJump = false;

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

        // Truyền trạng thái giữ nút nhảy
        playerMovement.HoldJump(holdJump);
    }

    // Các phương thức cho nút di chuyển trái
    public void OnLeftButtonDown() => moveLeft = true;
    public void OnLeftButtonUp() => moveLeft = false;

    // Các phương thức cho nút di chuyển phải
    public void OnRightButtonDown() => moveRight = true;
    public void OnRightButtonUp() => moveRight = false;

    // Các phương thức cho nút nhảy
    public void OnJumpButtonDown() => jump = true; // Bắt đầu nhảy
    public void OnJumpButtonHold() => holdJump = true; // Khi người dùng giữ nút nhảy
    public void OnJumpButtonUp() => holdJump = false; // Khi người dùng thả nút nhảy
}
*/
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;
    private bool holdJump = false;

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
        playerMovement.HoldJump(holdJump);

        // Thực hiện nhảy
        if (jump)
        {
            playerMovement.Jump();
            jump = false; // Reset trạng thái nhảy sau khi thực hiện
        }
    }

    // Xử lý đầu vào từ bàn phím và nút cảm ứng cho di chuyển
    private void HandleMovementInput()
    {
        // Input từ bàn phím
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Di chuyển sang trái
        {
            moveLeft = true;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Di chuyển sang phải
        {
            moveRight = true;
        }
        else
        {
            moveLeft = false;
            moveRight = false;
        }

        // Input từ nút cảm ứng (nếu có)
        // Đảm bảo các sự kiện OnLeftButtonDown, OnLeftButtonUp, OnRightButtonDown, OnRightButtonUp được gắn đúng
    }

    // Xử lý nhảy từ bàn phím hoặc nút cảm ứng
    private void HandleJumpInput()
    {
        // Nhảy với bàn phím
        if (Input.GetKeyDown(KeyCode.Space)) // Nếu nhấn Space
        {
            jump = true; // Thực hiện nhảy
        }

        // Giữ nút nhảy
        if (Input.GetKey(KeyCode.Space)) // Nếu đang giữ Space
        {
            holdJump = true;
        }
        else
        {
            holdJump = false;
        }

        // Xử lý nút cảm ứng (nếu có)
        // Đảm bảo các sự kiện OnJumpButtonDown, OnJumpButtonHold, OnJumpButtonUp được gắn đúng
    }

    // Các phương thức cho nút di chuyển trái
    public void OnLeftButtonDown() => moveLeft = true;
    public void OnLeftButtonUp() => moveLeft = false;

    // Các phương thức cho nút di chuyển phải
    public void OnRightButtonDown() => moveRight = true;
    public void OnRightButtonUp() => moveRight = false;

    // Các phương thức cho nút nhảy
    public void OnJumpButtonDown() => jump = true; // Bắt đầu nhảy
    public void OnJumpButtonHold() => holdJump = true; // Khi người dùng giữ nút nhảy
    public void OnJumpButtonUp() => holdJump = false; // Khi người dùng thả nút nhảy
}
