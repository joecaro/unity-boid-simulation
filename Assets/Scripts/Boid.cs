using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
	Flock _boidFlock;
	public Flock boidFlock
	{
		get
		{
			return _boidFlock;
		}
	}

	Collider2D _boidCollider;
	public Collider2D boidCollider { get { return _boidCollider; } }


	void Start()
	{
		_boidCollider = GetComponent<Collider2D>();

	}

	public void Initialize(Flock flock)
	{
		_boidFlock = flock;
	}

	public void Move(Vector2 velocity)
	{
		transform.up = velocity;
		transform.position += (Vector3)velocity * Time.deltaTime;
	}
}
