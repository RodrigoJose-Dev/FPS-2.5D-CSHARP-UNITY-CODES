using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
	private PlayerHealth playerHealth;
	private Enemy enemyClass;
	
	private Transform playerTransform;

	[Header("Enemy Awareness Settings")]
	[Range(0f, 50.0f)]
	[SerializeField]
	private float awarenessRadius = 8.0f;

	[Header("Enemy Attack Settings")]
	[SerializeField]
	private int damage = 10;
	[SerializeField]
	private float rangeToAttack = 1.5f;
	[SerializeField]
	private float cooldownTimeToAttack = 2.0f;
	[SerializeField]
	private float nextAttackTime;
		
	[HideInInspector]
	public bool isAggro = false;

	private Animator spriteAnim;

	private void Start()
	{
		playerHealth = FindObjectOfType<PlayerHealth>();
		enemyClass = FindObjectOfType<Enemy>();

		playerTransform = FindObjectOfType<PlayerController>().transform;

		spriteAnim = GetComponentInChildren<Animator>();
	}

	/// <summary>
	/// If the player gets close, the enemy will attack player. 
	/// </summary>
	private void Update()
	{
		//distance from enemy to player
		var distance = Vector3.Distance(transform.position, playerTransform.position);

		if(distance < awarenessRadius)
		{
			isAggro = true;
		}
				
		if (isAggro && distance <= rangeToAttack && Time.time > nextAttackTime && 
			!enemyClass.isDead) //-> if the enemy is close of the player and not dead, attack him
		{
			//attack animation
			spriteAnim.SetBool("isAttacking", true);
			
			//damage and next attack
			playerHealth.DamagePlayer(damage);
			nextAttackTime = Time.time + cooldownTimeToAttack;
			
			//canvas ui take damage img
			CanvasManager.Instance.DamageImgEnabled();
		}
		else if (isAggro && distance > rangeToAttack)
		{
			spriteAnim.SetBool("isAttacking", false);
		}
	}
		
}
