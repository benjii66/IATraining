using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AI_WaypointSystem))]
public class AI_WaypointSystemEditor : Editor
{
	AI_WaypointSystem pattern = null;
	SerializedProperty points = null;


	private void OnEnable()
	{
		pattern = (AI_WaypointSystem)target;
		points = serializedObject.FindProperty("waypoints");
	}


	private void OnSceneGUI()
	{
		if (points == null) return;
		serializedObject.Update();
		for (int i = 0; i < points.arraySize; i++)
		{
			points.GetArrayElementAtIndex(i).vector3Value = Handles.DoPositionHandle(points.GetArrayElementAtIndex(i).vector3Value, Quaternion.identity);
			serializedObject.ApplyModifiedProperties();
		}
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Add point"))
		pattern.AddPoint();

		if (GUILayout.Button("Remove point"))
		pattern.Clear();
	}
}
