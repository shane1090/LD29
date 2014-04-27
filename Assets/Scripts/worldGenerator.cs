using UnityEngine;
using System.Collections;

public class worldGenerator : MonoBehaviour 
{
	public int worldSizeX = 16;
	public int worldSizeY = 16;

	private int[] tileMap;

	public GameObject backgroundTile;
	public GameObject walkableGrassTile;
	public GameObject foregroudTile;
	public GameObject wallTile;
	public GameObject waterTile;

	private int totalTiles = 0;
	private int tilesPlaced = 0;
	private int previousX = 0;
	private int previousY = 0;

	private int realWorldX;
	private int realWorldY;

	private int tempRandom;

	private Transform player;

	private int waterLevel = 0;
	private float nextWaterUpdate = 4.0f;
	private float period = 1.0f;
	private float timer = 0.0f;

	public AudioClip[] audioClip;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		totalTiles = worldSizeX * worldSizeY;
		GenerateLevel ();
	}

	void GenerateLevel ()
	{
		tileMap = new int[totalTiles];
		
		for (tilesPlaced = 0; tilesPlaced < totalTiles; tilesPlaced++)
		{
			PlaceTile ();
		}

		PlaceWalls ();

		player.position = new Vector3(0, -(worldSizeY - 1), -1);
	}

	void PlaceTile ()
	{
		Vector3 pos = new Vector3((previousX - (worldSizeX / 2)), previousY, 0f);

		tempRandom = Random.Range(0,2);

		if (tempRandom == 0)
		{
			tileMap[tilesPlaced] = 0;
		}

		if (tempRandom == 1 && tilesPlaced < (totalTiles - worldSizeX)) // Assign a foreground tile
		{
			if (tilesPlaced > worldSizeX && tileMap[tilesPlaced - (worldSizeX+1)] != 0)
			{
				Instantiate(foregroudTile, pos, Quaternion.identity);
				tileMap[tilesPlaced] = 3;
			}
			else
			{
				if (previousY == 0)
					Instantiate(walkableGrassTile, pos, Quaternion.identity);
				else
					Instantiate(foregroudTile, pos, Quaternion.identity);

				tileMap[tilesPlaced] = 2;
			}
		}

		// Place background tile
		pos.z = 2f;
		
		// Blank tile - display background
		Instantiate(backgroundTile, pos, Quaternion.identity);

		previousX = previousX + 1;

		if (previousX > (worldSizeX - 1))
		{
			previousY = previousY - 1;
			previousX = 0;
		}
	}

	void PlaceWalls ()
	{
		previousX = -1;
		previousY = 0;

		// Place left hand walls
		for (tilesPlaced = 0; tilesPlaced < worldSizeY; tilesPlaced++)
		{
			Vector3 pos = new Vector3((previousX - (worldSizeX / 2)) , previousY, -3);
			Instantiate(wallTile, pos, Quaternion.identity);
			previousY = previousY - 1;
		}

		previousX = worldSizeX;
		previousY = 0;

		// Place right hand walls
		for (tilesPlaced = 0; tilesPlaced < worldSizeY; tilesPlaced++)
		{
			Vector3 pos = new Vector3((previousX - (worldSizeX / 2)) , previousY, -3);
			Instantiate(wallTile, pos, Quaternion.identity);
			previousY = previousY - 1;
		}

		previousX = -1;

		// Place bottom walls
		for (tilesPlaced = 0; tilesPlaced < worldSizeX + 2; tilesPlaced++)
		{
			Vector3 pos = new Vector3((previousX - (worldSizeX / 2)) , previousY, -3);
			Instantiate(wallTile, pos, Quaternion.identity);
			previousX = previousX + 1;
		}
	}

	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.R))
			RestartLevel ();

		if (waterLevel < worldSizeY)
		{
			if (timer > nextWaterUpdate)
			{
				timer = 0.0f;
				nextWaterUpdate = period;
				waterLevel++;

				previousX = -1;
				previousY = -worldSizeY + waterLevel;

				for (tilesPlaced = 0; tilesPlaced < worldSizeX + 2; tilesPlaced++)
				{
					Vector3 pos = new Vector3((previousX - (worldSizeX / 2)) , previousY, -2);
					Instantiate(waterTile, pos, Quaternion.identity);
					previousX = previousX + 1;
				}
			}

			if ((-worldSizeY + (waterLevel - 1)) > player.position.y)
			{
				PlaySound(0);
				RestartLevel ();
			}
		}

		if (player.position.y > 1)
		{
			Application.LoadLevel("Main Menu");
		}
	}

	void RestartLevel ()
	{
		tilesPlaced = 0;
		previousX = 0;
		previousY = 0;
		waterLevel = 0;
		nextWaterUpdate = 4.0f;

		// Destory all Tiles
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		foreach (GameObject go in tiles)
		{
			Destroy(go);
		}
		// Destory all Tiles
		GameObject[] tilesMineable = GameObject.FindGameObjectsWithTag("TileMineable");
		foreach (GameObject go in tilesMineable)
		{
			Destroy(go);
		}

		GenerateLevel ();
	}

	void PlaySound(int clip)
	{
		audio.clip = audioClip[clip];
		audio.Play();
	}
}
