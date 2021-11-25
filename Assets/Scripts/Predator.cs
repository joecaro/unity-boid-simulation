using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
	[SerializeField]
	private float maxSpeed = 2;
	[SerializeField]
	private float radius = 15f;
	[SerializeField]
	private Vector2 center;

	private Flock flock;
	List<Transform> list;
	private Vector2 velocity;


	float sqaureMaxSpeed;
	void Start()
	{
		sqaureMaxSpeed = maxSpeed * maxSpeed;
		velocity = Vector2.up * maxSpeed;

	}

	// Update is called once per frame
	void Update()
	{
		// Vector2 move = CalculateMove();

		// velocity += move * Time.deltaTime;

		// Move(velocity);
		Vector2 currrentPos = transform.position;

		Vector2 nextPos = new Vector2((Input.mousePosition.x - Screen.width / 2) * 0.045f, (Input.mousePosition.y - Screen.height / 2) * 0.045f);

		if (nextPos != currrentPos)
		{
			transform.up = (Vector2)transform.up - (nextPos - currrentPos);
		}

		transform.position = nextPos;
	}

	private Vector2 CalculateMove()
	{

		Vector2 centerOffset = center - (Vector2)this.transform.position;
		float t = centerOffset.magnitude / radius;
		if (t > 0.9f)
			return Vector2.zero;
		else
			return centerOffset * t * t;


	}

	private void Move(Vector2 velocity)
	{
		transform.up = velocity;
		transform.position += (Vector3)velocity * Time.deltaTime;
	}

	private void OnApplicationFocus(bool focusStatus)
	{
		Cursor.visible = false;
	}
}
