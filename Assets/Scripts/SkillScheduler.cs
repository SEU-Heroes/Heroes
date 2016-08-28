using UnityEngine;
using System.Collections;

class SkillScheduler{
    static public Skill.Start getStartFunction(int heroId,int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    case 0:
                        Debug.Log("ShanXiStart");
                        return ShanXiStart;
                    case 1:
                        Debug.Log("JumpStart");
                        return JumpStart;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    static public Skill.Update getUpdateFunction(int heroId, int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    case 0:
                        return ShanXiUpdate;
                    case 1:
                        return JumpUpdate;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    static public Skill.End getEndFunction(int heroId, int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    case 0:
                        return ShanXiEnd;
                    case 1:
                        return JumpEnd;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    static public Skill.Hit getHitFunction(int heroId, int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    case 0:
                        return ShanXiHit;
                    case 1:
                        return JumpHit;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    static void ShanXiStart(Hero h)
    {
        h.move(GameManager.getInstance().distanceStander,0);
    }

    static void ShanXiUpdate(Hero h, float time)
    {

    }

    static void ShanXiEnd(Hero h)
    {

    }

    static void ShanXiHit(Hero h)
    {

    }

    static void XuanFengTuiStart(Hero h)
    {
        h.move(5, 1.3f);
    }

    static void XuanFengTuiUpdate(Hero h, float time)
    {

    }

    static void XuanFengTuiEnd(Hero h)
    {

    }

    static void XuanFengTuiHit(Hero h)
    {

    }

    static void JumpStart(Hero h)
    {
        h.Jump();
    }

    static void JumpUpdate(Hero h, float time)
    {

    }

    static void JumpEnd(Hero h)
    {

    }

    static void JumpHit(Hero h)
    {

    }
}