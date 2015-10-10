using UnityEngine;
using System.Collections;

public class collissionDestroy : MonoBehaviour {

    public GameObject explode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
        Destroy(gameObject);
        GameObject explosion = (GameObject)Instantiate(explode, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));
        Destroy(explosion, 0.9f); // get rid of smoke animation

    }
}
