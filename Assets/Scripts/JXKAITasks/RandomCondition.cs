using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class RandomCondition : Conditional {

    public int successPercentage;

    public override TaskStatus OnUpdate()
    {
        int random = Random.Range(0, 100);

        if (random < successPercentage)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }

}
