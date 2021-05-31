﻿using System;
using UnityEngine;

public class AI_Detection : MonoBehaviour
{
    public event Action<Vector3> OnTargetDetected = null;

    public event Action OnTargetLost = null;

    [SerializeField] Transform target = null;
    [SerializeField, Range(1, 10)] int detectionRange = 5;

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


	private void Awake()
	{
        OnTargetDetected += (point) => IsDetected = false;
        OnTargetLost += () => IsDetected = false;
    }

    public void UpdateDetection()
	{
        if (!IsValid) return;
        if (IsAtRange)
        {
            OnTargetDetected?.Invoke(target.position);
            Debug.LogError("Detected");
        }
        else
        {
            OnTargetLost?.Invoke();
            Debug.LogError("Lost");
        }
    }

	private void Update()
	{
            //target position est ok
           //c'est valide
          //pourquoi ça call le range qu'une fois debuggé ?

        UpdateDetection();
    }

	private void OnDestroy()
	{
        OnTargetDetected = null;
        OnTargetLost = null;
	}


	private void OnDrawGizmos()
	{
		if(IsDetected && IsValid)
		{
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, target.position);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position + Vector3.up, .2f);
	}

}