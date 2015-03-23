using UnityEngine;
using System.Collections;

public class BG : MonoBehaviour {
	public SpriteRenderer Bg;

	// Use this for initialization
	void Start () {
		Bg = this.GetComponent<SpriteRenderer> ();
		float xmas = Screen.width*Camera.main.orthographicSize*2.0f /(Screen.height*1.0f);//
		float yScale =Camera.main.orthographicSize*2.0f  / Bg.bounds.size.y; 
		float xScale = 0;
		if (Screen.height > Screen.width)
			xScale = xmas / Bg.bounds.size.x;
		else
			xScale = 3.0f; //for web view etc . you can change 1.5 according to you
		transform.localScale = new Vector3 (xScale,yScale,1);// I am using 2d so z doesn't needed.
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
