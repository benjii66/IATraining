using System;
using UnityEngine;

[Serializable]
public class Helpers_Nommage : MonoBehaviour
{
    [SerializeField] string waitTimer = "Wait_Timer";
    [SerializeField] string wait = "Wait";
    [SerializeField] string chaseParameter = "Chase_Target";
    [SerializeField] string patternParameter = "Follow_Pattern";

    public string WaitTimer => waitTimer;
    public string Wait=> wait;
    public string ChaseParameter => chaseParameter;
    public string PatternParameter => patternParameter;

}
