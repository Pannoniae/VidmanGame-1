using UnityEngine;
using UnityEngine.Events;
<<<<<<< HEAD
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

    public void Update()
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
                anim.SetBool("isJumping", true);
            }
            if(!grounded)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
            }
            else
            {
                anim.SetBool("isFalling", false);
            }
        
    }
=======

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private Transform m_CeilingCheck;
	[SerializeField] private Collider2D m_CrouchDisableCollider;

	const float k_GroundedRadius = .2f;
	private bool m_Grounded;
	const float k_CeilingRadius = .2f;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		if (!crouch)
		{
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		if (m_Grounded || m_AirControl)
		{

			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				move *= m_CrouchSpeed;

				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (m_Grounded && jump)
		{
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
>>>>>>> parent of e2be3bb... Revert "asd"
}