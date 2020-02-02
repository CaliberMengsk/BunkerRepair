using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void StartGame(int sceneNumber)
	{
		SceneManager.LoadScene(sceneNumber);
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
