using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    float gravity = 9.81f;

    [SerializeField]
    int moveSpeed = 8;     // Spagettia
    float verticalSpeed;    // Spagettia
    float jumpForce = 2;    // Spagettia
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        FetchData();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        if (controller.isGrounded)
        {
            verticalSpeed = 0;
            if (Input.GetKeyDown("space"))
            {
                verticalSpeed = jumpForce;
            }
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        Vector3 moveDirection = new Vector3(0, 0, 0);
        if (direction.magnitude >= 0.1)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;        // Spagettia
            //angle = playerLogic.GetMoveAngle(directionX, directionZ);
            moveDirection = Quaternion.Euler(0, angle + transform.eulerAngles.y, 0) * Vector3.forward;
        }
        moveDirection.y = verticalSpeed;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void FetchData()
    {
        // moveSpeed = PlayerData.getSpeed();
        // jumpForce = PlayerData.getJumpForce();
    }
}
