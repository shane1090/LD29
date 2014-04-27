using UnityEngine;
using System.Collections;

public class tileScript : MonoBehaviour {

	public float tileHealth = 100f;
	public Sprite damagedTile;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void ReduceHealth ()
	{
		tileHealth-= 50f;

		if (damagedTile != null)
			spriteRenderer.sprite = damagedTile;

		if (tileHealth == 0f)
		{
			Destroy(gameObject);
		}
	}
}
