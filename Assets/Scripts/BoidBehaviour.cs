using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidBehaviour : ScriptableObject
{
	//create base class for behaviour methods
	public abstract Vector2 CalculateMove(Boid agent, List<Transform> context, Flock flock);
}
