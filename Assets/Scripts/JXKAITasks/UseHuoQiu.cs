using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;

public class UseHuoQiu : Action {

    private Hero hero;

    public Transform target;

    public override void OnStart()
    {
        hero = GetComponent<Hero>();
        target = GameObject.FindGameObjectWithTag(Tags.player01).transform;
    }

    public override TaskStatus OnUpdate()
    {
        if(GameObject.FindGameObjectsWithTag(Tags.fireball).Length == 0)
        {
            hero._isFacingLeft = CheckTowards.IsAtLeft(transform, target) ? false : true;
            hero.HandSkill(hero._attr._skills.FindSkillByName("HuoQiu"));
        }
        if (hero.IsSkillable() == 1)
        {
            hero.HandSkill(hero._attr._skills.FindSkillByName("HuoYanZhangKong"));
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

}
