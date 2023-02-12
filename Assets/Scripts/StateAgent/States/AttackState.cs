using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackState : State {
	public AttackState(StateAgent owner) : base(owner) {
	}

	public override void OnEnter() {
		Owner.navigation.targetNode = null;
		Owner.movement.Stop();
		Owner.animator.SetTrigger("Attack");

		AnimationClip[] Clips = Owner.animator.runtimeAnimatorController.animationClips;
		AnimationClip Clip = Clips.FirstOrDefault<AnimationClip>(Clip => Clip.name == "Zombie Attack");

		var Colliders = Physics.OverlapSphere(Owner.transform.position, 2);
		foreach (var Collider in Colliders) {
			if (Collider.gameObject == Owner.gameObject || Collider.gameObject.CompareTag(Owner.gameObject.tag)) continue;

			if (Collider.gameObject.TryGetComponent<StateAgent>(out var Component)) {
				Component.Health.value -= Random.Range(20, 50);
			}
		}
	}

	public override void OnExit() {
		
	}

	public override void OnUpdate() {
		
	}
}