using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed;
        [SerializeField] private float defaultSpeed;
        [SerializeField] private float sprintSpeed;
        
        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float airMultiplier;
        [SerializeField] private float jumpCooldown;
        private bool readyToJump = true;


        [Header("Ground Check")] 
        public float groundDrag;
        public float height;
        public LayerMask groundLayer;
        private bool grounded = false;

        public Transform orientation;
        private float horizontalInput;
        private float verticalInput;


        private Vector3 moveDir;

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        private void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f, groundLayer);
            PlayerInput();
            SpeedControl();
            
            rb.drag = grounded ? groundDrag : 0;
        }

        private void FixedUpdate()
        {
            Move();
            Sprint();
        }

        private void Sprint()
        {
            moveSpeed = Input.GetKey(KeyCode.LeftControl) ? sprintSpeed : defaultSpeed;
        }

        private void PlayerInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            /*if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }*/
        }

        private void Move()
        {
            moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (grounded)
            {
                rb.AddForce(moveDir.normalized * (moveSpeed * 10f), ForceMode.Force);
            }
            else if (!grounded)
            {
                rb.AddForce(moveDir.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
            }
        }
        
        private void SpeedControl()
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);

            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x , rb.velocity.y,limitedVelocity.z);
            }
        }

       /* private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f , rb.velocity.z);
            
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }*/
    }
}