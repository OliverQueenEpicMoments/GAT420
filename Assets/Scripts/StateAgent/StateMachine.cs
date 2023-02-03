using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {
	public State CurrentState { get; set; }

	private Dictionary<string, State> States = new Dictionary<string, State>();

	public void Update() {
		CurrentState?.OnUpdate();
	}

	public void StartState(string name)	{
		State NewState = States[name];
		if (NewState == null || NewState == CurrentState) return;

		CurrentState?.OnExit();
		CurrentState = NewState;
		CurrentState?.OnEnter();
	}

	public void AddState(State state) {
		if (States.ContainsKey(state.Name)) return;
		States[state.Name] = state;
	}
}