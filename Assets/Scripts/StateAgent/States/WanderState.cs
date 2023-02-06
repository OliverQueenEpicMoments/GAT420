using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State {
	private Vector3 Target;

	public WanderState(StateAgent owner) : base(owner) {
	}

	public override void OnEnter() {
		Owner.navigation.targetNode = null;
		Owner.movement.Resume();
		Target = Owner.transform.position + Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * Vector3.forward * 5;
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate()	{
		// Draw debug line from current position to target position
		Debug.DrawLine(Owner.transform.position, Target);
		Owner.movement.MoveTowards(Target);

		if (Owner.movement.Velocity.sqrMagnitude == 0) {
			Owner.statemachine.StartState(nameof(IdleState));
		}
	}
}