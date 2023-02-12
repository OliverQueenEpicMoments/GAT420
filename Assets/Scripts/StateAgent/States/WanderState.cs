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
		Owner.movement.MoveTowards(Target);
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate()	{
		
	}
}