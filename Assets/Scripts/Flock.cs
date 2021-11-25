using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
	[SerializeField]
	private int numberOfBoids = 10;
	[SerializeField]
	private float spawnRadius = 0.8f;
	[SerializeField]
	[Range(1f, 20f)]
	private float drive = 10f;
	[SerializeField]
	[Range(1f, 10f)]
	private float maxSpeed = 5f;
	[Range(1f, 5f)]
	public float neighborRadius = 1;
	[Range(0f, 5f)]
	public float avoidanceRadiusMultiplier = 0.5f;
	[Range(0f, 5f)]
	public float predatorAvoidanceMultiplie = 2f;

	List<Boid> agents = new List<Boid>();

	public Boid boidPrefab;
	public BoidBehaviour behaviour;

	float sqaureMaxSpeed;
	float sqaureNeighborRadius;
	float _sqaureAvoidanceRadius;
	public float sqaureAvoidanceRadius { get { return _sqaureAvoidanceRadius; } }

	public static List<GameObject> boids = new List<GameObject>();
	void Start()
	{
		sqaureMaxSpeed = maxSpeed * maxSpeed;
		sqaureNeighborRadius = neighborRadius * neighborRadius;
		_sqaureAvoidanceRadius = neighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;


		for (int i = 0; i <= numberOfBoids; i++)
		{
			Boid newBoid = Instantiate(boidPrefab, Random.insideUnitCircle * spawnRadius, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
			transform);
			newBoid.name = "Agent" + i;
			newBoid.Initialize(this);
			agents.Add(newBoid);
		}
	}

	// Update is called once per frame
	void Update()
	{
		// *get nearby agents* and calcuate movement
		foreach (Boid agent in agents)
		{
			// get transforms for nearby colliders
			List<Transform> context = GetNearbyAgents(agent);

			agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

			//get move vector from behaviour method
			Vector2 move = behaviour.CalculateMove(agent, context, this);


			// normalize move and call boid move method.
			move *= drive;

			if (move.sqrMagnitude > sqaureMaxSpeed)
				move = move.normalized * maxSpeed;

			agent.Move(move);
		}

	}

	List<Transform> GetNearbyAgents(Boid agent)
	{
		// create context list
		List<Transform> context = new List<Transform>();
		// list of colliders - find overlap with agent physics2d overlapcircleall method(finds ovelap in a circular area)
		Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
		// loop through colliders found and add collider transform to context if not this agent's collider
		foreach (Collider2D c in contextColliders)
		{
			if (c != agent.boidCollider)
				context.Add(c.transform);
		}
		return context;
	}
}
