﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class UpdateWorldMappingStatus : MonoBehaviour {

	public Text text;
	public Text tracking;

	void Start () {

		UnityARSessionNativeInterface.ARFrameUpdatedEvent += CheckWorldMapStatus;
	}

	void CheckWorldMapStatus(UnityARCamera cam){

        text.text = cam.worldMappingStatus.ToString ();
		tracking.text = cam.trackingState.ToString () + " " + cam.trackingReason.ToString ();
	}

	void OnDestroy(){

		UnityARSessionNativeInterface.ARFrameUpdatedEvent -= CheckWorldMapStatus;
	}
}