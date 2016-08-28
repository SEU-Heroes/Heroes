using UnityEngine;
using System.Collections;

class HeroAttr{

    static public int maxRage = 1000;
    static public int defenseForce = 32; 
    static public int maxHP = 1024;

    public string name;
    public int heroId;
    public float jump;
    public int move;
    public float height;
    public SkillTree skills;
 
    public int HP = 1024;
    public int fullRage;//已满的气力值条数
    public int Rage;//当前气力值

    public HeroAttr(string name, int id, float jump, int move, float height, SkillTree skills)
    {
        this.name = name;
        this.heroId = id;
        this.jump = jump;
        this.move = move;
        this.height = height;
        this.skills = skills;
    }

    public HeroAttr()
    {
        // TODO: Complete member initialization
    }
}
