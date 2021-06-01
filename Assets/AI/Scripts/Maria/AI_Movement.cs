using System;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    public event Action OnPositionReached = null;
    [SerializeField] Vector3 targetPosition = Vector3.one;
    [SerializeField, Range(0, 10)] float atPosRange = 2;
    [SerializeField, Range(0, 100)] float speed = 2;

    public bool IsAtPos => Vector3.Distance(targetPosition, transform.position) < atPosRange;

    public void SetTarget(Vector3 _target) => targetPosition = _target;

    public Vector3 TargetPosition => targetPosition;

    public void MoveTo()
	{
        float _step = speed * Time.deltaTime;
        if(IsAtPos)
		{
            OnPositionReached?.Invoke();
            return;
		}
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _step);
        transform.LookAt(targetPosition);
	}

    public void AddSpeed(float _speed) => speed += _speed;

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPosition, 1f);
    }

}
