using UnityEngine;

public class AI_WaitState : AI_States
{
	public override void InitState(AI_Brain _brain)
	{
		base.InitState(_brain);
		OnEnter += () => brain.FSM.SetFloat(brain.StringHelpers.WaitTimer, Random.Range(.1f, 1));
		OnExit += () => brain.FSM.SetBool(brain.StringHelpers.Wait, false);		
	}
}
