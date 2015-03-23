using UnityEngine;
using System.Collections;

public class TouchEvent : MonoBehaviour {
	public GameObject Infomation;
	// Use this for initialization
	void Start () {
		Infomation = GameObject.Find("Info");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount < 2 && Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
			Vector3 TouchPosition = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			RaycastHit2D hit = Physics2D.Raycast (TouchPosition, Vector2.zero);
			if (hit.collider.gameObject.tag == "Enemy") {
				hit.collider.gameObject.GetComponent<AI>().HitMe();
			}
			if (hit.collider.gameObject.tag == "TimeSlow") {
				Infomation.GetComponent<Info>().TimeSlow();
				DestroyObject(hit.collider.gameObject);
			}
			if (hit.collider.gameObject.tag == "DestroyAll") {
				Infomation.GetComponent<Info>().DestroyAll();
				DestroyObject(hit.collider.gameObject);
			}
		}
	}
}
