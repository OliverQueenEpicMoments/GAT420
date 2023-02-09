using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : State {
	public EvadeState(StateAgent owner) : base(owner) {
	}

	public override void OnEnter() {
		Owner.navigation.targetNode = null;
		Owner.movement.Resume();
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate() {
		if (Owner.EnemySeen) {
			Vector3 Direction = (Owner.transform.position - Owner.Percieved[0].transform.position).normalized;
			Owner.movement.MoveTowards(Owner.transform.position + Direction * 5);
		}
	}
}