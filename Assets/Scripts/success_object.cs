using UnityEngine;
using System.Collections;

public class success_object : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "dragon") {
			gameObject.SetActive (false);
		}
	}
}
