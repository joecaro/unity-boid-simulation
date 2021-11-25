using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehaviors : BoidBehaviour
{
	public BoidBehaviour[] behaviours;
	public float[] weights;


	public override Vector2 CalculateMove(Boid agent, List<Transform> context, Flock flock)
	{
		if (behaviours.Length != weights.Length)
			Debug.LogError("Data mismatch in " + name, this);

		Vector2 move = Vector2.zero;

		// loop through behaviors and modify to move
		for (int i = 0; i < behaviours.Length; i++)
		{
			Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock);


			if (partialMove != Vector2.zero)
			{

				if (partialMove.sqrMagnitude > weights[i] * weights[i])
				{
					partialMove.Normalize();
					partialMove *= weights[i];
				}

				move += partialMove;
			}
		}

		return move;
	}
}
