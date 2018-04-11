using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    LoadOnClick loader;

    public void LoadTheScene(int levelIndex)
    {
        loader = new LoadOnClick();

        loader.LoadScene(levelIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
