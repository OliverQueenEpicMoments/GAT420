using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
	private float Timer;

	public IdleState(StateAgent owner) : base(owner) {

	}

	public override void OnEnter() {
		Timer = 2;
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate()	{
		Timer -= Time.deltaTime;
		if (Timer <= 0) { 
			Owner.statemachine.StartState(nameof(PatrolState));
		}
	}
}