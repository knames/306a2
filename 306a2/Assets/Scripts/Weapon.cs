using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public float Damage = 10;
    //public LayerMask notToHit;

    float timeToFire = 0;
    public Transform firePoint;


    // Use this for initialization
    void Awake () {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null) {
            Debug.LogError("No firepoint found.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        Shoot(); // for testing purposes
	    if (fireRate == 0) // check if single fire wep
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else // not single burst
        {
            if (Input.GetButton ("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}
    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, 
                                            Camera.main.ScreenToWorldPoint(Input.mousePosition ).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.magenta);
        if (hit.collider != null) {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
        }
    }
}
