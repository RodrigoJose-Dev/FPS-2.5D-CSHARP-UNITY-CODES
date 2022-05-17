using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class PlayerHealth : MonoBehaviour
{
	[Header("Player Health and Armor Settings")]
	[Range(1, 100)]
	[SerializeField]
	[Tooltip("This property works with the player's health")]
	private int maxHealth = 100;
	[SerializeField]
	[Tooltip("This property works with the player's current health")]
	private int currentHealth;
	[Range(0, 100)]
	[SerializeField]
	[Tooltip("This property works with the player's armor")]
	private int maxArmor = 100;
	[SerializeField]
	[Tooltip("This property works with the player's current armor")]
	private int currentArmor;

	private void Start()
	{
		//send currentHealth value to UpdateHealth on CanvasManager class
		CanvasManager.Instance.UpdateHealth(currentHealth);
		CanvasManager.Instance.UpdateArmor(currentArmor);
	}
		
	/// <summary>
	/// if the player only has enough armor to absorb some of the damage
	/// then damage the armor first and then damage the player
	/// </summary>
	public void DamagePlayer(int damage)
	{ 
		
		if(currentArmor > 0)
		{
			if(currentArmor >= damage)
			{
				currentArmor -= damage;
			}else if(currentArmor < damage)
			{
				int remainingDamage;

				remainingDamage = damage - currentArmor;

				currentArmor = 0;

				currentHealth -= remainingDamage;
			}
		}
		else
		{
			currentHealth -= damage;
		}

		//Death state
		if(currentHealth <= 0)
		{
			Scene currentScene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(currentScene.buildIndex);
		}
		
		//updating health and armor on Canvas
		CanvasManager.Instance.UpdateHealth(currentHealth);
		CanvasManager.Instance.UpdateArmor(currentArmor);
	}

	public void GiveHealth(int amount, GameObject pickup)
	{
		
		if(currentHealth < maxHealth)
		{
			currentHealth += amount;
			Destroy(pickup);
		}

		if(currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		CanvasManager.Instance.UpdateHealth(currentHealth);
	}

	public void GiveArmor(int amount, GameObject pickup)
	{
		
		if (currentArmor < maxArmor)
		{
			currentArmor += amount;
			Destroy(pickup);
		}

		if (currentArmor > maxArmor)
		{
			currentArmor = maxArmor;
		}

		//updating armor on Canvas
		CanvasManager.Instance.UpdateArmor(currentArmor);
	}
}
