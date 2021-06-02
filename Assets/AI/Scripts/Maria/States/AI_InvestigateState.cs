using UnityEngine;

public class AI_InvestigateState : AI_States
{
	public override void InitState(AI_Brain _brain)
	{
		base.InitState(_brain);
		OnEnter += () => Investigation();
		OnUpdate += () => brain.Movement.MoveTo();
		OnExit += () => Reset();
	}

	#region Methods

	void Investigation()
	{
		Vector3 _point = ((AI_Brain)brain).Investigate.GetInvestigationPoint();
		brain.Movement.SetTarget(_point);
		brain.Statistiques.AddAttempt();
	}
	void Reset()
	{
		if (brain.Statistiques.AINeedReset)
		{
			((AI_Brain)brain).Investigate.AddRange(brain.Statistiques.Stress);
			brain.Movement.AddSpeed((float)brain.Statistiques.Stress);
			brain.Statistiques.AddFail();
			brain.Statistiques.ResetStats();
			brain.FSM.SetBool("Search", false);
			brain.Investigate.SetPlayerSet(false);
		}
	} 

	#endregion
}
