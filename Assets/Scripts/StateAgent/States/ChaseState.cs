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
		if (Owner.EnemySeen) {
			// Reset timer
			Owner.Timer.value = 2;

			// Move towards the perceived object position
			Owner.movement.MoveTowards(Owner.Percieved[0].transform.position);
		} 
	}
}