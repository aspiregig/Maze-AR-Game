using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float Force = 10;
    public float Radius = 10;
    public GameObject explotion;


    void OnTriggerEnter(Collider other)
    {
        Rigidbody impact = other.GetComponent<Rigidbody>();

        if (impact)
        {
            impact.AddExplosionForce(100f * Force, transform.position, 100f * Radius);
        }

        Destroy(gameObject);
        //create fireball explotion after collides
        GameObject fireballexplotion = Instantiate(explotion);
        fireballexplotion.transform.position = transform.position;

        Destroy(fireballexplotion, 2f);
    }
    
}
