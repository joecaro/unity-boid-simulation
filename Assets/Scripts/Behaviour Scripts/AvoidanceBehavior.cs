using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredBoidBehavior
{
	public override Vector2 CalculateMove(Boid agent, List<Transform> context, Flock flock)
	{
		// if no neighbors do nothing
		if (context.Count == 0)
		{
			return Vector2.zero;
		}

		// if neighbors, add points and average

		Vector2 avoidanceMove = Vector2.zero;
		// number of neighbors to avoid
		int nAvoid = 0;
		// optional: filtered list to look for specific neighbors
		List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
		foreach (Transform item in filteredContext)
		{
			if (item.name == "Predator")
			{
				avoidanceMove += ((Vector2)(agent.transform.position - item.transform.position) * flock.predatorAvoidanceMultiplie);
			}
			else if (Vector2.SqrMagnitude(item.transform.position - agent.transform.position) < flock.sqaureAvoidanceRadius)
			{
				nAvoid++;
				avoidanceMove += ((Vector2)(agent.transform.position - item.transform.position));
			}

		}

		// calculate avoidanceMove
		if (nAvoid > 0)
			avoidanceMove /= nAvoid;

		return avoidanceMove;
	}
}
