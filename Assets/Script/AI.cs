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
	public float ScaleValue;
	public Sprite DeathSprite;
	public GameObject Player;
	public bool Imdeath;
	public bool NoyetAttack;

	// Use this for initialization
	void Start () {
		InfoManager = GameObject.Find("Info");
		StartCoroutine (RandomSpeed ());
		Spawn = GameObject.Find("SpawnPoint");
		Speed += Spawn.GetComponent<SpawnPoint> ().tempSpeed;
		Player = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine (AttackMode ());
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 CameraPos = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		Vector3 myPos = transform.position;
		if (transform.position.x > CameraPos.x + 0.5f || transform.position.x < -CameraPos.x - 0.5f ) {
			myPos.x *= -1;
		}
		if (transform.position.y > CameraPos.y + 0.5f|| transform.position.y < -CameraPos.y - 0.5f	) {
			myPos.y *= -1;
		}
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

			Vector3 moveDirection = gameObject.transform.position - Player.transform.position; 
			if (moveDirection != Vector3.zero) 
			{
				float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
				float Degree = transform.rotation.z + 90;
				transform.Rotate(0,0,Degree);
			}
			if(Vector3.Distance(this.transform.position,Player.transform.position) < 0.5f)
			{
				InfoManager.GetComponent<Info>().Life--;
				DestroyObject (this.gameObject);
			}
			if (transform.localScale.magnitude  > 1.05f)
			{

				transform.localScale -= new Vector3(1.0f,1.0f, 0) * ScaleValue * Time.deltaTime;
			}

			//this.GetComponent<BoxCollider2D>().enabled = false;
			//this.GetComponent<SpriteRenderer>().sortingOrder = 10;
			if(!Imdeath)
				this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,Player.gameObject.transform.position, 5 * Time.deltaTime);
		}


	}

	public void HitMe()
	{
		InfoManager.GetComponent<Info>().Score++;
		StopMoving = true;
		Imdeath = true;
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
		if (!InfoManager.GetComponent<Info> ().LotionUp) {
			AttackPls = !AttackPls;

		} else
			NoyetAttack = true;
	}

}
