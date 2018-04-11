using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public int scoreToWin = 400;
    
    public GameObject worldObject;
	public GameObject worldBarrier1;
	public GameObject worldBarrier2;
    public static float worldSizeX; 
    public static float worldSizeY; 
	public Player player;

    void Awake()
    {
        worldSizeX = worldObject.transform.localScale.x * 10;
        worldSizeY = worldObject.transform.localScale.z * 10;

		worldBarrier1.SetActive (true);
	}
	
	void Update () 
	{
		if (player.GetCurrentMass() > 100f) {
			worldBarrier1.SetActive(false);
		}
        if (player.enabled)
        {
            CheckIfWon();
        }
	}

    public static Vector2 GetRandomPosition()
    {
        float randX = Random.Range(0f, worldSizeX);
        float randY = Random.Range(0f, worldSizeY);

        return new Vector2(randX, randY);
    }

	void CheckIfWon()
	{
		if(ScoreManager.playerScore >= scoreToWin)
		{
            ScoreManager.won = true;
            LoadOnClick load = new LoadOnClick();

            load.LoadScene(4);
		}
	}

    public static void SetFailState()
    {
        LoadOnClick loader = new LoadOnClick();
        ScoreManager.won = false;
        ScoreManager.gameOver = true;
        loader.LoadScene(4);
    }
}
