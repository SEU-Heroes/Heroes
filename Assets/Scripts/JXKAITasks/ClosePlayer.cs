using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class ClosePlayer : Action {

    private Transform target;
    private Hero hero;

    public override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(Tags.player).transform;
        hero = GetComponent<Hero>();
    }

    public override TaskStatus OnUpdate()
    {
        hero.HandDirection(CheckTowards.IsAtLeft(transform, target) ? InputReceiver.joyDir.right : InputReceiver.joyDir.left);
        return TaskStatus.Running;
    }

}
