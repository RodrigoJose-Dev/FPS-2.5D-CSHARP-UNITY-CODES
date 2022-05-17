using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
	public static ActionManager _instance;
	public static ActionManager Instance { get { return _instance; } }

	public Button resumeBtn;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	/// <summary>
	/// Action methods to game
	/// </summary>

	public void PassTheScene(string name)
	{
		SceneManager.LoadScene(name);
	}
			
	public void PauseGameState(int scaleValue)
	{
		Time.timeScale = scaleValue;
	}

	public void PauseGameStateBtnMenu()
	{
		resumeBtn.onClick.AddListener(PauseGameStateBtnMenu);

		PlayerController playerController = FindObjectOfType<PlayerController>();

		if(Time.timeScale == 0)
		{
			Time.timeScale = 1;
			playerController.pauseState = false;
		}
	}

	public void unpauseGameBtnMenu()
	{
		Time.timeScale = 1;
	}

	public void CloseAppBtnMenu()
	{
		Application.Quit();
	}


}
