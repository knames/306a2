using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public Transform[] spawnPoints;
	public float spawnTime = 5f;
	public float waitTime = 5f;
	public float spawnRateIncrease = 0.1f;
	public GameObject enemy;


	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", spawnTime, spawnTime);
		StartCoroutine(increaseSpawnRate());
	}

	IEnumerator increaseSpawnRate() // increases the rate of spawn every 5 seconds
	{
		yield return new WaitForSeconds(waitTime);
		if (spawnTime > 1.5f)
		{
			spawnTime -= spawnRateIncrease;
			StartCoroutine(increaseSpawnRate());
		}

	}


	void Spawn()
	{
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}



	// Update is called once per frame
	void Update () {
	
	}

}
