using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredBoidBehavior
{
	public override Vector2 CalculateMove(Boid agent, List<Transform> context, Flock flock)
	{
		// if no neighbors do nothing
		if (context.Count == 0)
		{
			return Vector2.zero;
		}

		// if neighbors, add points and average

		Vector2 cohesionMove = Vector2.zero;
		// optional: filtered list to look for specific neighbors
		List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
		foreach (Transform item in filteredContext)
		{
			cohesionMove += (Vector2)item.position;

		}
		// calculate cohesionMove
		cohesionMove /= context.Count;

		cohesionMove -= (Vector2)agent.transform.position;

		return cohesionMove;
	}
}
