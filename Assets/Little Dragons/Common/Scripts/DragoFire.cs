using UnityEngine;
using System.Collections;

public class DragoFire : MonoBehaviour {
    private Animator anim;
    [Header("Dragon Fire")]
    public float FireBallSpeed = 500;
    public Transform FirePoint;
    public GameObject FireBall;
    public GameObject FireBreath;
    GameObject firebreathinstance;
    ParticleSystem.EmissionModule emision;

    // Use this for initialization

    void Start () {
        anim = GetComponent<Animator>();
        //Set the Fire Breath
        GameObject firebreathinstance = Instantiate(FireBreath);
        firebreathinstance.transform.parent = FirePoint;
        firebreathinstance.transform.position = FirePoint.position;
        emision = firebreathinstance.GetComponent<ParticleSystem>().emission;
        emision.rate = new ParticleSystem.MinMaxCurve(0);
    }
	
    public void FireAttack(int type)
    {

        if (FireBall && type == 1)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack Fire") || anim.GetNextAnimatorStateInfo(0).IsTag("Attack Fire") ||
                anim.GetCurrentAnimatorStateInfo(1).IsTag("Attack Fire") || anim.GetNextAnimatorStateInfo(1).IsTag("Attack Fire"))
            {
                GameObject fireball = Instantiate(FireBall);
                fireball.transform.position = FirePoint.transform.position;
                Vector3 dir = FirePoint.forward;
                if (anim.GetFloat("UpDown") <= 0.1 && anim.GetFloat("UpDown") >= -0.1  )
                {
                    dir = new Vector3(FirePoint.forward.x,transform.forward.y,FirePoint.forward.z);
                }
                fireball.GetComponent<Rigidbody>().AddForce(dir * FireBallSpeed);
            }

        }

        if (FireBreath && type == 2)
        {
            emision.rate = new ParticleSystem.MinMaxCurve(500f);
          
        }

        if (FireBreath && type == 3)
        {
            emision.rate = new ParticleSystem.MinMaxCurve(0);
        }


    }
}
