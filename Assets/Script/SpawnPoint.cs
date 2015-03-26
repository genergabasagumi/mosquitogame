using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public float SpawnCount;
	public int AmountRush;
	
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
	public bool FinishRush;

	public GameObject Warning;
	public GameObject[] Mos;
	public GameObject Tap;
	// Use this for initialization
	void Start () {
		MinUPs = 10.0f;
		MaxUPs = 20.0f;

		StartCoroutine ("SpawnWave");
		StartCoroutine ("PowerUps");
		CameraPos = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));


	}
	
	// Update is called once per frame
	void Update () {
		Mos = GameObject.FindGameObjectsWithTag("Enemy");
		if (FinishRush) {

			if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && Mos.Length == 0)
			{
				StartCoroutine ("PowerUps");
				StartCoroutine("SpawnWave");
				if(Tap.activeSelf)
				{
					Tap.SetActive(false);
				}
				FinishRush = false;
			}
		}
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
			if(WaveCount % 5 == 0)
			{
				if(SpawnCount < 15)
				SpawnCount++;
				if(tempSpeed < 10 )
					tempSpeed++;
				if(SpawnWait > 0.1f)
					SpawnWait -=0.2f;
				if(!Tap.activeSelf)
				{
					Tap.SetActive(true);
				}
				StartCoroutine("MosquitoRush");
				StopCoroutine ("PowerUps");
				StopCoroutine("SpawnWave");	

			}

		}
	}
	IEnumerator MosquitoRush()
	{
		Warning.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		Warning.SetActive (false);

		for(int w = 0; w < AmountRush;w++)
		{
			SpawnPos.x = Random.Range(-CameraPos.x,CameraPos.x);
			SpawnPos.y = Random.Range(-CameraPos.y,CameraPos.y);
			Instantiate(Mosquito,SpawnPos,Quaternion.identity);
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(5.0f);
		FinishRush = true;
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
