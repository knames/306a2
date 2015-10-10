using UnityEngine;
using System.Collections;

public class smoke : MonoBehaviour {

	// Use this for initialization
	void Start () {
        wait();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this);
    }
}
