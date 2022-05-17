using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItemPickup : MonoBehaviour
{
	[Header("Item Settings")]
	[SerializeField]
	[Tooltip("If it's a healing item")]
	private bool isHealth;
	[SerializeField]
	[Tooltip("If it's an armor item")]
	private bool isArmor;
	[SerializeField]
	[Tooltip("If it's an ammo item")]
	private bool isAmmo;
	
	[SerializeField]
	[Range(0, 100)]
	[Tooltip("Quantity that the item carries")]
	private int amount;

	/// <summary>
	/// If health, armor or ammo is true, call class PlayersHealth and 
	/// method GiveHealth, GiveArmor or GiveAmmo
	/// </summary>
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (isHealth)
			{
				other.GetComponent<PlayerHealth>().GiveHealth(amount, this.gameObject);
			}
			if (isArmor)
			{
				other.GetComponent<PlayerHealth>().GiveArmor(amount, this.gameObject);
			}
			if (isAmmo)
			{
				other.GetComponentInChildren<Gun>().GiveAmmo(amount, this.gameObject);
			}

		}
	}

}
