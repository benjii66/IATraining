using System;
using UnityEngine;

public class AI_Detection : MonoBehaviour
{
	#region Serialize Fields and Events

	public event Action<Vector3> OnTargetDetected = null;
	public event Action OnTargetLost = null;

	[SerializeField] Transform target = null;
	[SerializeField, Range(1, 10)] int detectionRange = 5;

	#endregion

	#region Accesseurs

	public int DetectionRange => detectionRange;
	public bool IsDetected { get; private set; } = false;
	public bool IsAtRange
	{
		get
		{
			if (!IsValid) return false;
			return Vector3.Distance(transform.position, target.position) < detectionRange;
		}
	}
	public bool IsValid => target;

	#endregion


	#region Unity Methods

	private void Awake()
	{
		OnTargetDetected += (point) => IsDetected = false;
		OnTargetLost += () => IsDetected = false;
	}

	private void Update() => UpdateDetection();

	private void OnDestroy()
	{
		OnTargetDetected = null;
		OnTargetLost = null;
	} 

	#endregion

	public void UpdateDetection()
	{
		if (!IsValid) return;
		if (IsAtRange)
			OnTargetDetected?.Invoke(target.position);
		else
			OnTargetLost?.Invoke();
	}


	private void OnDrawGizmos()
	{
		if (IsDetected && IsValid)
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(transform.position, target.position);
		}
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, detectionRange);
	}

}
