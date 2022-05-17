using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Gun : MonoBehaviour
{
  
  private EnemyManager enemyManager;
  private Enemy enemyClass;

  private BoxCollider bcGunTrigger;

  [Header("Gun Settings")]
  [SerializeField]
  [Range(0f, 50.0f)]
  [Tooltip("This property works with the gun range")]
  private float verticalRange = 20.0f;
  [SerializeField]
  [Range(0f, 50.0f)]
  [Tooltip("This property works with the gun small damage")]
  private float gunShotRadius = 20.0f;
  [SerializeField]
  [Range(0f, 50.0f)]
  [Tooltip("This property works with the gun range")]
  private float range = 20.0f;
  [SerializeField]
  [Range(0f, 50.0f)]
  [Tooltip("This property works with the gun fire rate")]
  private float fireRate = 1.0f;
  [SerializeField]
  [Range(0f, 5.0f)]
  [Tooltip("This property works with the next time to gun fire")]
  private float nextTimeToFire = 1.0f;

  [Header("Gun Ammo Settings")]
  [SerializeField]
  [Range(0f, 100)]
  [Tooltip("This property works with the max ammo of the gun")]
  private int maxAmmo = 100;
  [SerializeField]
  [Tooltip("This property works with the current ammo of the gun")]
  private int currentAmmo;

  [Header("Gun Damage Settings")]
  [SerializeField]
  [Range(0f, 50.0f)]
  [Tooltip("This property works with the gun small damage")]
  private float smallDamage = 2.0f;
  [SerializeField]
  [Range(0f, 100.0f)]
  [Tooltip("This property works with the gun big damage")]
  private float bigDamage = 100.0f;
  
  [Header("Gun Layers")]
  [SerializeField]
  [Tooltip("This property works with the layer mask")]
  private LayerMask raycastLayerMask;
  [SerializeField]
  [Tooltip("This property works with the enemy layer mask")]
  private LayerMask enemyLayerMask;

  [HideInInspector]
  public bool isShooting;

  private void Start()
  {
    enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    enemyClass = FindObjectOfType<Enemy>();

    bcGunTrigger = GetComponent<BoxCollider>();

    //vertical range and range box
    bcGunTrigger.size = new Vector3(1, verticalRange, range);
    bcGunTrigger.center = new Vector3(0, 0, range * 0.5f); 

    //setting ammo on canvas
    CanvasManager.Instance.UpdateAmmo(currentAmmo);
  }

  private void Update()
  {
    //shooting
    if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && currentAmmo > 0)
    {
      //if is shooting, stabilizes the gun in GunAnimation class
      isShooting = true;

      Fire();
    }
		else if(Input.GetMouseButtonUp(0) && Time.time > 0.5f)
		{
      //if is shooting, stabilizes the gun in GunAnimation class
      isShooting = false;
    }
    		
  }

  
  void Fire()
  {
    
    //simulate gun shot radius
    Collider[] enemyColliders;
    enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

    //alert any enemy in earshot
    foreach(var enemyCol in enemyColliders)
		{
      enemyCol.GetComponent<EnemyAwareness>().isAggro = true;
		}

    //play audio
    FindObjectOfType<AudioManager>().Play("EagleShotFX");

    //foreach enemies in enemiesInTrigger list
    foreach (var enemy in enemyManager.enemiesInTrigger)
    {     

      //get direction to enemy
      var dir = enemy.transform.position - transform.position;

      RaycastHit hit;
      if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))
      {
        if (hit.transform == enemy.transform)
        {
          //range check
          float distance = Vector3.Distance(enemy.transform.position, transform.position);

          if (distance > range * 0.5f)
          {
            //damage to enemy small
            enemy.TakeDamage(smallDamage);
                        
          }
          else
          {
            //damage to enemy big
            enemy.TakeDamage(bigDamage);
                        
          }


        }
      }

    }

    //reset timer to shoot
    nextTimeToFire = Time.time + fireRate;

    //deduct 1 ammo
    currentAmmo--;
    //show current ammo on canvas;
    CanvasManager.Instance.UpdateAmmo(currentAmmo);
  }

  public void GiveAmmo(int amount, GameObject pickup)
	{
    if(currentAmmo < maxAmmo)
		{
      currentAmmo += amount;
      Destroy(pickup);
		}

    if(currentAmmo > maxAmmo)
		{
      currentAmmo = maxAmmo;
		}
    
    //play fx ammo sound
    FindObjectOfType<AudioManager>().Play("AmmoFX");
    //show current ammo on canvas;
    CanvasManager.Instance.UpdateAmmo(currentAmmo);
        
  }

  private void OnTriggerEnter(Collider other)
  {
    //add potencial enemy to shoot
    Enemy enemy = other.transform.GetComponent<Enemy>();

    if (enemy)
    {
      enemyManager.AddEnemy(enemy);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    //remove potencial enemy to shoot
    Enemy enemy = other.transform.GetComponent<Enemy>();

    if (enemy)
    {
      enemyManager.RemoveEnemy(enemy);
    }
  }

}
