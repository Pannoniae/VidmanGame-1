using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour {
    [FormerlySerializedAs("m_JumpForce")] [SerializeField] private float jumpForce = 400f;
    [FormerlySerializedAs("m_CrouchSpeed")] [Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;
    [FormerlySerializedAs("m_MovementSmoothing")] [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [FormerlySerializedAs("m_AirControl")] [SerializeField] private bool airControl = false;
    [FormerlySerializedAs("m_WhatIsGround")] [SerializeField] private LayerMask whatIsGround;
    [FormerlySerializedAs("m_GroundCheck")] [SerializeField] private Transform groundCheck;
    [FormerlySerializedAs("m_CeilingCheck")] [SerializeField] private Transform ceilingCheck;
    [FormerlySerializedAs("m_CrouchDisableCollider")] [SerializeField] private Collider2D crouchDisableCollider;

    const float groundedRadius = .2f;
    private bool grounded;
    const float ceilingRadius = .2f;
    private new Rigidbody2D rigidbody2D;
    private bool facingRight = true;
    private Vector3 velocity = Vector3.zero;

    [Header("Events")] [Space] public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> {
    }

    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate() {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        foreach (var t in colliders) {
            if (t.gameObject != gameObject) {
                grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


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
                    OnCrouchEvent.Invoke(true);
                }

                move *= crouchSpeed;

                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = false;
            }
            else {
                if (crouchDisableCollider != null)
                    crouchDisableCollider.enabled = true;

                if (wasCrouching) {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            var velocity = rigidbody2D.velocity;
            Vector3 targetVelocity = new Vector2(move * 10f, velocity.y);
            rigidbody2D.velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref this.velocity,
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
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }


    private void Flip() {
        facingRight = !facingRight;

        var transform1 = transform;
        var theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if(Input.GetButtonDown("Jump") && grounded)
        {
            anim.SetBool("hasJumped", true);
        }
        else if(Input.GetButtonUp("Jump") || !grounded)
        {
            anim.SetBool("hasJumped", false);
        }
        if(!grounded)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }
    }
}