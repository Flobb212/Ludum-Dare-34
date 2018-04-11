using UnityEngine;
using System.Collections;

public class LoadOnClick //: MonoBehaviour 
{

	public void LoadScene(int levelIndex) 
	{
		Application.LoadLevel (levelIndex);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
