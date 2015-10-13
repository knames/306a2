
using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{

    public Vector3 bulletOffset = new Vector3(2.45f, 3.5f, 0);

    public GameObject bulletPrefab;
    public GameObject fireAnimation;
    int bulletLayer;

    public AudioClip shotFired;

    public float fireDelay = 0.25f;
    float cooldownTimer = 0;

    void Start()
    {
        bulletLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {

            // SHOOT!
            cooldownTimer = fireDelay;

            Vector3 offset = transform.rotation * bulletOffset;
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, transform.rotation * Quaternion.Euler(0,0,90));
            bulletGO.layer = bulletLayer;
            GameObject smoke = (GameObject)Instantiate(fireAnimation, transform.position + offset, transform.rotation * Quaternion.Euler(0, 0, 90));


            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(shotFired);

            Destroy(smoke, 0.9f); // get rid of smoke animation
            Destroy(bulletGO, 10); // destroy bullet after it falls off the screen
            cooldownTimer = 1;
        }
    }
}
