using UnityEngine;
using System.Collections;

public class tileScript : MonoBehaviour {

	public float tileHealth = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void ReduceHealth ()
	{
		tileHealth-= 50f;

		if (tileHealth == 0f)
		{
			Destroy(gameObject);
		}
	}
}
