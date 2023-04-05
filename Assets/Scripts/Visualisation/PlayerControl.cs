using UnityEngine;
using data;
using logic;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    PhotonView pv;
    [SerializeField]
    CharacterController controller;
    float gravity = 9.81f;
    PlayerLogic plogic;
    Movement mlogic;
    Transform orientation;

    [SerializeField]
    float moveSpeed;
    float jumpForce;
    float verticalSpeed;    // Spagettia
    float horizontal;
    float vertical;
    Vector3 moveDirection = new Vector3();
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        mlogic = GetComponent<Movement>();
        plogic = GetComponent<PlayerLogic>();
        orientation = GameObject.Find("Orientation").GetComponent<Transform>();
    }

    private void Start()
    {
        FetchData();
    }

    void Update()
    {
        if (!pv.IsMine) return;
        PlayerInput();
        GetDirection();
        HandleJump();
        MoveCharacter();
    }
    void HandleJump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = -1;
            if (Input.GetKeyDown("space"))
            {
                moveDirection.y += 20;
            }
        }
        else
        {
            moveDirection.y -= 7 * gravity * Time.deltaTime;
        }
    }

    void GetDirection()
    {
        moveDirection = mlogic.MovePlayer(horizontal, vertical, orientation.forward, orientation.right);
    }

    void MoveCharacter()
    {
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