using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class StateMachine {
	public State CurrentState { get; set; }

	private Dictionary<State, List<KeyValuePair<Transition, State>>>StateTransitions = new Dictionary<State, List<KeyValuePair<Transition, State>>>();
	private Dictionary<string, State> States = new Dictionary<string, State>();

	private List<KeyValuePair<Transition, State>> AnyTransitions = new List<KeyValuePair<Transition, State>>();

	public void Update() {
		// Check state transitions
		// Get transitions for current state
		if (StateTransitions.TryGetValue(CurrentState, out var transitions)) {
			// Check all transtions
			foreach (var transition in transitions)	{
				// Check for transition to new state
				if (transition.Key.ToTransition()) {
					StartState(transition.Value.Name);
					break;
				}
			}
		}
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

	public void AddTransition(string stateFrom, Transition transition, string stateTo) {
		if (!StateTransitions.ContainsKey(GetStateFromName(stateFrom))) {
			StateTransitions[GetStateFromName(stateFrom)] = new List<KeyValuePair<Transition, State>>();
		}
		StateTransitions[GetStateFromName(stateFrom)].Add(new KeyValuePair<Transition, State>(transition, GetStateFromName(stateTo)));
	}

	public void AddAnyTransition(Transition transition, string stateTo) {
		AnyTransitions.Add(new KeyValuePair<Transition, State>(transition, GetStateFromName(stateTo)));
	}

	public State GetStateFromName(string name) {
		foreach (var state in States) {
			if (string.Equals(state.Key, name, System.StringComparison.OrdinalIgnoreCase)) {
				return state.Value;
			}
		}
		return null;
	}
}