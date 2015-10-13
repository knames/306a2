using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour {

    public float speed;
	public AudioClip playerDead;
    //private Rigidbody2D rb2d;


    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //rb2d.angularVelocity = 0;
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        float input = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical"))); // change idle/movement animation

        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed * input);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy" )
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>(); // remove player from view
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
            

            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(playerDead);

			StartCoroutine(pause());
			GetComponent<BoxCollider2D>().enabled = false; // so it doesnt spam screams if hit multiple times

		}

    }

	IEnumerator pause()
	{
		yield return new WaitForSeconds(1);
		Application.LoadLevel(Application.loadedLevel);
	}
}


