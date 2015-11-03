using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float speed = 5; // the original speed of the spider

	public int hitpoints = 3; // how many hits the spider can take
	public float lowHpThreshHold = 1; // when it is considered having low hp to retreat

	public float threatZone = 9f; 	// the range the spiders are away of the spider. Chosen because it's ~ half the camera distance
	public float inRange = 5.5f; 	// the range the player is to attack it, should be less than the threat zone but not miniscule
	public float friendRange = 3f; 	// the range when friendlies are close enough to help attack. 
									//Chosen based on spider size, only other spiders in range will help chase
									// down the player
	public float monsterTooFarFromSpawn = 5f; 	// about a quarter of the map, allows spiders to "control a corner" of the map if the player
												// is out of range.

	delegate void MyDelegate();
	MyDelegate enemyAction;
	
	float distance;
	bool targetPlayer = false;
	bool runFromPlayer = false;
	GameObject player;
	UnityEngine.Color originalColor;

	float velocity;
	Transform facing;
	Vector3 spawn;

	Animator anim;

	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		enemyAction = TargetInZone;
		InvokeRepeating ("Wait_decision", 1, 1);
		velocity = speed;
		spawn = this.transform.position;
		originalColor = GetComponent<SpriteRenderer>().color;
		anim = GetComponent<Animator> ();
	}
	
	void Update()
	{
		if (velocity == 0)
			anim.Play ("Idle");

		//		enemyAction();
		distance = Vector3.Distance (this.gameObject.transform.position, player.transform.position);
		//Debug.Log ("distance: " + distance);

		
		GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * velocity);

		if (targetPlayer) {
			float z = Mathf.Atan2((player.transform.position.y - transform.position.y), 
			                      (player.transform.position.x - transform.position.x)) * 
				Mathf.Rad2Deg - 90;

			if (runFromPlayer)
				z = -z;
			transform.eulerAngles = new Vector3(0, 0, z);
			
		}
		
	}

	public void spiderHit(){
		hitpoints--;
	}

	public int getHP(){
		return hitpoints;
	}

	private bool isFriendClose(){
		GameObject[] enemy = GameObject.FindGameObjectsWithTag ("enemy");
		foreach (GameObject e in enemy){
			if (e != this.gameObject){
				if (Vector3.Distance (e.gameObject.transform.position, this.transform.position) < friendRange) {
					return true;
				}
			}
		}
		return false;
	}


	/*  Decision Functions  -----------------------------------------------*/
	
	// is a target within line of sight of enemy?
	private void TargetInZone()
	{
		if(distance < threatZone)
		{
			Debug.Log("Threat Zone True");
			enemyAction = InRange;
		}
		else
		{
			Debug.Log("Threat Zone False");
			enemyAction = tooFarFromSpawn;
		}
		
	}
	private void InRange(){
		if (distance < inRange) {
			Debug.Log("inRange True");
			enemyAction = friendsNear;
		} 
		else {
			Debug.Log("inRange False");
			enemyAction = hpLow;
		}
	}

	private void friendsNear(){
		if (isFriendClose()) {
			Debug.Log("Friends Near True");
			enemyAction = attack;
		} else {
			Debug.Log("Friends Near False");
			enemyAction = hpLow;
		}
	}

	private void tooFarFromSpawn(){
		//Debug.Log (Vector3.Distance (spawn, this.gameObject.transform.position));
		if (Vector3.Distance (spawn, this.gameObject.transform.position) > monsterTooFarFromSpawn) {
			Debug.Log("Too Far From Spawn True");
			targetPlayer = false;
			float z = Mathf.Atan2((spawn.y - transform.position.y), 
			                      (spawn.x - transform.position.x)) * 
				Mathf.Rad2Deg - 90;
			
			transform.eulerAngles = new Vector3(0, 0, z);
			velocity = speed;
			enemyAction = TargetInZone;
		}
		else {
			Debug.Log("Too Far From Spawn False");
			enemyAction = random;
		}
	}

	private void hpLow(){
		if (hitpoints > lowHpThreshHold) {
			Debug.Log("HP Low False");
			enemyAction = attack; // again because attack/advance are samesies.
		} 
		else 
		{
			Debug.Log("HP Low True");
			enemyAction = retreat;
		}
	}

	private void random(){
		if (randomBool()) {
			Debug.Log("Random True");
			enemyAction = randomWalk;
		} 
		else {
			Debug.Log("Random False");
			enemyAction = idleAnimation;
		}

	}
	

	/*  Action Functions -----------------------------------------------*/
	
	private void attack(){
		this.gameObject.GetComponent<SpriteRenderer>().color = originalColor;
		// code to target player
		Debug.Log("Attacking");
		runFromPlayer = false;
		targetPlayer = true;
		velocity = speed;
		enemyAction = TargetInZone;
	}
	
	private void retreat(){
		this.gameObject.GetComponent<SpriteRenderer> ().color = Color.yellow;
		Debug.Log("Retreating");
		targetPlayer = true;
		runFromPlayer = true;
		float z = Mathf.Atan2((player.transform.position.y - transform.position.y), 
		                      (player.transform.position.x - transform.position.x)) * 
			Mathf.Rad2Deg - 90;
		
		transform.eulerAngles = new Vector3(0, 0, -z);
		velocity = speed;	
		enemyAction = TargetInZone;
	}

	private void advance(){
		Debug.Log("Advance");
		enemyAction = attack; // they're the same thing for this assignment
	}
	
	private void randomWalk(){
		Debug.Log("Random Walk");
		this.gameObject.GetComponent<SpriteRenderer>().color = originalColor;
		targetPlayer = false;
		velocity = speed;
		transform.eulerAngles = new Vector3 (0, 0, Random.Range (0, 360));
		enemyAction = TargetInZone;
	}

	public void idleAnimation(){
		// Color the spider during idle. 
		GetComponent<SpriteRenderer>().color = Color.green;
		Debug.Log("Idling");
		velocity = 0;
		targetPlayer = false;
		enemyAction = TargetInZone;
	}

	
	/* extra methods -----------------------------------------------*/
	
	// randomly generates true or false
	private bool randomBool()
	{
		if(Random.value >= 0.5f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	// takes longer to make decisions for demonstration purposes
	private void Wait_decision()
	{
		enemyAction();
	}

}
