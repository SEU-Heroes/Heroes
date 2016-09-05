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
    public int _addRage;///技能命中后会增加的怒气值
    public int _aggressivity;//技能的伤害值
    public int _skillId;//技能ID
    public int _heroId;//技能所属角色ID
    public String _skillName;//技能名字
    public float _beforeAT;//前摇时间
    public float _afterATFirst;//后摇前半段时间
    public float _afterATLast;//后摇后半段时间
    public float _time;//技能发动的时间
    public bool _isChild;//生成的技能物体是否是人物的子物体
    public float _existTime;//生成物体的存在时间
    public float _hitAddRage;//打中后被攻击的角色增加的怒气值
    
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

    public GameObject _instantiation;

    private bool _skillHasStarted = false;


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
            _useHero.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            if (_instantiation == null)
            {
                // 根据角色朝向初始化生成物体的偏移
                Vector3 realOffset = new Vector3((hero._isFacingLeft ? -1 : 1) * _offset.x, _offset.y, 0);
                //生成技能物体
                _instantiation = GameManager.GetInstance().Instantiate(hero._skillCreator[_skillId], hero.transform.localPosition + realOffset, Quaternion.identity);
                _instantiation.transform.Rotate(hero._isFacingLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0));
                if (_instantiation.GetComponent<Trigger>() != null)
                {
                    _instantiation.GetComponent<Trigger>().existTime = _existTime;
                    _instantiation.GetComponent<Trigger>().skill = this;
                    _instantiation.GetComponent<Trigger>().hero = this._useHero;
                }
                else if (_instantiation.GetComponent<TriggerAndDestroy>() != null)
                {
                    _instantiation.GetComponent<TriggerAndDestroy>().existTime = _existTime;
                    _instantiation.GetComponent<TriggerAndDestroy>().skill = this;
                    _instantiation.GetComponent<TriggerAndDestroy>().hero = this._useHero;
                }
                else if (_instantiation.GetComponent<TianFengHuoWuBorner>() != null)
                {
                    _instantiation.GetComponent<TianFengHuoWuBorner>().existTime = _existTime;
                    _instantiation.GetComponent<TianFengHuoWuBorner>().skill = this;
                    _instantiation.GetComponent<TianFengHuoWuBorner>().hero = this._useHero;
                }

                //判断是否将技能物体作为角色的子物体
                if (_isChild)
                {
                    _instantiation.transform.parent = hero.transform;
                }
                else
                {
                    _instantiation = null;
                }
            }
        };
        _start += SkillScheduler.GetBeforeATFunction(_heroId, _skillId);
        _update = delegate(Hero hero, float time)
        {
            if (time > _time + _beforeAT + _afterATFirst + _afterATLast)
            {
                if (!_skillHasStarted)
                {
                    _skillHasStarted = true;
                    SkillScheduler.GetStartFunction(_heroId, _skillId)(hero);
                }
                _end(hero);
                hero._nowState = Hero.state.still;
            }
            else if (time > _afterATFirst+_time+_beforeAT)
            {
                if (!_skillHasStarted)
                {
                    _skillHasStarted = true;
                    SkillScheduler.GetStartFunction(_heroId, _skillId)(hero);
                }
                hero._nowState = Hero.state.LastHalfAfterAT;
            }
            else if (time > _beforeAT +_time)
            {
                SetAnimBool(false);
                if (!_skillHasStarted)
                {
                    _skillHasStarted = true;
                    SkillScheduler.GetStartFunction(_heroId, _skillId)(hero);
                }
                hero._nowState = Hero.state.FirstHalfAfterAT;
            }
            else if (time > _beforeAT)
            {
                if (!_skillHasStarted)
                {
                    _skillHasStarted = true;
                    SkillScheduler.GetStartFunction(_heroId, _skillId)(hero);
                }
                hero._nowState = Hero.state.acting;
            }
        };
        _update += SkillScheduler.GetUpdateFunction(_heroId,_skillId);
        _end = SkillScheduler.GetEndFunction(_heroId,_skillId);
        _end += delegate(Hero hero)
        {
            _useHero.GetComponent<Rigidbody2D>().gravityScale = 5;
            SetAnimBool(false);
            _skillHasStarted = false;
        };
        _hit = SkillScheduler.GetHitFunction(_heroId,_skillId);
        _hit += delegate(Hero hero)
        {
            _useHero.RageAdd(_addRage);
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
    }

    //设置技能的动画状态
    public void SetAnimBool(bool b)
    {
        _useHero.gameObject.GetComponent<Animator>().SetBool(_skillName, b);
    }
}