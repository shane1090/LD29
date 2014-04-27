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

	void Start () 
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

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
		if (grounded && Input.GetKeyDown(KeyCode.Space))
		{
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			Vector2 rayDirection = new Vector2(0,0);

			if (!facingRight)
				rayDirection = -Vector2.right;
			else
				rayDirection = Vector2.right;

			Debug.DrawRay (transform.position, rayDirection * 0.5f, Color.green);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 0.5f, whatIsGround, -3, -3);
			if (hit != null && hit.transform != null)
			{
				if (hit.collider.gameObject.tag == "TileMineable")
				{
					hit.collider.gameObject.SendMessage( "ReduceHealth" );
				}
			}
		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
