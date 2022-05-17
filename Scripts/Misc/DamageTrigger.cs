using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
	private PlayerHealth playerHealth;

	[SerializeField] private int damageAmount = 10;
	[SerializeField] private float timeBetweenDamage = 0.5f;

	private float damageCounter;
	private bool damagingPlayer;

	private void Start()
	{
		playerHealth = FindObjectOfType<PlayerHealth>();

		damageCounter = timeBetweenDamage;
	}

	private void Update()
	{
		if (damagingPlayer)
		{
			//damage player every time between damage
			if(damageCounter >= timeBetweenDamage)
			{
				//damage player
				
				playerHealth.DamagePlayer(damageAmount);
								
				//restart counter
				damageCounter = 0f;
			}

			//add to counter
			damageCounter += Time.deltaTime;

			//canvas ui take damage img
			CanvasManager.Instance.DamageImgEnabled();

		}
		else
		{
			//keep damage counter reset
			damageCounter = 0f;
			CanvasManager.Instance.DamageImgDisabled();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			damageCounter = timeBetweenDamage;
			damagingPlayer = true;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			damagingPlayer = false;
		}
	}
}
