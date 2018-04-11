using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{

	public static int playerScore;
	public static int scoreMultiplyer;
    public static bool won;
    public static bool gameOver = false;

	public static void ResetMultiplier () 
	{
		scoreMultiplyer = 1;
	}
}
