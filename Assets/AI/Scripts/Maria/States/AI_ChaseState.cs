using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ChaseState : AI_States
{
	public override void InitState(AI_Brain _brain)
	{
		base.InitState(_brain);
		//OnExit += () => brain.Statistiques.AddReward();
	}


	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		OnUpdate += () => brain.Movement.MoveTo();
	}
}
