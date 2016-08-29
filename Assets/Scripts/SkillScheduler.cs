using UnityEngine;
using System.Collections;

/*
 * 需求：
 * 有所有技能的相应函数
 * 能按照人物和技能ID返回相应的技能函数
 */

/*
 * 版本 V1.0 有旋风腿和跳跃的函数
 */

class SkillScheduler{
    static public Skill.Start getStartFunction(int heroId,int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    case 0:
                        return XuanFengTuiStart;
                    case 1:
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
                        return DefalutUpdate;
                    case 1:
                        return DefalutUpdate;
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
                        return DefalutEnd;
                    case 1:
                        return DefalutEnd;
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
                        return DefalutHit;
                    case 1:
                        return DefalutHit;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    static void ShanXiStart(Hero h)
    {
        h.Move(GameManager._distanceStander,0.1f);
    }


    static void XuanFengTuiStart(Hero h)
    {
        h.Move(4, 1.3f);  
    }

    static void JumpStart(Hero h)
    {
        h.Jump();
        h._nowState = Hero.state.jumping;
    }

    /// <summary>
    /// 缺省的Update函数，无内容
    /// </summary>
    /// <param name="h"></param>
    /// <param name="time"></param>
    /// 作者：胡皓然
    static void DefalutUpdate(Hero h, float time)
    {

    }

    static void DefalutEnd(Hero h)
    {

    }

    static void DefalutHit(Hero h)
    {

    }
}