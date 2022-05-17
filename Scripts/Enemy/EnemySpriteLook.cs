using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteLook : MonoBehaviour
{
	private Transform target;

	[SerializeField] private bool lookingVertical;

	private void Start()
	{
		target = FindObjectOfType<PlayerController>().transform;
	}

	private void Update()
	{
		if (lookingVertical)
		{
			transform.LookAt(target);
		}
		else
		{
			Vector3 modifiedTarget = target.position;
			modifiedTarget.y = transform.position.y;

			transform.LookAt(modifiedTarget);
		}

	}
}
