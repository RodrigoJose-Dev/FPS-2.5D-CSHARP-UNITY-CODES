using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
	[SerializeField]
	[Tooltip("This property works with the identification of the keys")]
	private bool isRedKey, isGreenKey, isBlueKey;

	/// <summary>
	/// this function identifies the color of the key obtained, 
	/// places the key in the inventory and passes it to the canvas
	/// </summary>
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (isRedKey)
			{
				other.GetComponent<Inventory>().hasRed = true;
				CanvasManager.Instance.UpdateKeys(keyColor: "red");
			}

			if (isGreenKey)
			{
				other.GetComponent<Inventory>().hasGreen = true;
				CanvasManager.Instance.UpdateKeys(keyColor: "green");
			}

			if (isBlueKey)
			{
				other.GetComponent<Inventory>().hasBlue = true;
				CanvasManager.Instance.UpdateKeys(keyColor: "blue");
			}

			Destroy(gameObject);
		}
	}
}
