using UnityEngine;
using System.Collections;

public class cross_controller : MonoBehaviour {

	public GameObject default_right;
	public GameObject default_left;
	public int type;

	//type :11 - L type load right
	//type :12 - L type load left
	//type :21 - T type load 
	//type :31 - X type load 

	// Use this for initialization
	void Start () {
		init ();
	}
	
	// Update is called once per frame
	void Update () {		
		
	}

	void init() {
		default_right.SetActive (false);
		default_left.SetActive (false);
	}

	void makePoint () {		
		

	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "dragon") {				
			R.type = type;
			//====================      L Type    ====================
			if (type == 1) {
				if (dragon_controller.velocity_x == 0 && dragon_controller.velocity_z > 0) {
					default_right.SetActive (true);
					default_left.SetActive (false);
				}
				else if (dragon_controller.velocity_x < 0 && dragon_controller.velocity_z== 0) {
					default_right.SetActive (false);
					default_left.SetActive (true);
				}
			}
			if (type == 2) {
				if (dragon_controller.velocity_x == 0 && dragon_controller.velocity_z < 0) {
					default_right.SetActive (false);
					default_left.SetActive (true);
				}
				else if (dragon_controller.velocity_x < 0 && dragon_controller.velocity_z == 0) {
					default_right.SetActive (true);
					default_left.SetActive (false);
				}
			}
			if (type == 3) {
				if (dragon_controller.velocity_x == 0 && dragon_controller.velocity_z < 0) {
					default_right.SetActive (true);
					default_left.SetActive (false);
				}
				else if (dragon_controller.velocity_x > 0 && dragon_controller.velocity_z== 0) {
					default_right.SetActive (false);
					default_left.SetActive (true);
				}
			}
			if (type == 4) {
				if (dragon_controller.velocity_x == 0 && dragon_controller.velocity_z > 0) {
					default_right.SetActive (false);
					default_left.SetActive (true);
				}
				else if (dragon_controller.velocity_x > 0 && dragon_controller.velocity_z == 0) {
					default_right.SetActive (true);
					default_left.SetActive (false);
				}
			}
			//====================      T Type    ====================
			if(!R.turn_left && !R.turn_right) {
				if (type == 11) {
					if (dragon_controller.velocity_x == 0 && dragon_controller.velocity_z < 0) {
						default_right.SetActive (true);
						default_left.SetActive (false);
					}
				}
				if (type == 12) {
					print("RTRTRTRT======");
					if (dragon_controller.velocity_x == 0 && dragon_controller.velocity_z > 0) {
						default_right.SetActive (true);
						default_left.SetActive (false);
					}
				}
				if (type == 13) {
					if (dragon_controller.velocity_x > 0 && dragon_controller.velocity_z == 0) {
						default_right.SetActive (true);
						default_left.SetActive (false);
					}
				}
				if (type == 14) {
					print("RTRTRTRT!!!");
					if (dragon_controller.velocity_x < 0 && dragon_controller.velocity_z == 0) {
						default_right.SetActive (true);
						default_left.SetActive (false);
					}
				}	
			}
		}
	}

	void OnTriggerExit (Collider col) {
		default_right.SetActive (false);
		default_left.SetActive (false);
	}

}
