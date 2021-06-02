using UnityEngine;

public class AI_ChaseState : AI_States
{
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		OnUpdate += () => brain.Movement.MoveTo();
	}
}
