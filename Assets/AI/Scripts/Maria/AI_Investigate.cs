using System.Collections.Generic;
using UnityEngine;

public class AI_Investigate : MonoBehaviour
{

	[SerializeField, Range(1, 10)] float investigationRadius = 5;
	float initRadius = 5;
	[SerializeField] Color investigationRadiusColor = Color.white;
	[SerializeField] Transform player = null;
	Vector3 investigationStartPosition = Vector3.zero, investigationPoint = Vector3.zero;
	[SerializeField] bool isPlayerSet = false;
	[SerializeField] bool allowGizmos = true;
	List<Vector3> historicPoints = new List<Vector3>();

	public bool IsPlayerSet => isPlayerSet;
	public void SetLastSeenTarget(Vector3 _lastPoint)
	{
		investigationRadius = initRadius;
		historicPoints.Clear();
		investigationStartPosition = _lastPoint;
	}

	private void Awake()
	{
		initRadius = investigationRadius;
	}

	public bool SetPlayerSet(bool _value) => isPlayerSet = _value;
	public bool SetGizmo(bool _value) => allowGizmos= _value;
	public void SetRadiusMultiplicator(float _multiplicator) => investigationRadius = initRadius * _multiplicator;

	public void AddRange(int _value) => investigationRadius += _value;

	public Vector3 GetInvestigationPoint()
	{
		if (!isPlayerSet)
		{
			investigationStartPosition = player.position;
			isPlayerSet = true;
		}

		int _angle = Random.Range(0, 360);
		float _x = Mathf.Cos(_angle * Mathf.Deg2Rad) * investigationRadius;
		float _y = 0.5f;
		float _z = Mathf.Sin(_angle * Mathf.Deg2Rad) * investigationRadius;
		investigationPoint = new Vector3(_x, _y, _z) + investigationStartPosition;
		historicPoints.Add(investigationPoint);
		return investigationPoint;
	}

	private void OnDrawGizmos()
	{
		if (allowGizmos)
		{

			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube(investigationStartPosition, Vector3.one);
			Gizmos.DrawLine(transform.position, investigationStartPosition);
			Gizmos.color = investigationRadiusColor;
			Gizmos.DrawWireSphere(investigationStartPosition, investigationRadius);
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(investigationPoint, Vector3.one);
			Gizmos.DrawLine(transform.position, investigationPoint);

			for (int i = 0; i < historicPoints.Count; i++)
			{
				Gizmos.color = Color.Lerp(Color.red, Color.green, (float)i / historicPoints.Count);
				Gizmos.DrawWireSphere(historicPoints[i], .1f);
				Gizmos.DrawLine(historicPoints[i], Vector3.one);
			}
		}
	}
}
