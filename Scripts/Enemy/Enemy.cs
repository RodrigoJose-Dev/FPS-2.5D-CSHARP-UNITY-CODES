using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private EnemyManager enemyManager;
  private EnemyAI enemyAI;
  private AngleToPlayer angleToPlayer;

  [Header("Hit Effect")]
  [SerializeField]
  private GameObject gunHitEffect;
  [Header("Drop")]
  [SerializeField]
  private GameObject itemToDrop;
  [Tooltip("This property works with the item drop after enemy dead")]
  private bool droped;
  [HideInInspector]
  public bool isDead;
 
  [Header("Enemy Health Settings")]
  [SerializeField]
  [Range(0f, 1000.0f)]
  [Tooltip("This property works with the enemy health value")]
  private float enemyHealth = 50f;

  private Animator spriteAnim;

  private void Start()
  {
    enemyManager = FindObjectOfType<EnemyManager>();
    enemyAI = GetComponentInChildren<EnemyAI>();
    angleToPlayer = GetComponent<AngleToPlayer>();
        
    spriteAnim = GetComponentInChildren<Animator>();
  }
  private void Update()
  {
    //beginning of update set animations rotational index
    spriteAnim.SetFloat("spriteRot", angleToPlayer.lastIndex);

    if (enemyHealth <= 0) //-> killing enemy if the life becomes zero
    {
      //stoping enemy after dead
      enemyAI.StopEnemy();

      //dead state so as not to hurt the player
      isDead = true;

      //removing enemie from vector in enemymanager
      enemyManager.RemoveEnemy(this);

      //death animation
      spriteAnim.SetTrigger("isDead");

      //destroy enemy
      Destroy(gameObject, 2f);

      if (itemToDrop != null && !droped && Time.time > 2f)
      {
        DropItem();
        droped = true;
      }
    }
  }

  public void TakeDamage(float damage)
  { 
    GameObject effect = Instantiate(gunHitEffect, transform.position, Quaternion.identity);
    enemyHealth -= damage;

    Destroy(effect, 2f);
  }

  void DropItem()
  {
    Instantiate(itemToDrop, transform.position, Quaternion.identity);
  }
}
