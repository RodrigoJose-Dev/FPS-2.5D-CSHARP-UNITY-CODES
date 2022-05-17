using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
	//class Enemy Awareness
	private EnemyAwareness enemyAwareness;

	private Transform playersTransform;
	private NavMeshAgent enemyNMA;

	private void Awake()
	{
		enemyNMA = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		enemyAwareness = GetComponent<EnemyAwareness>();
		playersTransform = FindObjectOfType<PlayerController>().transform;
	}

	/// <summary>
	/// If the player gets close, the enemy will become aggressive and chase the player. 
	/// </summary>
	private void FixedUpdate()
	{
		if (enemyAwareness.isAggro)
		{
			ChasePlayer();
		}
		else
		{
			StopEnemy();
		}

	}

	private void ChasePlayer()
	{
		enemyNMA.SetDestination(playersTransform.position);
	}

	public void StopEnemy()
	{
		enemyNMA.SetDestination(transform.position);
	}

}
