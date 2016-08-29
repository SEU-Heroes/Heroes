using UnityEngine;
using System.Collections;

/*
 * 需求：
 * 能存放一切角色的属性
 */

/*
 * 版本 V1.0 有角色的基础属性和通用属性
 */

class HeroAttr{
    //角色固定信息
    public string _name;//角色名字
    public int _heroId;//角色ID
    public float _jump;//角色跳跃力
    public int _move;//角色移动力
    public float _height;//角色身高
    public SkillTree _skills;//角色技能树
    
    //当前属性值
    public int _HP = 1024;//当前HP
    public int _fullRage;//已满的气力值条数
    public int _Rage;//当前气力值

    public HeroAttr(string name, int id, float jump, int move, float height, SkillTree skills)
    {
        this._name = name;
        this._heroId = id;
        this._jump = jump;
        this._move = move;
        this._height = height;
        this._skills = skills;
    }

    public HeroAttr()
    {
        // TODO: Complete member initialization
    }
}
