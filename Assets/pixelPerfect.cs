using UnityEngine;
using System.Collections;

public class pixelPerfect : MonoBehaviour 
{
	public int zoomLevel = 1;

	protected int orthoSize;
	protected int currentZoom;

	// Use this for initialization
	void Start () 
	{
		if (camera.isOrthoGraphic == false){
			camera.orthographic = true;
		}
		
		if(zoomLevel < 1)  // Avoid divide by zero
			zoomLevel = 1;
		
		orthoSize = (Screen.height / 2) / zoomLevel;
		camera.orthographicSize = orthoSize;
	}
}
