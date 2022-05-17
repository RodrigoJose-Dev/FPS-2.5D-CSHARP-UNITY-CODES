using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
	private Transform target;

	private void Start()
	{
		target = FindObjectOfType<PlayerController>().transform; 
	}

	/// <summary>
	/// Look at Player
	/// </summary>
	private void Update()
	{
		transform.LookAt(target);
	}
}
