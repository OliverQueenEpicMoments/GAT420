using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {
	private float Timer;

	public IdleState(StateAgent owner) : base(owner) {

	}

	public override void OnEnter() {
		Debug.Log("Idle Enter");
		Timer = 2;
	}

	public override void OnExit() {
		Debug.Log("Idle Exit");
	}

	public override void OnUpdate()	{
		Debug.Log("Idle Update");
		Timer -= Time.deltaTime;
		if (Timer <= 0) { 
			Owner.statemachine.StartState(nameof(PatrolState));
		}
	}
}