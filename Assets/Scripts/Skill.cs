using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 需求：
 * 有技能的4个函数来处理人物状态：开始，更新，结束，命中
 */

class Skill
{
    //技能属性
    public bool _isCreator;//是否会创造一个物体
    public Vector3 _offset;//技能生成物体相对于角色的位置
    public int _AddRage;///技能命中后会增加的怒气值
    public int _aggressivity;//技能的伤害值
    public int _skillId;//技能ID
    public int _heroId;//技能所属角色ID
    public String _skillName;//技能名字
    public int _BeforeAT;//前摇时间
    public int _AfterATFirst;//后摇前半段时间
    public int _AfterATLast;//后摇后半段时间
    public int _Time;//技能发动的时间
    public bool _isChild;//生成的技能物体是否是人物的子物体
    
    public Hero _useHero;//使用技能的角色
    public Hero _hitHero;//被击中的角色
    public int _hitCount;//技能击中对面次数

    //4个技能委托声明
    public delegate void Start(Hero h);
    public delegate void Update(Hero h,float time);
    public delegate void End(Hero h);
    public delegate void Hit(Hero h);

    //4个技能委托
    public Start _start;
    public Update _update;
    public End _end;
    public Hit _hit;

    /// <summary>
    /// 设置技能的四个function
    /// </summary>
    /// 作者：胡皓然
    public void SetFunc()
    {
        _start = delegate(Hero hero)
        {
            _useHero = hero;
            SetAnimBool(true);
            _useHero._nowState = Hero.state.BeforeAT;

            //生成技能物体
            GameObject o = GameManager.GetInstance().Instantiate(GameManager._factory.GetSkillObject(hero._attr._heroId, _skillId), hero.transform.localPosition + _offset, Quaternion.identity);
            o.transform.Rotate(hero._isFacingLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0));

            //判断是否将技能物体作为角色的子物体
            if (_isChild)
            {
                o.transform.parent = hero.transform;
            }
            o.GetComponent<Trigger>().skill = this;
        };
        _start += SkillScheduler.getStartFunction(_heroId,_skillId);
        _update = delegate(Hero hero, float time)
        {
            if (time > _AfterATFirst)
            {
                hero._nowState = Hero.state.LastHalfAfterAT;
            }
            else if (time > _BeforeAT)
            {
                hero._nowState = Hero.state.FirstHalfAfterAT;
            }
        };
        _update += SkillScheduler.getUpdateFunction(_heroId,_skillId);
        _end = SkillScheduler.getEndFunction(_heroId,_skillId);
        _end += delegate(Hero hero)
        {
            _useHero._nowState = Hero.state.still;
            SetAnimBool(false);
        };
        _hit = SkillScheduler.getHitFunction(_heroId,_skillId);
        _hit += delegate(Hero hero)
        {
            hero.HPReduce(_aggressivity);
        };
    }

    public Skill()
    {

    }

    public Skill(int hId, int sId)
    {
        _heroId = hId;
        _skillId = sId;
        SetFunc();
        //
    }

    //设置技能的动画状态
    public void SetAnimBool(bool b)
    {
        _useHero.gameObject.GetComponent<Animator>().SetBool(_skillName, b);
    }
}