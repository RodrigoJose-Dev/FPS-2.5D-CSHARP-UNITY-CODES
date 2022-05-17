using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
	//player's inventory
	public bool hasRed, hasGreen, hasBlue;

	private void Start()
	{	//clear keys when the game starts
		CanvasManager.Instance.ClearKeys();
	}

}
