using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour 
{
	public float maxSpeed = 10f;
	public float jumpForce = 500f;
	bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public AudioClip[] audioClip;

	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		float move = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update ()
	{
		if (grounded && Input.GetButtonDown("Jump"))
		{
			PlaySound(0);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}

		if (Input.GetButtonDown("Mine"))
		{
			anim.SetTrigger("Digging");
			Vector2 rayDirection = new Vector2(0,0);

			if (!facingRight)
				rayDirection = -Vector2.right;
			else
				rayDirection = Vector2.right;

			Debug.DrawRay (transform.position, rayDirection * 0.5f, Color.green);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 0.5f, whatIsGround, 0, 0);
			if (hit != null && hit.transform != null)
			{
				if (hit.collider.gameObject.tag == "TileMineable")
				{
					PlaySound(1);
					hit.collider.gameObject.SendMessage( "ReduceHealth" );
				}
			}
		}
	}

	void PlaySound(int clip)
	{
		audio.clip = audioClip[clip];
		audio.Play();
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}