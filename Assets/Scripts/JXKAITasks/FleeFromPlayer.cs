using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class FleeFromPlayer : Action
{
    public Transform target;

    public int speed = 3;

    private float sqrFleeDistance = 25f;
    private Hero hero;

    private int forceExit = 0; // 强制退出移动操作

    public override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(Tags.player01).transform;
        hero = GetComponent<Hero>();
    }

    public override TaskStatus OnUpdate()
    {
        if (target == null)
            return TaskStatus.Failure;

        forceExit = Random.Range(0, 100);

        if(Vector3.SqrMagnitude(target.position - transform.position) > sqrFleeDistance || forceExit > 95)
        {
            hero._isFacingLeft = CheckTowards.IsAtLeft(transform, target) ? false : true;
            return TaskStatus.Success;
        }

        if (Mathf.Abs(transform.position.x) < 8)
        {
            if (hero.IsMoveable())
            {
                hero.HandDirection(CheckTowards.IsAtLeft(transform, target) ? InputReceiver.joyDir.left : InputReceiver.joyDir.right);
                hero._isFacingLeft = CheckTowards.IsAtLeft(transform, target) ? true : false;
            }
        }   


        return TaskStatus.Running;
    }


}
