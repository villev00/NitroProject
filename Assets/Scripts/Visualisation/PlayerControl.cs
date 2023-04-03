using UnityEngine;
using data;
using logic;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    float gravity = 9.81f;
    PlayerData pdata;
    Movement mlogic;
    Transform orientation;

    [SerializeField]
    float moveSpeed;
    float jumpForce;
    float verticalSpeed;    // Spagettia
    float horizontal;
    float vertical;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        mlogic = GetComponent<Movement>();
        pdata = GetComponent<PlayerData>();
        orientation = GameObject.Find("Orientation").GetComponent<Transform>();
    }

    private void Start()
    {
        FetchData();
    }

    void Update()
    {
        PlayerInput();
        if(horizontal != 0 || vertical != 0)
        {
            Vector3 moveDirection = mlogic.MovePlayer(horizontal,vertical,orientation.forward, orientation.right);
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
       // transform.rotation = orientation.rotation;
        /*

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

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 moveDirection = mlogic.ResetDirection();
        if (direction.magnitude >= 0.1)
        {
            moveDirection = mlogic.GetDirection(direction.x, direction.z);
        }
        moveDirection.y = verticalSpeed;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        */

    }
    void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    
    public void FetchData()
    {
        moveSpeed = pdata.moveSpeed;
        jumpForce = pdata.jumpForce;
    }
}
