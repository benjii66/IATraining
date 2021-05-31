using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PatternState : AI_States
{
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate(animator, stateInfo, layerIndex);
		OnEnter += () =>
		{
			brain.Movement.SetTarget(brain.Pattern.PickPoint());
			//entre pas de suite dedans

		};
		OnUpdate += () =>
		{
			brain.Movement.MoveTo();
			//il bouge quand il faut
		};
	}
}
