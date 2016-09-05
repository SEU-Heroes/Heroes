using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class UseShanXi : Action {

    private Hero hero;

    public Transform target;

    public override void OnStart()
    {
        hero = GetComponent<Hero>();
        target = GameObject.FindGameObjectWithTag(Tags.player01).transform;
    }

    public override TaskStatus OnUpdate()
    {
        hero._isFacingLeft = CheckTowards.IsAtLeft(transform, target) ? false : true;        
        hero.HandSkill(hero._attr._skills.FindSkillById(2));
        return TaskStatus.Success;
    }

}
