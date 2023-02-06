using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State {
	public ChaseState(StateAgent owner) : base(owner) {

	}

	public override void OnEnter() {
		Owner.navigation.targetNode = null;
		Owner.movement.Resume();
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate()	{
		if (Owner.Percieved.Length == 0) {
			Owner.statemachine.StartState(nameof(IdleState));
		} else {
			// Move towards the perceived object position
			Owner.movement.MoveTowards(Owner.Percieved[0].transform.position);

			// Create a direction vector toward the percieved object from the owner
			Vector3 Direction = Owner.Percieved[0].transform.position - Owner.transform.position;

			// Get the distance to the perceived object 
			float Distance = Direction.magnitude;

			// Get the angle between the owner forward vector and the direction vector 
			float Angle = Vector3.Angle(Owner.transform.forward, Direction);

			// If within range and angle, attack
			if (Distance < 2 && Angle < 20) {
				Owner.statemachine.StartState(nameof(AttackState));
			}
		}
	}
}