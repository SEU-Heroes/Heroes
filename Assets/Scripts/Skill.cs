using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Skill
{
    public bool isCreator;
    public Vector3 offset;
    public int AddRage;
    public int aggressivity;

    public Hero useHero;
    public Hero hitHero;

    public int skillId;
    public int heroId;
    public String skillName;
    public int BeforeAT;
    public int AfterAT;
    public bool isChild;

    public int hitCount;//技能击中对面次数

    public delegate void Start(Hero h);
    public delegate void Update(Hero h,float time);
    public delegate void End(Hero h);
    public delegate void Hit(Hero h);

    public Start start;
    public Update update;
    public End end;
    public Hit hit;

    public void setFunc()
    {
        start = SkillScheduler.getStartFunction(heroId,skillId);
        start += delegate(Hero hero)
        {
            useHero = hero;
            setAnimBool(true);
            useHero.nowState = Hero.state.BeforeAT;
        };
        update = SkillScheduler.getUpdateFunction(heroId,skillId);
        end = SkillScheduler.getEndFunction(heroId,skillId);
        end += delegate(Hero hero)
        {
            useHero.nowState = Hero.state.still;
            setAnimBool(false);
        };
        hit = SkillScheduler.getHitFunction(heroId,skillId);
        hit += delegate(Hero hero)
        {
            hero.HPReduce(aggressivity);
        };
    }

    public Skill()
    {

    }

    public Skill(int hId, int sId)
    {
        heroId = hId;
        skillId = sId;
        setFunc();
        //
    }

    public void setAnimBool(bool b)
    {
        useHero.gameObject.GetComponent<Animator>().SetBool(skillName, b);
    }
}