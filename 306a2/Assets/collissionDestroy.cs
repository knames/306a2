using UnityEngine;
using System.Collections;

public class collissionDestroy : MonoBehaviour {

    public GameObject explode;

	public AudioClip explosionSound;
	public AudioClip deadSpiderSound;


	private GameController gameController;


	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)
			Debug.Log("Cannot find 'GameController' script");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter2D(Collision2D col)
    {
		AudioSource audio = GetComponent<AudioSource>();
		if (col.gameObject.tag == "enemy" || col.gameObject.tag == "wall")
        {
            if (col.gameObject.tag == "enemy")
            {
                Destroy(col.gameObject);
				audio.PlayOneShot(deadSpiderSound);
				scoreManager.score+=1;
            }
			
			audio.PlayOneShot(explosionSound);

			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Destroy(GetComponent<BoxCollider2D>());
			Destroy(gameObject, 5f); // destroys rocket
            GameObject explosion = (GameObject)Instantiate(explode, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));
            Destroy(explosion, 0.9f); // get rid of smoke animation

			
		}

    }
}
