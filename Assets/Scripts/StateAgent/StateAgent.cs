using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent {
    public StateMachine statemachine = new StateMachine();
    public GameObject[] Percieved;
    public Camera MainCamera;

	// Condition parameters
	public FloatRef Health = new FloatRef();
	public FloatRef Timer = new FloatRef();
	public FloatRef EnemyDistance = new FloatRef();

	public BoolRef EnemySeen = new BoolRef();
	public BoolRef AnimationDone = new BoolRef();
	public BoolRef AtDestination = new BoolRef();

	void Start() {
        MainCamera = Camera.main;
		Health.value = 100;

        statemachine.AddState(new IdleState(this));
        statemachine.AddState(new PatrolState(this));
        statemachine.AddState(new ChaseState(this));
        statemachine.AddState(new WanderState(this));
        statemachine.AddState(new AttackState(this));
        statemachine.AddState(new DeathState(this));
        statemachine.AddState(new EvadeState(this));

		// Create Conditions
		Condition TimerExpiredCondition = new FloatCondition(Timer, Condition.Predicate.LESS_EQUAL, 0);
		Condition EnemySeenCondition = new BoolCondition(EnemySeen, true);
		Condition EnemyNotSeenCondition = new BoolCondition(EnemySeen, false);
		Condition EnemyNear = new FloatCondition(EnemyDistance, Condition.Predicate.LESS_EQUAL, 2);
		Condition HealthLowCondition = new FloatCondition(Health, Condition.Predicate.LESS_EQUAL, 30);
		Condition HealthOkCondition = new FloatCondition(Health, Condition.Predicate.GREATER, 30);
		Condition DeathCondition = new FloatCondition(Health, Condition.Predicate.LESS_EQUAL, 0);
		Condition AnimationDoneCondition = new BoolCondition(AnimationDone, true);
		Condition AtDestinationCondition = new BoolCondition(AtDestination, true);

		// Create Transitions
		statemachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { EnemySeenCondition, HealthOkCondition }), nameof(ChaseState));
		statemachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { EnemySeenCondition, HealthLowCondition }), nameof(EvadeState));
		statemachine.AddTransition(nameof(IdleState), new Transition(new Condition[] { TimerExpiredCondition }), nameof(PatrolState));

		statemachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { TimerExpiredCondition }), nameof(WanderState));
		statemachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { EnemySeenCondition, HealthOkCondition }), nameof(ChaseState));
		statemachine.AddTransition(nameof(PatrolState), new Transition(new Condition[] { EnemySeenCondition, HealthLowCondition }), nameof(EvadeState));

		statemachine.AddTransition(nameof(ChaseState), new Transition(new Condition[] { EnemyNotSeenCondition, TimerExpiredCondition }), nameof(IdleState));
		statemachine.AddTransition(nameof(ChaseState), new Transition(new Condition[] { new FloatCondition(EnemyDistance, Condition.Predicate.LESS_EQUAL, 2.5f) }), nameof(AttackState));
		statemachine.AddTransition(nameof(ChaseState), new Transition(new Condition[] { EnemySeenCondition, HealthLowCondition }), nameof(EvadeState));

		statemachine.AddTransition(nameof(WanderState), new Transition(new Condition[] { AtDestinationCondition }), nameof(IdleState));
		statemachine.AddTransition(nameof(WanderState), new Transition(new Condition[] { new FloatCondition(EnemyDistance, Condition.Predicate.LESS_EQUAL, 2.5f) }), nameof(AttackState));
		statemachine.AddTransition(nameof(WanderState), new Transition(new Condition[] { EnemySeenCondition, HealthLowCondition }), nameof(EvadeState));

		statemachine.AddTransition(nameof(EvadeState), new Transition(new Condition[] { AtDestinationCondition }), nameof(IdleState));

		statemachine.AddTransition(nameof(AttackState), new Transition(new Condition[] { AnimationDoneCondition }), nameof(ChaseState));

		statemachine.AddAnyTransition(new Transition(new Condition[] { DeathCondition }), nameof(DeathState));

        statemachine.StartState(nameof(IdleState));
	}

    void Update() {
        Percieved = perception.GetGameObjects();

		// update condition parameters
		EnemySeen.value = (Percieved.Length != 0);
		EnemyDistance.value = (EnemySeen) ? (Vector3.Distance(transform.position, Percieved[0].transform.position)) : float.MaxValue;
		Timer.value -= Time.deltaTime;
		AtDestination.value = ((movement.Destination - transform.position).sqrMagnitude <= 1);
		AnimationDone.value = (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && !animator.IsInTransition(0));

		statemachine.Update();
        if (navigation.targetNode != null) {
            movement.MoveTowards(navigation.targetNode.transform.position);
        }
		animator.SetFloat("Speed", movement.Velocity.magnitude);
    }

	private void OnGUI() {
		Vector3 Point = MainCamera.WorldToScreenPoint(transform.position);
		GUI.backgroundColor = Color.black;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		Rect rect = new Rect(0, 0, 100, 20);
		rect.x = Point.x - (rect.width / 2);
		rect.y = Screen.height - Point.y - rect.height - 20;
		GUI.Label(rect, statemachine.CurrentState.Name);
	}
}