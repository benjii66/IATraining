using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PatternState : AI_States
{
    public override void InitState(AI_Brain _brain)
    {
        base.InitState(_brain);
        OnEnter += () => brain.Movement.SetTarget(brain.Pattern.PickPoint());
        
        OnUpdate += () => brain.Movement.MoveTo();
    }
}
