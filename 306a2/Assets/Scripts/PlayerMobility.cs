using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour {

    public float speed;
    //private Rigidbody2D rb2d;


    void FixedUpdate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //rb2d.angularVelocity = 0;
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        float input = Input.GetAxis("Vertical");
       GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed * input);
    }
	
}


