﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
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


	
	public bool LotionUp;
	public float TimeLotion;

	public GameObject Hand;
	// Use this for initialization

	void Awake()
	{
		highScore = PlayerPrefs.GetInt("High Score");
		highScoreText.text = highScore.ToString();
		Advertisement.Initialize ("26936", true);
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
			//highScore = PlayerPrefs.GetInt("High Score");

			if(highScore < Score)
			{
				PlayerPrefs.SetInt("High Score", Score);
				highScore = PlayerPrefs.GetInt("High Score");
			}
			highScoreText.text = highScore.ToString();

			if(!EndWindow.activeSelf)
			{
				DestroyObject(SpawnPoint);
				DestroyAll();
				StartCoroutine("Ads");
				EndWindow.SetActive(true);
			}
			if(InGame.activeSelf)
				InGame.SetActive(false);


		}
		/*
		for(int i = Heart.Length-1; i >= 0;i--)
		{
			if(Life == i)
			{
				if(Heart[i].activeSelf)
				{
					Heart[i].SetActive(false);
				}
			}
		}*/
		for (int i = Heart.Length - 1; i >= 0; i--) 
		{
			if(Life == i)
			{
				if(!Heart[i].activeSelf)
				{
					Heart[i].SetActive(true);
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
	public void Lotion()
	{
		LotionUp = true;
		StartCoroutine (LotionCount ());
	}
	IEnumerator SlowDown()
	{
		timeS = 0.25f;
		yield return new WaitForSeconds (2.0f);
		timeS = 1;
	}
	IEnumerator LotionCount()
	{
		yield return new WaitForSeconds (TimeLotion);
		LotionUp = false;
		GameObject[] Mos = GameObject.FindGameObjectsWithTag("Enemy");
		for (int q = 0; q < Mos.Length; q++) {
			if(Mos[q].GetComponent<AI>().NoyetAttack)
			{
				Mos[q].GetComponent<AI>().AttackPls = true;
			}
			
		}
	}
	IEnumerator Ads()
	{
		yield return new WaitForSeconds (0.5f);
		if(Advertisement.isReady()){
			Advertisement.Show();
		}
	}
}
