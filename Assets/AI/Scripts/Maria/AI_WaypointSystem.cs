using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_WaypointSystem : MonoBehaviour
{
	[SerializeField] List<Vector3> wayPoint = new List<Vector3>();
	[SerializeField] int indexPoint = 1;
	[SerializeField] Color cubeColor = Color.white;
	[SerializeField] Color wireColor = Color.white;

	public Vector3 PickPoint()
	{
		indexPoint++;
		indexPoint %= wayPoint.Count;
		return wayPoint[indexPoint];
	}

	public void AddPoint()
	{
		Vector3 _point = wayPoint.Count == 0 ? Vector3.zero : wayPoint[wayPoint.Count - 1] + Vector3.forward;
		wayPoint.Add(_point);
	}

	public void Clear() => wayPoint.Clear();

	private void OnDrawGizmos()
	{
		for (int i = 0; i < wayPoint.Count; i++)
		{
			Gizmos.color = wireColor;
			if (i < wayPoint.Count - 1) Gizmos.DrawLine(wayPoint[i], wayPoint[i + 1]);
			Gizmos.color = cubeColor;
			Gizmos.DrawWireCube(wayPoint[i],Vector3.one);
		}
	}
}
