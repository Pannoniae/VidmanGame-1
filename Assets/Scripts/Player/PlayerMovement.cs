
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour {

    [FormerlySerializedAs("m_JumpForce")] [SerializeField]
    private float jumpForce = 400f;

    [FormerlySerializedAs("m_CrouchSpeed")] [Range(0, 1)] [SerializeField]
    private float crouchSpeed = .36f;

    [FormerlySerializedAs("m_MovementSmoothing")] [Range(0, .3f)] [SerializeField]
    private float movementSmoothing = .05f;

    [FormerlySerializedAs("m_AirControl")] [SerializeField]
    private bool airControl = false;

    [FormerlySerializedAs("m_WhatIsGround")] [SerializeField]
    private LayerMask whatIsGround;

    [FormerlySerializedAs("m_GroundCheck")] [SerializeField]
    private Transform groundCheck;

    [FormerlySerializedAs("m_CeilingCheck")] [SerializeField]
    private Transform ceilingCheck;

    [FormerlySerializedAs("m_CrouchDisableCollider")] [SerializeField]
    private Collider2D crouchDisableCollider;

    bool isCrouchColliderPresent = false;

    private Rigidbody2D body;

    const float groundedRadius = .2f;
    private bool grounded;
    const float ceilingRadius = .2f;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    private Collider2D[] colliders = new Collider2D[100]; // increase if not enough

    private bool wasCrouching = false;

    public Animator anim;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");

    private void Awake() {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        if (crouchDisableCollider != null) {
            isCrouchColliderPresent = true;
        }
    }
    

/*Physics2D.OverlapCircleNonAlloc(groundCheck.position, groundedRadius, colliders, whatIsGround);
foreach (var t in colliders) {
    if (t == null) continue;
    if (t.gameObject != gameObject) {
        grounded = true;
        if (!wasGrounded)
            OnLandEvent.Invoke();
    }
}*/


    public void Move(float move, bool crouch, bool jump) {
        if (!crouch) {
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) {
                crouch = true;
            }
        }

        if (grounded || airControl) {
            if (crouch) {
                if (!wasCrouching) {
                    wasCrouching = true;
                }

                move *= crouchSpeed;

                if (isCrouchColliderPresent)
                    crouchDisableCollider.enabled = false;
            }
            else {
                if (isCrouchColliderPresent)
                    crouchDisableCollider.enabled = true;

                if (wasCrouching) {
                    wasCrouching = false;
                }
            }

            var velocity1 = body.velocity;
            Vector3 targetVelocity = new Vector2(move * 10f, velocity1.y);
            body.velocity = Vector3.SmoothDamp(velocity1, targetVelocity, ref velocity,
                movementSmoothing);

            if (move > 0 && !facingRight) {
                Flip();
            }
            else if (move < 0 && facingRight) {
                Flip();
            }
        }

        if (grounded && jump) {
            grounded = false;
            body.AddForce(new Vector2(0f, jumpForce));
        }
    }


    private void Flip() {
        facingRight = !facingRight;

        var transform1 = transform;
        var theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
        if (!Util.FloatEquals(Input.GetAxisRaw("Horizontal"), 0f)) {
            anim.SetBool(IsWalking, true);
        }
        else {
            anim.SetBool(IsWalking, false);
        }

        if (Input.GetButtonDown("Jump") && grounded) {
            anim.SetBool(IsJumping, true);
        }

        if (!grounded) {
            anim.SetBool(IsJumping, false);
            anim.SetBool(IsFalling, true);
        }
        else {
            anim.SetBool(IsFalling, false);
        }
    }

    void FixedUpdate() {
        Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        bool wasGrounded = grounded;
        grounded = false;

        if (Util.FloatEquals(velocity.y, 0)) {
            grounded = true;
        }

        if (!wasGrounded) {
        }

        print((grounded, velocity));
    }
}