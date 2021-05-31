using UnityEngine;

public class AI_Brain : MonoBehaviour
{
	#region Serialize Fields

	[Header("Scripts")]
	[SerializeField] Animator fsm = null;
	[SerializeField] AI_Movement movement = null;
	[SerializeField] AI_Detection detection = null;
	[SerializeField] AI_WaypointSystem pattern = null;
	[SerializeField] P_Player player = null;


	[Header("Parameters")]
	[SerializeField] Helpers_Nommage stringHelpers = null; 

	[Header("States")]
	[SerializeField] AI_ChaseState chaseState = null;
	[SerializeField] AI_PatternState patternState = null;
	[SerializeField] AI_WaitState waitState = null;
	

	#endregion

	public bool IsValid => fsm && movement && detection && pattern && player && chaseState && patternState && waitState;

	#region Accessors

	public Animator FSM => fsm;

	public AI_Movement Movement => movement;

	public AI_Detection Detection => detection;

	public AI_WaypointSystem Pattern => pattern;

	public Helpers_Nommage StringHelpers => stringHelpers;

	#endregion


	void Start()=> InitFSM();

	private void Update()
	{
		UpdateBrain();
	}

	void InitFSM()
	{
		GetAllComponents();
		GetStatesComponents();
		if (!fsm) return;
		fsm.SetBool("Follow_Pattern", true);
		
		AI_States[] _states = fsm.GetBehaviours<AI_States>();
		for (int i = 0; i < _states.Length; i++)
			_states[i].InitState(this);		

		if (!IsValid) return;

		detection.OnTargetDetected += (position) =>
		{
			movement.SetTarget(position);
			fsm.SetBool("Follow_Pattern", false);
			Debug.Log($"Detected :{fsm.GetBool("Follow_Pattern")}");
			Debug.LogError("plus de pattern");
		};

		detection.OnTargetLost += () =>
		{
			fsm.SetBool("Follow_Pattern", true);
			Debug.Log($"Non Detected :{fsm.GetBool("Follow_Pattern")}");
			Debug.LogError("On pattern after the lost of the player");
		};

		movement.OnPositionReached += () =>
		{
			fsm.SetBool("Wait", true);
		};
	}

	void GetAllComponents()
	{
		movement = GetComponent<AI_Movement>();
		detection = GetComponent<AI_Detection>();
		pattern = GetComponent<AI_WaypointSystem>();
	}

	void GetStatesComponents()
	{
		chaseState = fsm.GetBehaviour<AI_ChaseState>();
		waitState = fsm.GetBehaviour<AI_WaitState>();
		patternState = fsm.GetBehaviour<AI_PatternState>();
	}

	void UpdateBrain()
	{
		if (!IsValid) return;
		detection.UpdateDetection();
	}

}
