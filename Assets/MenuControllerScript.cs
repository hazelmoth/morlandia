using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerScript : MonoBehaviour {

	public void QuitApp()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		SceneManager.LoadSceneAsync ("Main");
	}
}
