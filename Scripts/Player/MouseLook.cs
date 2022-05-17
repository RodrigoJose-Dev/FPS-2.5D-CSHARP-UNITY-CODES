using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
  [Header("Mouse Settings")]
  [SerializeField]
  [Range(1.0f, 10.0f)]
  [Tooltip("This property works with the mouse sensitivity")]
  private float mouseSensitivity = 1.5f;
  [SerializeField]
  [Range(1.0f, 10.0f)]
  [Tooltip("This property works with the mouse smoothing")]
  private float smoothing = 1.5f;

  private float currentLookingPos;

  private float xMousePos;
  private float smoothedMousePos;

  private Scene scene;

	private void Start()
	{
    scene = SceneManager.GetActiveScene();
  }

	private void Update()
  {
    GetInput();
    ModifyInput();

    #region lock cursor
    
    if (scene.name == "Game" && Time.timeScale == 0)
    {
      transform.localRotation = transform.localRotation;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;

      FindObjectOfType<AudioManager>().Stop("EagleShotFX");
    }
    
    if (scene.name == "Game" && Time.timeScale == 1)
		{
      RotatePlayer();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }
    
    if (scene.name == "MainMenu")
		{
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
		#endregion

	}

	void GetInput()
  {
    xMousePos = Input.GetAxisRaw("Mouse X");
  }

  void ModifyInput()
  { //-> add sensitivity and smothing to mouse X
    xMousePos *= mouseSensitivity * smoothing;
    smoothedMousePos = Mathf.Lerp(smoothedMousePos, xMousePos, 1.0f / smoothing);
  }

  void RotatePlayer()
  { //-> rotating the player cam with mouse
    currentLookingPos += smoothedMousePos;
    transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
  }
   
  
}
