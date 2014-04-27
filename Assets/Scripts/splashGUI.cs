using UnityEngine;
using System.Collections;

public class splashGUI : MonoBehaviour {

	public void Update() {

		if (Input.GetButtonDown("Jump"))
		{
			Application.LoadLevel("Game");
		}
	}
}
