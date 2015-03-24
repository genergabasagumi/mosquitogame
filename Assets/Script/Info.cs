using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Info : MonoBehaviour {
	public int Score;
	public int Life;

	public GameObject[] Heart;

	//public Text LifeText;
	public Text ScoreText;
	public Text ScoreEnd;
	public GameObject EndWindow;
	public GameObject InGame;
	public GameObject SpawnPoint;
	public float timeS;
	public int highScore;
	public Text highScoreText;
	// Use this for initialization

	void Awake()
	{
		highScore = PlayerPrefs.GetInt("High Score");
		highScoreText.text = highScore.ToString();

	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = timeS;
		//LifeText.text = "Life : " + Life.ToString ();
		ScoreText.text = "Score : " + Score.ToString ();
		ScoreEnd.text = "Score : " + Score.ToString ();
		if(Life < 0)
		{
			highScore = PlayerPrefs.GetInt("High Score");

			if(highScore < Score)
			{
				PlayerPrefs.SetInt("High Score", Score);
				highScore = PlayerPrefs.GetInt("High Score");
			}
			highScoreText.text = highScore.ToString();

			if(!EndWindow.activeSelf)
			{
				EndWindow.SetActive(true);
			}
			if(InGame.activeSelf)
				InGame.SetActive(false);
			DestroyObject(SpawnPoint);
			 DestroyAll();

		}

		for(int i = Heart.Length-1; i >= 0;i--)
		{
			if(Life == i)
			{
				if(Heart[i].activeSelf)
				{
					Heart[i].SetActive(false);
				}
			}
		}
	}
	public void TimeSlow()
	{
		StartCoroutine (SlowDown ());
	}
	public void DestroyAll()
	{
		GameObject[] Mos = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < Mos.Length; i++) {
			if(Life < 0)
				DestroyObject(Mos[i]);
			else
				Mos[i].GetComponent<AI>().HitMe();

		}
	}
	IEnumerator SlowDown()
	{
		timeS = 0.25f;
		yield return new WaitForSeconds (2.0f);
		timeS = 1;
	}

}
