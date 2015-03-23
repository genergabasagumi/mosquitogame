using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	public GameObject InfoManager;
	private float RotateSpeed;
	private float RandomWaitTime;
	public float Speed;
	public bool Rot;
	public GameObject Spawn;
	public bool AttackPls;
	public bool StopMoving;
	private float ScaleValue;
	public Sprite DeathSprite;
	// Use this for initialization
	void Start () {
		InfoManager = GameObject.Find("Info");
		StartCoroutine (RandomSpeed ());
		Spawn = GameObject.Find("SpawnPoint");
		Speed += Spawn.GetComponent<SpawnPoint> ().tempSpeed;
		StartCoroutine (AttackMode ());
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 CameraPos = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		Vector3 myPos = transform.position;
		if (transform.position.x > CameraPos.x || transform.position.x < -CameraPos.x )
			myPos.x *= -1;
		if (transform.position.y > CameraPos.y || transform.position.y < -CameraPos.y )
			myPos.y *= -1;
		transform.position = myPos;
		if(!StopMoving)
		{
			transform.Translate (Vector2.up * (Speed) * Time.deltaTime);
			if(Rot)
				transform.Rotate(Vector3.forward * Time.deltaTime * RotateSpeed, Space.Self);
			else
				transform.Rotate(Vector3.back * Time.deltaTime * RotateSpeed, Space.Self);
		}
		if (AttackPls)
		{
			StopMoving = true;
			ScaleValue += 0.5f * Time.deltaTime;
			transform.localScale += new Vector3(ScaleValue,ScaleValue, 0);
			this.GetComponent<BoxCollider2D>().enabled = false;
			this.GetComponent<SpriteRenderer>().sortingOrder = 10;

		}
		if (ScaleValue > 0.3f) {
			InfoManager.GetComponent<Info>().Life--;
			DestroyObject (this.gameObject);
		}

	}

	public void HitMe()
	{
		InfoManager.GetComponent<Info>().Score++;
		StopMoving = true;
		this.GetComponent<BoxCollider2D>().enabled = false;
		this.GetComponent<SpriteRenderer> ().sprite = DeathSprite;
		StopAllCoroutines();
		DestroyObject (this.gameObject,1.0f);
	}
	IEnumerator RandomSpeed()
	{
		while (true) {

			yield return new WaitForSeconds (RandomWaitTime);
			RandomWaitTime = Random.Range(0.1f,2.0f);
			Rot = !Rot;
			RotateSpeed = Random.Range (1, 360);
		}
	}
	IEnumerator AttackMode()
	{
		yield return new WaitForSeconds (5);
		AttackPls = !AttackPls;
	}

}
