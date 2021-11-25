using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredBoidBehavior
{
	public override Vector2 CalculateMove(Boid agent, List<Transform> context, Flock flock)
	{
		// if no neighbors do nothing
		if (context.Count == 0)
		{
			return agent.transform.up;
		}

		// if neighbors, add points and average

		Vector2 alignmentMove = Vector2.zero;
		// optional: filtered list to look for specific neighbors
		List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
		foreach (Transform item in filteredContext)
		{
			alignmentMove += (Vector2)item.transform.up;
		}

		// calculate alignmentMove
		alignmentMove /= context.Count;


		return alignmentMove;
	}
}
