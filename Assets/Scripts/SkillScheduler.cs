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

class SkillScheduler : MonoBehaviour{
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
                        return ShanXiStart;
                    case 3:
                        return XuanFengTuiStart;
                    case 4:
                        return HuoQiuStart;
                    case 5:
                        return HuoYanZhangKongStart;
                    case 6:
                        return TianFengHuoWuStart;
                    default:
                        return null;
                }
            case 1:
                switch (skillId)
                {
                    case 0:
                        return ZhenDangShaStart;
                    case 1:
                        return XunMengTuJiStart;
                    case 2:
                        return KuaiSuZhuaQuStart;
                    case 3:
                        return XueBaoStart;
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
                    case 3:
                        return DefalutUpdate;
                    case 4:
                        return DefalutUpdate;
                    case 5:
                        return DefalutUpdate;
                    case 6:
                        return DefalutUpdate;
                    default:
                        return null;
                }
            case 1:
                switch (skillId)
                {
                    case 0:
                        return DefalutUpdate;
                    case 1:
                        return DefalutUpdate;
                    case 2:
                        return DefalutUpdate;
                    case 3:
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
                    case 3:
                        return DefalutEnd;
                    case 4:
                        return DefalutEnd;
                    case 5:
                        return DefalutEnd;
                    case 6:
                        return DefalutEnd;
                    default:
                        return null;
                }
            case 1:
                switch (skillId)
                {
                    case 0:
                        return DefalutEnd;
                    case 1:
                        return DefalutEnd;
                    case 2:
                        return DefalutEnd;
                    case 3:
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
                        return ShanXiHit;
                    case 3:
                        return XuanFengTuiHit;
                    case 4:
                        return HuoQiuHit;
                    case 5:
                        return DefalutHit;
                    case 6:
                        return DefalutHit;
                    default:
                        return null;
                }
            case 1:
                switch (skillId)
                {
                    case 0:
                        return ZhenDangShaHit;
                    case 1:
                        return XunMengTuJiHit;
                    case 2:
                        return KuaiSuZhuaQuHit;
                    case 3:
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

    static void ShanXiHit(Hero h)
    {
        h.StartDizzy(1500);
    }

    static void XuanFengTuiHit(Hero h)
    {
        h.StartDizzy(200);
    }

    static void HuoQiuStart(Hero h)
    {
        
    }

    static void HuoQiuHit(Hero h)
    {

    }

    static void HuoYanZhangKongStart(Hero h)
    {

    }

    static void TianFengHuoWuStart(Hero h)
    {

    }

    static void ZhenDangShaStart(Hero h)
    {

    }

    static void ZhenDangShaHit(Hero h)
    {
        h.StartDizzy(1000);
    }

    static void XunMengTuJiStart(Hero h)
    {
        h.Move(25, 0.1f);
    }

    static void XunMengTuJiHit(Hero h)
    {

    }

    static void KuaiSuZhuaQuStart(Hero h)
    {

    }

    static void KuaiSuZhuaQuHit(Hero h)
    {
        h.StartDizzy(1000);
    }

    static void XueBaoStart(Hero h)
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