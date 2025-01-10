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
        // Lấy tham chiếu đến PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Xử lý input cho di chuyển ngang
        float inputAxis = 0f;
        if (moveLeft)
        {
            inputAxis = -1f;
        }
        else if (moveRight)
        {
            inputAxis = 1f;
        }

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
    public void OnLeftButtonDown()
    {
        moveLeft = true;
        Debug.Log("Left button pressed");
    }
    public void OnLeftButtonUp()
    {
        moveLeft = false;
        Debug.Log("Left button released");
    }

    // Các phương thức cho nút di chuyển phải
    public void OnRightButtonDown() => moveRight = true;
    public void OnRightButtonUp() => moveRight = false;

    // Các phương thức cho nút nhảy
    public void OnJumpButtonDown() => jump = true; // Bắt đầu nhảy
}
*/
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
        // Xử lý input từ keyboard
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        // Xử lý input axis
        float inputAxis = 0f;
        if (moveLeft) inputAxis = -1f;
        if (moveRight) inputAxis = 1f;

        // Gửi giá trị input tới PlayerMovement
        playerMovement.SetHorizontalInput(inputAxis);

        // Xử lý nhảy
        if (jump)
        {
            playerMovement.Jump();
            jump = false;
        }

        // Reset các giá trị khi thả phím
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveRight = false;
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