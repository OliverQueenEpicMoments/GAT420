using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutonomousAgentData", menuName = "AI/AutonomousAgentData")] 
public class AutonomousAgentData : ScriptableObject {
	[Header("Wander")]
	[Range(0, 20)] public float WanderDistance = 4;
	[Range(0, 20)] public float WanderRadius = 4;
	[Range(0, 20)] public float WanderDisplacement = 4;
	[Header("Flocking")]
	[Range(0, 20)] public float SeparationRadius = 1;
	[Header("Weights")]
	[Range(0, 5)] public float SeekWeight = 1;
	[Range(0, 5)] public float FleeWeight = 1;
	[Range(0, 5)] public float CohesionWeight = 1;
	[Range(0, 5)] public float SeparationWeight = 1;
	[Range(0, 5)] public float AlignmentWeight = 1;
}