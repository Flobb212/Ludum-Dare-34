using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour 
{
	//Declare Variables
	public Player player;
	public Text massText;
	public Text scoreText;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetMass();
		GetScore();
	}

	void GetMass()
	{
		massText.text = player.GetCurrentMass().ToString("0.00");
	}

	void GetScore()
	{
		scoreText.text = ScoreManager.playerScore.ToString("0.00");
	}
}
