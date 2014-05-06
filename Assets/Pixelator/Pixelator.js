// Pixelator.js
//    by Randy McMillen (aka Coldfire)

#pragma strict

var LiveUpdate : boolean;
var ZoomLevel : int = 1; // Set default to render at 1:1 pixel ratio

protected var OrthoSize : int;
protected var CurrentZoom : int;

function Start () {
	if (camera.isOrthoGraphic == false){
		camera.orthographic = true;
	}
		
	if(ZoomLevel < 1)  // Avoid divide by zero
		ZoomLevel = 1;
		
	OrthoSize = (Screen.height / 2) / ZoomLevel;
	camera.orthographicSize = OrthoSize;
		
	
}

function FixedUpdate () {
	if(LiveUpdate == true){
		if(ZoomLevel < 1)  // Avoid divide by zero
			ZoomLevel = 1;
	
		if(camera.isOrthoGraphic && OrthoSize != (Screen.height / 2) / ZoomLevel){
			OrthoSize = (Screen.height / 2) / ZoomLevel;
			camera.orthographicSize = OrthoSize;
		}	
	
		if(CurrentZoom != ZoomLevel){
			OrthoSize = (Screen.height / 2) / ZoomLevel;
			camera.orthographicSize = OrthoSize;
			CurrentZoom = ZoomLevel;
		}
	}
}