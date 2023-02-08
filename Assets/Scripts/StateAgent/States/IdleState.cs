using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
	public IdleState(StateAgent owner) : base(owner) {

	}

	public override void OnEnter() {
		Owner.movement.Stop();
		Owner.Timer.value = Random.Range(1, 3);
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate()	{
		
	}
}