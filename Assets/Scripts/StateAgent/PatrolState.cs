using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {
	public PatrolState(StateAgent owner) : base(owner)
	{
	}

	public override void OnEnter() {
		Owner.movement.Resume();
		Owner.navigation.targetNode = Owner.navigation.GetNearestNode();
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate()	{
		if (Owner.Percieved.Length > 0) {
			Owner.statemachine.StartState(nameof(ChaseState));
		}
	}
}