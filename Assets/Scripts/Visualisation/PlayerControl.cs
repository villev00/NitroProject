using UnityEngine;
using data;
using logic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Events;


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
    Transform cameraHolder;
    public UnityAction statChange;
    float time;
    bool isJumping;

    //Animator anime;

    float moveSpeed;
    float jumpForce;    // Spagettia
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
            orientation = transform.GetChild(0).GetComponent<Transform>();
            cameraHolder = orientation.GetChild(0).GetComponent<Transform>();
            FetchData();
            spellUI.spellManager = gameObject;
            spellUI.pv = pv;
            statChange += FetchData;
        }
    }
    private void Start()
    {       
        if (pv.IsMine)
        {
            //anime = GetComponent<Animator>();
            Camera.main.GetComponent<CameraRotate>().FindPlayer(orientation, cameraHolder);
        }
        time = 0;
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
        time += Time.deltaTime;
        if (controller.isGrounded)
        {
            //if (anime.GetBool("isJumping") || anime.GetBool("isFalling"))
            //{
            //    anime.SetTrigger("isGrounded");
            //    anime.SetBool("isJumping", false);
            //    anime.SetBool("isFalling", false);
            //}
            isJumping = false;
            time = 0;
            moveDirection.y = -1;
            if (Input.GetKeyDown("space"))
            {
                //anime.SetBool("isJumping", true);
                isJumping = true;
                moveDirection.y += 1.1f; 
            }
        }
        else
        {
            // Gravity
            float gravityMultiplier = 0.8f;
            if (isJumping) moveDirection.y += jumpForce - gravity * gravityMultiplier * time; // speed was 0.4
            else
            {
                //if(!anime.GetBool("isJumping"))
                //anime.SetBool("isFalling", true);
                moveDirection.y -= gravity * gravityMultiplier * time;
            }
        }
    }

    void GetDirection()
    {
        moveDirection = mLogic.MovePlayer(horizontal, vertical, orientation.forward, orientation.right);
    }

    void MoveCharacter()
    {
        Vector3 finalDirection = mLogic.NormalizeHorizontalMovement(moveDirection); // Final direction is a normalized vector with original jump height
        controller.Move(finalDirection * moveSpeed * Time.deltaTime);
    }
    void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    public void FetchData()
    {
        moveSpeed = pLogic.GetSpeed(); // movespeed is not updated correctly
        moveSpeed *= 1.5f;
        moveSpeed = 5;
        //jumpForce = pLogic.GetJumpForce();
        jumpForce = 1.9f; // jumpForcee is not updated correctly
    }
}