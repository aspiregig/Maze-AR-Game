﻿using UnityEngine;
using System.Collections;
using Vuforia;

public class ButtonEventHandler2 : MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject right;
	public GameObject left;
	public GameObject indicator;
	public GameObject right_indicator;
	public GameObject left_indicator;

	#region PUBLIC_METHODS
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
	{
		switch (vb.VirtualButtonName) {
		case "right":
			R.turn_right = true;
			break;
		case "left":
			R.turn_left = true;
			break;			
		}	
		print ("press ---------------- ");
	}

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
	{
		switch (vb.VirtualButtonName) {
		case "right":
			R.turn_right = false;
			break;
		case "left":
			R.turn_left = false;
			break;			
		}	

		print ("release ---------------- ");
	}
	#endregion //PUBLIC_METHODS


	#region MONOBEHAVIOUR_METHODS
	// Use this for initialization
	void Start ()
	{
		VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour> ();
		for (int i = 0; i < vbs.Length; i++)
		{
			vbs [i].RegisterEventHandler (this);
		}
		R.turn_right = false;
		R.turn_left = false;
	}


	// Update is called once per frame
	void Update () {
		print ("right ---   "+R.turn_right);
		print ("left ---   "+R.turn_left);
		right.SetActive (R.turn_right);
		right_indicator.SetActive (R.turn_right);

		left.SetActive (R.turn_left);
		left_indicator.SetActive (R.turn_left);

		watch_indicator ();
	}

	void watch_indicator() {
		if (left.GetComponent<MeshRenderer> ().enabled || right.GetComponent<MeshRenderer> ().enabled)
			indicator.SetActive (true);
		else
			indicator.SetActive (false);
	}
	#endregion // MONOBEHAVIOUR_METHODS

}
 
