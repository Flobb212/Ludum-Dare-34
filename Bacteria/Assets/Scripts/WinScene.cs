using UnityEngine;

using System.Collections;
using UnityEngine.UI;

public class WinScene : MonoBehaviour {

    public GameObject winLoseTextObj;
    public GameObject scoreTextObj;

	// Use this for initialization
	void Start () {
        Text theText = winLoseTextObj.GetComponent<Text>();
            
        if (ScoreManager.won == true)
        {
            theText.text = "YOU WON!";
        }
        
        if (ScoreManager.gameOver && !ScoreManager.won)
        {
            theText.text = "Unlucky, " + System.Environment.NewLine + " Chucky";
        }

        scoreTextObj.GetComponent<Text>().text = ScoreManager.playerScore.ToString();

        ScoreManager.gameOver = false;
        ScoreManager.won = false;
        ScoreManager.playerScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
