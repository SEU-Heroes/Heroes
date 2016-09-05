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

    static public Skill.Start GetBeforeATFunction(int heroid, int skillid)
    {
        switch (heroid)
        {
            case 0:
                switch (skillid)
                {
                    case 0:
                        return DefaultBeforeAT;
                    case 1:
                        return DefaultBeforeAT;
                    case 2:
                        return DefaultBeforeAT;
                    case 3:
                        return DefaultBeforeAT;
                    case 4:
                        return DefaultBeforeAT;
                    case 5:
                        return DefaultBeforeAT;
                    case 6:
                        return TianFengHuoWuBeforeAT;
                    default:
                        return null;
                }
            case 1:
                switch (skillid)
                {
                    case 0:
                        return DefaultBeforeAT;
                    default:
                        return null;
                }
            default:
                return null;
        }
    }

    static public Skill.Start GetStartFunction(int heroId,int skillId)
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

    static public Skill.Update GetUpdateFunction(int heroId, int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    case 0:
                        return JumpUpdate;
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
                        return TianFengHuoWuUpdate;
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

    static public Skill.End GetEndFunction(int heroId, int skillId)
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

    static public Skill.Hit GetHitFunction(int heroId, int skillId)
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
                        return TianFengHuoWuHit;
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

    static void DefaultBeforeAT(Hero h)
    {

    }

    static void TianFengHuoWuBeforeAT(Hero h)
    {
        
        h.Move(new Vector3(0.3f, 2.5f, 0), 0.4f);
        h.GetComponent<Rigidbody2D>().gravityScale = 0;
    }



    static void ShanXiStart(Hero h)
    {
        h.Move(new Vector3(5, 0, 0),0.1f);
    }


    static void XuanFengTuiStart(Hero h)
    {
        h.Move(new Vector3(3, 0, 0), 1.3f);  
    }

    static void JumpStart(Hero h)
    {
        h.Jump(new Vector2(0,h._jumpForce));
    }

    static void JumpUpdate(Hero h, float time)
    {
        h._nowState = Hero.state.jumping;
    }

    static void BackJumpStart(Hero h)
    {
        h.Jump(new Vector2((h._isFacingLeft ? 1 : -1) * h._backJumpForce*1.5f, h._backJumpForce));
    }

    static void HuoQiuStart(Hero h)
    {
    }

    static void HuoYanZhangKongStart(Hero h)
    {
        GameObject[] fireBalls = GameObject.FindGameObjectsWithTag(Tags.fireball);
        foreach (GameObject fireball in fireBalls)
        {
            fireball.GetComponent<Rigidbody2D>().velocity = new Vector3(h._isFacingLeft?-5:5, 0, 0);
        }
    }

    static void ShanXiHit(Hero h)
    {
        h.StartDizzy(1500);
    }

    static void XuanFengTuiHit(Hero h)
    {
        h.StartDizzy(200);
    }

    static void HuoQiuHit(Hero h)
    {
        h.StartDizzy(300);
    }


    static void TianFengHuoWuStart(Hero h)
    {

    }

    static void TianFengHuoWuHit(Hero h)
    {
        h.StartDizzy(200);
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

    static void TianFengHuoWuUpdate(Hero h, float time)
    {
        if(h._nowState >= Hero.state.LastHalfAfterAT)
            h.GetComponent<Rigidbody2D>().gravityScale = 5;
    }

    static void DefalutEnd(Hero h)
    {

    }

    static void DefalutHit(Hero h)
    {

    }
}