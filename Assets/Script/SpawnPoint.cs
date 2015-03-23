using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public float SpawnCount;
	public float SpawnStartWait;
	public float WaveWait;
	public float SpawnWait;
	public float WaveCount;
	public GameObject Mosquito;
	private Vector3 CameraPos;
	public float tempSpeed;
	private Vector2 SpawnPos;

	public GameObject[] Ups;

	public float MinUPs;
	public float MaxUPs;

	private GameObject currentUps;

	// Use this for initialization
	void Start () {
		MinUPs = 10.0f;
		MaxUPs = 20.0f;
		StartCoroutine (SpawnWave ());
		StartCoroutine (PowerUps ());
		CameraPos = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));

	}
	
	// Update is called once per frame
	void Update () {
	}
	IEnumerator SpawnWave ()
	{
		while (true) {

			yield return new WaitForSeconds(SpawnStartWait);
			for (int i = 0; i <  SpawnCount ; i++)
			{

				SpawnPos.x = Random.Range(-CameraPos.x,CameraPos.x);
				SpawnPos.y = Random.Range(-CameraPos.y,CameraPos.y);
				Instantiate(Mosquito,SpawnPos,Quaternion.identity);
				yield return new WaitForSeconds(SpawnWait);
			}
			WaveCount++;
			yield return new WaitForSeconds(WaveWait);
			if(WaveCount % 3 == 0)
			{
				SpawnCount++;
				if(tempSpeed < 10 )
					tempSpeed++;
				if(SpawnWait > 0.1f)
					SpawnWait -=0.2f;
			}

		}
	}

	IEnumerator PowerUps()
	{
		while(true)
		{
			float TimeRandom = Random.Range (MinUPs, MaxUPs);
			yield return new WaitForSeconds (TimeRandom);
			float randomUps = Random.Range (0, Ups.Length);
			for (int q = 0; q < Ups.Length; q++) {
				if(randomUps == q)
				{
					currentUps = Ups[q];

				}
			}
			SpawnPos.x = Random.Range(-CameraPos.x,CameraPos.x);
			SpawnPos.y = Random.Range(-CameraPos.y,CameraPos.y);
			GameObject temp = Instantiate (currentUps, SpawnPos, Quaternion.identity) as GameObject ;
			DestroyObject (temp, 5.0f);
		}
	}
}
