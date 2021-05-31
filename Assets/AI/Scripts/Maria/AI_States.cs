using System;
using UnityEngine;

public abstract class AI_States : StateMachineBehaviour
{
    public event Action OnEnter = null;
    public event Action OnUpdate = null;
    public event Action OnExit = null;
    protected AI_Brain brain = null;


    public virtual void InitState(AI_Brain _brain) => brain = _brain;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnEnter?.Invoke();
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnUpdate?.Invoke();
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => OnExit?.Invoke();
}
