using UnityEngine;
using System.Collections;

public class moveForward : MonoBehaviour {
    float maxSpeed = 5f;
    //public Vector3 direction;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        pos += transform.rotation * Quaternion.Euler(0,0,-90) * velocity;
        transform.position = pos;
	}

    
}
