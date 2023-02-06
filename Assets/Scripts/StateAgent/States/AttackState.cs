using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackState : State {
	private float Timer;

	public AttackState(StateAgent owner) : base(owner) {
	}

	public override void OnEnter() {
		Owner.navigation.targetNode = null;
		Owner.movement.Stop();
		Owner.animator.SetTrigger("Attack");

		AnimationClip[] Clips = Owner.animator.runtimeAnimatorController.animationClips;
		AnimationClip Clip = Clips.FirstOrDefault<AnimationClip>(Clip => Clip.name == "Zombie Attack");

		Timer = (Clip != null) ? Clips.Length : 1;
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate() {
		Timer -= Time.deltaTime;
		if (Timer <= 0) {
			Owner.statemachine.StartState(nameof(ChaseState));
		}
	}
}