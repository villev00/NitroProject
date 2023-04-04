using UnityEngine;
using data;
using logic;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    float gravity = 9.81f;
    //PlayerData pdata;
    PlayerLogic plogic;
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
        //pdata = GetComponent<PlayerData>();
        plogic = GetComponent<PlayerLogic>();
        orientation = GameObject.Find("Orientation").GetComponent<Transform>();
    }

    private void Start()
    {
        FetchData();
    }

    void Update()
    {
        PlayerInput();
        Vector3 moveDirection = mlogic.MovePlayer(horizontal,vertical,orientation.forward, orientation.right);
        
        //if(horizontal != 0 || vertical != 0)
        //{
            
        //}
        if (controller.isGrounded)
        {
            moveDirection.y = -1;
            if (Input.GetKeyDown("space"))
            {
                moveDirection.y += 10;
            }
        }
        else
        {
            moveDirection.y -= 5 * gravity * Time.deltaTime;
        }
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

    }
    void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    
    public void FetchData()
    {
        moveSpeed = plogic.GetSpeed();
        jumpForce = plogic.GetJumpForce();
    }
}
