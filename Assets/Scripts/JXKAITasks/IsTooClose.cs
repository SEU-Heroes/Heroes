using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class IsTooClose : Conditional {

    public Transform target;

    private float sqrFleeDistance = 25f;

    public override void OnStart()
    {
        // 此处应在后期加上异常处理
        target = GameObject.FindGameObjectWithTag(Tags.player01).transform;
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
