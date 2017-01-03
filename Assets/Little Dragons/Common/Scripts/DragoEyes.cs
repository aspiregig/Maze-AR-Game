using UnityEngine;
using System.Collections;

public class DragoEyes : MonoBehaviour {

    public Animator DragoEyesAnims;
    public Transform EyesMesh;
    public Material[] EyesColors;
    int dragmaterial;
    int dragoEyes;
	// Use this for initialization
	void Start () {
        dragoEyes = 1;
        dragmaterial = 0;
	}
	
	// Update is called once per frame
	void Update () {
        DragoEyesAnims.SetInteger("DragoEyes", dragoEyes);
	}

    public void Eyes(int v)
    {
        dragoEyes = v;
    }

   public void ChangeColor(Transform eyes)
    {
        eyes.GetComponent<MeshRenderer>().material = EyesColors[dragmaterial];
        dragmaterial++;

        if (dragmaterial == EyesColors.Length)
        {
            dragmaterial = 0;
        }
    }
}

