using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour 
{

	private Transform player;
	public Vector2 maxXandY;
	public Vector2 minXandY;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		TrackPlayer ();	
	}

	void TrackPlayer ()
	{
		float targetX = player.position.x;
		float targetY = player.position.y;

		//targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
		//targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
