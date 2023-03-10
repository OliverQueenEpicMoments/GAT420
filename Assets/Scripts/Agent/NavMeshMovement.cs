using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovement : Movement {
	[SerializeField] NavMeshAgent navMeshAgent;

	public override Vector3 Velocity {
		get => navMeshAgent.velocity;
		set => navMeshAgent.velocity = value;
	}

	public override Vector3 Destination	{
		get => navMeshAgent.destination;
		set => navMeshAgent.destination = value;
	}

	void LateUpdate() {
		navMeshAgent.speed = MaxSpeed;
		navMeshAgent.acceleration = MaxForce;
		navMeshAgent.angularSpeed = TurnRate;
	}

	public override void ApplyForce(Vector3 force) {
		//
	}

	public override void MoveTowards(Vector3 target) {
		navMeshAgent.SetDestination(target);
	}

	public override void Resume() {
		navMeshAgent.isStopped = false;
	}

	public override void Stop()	{
		navMeshAgent.isStopped = true;
	}
}