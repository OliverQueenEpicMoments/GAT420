using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentController : MonoBehaviour {
    [System.Serializable]
    public struct NavLocation {
        public string Name;
		public NavNode navNode;
    }

    [SerializeField] NavAgent navAgent;
    [SerializeField] NavLocation[] navLocations;

    void Start() {
		for (int i = 0; i < navLocations.Length; i++) {
            navLocations[i].navNode.name = navLocations[i].Name;
		}
	}

	private void OnGUI() {
        for (int i = 0; i < navLocations.Length; i++) {
            if (GUI.Button(new Rect(10, 10 + (i * 25), 100, 20), navLocations[i].Name)) {
                navAgent.SetDestination(navLocations[i].navNode);
            }
		}
	}
}