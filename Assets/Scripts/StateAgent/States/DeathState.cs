using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State {
	public DeathState(StateAgent owner) : base(owner) {
	}

	public override void OnEnter() {
		Owner.animator.SetBool("IsDead", true);
		Owner.movement.Stop();
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate() {
		if (Owner.AnimationDone) GameObject.Destroy(Owner.gameObject, 4.5f);
	}
}