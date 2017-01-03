using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class onclickcamera : MonoBehaviour {

	bool b_camera_back; // default: back
	bool b_mode_release; //default release

	public UIButton btnRight;
	public UIButton btnLeft;

	// Use this for initialization
	void Start () {
		b_camera_back = false;
		b_mode_release = false;

		check_camera_status ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!b_mode_release) {
			btnTxt.text = "Debug";
			mcamera.SetActive (true);
			GetComponent<Vuforia.VideoBackgroundManager> ().SetVideoBackgroundEnabled(false);
		} else {
			btnTxt.text = "Release";
			mcamera.SetActive (false);
			GetComponent<Vuforia.VideoBackgroundManager> ().SetVideoBackgroundEnabled(true);
		}

//		if (btnRight.state == UIButton.State.Pressed)
//			R.turn_right = true;
//		else
//			R.turn_right = false;
//		
//		if (btnLeft.state == UIButton.State.Pressed)
//			R.turn_left = true;
//		else
//			R.turn_left = false;
		
		
	}
	//=========================================  UI button
	public void onclickCamera() {		
		check_camera_status ();
	}

	public void onclickMode() {
		check_release_status ();
	}

	//=========================================== UI button
	void check_release_status() {		
		b_mode_release = !b_mode_release;
	}

	void check_camera_status() {
		b_camera_back = !b_camera_back;
		Vuforia.CameraDevice.CameraDirection currentDir = Vuforia.CameraDevice.Instance.GetCameraDirection();
		if (b_camera_back) {
			RestartCamera(Vuforia.CameraDevice.CameraDirection.CAMERA_FRONT);
			camBtnTxt.text = "Back Camera";
		} else {
			RestartCamera(Vuforia.CameraDevice.CameraDirection.CAMERA_BACK);
			camBtnTxt.text = "Front Camera";
		}
	}

	private void RestartCamera(Vuforia.CameraDevice.CameraDirection newDir)
	{
		Vuforia.CameraDevice.Instance.Stop();
		Vuforia.CameraDevice.Instance.Deinit();
		Vuforia.CameraDevice.Instance.Init(newDir);
		Vuforia.CameraDevice.Instance.Start();
	}

	public UILabel btnTxt;
	public UILabel camBtnTxt;
	public GameObject mcamera;
}
