using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.Movement
{
    public class PlayerMovementManager : MonoBehaviour
    {
        public static PlayerMovementManager instance;
        [Header("Movement")]
        private float moveSpeed;
        public float walkSpeed;
        public float sprintSpeed;
        public bool isSprinting;
        public bool outOfStam;
        public float maxStam;
        public float groundDrag;
        private float accel = 0.3f;

        [Header("Jumping")]
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

        [Header("Crouching")]
        public float crouchSpeed;
        public float crouchHeight;
        private float startHeight;

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask thisGround;
        bool grounded;

        public Transform orientation;

        public Camera _cam;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;

        Rigidbody rb;

        public MovementState state;

        public enum MovementState
        {
            walking,
            sprinting,
            air,
            crouching,
        }
        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            readyToJump = true;
            startHeight = transform.localScale.y;
        }

        void Update()
        {
            //ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, thisGround);

            MyInput();
            SpeedCap();
            StateHandler();

            // handle drag
            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        void FixedUpdate()
        {
            MovePlayer();

            //_cam.GetComponent<Camera>().fieldOfView = moveSpeed*2f+40;

            if (isSprinting == true)
            {
                if (moveSpeed <= sprintSpeed)
                {
                    moveSpeed += accel;
                    //moveSpeed = Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed);
                    moveSpeed = sprintSpeed;
                }
                else if (moveSpeed >= sprintSpeed)
                {
                    moveSpeed -= accel;
                    //moveSpeed = Mathf.Clamp(moveSpeed, sprintSpeed, walkSpeed);
                    moveSpeed = sprintSpeed;
                }
            }
            else if (isSprinting == false)
            {
                if (moveSpeed >= walkSpeed)
                {
                    moveSpeed -= accel;
                    //moveSpeed = Mathf.Clamp(moveSpeed, sprintSpeed, walkSpeed);
                    moveSpeed = walkSpeed;
                }
                else if (moveSpeed <= walkSpeed)
                {
                    moveSpeed += accel;
                    //moveSpeed = Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed);
                    moveSpeed = walkSpeed;
                }
            }
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // when to jump
            if (Input.GetKey(Game.Player.InputManager.instance.jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                // find out about this
                Invoke(nameof(ResetJump), jumpCooldown);


            }
        }

        public void Crouch(bool toCrouch) {
            if (toCrouch) {
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            else {
                transform.localScale = new Vector3(transform.localScale.x, startHeight, transform.localScale.z);
            }
        }

        private void StateHandler()
        {
            if (Input.GetKey(Game.Player.InputManager.instance.crouchKey))
            {
                state = MovementState.crouching;
                moveSpeed = crouchSpeed;
            }

            if (grounded && Input.GetKey(Game.Player.InputManager.instance.sprintKey))
            {
                state = MovementState.sprinting;
                StaminaSlide.instance.UseStamina(1);
                //moveSpeed = sprintSpeed;
                if (outOfStam == false)
                {
                    isSprinting = true;
                }
                else
                {
                    isSprinting = false;
                }
            }

            else if (grounded)
            {
                state = MovementState.walking;
                //moveSpeed = walkSpeed;
                isSprinting = false;
            }

            else
            {
                state = MovementState.air;
            }
        }

        private void MovePlayer()
        {
            //calc movement dir
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            //lerp player rotation :))
            if (grounded && ((Mathf.Abs(verticalInput) == 1) || Mathf.Abs(horizontalInput) == 1)) 
                transform.rotation = Quaternion.Slerp(transform.rotation, orientation.rotation, 10f * Time.deltaTime);

            //grounded
            if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            //in air
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        }

        private void SpeedCap()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            //limit vel if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            // reset y vel
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // jump force
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }
    }
}
