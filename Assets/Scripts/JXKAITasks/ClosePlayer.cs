using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class ClosePlayer : Action {

    private Transform target;
    private Hero hero;
    private int forceExit;

    public override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(Tags.player01).transform;
        hero = GetComponent<Hero>();
    }

    public override TaskStatus OnUpdate()
    {
        forceExit = Random.Range(0, 100);
        if (forceExit > 95)
            return TaskStatus.Success;

        hero.HandDirection(CheckTowards.IsAtLeft(transform, target) ? InputReceiver.joyDir.right : InputReceiver.joyDir.left);
        return TaskStatus.Running;
    }

}
