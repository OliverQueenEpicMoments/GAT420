using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAgent : Agent {
    public StateMachine statemachine = new StateMachine();
    public GameObject[] Percieved;
    public Camera MainCamera;

    void Start() {
        MainCamera = Camera.main;

        statemachine.AddState(new IdleState(this));
        statemachine.AddState(new PatrolState(this));
        statemachine.AddState(new ChaseState(this));
        statemachine.AddState(new WanderState(this));
        statemachine.AddState(new AttackState(this));
        statemachine.StartState(nameof(IdleState));
    }

    void Update() {
        Percieved = perception.GetGameObjects();
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