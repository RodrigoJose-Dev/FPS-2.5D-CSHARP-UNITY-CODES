using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [Header("Player Settings")]
  [SerializeField]
  [Range(5.0f, 12.0f)]
  [Tooltip("This property works with the player's speed")]
  private float playerSpeed = 10.0f;
  [SerializeField]
  [Range(-10.0f, 10.0f)]
  [Tooltip("This property works with the player's gravity")]
  private float playerGravity = -10.0f;

  private float momentumDamping = 5.0f;

  [HideInInspector]
  public bool gunMoving;

  [HideInInspector]
  public bool pauseState;

  private Vector3 inputVector;
  private Vector3 moveVector;

  private Animator camAnim;
  private CharacterController pCharController;

	private void Awake()
	{
    camAnim = GetComponentInChildren<Animator>();
    camAnim.SetBool("isWalking", false);
  }

	private void Start()
  {
    pCharController = GetComponent<CharacterController>();
   
  }

  private void Update()
  {
    GetInput();
    MovePlayer();

  }

  //This function is responsible for taking inputs
  void GetInput()
  {
		#region player move
		//setting input keys
		if (
      Input.GetKey(KeyCode.W) ||
      Input.GetKey(KeyCode.A) ||
      Input.GetKey(KeyCode.S) ||
      Input.GetKey(KeyCode.D)
    )
    {
      inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
      inputVector.Normalize(); //-> fixed diagonal direction speed
      inputVector = transform.TransformDirection(inputVector); //-> fixed the direction in which the player is looking

      //move animation starts
      camAnim.SetBool("isWalking", true);

      //gun moving animation in class GunAnimation
      gunMoving = true;
    }
    else
    { //-> if the player stops the move, the lerp it towards zero
      inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);

			//move animation ends
			camAnim.SetBool("isWalking", false);

      //gun moving animation in class GunAnimation
      gunMoving = false;
    }
		#endregion
		
    //pause key
		if (Input.GetKeyDown(KeyCode.Escape) && !pauseState)
		{
      
      bool isPaused = true;

      //show pnl
      CanvasManager.Instance.ShowPausePnl(isPaused);

      //freeze time
      ActionManager.Instance.PauseGameState(0);

      pauseState = true;
    }
		else if (Input.GetKeyDown(KeyCode.Escape) && pauseState)
		{
      bool isPaused = false;

      //hide pnl
      CanvasManager.Instance.ShowPausePnl(isPaused);

      //unfreeze time
      ActionManager.Instance.PauseGameState(1);

      pauseState = false;
		}


    moveVector = (inputVector * playerSpeed) + (Vector3.up * playerGravity);
  }

  //This function moves the player
  void MovePlayer()
  {
    pCharController.Move(moveVector * Time.deltaTime);
  }


}
