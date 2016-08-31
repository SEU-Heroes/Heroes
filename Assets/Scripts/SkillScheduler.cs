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
                        return JumpStart;
                    case 1:
                        return BackJumpStart;
                    case 2:
                        return LieYanTuXiStart;
                    case 3:
                        return XuanFengTuiStart;
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
                    case 2:
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
                    case 2:
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
                    case 2:
                        return DefalutHit;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    static void LieYanTuXiStart(Hero h)
    {
        h.Move(5,0.1f);
    }


    static void XuanFengTuiStart(Hero h)
    {
        h.Move(3, 1.3f);  
    }

    static void JumpStart(Hero h)
    {
        h.Jump(new Vector2(0,h._jumpForce));
    }

    static void BackJumpStart(Hero h)
    {
        h.Jump(new Vector2((h._isFacingLeft ? 1 : -1) * h._backJumpForce*1.5f, h._backJumpForce));
    }

    static void LieYanTuXi(Hero h)
    {
        
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