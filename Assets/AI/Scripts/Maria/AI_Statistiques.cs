using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AI_Statistiques 
{
	public event Action<float> OnPanicUpdate = null;
	public event Action OnResetAI = null;
	[SerializeField] int objectiveAttempt = 0, totalRewards = 0, totalFail = 0, stressLevel = 0, stressPercent = 0;
	[SerializeField, Range(0, 100)] float attemptPercentReset = 20, currentFailProgressAtObjective = 0, globalProgress = 0, globalStressLevel = 0;
	[SerializeField] bool UsePanicLevel = false;

	public float GlobalProgressRatio => (float)totalRewards / totalFail;

	public float PanicValue => 1 + (AIFail / 50);

	public int Stress
	{
		get => stressLevel;

		set
		{
			if(totalFail != 0)
			{
				stressPercent++;
				if (stressPercent == 2)
					stressLevel++;
				stressLevel += (stressPercent / 100);
			}
		}
	}

	public float AIFail => (objectiveAttempt / 50f) * 100;

	public bool AINeedReset => AIFail > attemptPercentReset;

	public void AddReward(int _reward = 1) => totalRewards += _reward;

	public void AddFail(int _fail = 1)
	{
		totalFail += _fail;
		Stress++;
		globalProgress = GlobalProgressRatio;
	}

	public void AddAttempt(int _attempt = 1)
	{
		if (UsePanicLevel)
		{
			globalStressLevel = PanicValue;
			OnPanicUpdate?.Invoke(globalStressLevel);
		}

		objectiveAttempt += _attempt;
		currentFailProgressAtObjective = AIFail;
	}


	public void ResetStats()
	{
		OnPanicUpdate?.Invoke(globalStressLevel);
		OnResetAI?.Invoke();
		globalStressLevel = 1;
		objectiveAttempt = 0;
		currentFailProgressAtObjective = AIFail;
	}

}
