using UnityEngine;

public class MovementController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    

    [Header("Config")]
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public bool CanMove = true;
    public bool CanRotate = true;
    [Header("Debug")]
    public Vector3 overallDirection;
    public float moveMinimum = 0.001f;

    float Horizontal;
    float Vertical;

    float turnSmoothVelocity;

    Vector3 velocity;

    Animator animator;

    
    Vector3 lastPosition;

    public bool isMoving
    {
        get
        {
            float distance = Vector3.Distance(transform.position, lastPosition);
            return (distance > moveMinimum);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
        lastPosition = transform.position;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(Horizontal, 0f, Vertical);

        if (direction != Vector3.zero)
            MovePlayer(direction);
        else
            animator.SetBool("isMoving", false);



        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void MovePlayer(Vector3 direction) 
    {
        if (!CanMove) 
        {
            return;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            if (CanRotate) 
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            animator.SetBool("isMoving", true);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            overallDirection = moveDir;
            controller.Move(moveDir * speed * Time.deltaTime); 
        }
        else 
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void RotatePlayer(Vector3 direction) 
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

   
}
