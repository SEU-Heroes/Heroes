using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Skill
{
    public bool isCreator;
    public int skillId;
    public Vector3 offset;
    public int AddRage;
    public int aggressivity;

    public Hero useHero;
    public Hero hitHero;

    public delegate void Start(Hero h);
    public delegate void Update(Hero h,float time);
    public delegate void End(Hero h);
    public delegate void Hit(Hero h);

    public void setFunc(Start s, Update u, End e)
    {
        start = s;
        update = u;
        end = e;
    }

    public Start start;
    public Update update;
    public End end;

    public Skill(Start s, Update u, End e)
    {
        setFunc(s, u, e);
    }

    public void hit(Hero h)
    {
        hitHero = h;
        useHero.hit(hitHero);
    }
}