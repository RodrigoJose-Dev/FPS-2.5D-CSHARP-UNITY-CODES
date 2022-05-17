using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Door : MonoBehaviour
{
	private Inventory inventory;

	[SerializeField]
	private bool requiresKey;
	[SerializeField]
	private bool reqRed, reqGreen, reqBlue;

	private Animator doorAnim;

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();

		doorAnim = GetComponentInChildren<Animator>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (requiresKey)
			{
				//if the player has the exact key in inventory, the door opens
				if(reqRed && other.GetComponent<Inventory>().hasRed)
				{
					//open door
					doorAnim.SetTrigger("OpenDoor");

					//clear key from inventory after pass the door
					inventory.hasRed = false;

					//clear key from UI
					CanvasManager.Instance.ClearKeys();
				}

				if (reqGreen && other.GetComponent<Inventory>().hasGreen)
				{
					//open door
					doorAnim.SetTrigger("OpenDoor");

					//clear key from inventory after pass the door
					inventory.hasGreen = false;

					//clear key from UI
					CanvasManager.Instance.ClearKeys();
				}

				if (reqBlue && other.GetComponent<Inventory>().hasBlue)
				{
					//open door
					doorAnim.SetTrigger("OpenDoor");

					//clear key from inventory after pass the door
					inventory.hasBlue = false;

					//clear key from UI
					CanvasManager.Instance.ClearKeys();
				}
			}
			else
			{
				//open door
				doorAnim.SetTrigger("OpenDoor");
			}
						
		}
	}
}
