using System;
using UnityEngine;

public abstract class AI_States : StateMachineBehaviour
{
	#region Events

	public event Action OnEnter = null;
	public event Action OnUpdate = null;
	public event Action OnExit = null; 

	#endregion

	protected AI_Brain brain = null;


	#region Methods

	public virtual void InitState(AI_Brain _brain) => brain = _brain;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnEnter?.Invoke();
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnUpdate?.Invoke();
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke(); 

	#endregion
}
