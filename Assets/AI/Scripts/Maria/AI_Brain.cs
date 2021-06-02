using UnityEngine;

public class AI_Brain : MonoBehaviour
{
	#region Serialize Fields

	[Header("Scripts")]
	[SerializeField] Animator fsm = null;
	[SerializeField] AI_Movement movement = null;
	[SerializeField] AI_Detection detection = null;
	[SerializeField] AI_WaypointSystem pattern = null;
	[SerializeField] AI_Investigate investigate = null;
	[SerializeField] P_Player player = null;
	[SerializeField] AI_Statistiques statistiques = new AI_Statistiques();


	[Header("Parameters")]
	[SerializeField] Helpers_Nommage stringHelpers = null;
	[SerializeField] bool hasChased = false;

	[Header("States")]
	[SerializeField] AI_ChaseState chaseState = null;
	[SerializeField] AI_PatternState patternState = null;
	[SerializeField] AI_WaitState waitState = null;
	[SerializeField] AI_InvestigateState investigateState = null;


	#endregion

	public bool IsValid => fsm && movement && detection && pattern && player && chaseState && patternState && waitState && investigateState;

	#region Accessors

	public Animator FSM => fsm;
	public AI_Movement Movement => movement;
	public AI_Detection Detection => detection;
	public AI_WaypointSystem Pattern => pattern;
	public Helpers_Nommage StringHelpers => stringHelpers;
	public AI_Statistiques Statistiques => statistiques;
	public AI_Investigate Investigate => investigate;
	public bool HasChased => hasChased;

	#endregion


	void Start() => InitFSM();

	private void Update() => UpdateBrain();

	void InitFSM()
	{
		GetAllComponents();
		GetStatesComponents();
		if (!fsm) return;

		AI_States[] _states = fsm.GetBehaviours<AI_States>();

		for (int i = 0; i < _states.Length; i++)
			_states[i].InitState(this);

		if (!IsValid) return;

		DetectionEvent();
		movement.OnPositionReached += () =>	fsm.SetBool(stringHelpers.Wait, true);		
	}
	void DetectionEvent()
	{
		detection.OnTargetDetected += (position) => TargetDetected(position);
		detection.OnTargetLost += () => TargetLost();
	}		
	void TargetDetected(Vector3 _position)
	{
		movement.SetTarget(_position);
		fsm.SetBool(stringHelpers.ChaseParameter, true);
		fsm.SetBool(stringHelpers.PatternParameter, false);
		hasChased = true;
	}
	void TargetLost()
	{
		fsm.SetBool(stringHelpers.ChaseParameter, false);
		if (!hasChased)
			fsm.SetBool(stringHelpers.PatternParameter, true);
		else
		{
			investigate.SetGizmo(true);
			fsm.SetBool("Search", true);
		}
		
		if (statistiques.AINeedReset)
		{
			fsm.SetBool("Search", false);
			fsm.SetBool(stringHelpers.PatternParameter, true);
			investigate.SetGizmo(false);
		}
	}
	void GetAllComponents()
	{
		movement = GetComponent<AI_Movement>();
		detection = GetComponent<AI_Detection>();
		pattern = GetComponent<AI_WaypointSystem>();
		investigate = GetComponent<AI_Investigate>();
	}
	void GetStatesComponents()
	{
		chaseState = fsm.GetBehaviour<AI_ChaseState>();
		waitState = fsm.GetBehaviour<AI_WaitState>();
		patternState = fsm.GetBehaviour<AI_PatternState>();
		investigateState = fsm.GetBehaviour<AI_InvestigateState>();
	}
	void UpdateBrain()
	{
		if (!IsValid) return;
		detection.UpdateDetection();
	}
}
