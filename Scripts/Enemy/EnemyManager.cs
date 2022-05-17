using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyManager : MonoBehaviour
{

  [Header("List of enemies in gun trigger")]
  public List<Enemy> enemiesInTrigger = new List<Enemy>();

  //add and remove enemy from gun trigger
  public void AddEnemy(Enemy enemy)
  {
    enemiesInTrigger.Add(enemy);
  }

  public void RemoveEnemy(Enemy enemy)
  {
    enemiesInTrigger.Remove(enemy);
        
  }
}
