using UnityEngine;
using data;
using logic;
using Photon.Pun;
using Photon.Realtime;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    PhotonView pv;
    SpellUI spellUI;
    [SerializeField]
    CharacterController controller;
    float gravity = 9.81f;
    PlayerLogic pLogic;
    Movement mLogic;
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
        if (pv.IsMine)
        {
            controller = GetComponent<CharacterController>();
            mLogic = GetComponent<Movement>();
            pLogic = GetComponent<PlayerLogic>();
            spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
            orientation = transform.GetChild(1).GetComponent<Transform>();
            FetchData();
            spellUI.spellManager = gameObject;
            spellUI.pv = pv;
        }
    }


    void Update()
    {
        if (!pv.IsMine) return;
        PlayerInput();
        GetDirection();
        //HandleJump();
        if (controller.isGrounded)
        {
            moveDirection.y = -1;
            if (Input.GetKeyDown("space"))
            {
                moveDirection.y += 10; // jumpForcee is not updated correctly
            }
        }
        else
        {
            moveDirection.y -= 4 * gravity * Time.deltaTime;
        }
        MoveCharacter();
    }
    void HandleJump()
    {
        moveDirection.y -= gravity * Time.deltaTime;

        if (controller.isGrounded && Input.GetKeyDown("space"))
        {
            moveDirection.y = 20; // jumpForcee is not updated correctly
        }
        /*
        if (controller.isGrounded)
        {
            moveDirection.y = -1;
            if (Input.GetKeyDown("space"))
            {
                moveDirection.y += 20; // jumpForcee is not updated correctly
            }
        }
        else
        {
            moveDirection.y -= 7 * gravity * Time.deltaTime;
        }
         */
    }

    void GetDirection()
    {
        moveDirection = mLogic.MovePlayer(horizontal, vertical, orientation.forward, orientation.right);
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
        moveSpeed = pLogic.GetSpeed();
        jumpForce = pLogic.GetJumpForce();
    }
}