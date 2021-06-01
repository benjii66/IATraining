using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AI_Brain),true)]
public class AI_BrainEditor : Editor
{
   AI_Brain brain = null;
	SerializedProperty stats = null;

	private void OnEnable()
	{
		brain = (AI_Brain)target;
		stats = serializedObject.FindProperty("statistiques");
	}

	private void OnSceneGUI()
	{
		Handles.BeginGUI();
		GUILayout.Window(0, new Rect(Screen.width * .01f, Screen.height * .1f, Screen.width * .4f, Screen.height * .3f), BrainDataWindow, "Brain Data");
		Handles.EndGUI();
	}

	void BrainDataWindow(int _id)
	{
		if (stats == null) return;
		serializedObject.Update();
		GUILayout.Label($"All Rewards = {stats.FindPropertyRelative("totalRewards").intValue}");
		GUILayout.Label($"All Fail = {stats.FindPropertyRelative("totalFail").intValue}");
		SerializedProperty _failPercent = stats.FindPropertyRelative("attemptPercentReset");
		GUILayout.Label($"Fail Reset % = {_failPercent.floatValue}");
		_failPercent.floatValue = GUILayout.HorizontalSlider(_failPercent.floatValue, 0, 100);
		GUILayout.Space(9);
		GUILayout.Label($"Attempts = {stats.FindPropertyRelative("objectiveAttempt").intValue}");
		GUILayout.Label($"Panic level = {stats.FindPropertyRelative("globalStressLevel").floatValue}");
		serializedObject.ApplyModifiedProperties();
		GUILayout.Space(9);
		GetFSMStatus();
	}

	void GetFSMStatus()
	{
		GUILayout.Toggle(brain.FSM.GetBool("Search"), "Search Bool");
		GUILayout.Toggle(brain.FSM.GetBool("Follow_Pattern"), "Pattern Bool");
		GUILayout.Toggle(brain.FSM.GetBool("Chase_Target"), "Chase Bool");
		GUILayout.Label($"Waiting Time : {brain.FSM.GetFloat("Wait_Timer").ToString()}");
		GUILayout.Label($"Target : {brain.Movement.TargetPosition.ToString()}");
	}

	GUIStyle GetBox(Color _textColor)
	{
		GUIStyle _box = new GUIStyle(GUI.skin.box);
		_box.normal.textColor = _textColor;
		_box.fontStyle = FontStyle.Bold;
		return _box;
	}
	GUIStyle GetLabel(Color _textColor)
	{
		GUIStyle _label = new GUIStyle(GUI.skin.label);
		_label.normal.textColor = _textColor;
		return _label;
	}

}

