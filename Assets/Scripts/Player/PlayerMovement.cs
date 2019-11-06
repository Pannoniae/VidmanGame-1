using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour {
    [FormerlySerializedAs("m_JumpForce")] [SerializeField]
    private float jumpForce = 500f;

    [FormerlySerializedAs("m_CrouchSpeed")] [Range(0, 1)] [SerializeField]
    private float crouchSpeed = .36f;

    private Rigidbody2D body;

    private bool grounded;
    private bool facingRight = true;

    public float runSpeed = 40f;

    float horizontalMove;
    bool jump;
    bool crouch;

    private bool wasCrouching;

    public Animator anim;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    public GameManager gm;

    private void Awake() {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }


    public void Move(float move) {
        if (crouch) {
            if (!wasCrouching) {
                wasCrouching = true;
            }

            move *= crouchSpeed;
        }
        else {
            if (wasCrouching) {
                wasCrouching = false;
            }
        }

        body.velocity = new Vector2(move * 10, body.velocity.y);


        if (move > 0 && !facingRight) {
            Flip();
        }
        else if (move < 0 && facingRight) {
            Flip();
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

    public PlayerMovement() {
        wasCrouching = false;
    }

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

        if (Input.GetKeyDown(KeyCode.E)) {
            // ReSharper disable once Unity.PreferNonAllocApi
            var objects = gm.elements;
            foreach (var o in objects) {
                if (o is Button) {
                    print("a");
                    o.flip();
                }
            }
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
        Move(horizontalMove * Time.fixedDeltaTime);
        jump = false;
        grounded = false;

        if (IsGrounded()) {
            grounded = true;
        }

        print(IsGrounded());
    }

    public bool IsGrounded() {
        const float distance = 1.5f;
        var position = body.position + new Vector2(0, -distance);
        const float radius = 0.5f;

        var hit = Physics2D.OverlapCircle(position, radius, LayerMask.GetMask("Ground"));
        //DrawEllipse(position, Vector3.forward, Vector3.up, radius, radius, 360, Color.green);
        return hit != null;
    }
}