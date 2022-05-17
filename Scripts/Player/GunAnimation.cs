using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
	private PlayerController playerController;
	private Gun gunClass;

	private Animator anim;

	private void Start()
	{
		playerController = FindObjectOfType<PlayerController>();
		gunClass = FindObjectOfType<Gun>();

		anim = GetComponent<Animator>();
	}

	/// <summary>
	/// When the player is moving or shooting, stabilizes the gun
	/// </summary>
	private void Update()
	{
		if(playerController.gunMoving && !gunClass.isShooting)
		{
			anim.SetBool("isMoving", true);

		}
		else if (!playerController.gunMoving || gunClass.isShooting)
		{
			anim.SetBool("isMoving", false);
		}

	}
}
