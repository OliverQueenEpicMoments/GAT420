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
			Owner.movement.MoveTowards(Owner.Percieved[0].transform.position);
		}
	}
}