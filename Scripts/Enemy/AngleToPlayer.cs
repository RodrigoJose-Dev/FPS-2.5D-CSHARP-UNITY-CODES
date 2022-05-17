using UnityEngine;

public class AngleToPlayer : MonoBehaviour
{
	private Transform player;
	private Vector3 targetPos;
	private Vector3 targetDir;

	[SerializeField] private float angle;
	public int lastIndex;

	private SpriteRenderer sprite;

	private void Start()
	{
		player = FindObjectOfType<PlayerController>().transform;
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		//get target position and direction
		targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
		targetDir = targetPos - transform.position;

		//get angle
		angle = Vector3.SignedAngle(targetDir, transform.forward, transform.up);

		//flip sprite if needed
		Vector3 tempScale = Vector3.one;
		if(angle > 0)
		{
			tempScale.x *= -1;
		}

		sprite.transform.localScale = tempScale;

		lastIndex = GetIndex(angle);
	}

	private int GetIndex(float angle)
	{
		//front
		if (angle > -22.5f && angle < 22.6f)
			return 0;
		if (angle >= 22.5f && angle < 67.5f)
			return 7;
		if (angle >= 67.5f && angle < 112.5f)
			return 6;
		if (angle >= 112.5f && angle < 157.5f)
			return 5;

		//back
		if (angle <= -157.5f || angle >= 157.5f)
			return 4;
		if (angle >= -157.4f && angle < -112.5f)
			return 3;
		if (angle >= -112.5f && angle < -67.5f)
			return 2;
		if (angle >= -67.5f && angle <= -22.5f)
			return 1;

		return lastIndex;
	}
	
}
