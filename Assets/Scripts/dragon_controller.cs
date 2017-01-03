using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class dragon_controller : MonoBehaviour {

	public static float velocity_z;
	public static float velocity_x;
	Vector3 right_angle;
	Vector3 left_angle;
	bool is_start;
	bool ar_start;
	int count_down;

	bool b_game_over;
	bool b_game_success;
	bool b_game_success_3;
	int n_game_over;
	int n_game_success;
	Vector3 rotation_axis;
	float rotation_angle;
	float delta_angle;
	bool turn_active;
	int turn_active_count;

	public GameObject plane;
	public UILabel count;
	public GameObject dragon_camera;
	public GameObject particle;

	public GameObject moving_camera;
	public GameObject success_cam1;
	public GameObject success_dragon1;
	public GameObject success_cam2;
	public GameObject success_dragon2;
	public GameObject success_cam3;
	public GameObject success_dragon3;

	// Use this for initialization
	void Start () {
		init ();
	}

	void init () {
		velocity_x = 0;
		velocity_z = 0.85f;
		right_angle = new Vector3 (0, 90, 0);
		left_angle = new Vector3 (0, -90, 0);
		is_start = false;
		ar_start = false;
		count_down = 250;
		b_game_over = false;
		b_game_success = false;
		n_game_over = 400;
		n_game_success = 400;
		rotation_axis = new Vector3 (0, 1, 0);
		rotation_angle = 0;
		delta_angle = 1;
		turn_active_count = 10;
		turn_active = false;
	}
	
	// Update is called once per frame
	void Update () {	
		action_gameOver ();
		action_gameSuccess ();
		switch_Cam ();
		Control_finalCamDragon();
		Watch_turnActive ();

		if (is_start && ar_start) {
			dragon_movement ();
			Control_movingCam ();
		}			
		else if (!is_start && ar_start)
			count_downTime ();
		if (!ar_start) {
			if (plane.GetComponent<MeshCollider> ().enabled)
				ar_start = true;
		}
		else if (ar_start) {
			if (!plane.GetComponent<MeshCollider> ().enabled)
				ar_start = false;	
		}
		print (R.success_1);
	}

	void Control_movingCam () {
		moving_camera.transform.position = new Vector3 (moving_camera.transform.position.x + velocity_x, moving_camera.transform.position.y, moving_camera.transform.position.z + velocity_z);
	}

	void switch_Cam(){
		moving_camera.SetActive (is_start && ar_start);
	}

	void Control_finalCamDragon() {
		success_cam1.SetActive (R.success_1);
		success_dragon1.SetActive (R.success_1);
		success_cam2.SetActive (R.success_2);
		success_dragon2.SetActive (R.success_2);
		success_cam3.SetActive (R.success_3);
		success_dragon3.SetActive (R.success_3);
	}

	void dragon_movement () {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x + velocity_x, gameObject.transform.position.y, gameObject.transform.position.z + velocity_z);
	}


	void count_downTime () {
		count_down--;
		if (count_down == 0) {
			count.text = "";
			gameObject.GetComponent<Animator> ().SetTrigger ("tri_start");
			is_start = true;
			R.game_start = true;
		} else if (count_down <= 5) {
			count.text = "";
			
		} else if (count_down < 30 && count_down > 5) {
			count.text = "start";

		} else if (count_down < 80) {
			count.text = "1";

		} else if (count_down < 130) {
			count.text = "2";

		} else if (count_down < 180) {
			count.text = "3";
			
		} else {
			count.text = "";
		}
	}

	void turn_right () {
		if (!turn_active) {
			turn_active = true;
			if (velocity_x == 0) {
				velocity_x = velocity_z;
				velocity_z = 0;
			}
			else if (velocity_z == 0) {
				velocity_z = -velocity_x;
				velocity_x = 0;
			}
			gameObject.transform.Rotate (right_angle);
		}
	}

	void turn_left () {
		if (!turn_active) {
			turn_active = true;
			if (velocity_x == 0) {
				velocity_x = -velocity_z;
				velocity_z = 0;
			} else if (velocity_z == 0) {
				velocity_z = velocity_x;
				velocity_x = 0;
			}
			gameObject.transform.Rotate (left_angle);
		}	
	}

	void game_success () {
		R.success_1 = true;
		velocity_x = 0;
		velocity_z = 0;																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																															
		gameObject.GetComponent<Animator> ().SetTrigger ("tri_success");
		b_game_success = true;

		n_game_success = 250;
	}
	void game_success2 () {
		R.success_2 = true;
		velocity_x = 0;
		velocity_z = 0;																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																															
		gameObject.GetComponent<Animator> ().SetTrigger ("tri_success_2");
		b_game_success = true;

		n_game_success = 250;
	}
	void game_success3 () {
		R.success_3 = true;
		velocity_x = 0;
		velocity_z = 0;																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																															
		gameObject.GetComponent<Animator> ().SetTrigger ("tri_success_3");
		b_game_success = true;

		n_game_success = 90;
		b_game_success_3 = true;

	}
	void game_success4 () {
		velocity_x = 0;
		velocity_z = 0;																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																															
		gameObject.GetComponent<Animator> ().SetTrigger ("tri_success_4");
		b_game_success = true;
		n_game_success = 250;

	}


	void game_fail () {
		velocity_x = 0;
		velocity_z = 0;
		gameObject.GetComponent<Animator> ().SetTrigger ("tri_fail");
		b_game_over = true;
		n_game_over = 70;
	}

	void action_gameSuccess () {
		if (b_game_success) {
			n_game_success--;
			if (n_game_success == 0) {
				b_game_success = false;
				R.success_1 = false;
				R.success_2 = false;
				R.success_3 = false;

				SceneManager.LoadScene ("first");
			}
			//dragon_camera.transform.RotateAround (gameObject.transform.position, rotation_axis, delta_angle);


			gameObject.transform.RotateAround (gameObject.transform.position, rotation_axis, delta_angle);
		}
		dragon_camera.SetActive (false);
		particle.SetActive (b_game_success);

		if (b_game_success_3) {
			gameObject.GetComponent<Animator> ().SetTrigger ("tri_success_3");
		}
	}

	void action_gameOver () {
		if (b_game_over) {
			n_game_over--;
			if (n_game_over == 0) {
				b_game_over = false;
				SceneManager.LoadScene ("first");
			}
		}
	}

	void Watch_turnActive(){
		if (turn_active) {
			turn_active_count--;
			if (turn_active_count == 0) {
				turn_active_count = 10;
				turn_active = false;
			}
		}
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "tag_cross") {
			if (R.type == 33) {
				if (R.turn_right)
					turn_right ();
				else if (R.turn_left)
					turn_left ();
			}
			if (R.type == 11) {
				if (R.turn_right && velocity_x < 0) {
					turn_right ();
				}
				else if (R.turn_left && velocity_x > 0) {
					turn_left ();
				}
				else if (velocity_z < 0) {
					if (R.turn_right)
						turn_right ();
					else if (R.turn_left)
						turn_left ();
				}
			}
			if (R.type == 12) {
				if (R.turn_left && velocity_x < 0) {
					turn_left ();
				}
				else if (R.turn_right && velocity_x > 0) {
					turn_right ();
				}
				else if (velocity_z > 0) {
					if (R.turn_right)
						turn_right ();
					else if (R.turn_left)
						turn_left ();
				}
			}
			if (R.type == 13) {
				if (R.turn_left && velocity_z > 0) {
					turn_left ();
				}
				else if (R.turn_right && velocity_z < 0) {
					turn_right ();
				}
				else if (velocity_x > 0) {
					if (R.turn_right)
						turn_right ();
					else if (R.turn_left)
						turn_left ();
				}
			}
			if (R.type == 14) {
				if (R.turn_left && velocity_z < 0) {
					turn_left ();
				}
				else if (R.turn_right && velocity_z > 0) {
					turn_right ();
				}
				else if (velocity_x < 0) {
					if (R.turn_right)
						turn_right ();
					else if (R.turn_left)
						turn_left ();
				}
			}
		}
		else {
			if (col.tag == "default-right") {
				turn_right ();
			}
			if (col.tag == "default-left") {
				turn_left ();
			}
			if (col.tag == "tag_success") {
				game_success ();
			}
			if (col.tag == "tag_success_2") {
				game_success2 ();
			}
			if (col.tag == "tag_success_3") {
				game_success3 ();
			}

			if (col.tag == "tag_success_4") {
				game_success4 ();
			}
			if (col.tag == "tag_fail") {
				game_fail ();
			}	
		}
	}

	void OnTriggerStay (Collider col) {
		
	}

}
