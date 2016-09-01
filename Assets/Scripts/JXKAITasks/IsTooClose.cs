using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class IsTooClose : Conditional {

    public Transform target;

    private float sqrFleeDistance = 25f;

    public override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(target.position - transform.position) < sqrFleeDistance)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
