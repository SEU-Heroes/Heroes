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

class SkillScheduler : MonoBehaviour
{

    static public Skill.Start GetBeforeATFunction(int heroid, int skillid)
    {
        switch (heroid)
        {
            case 0:
                switch (skillid)
                {
                    case 6:
                        return TianFengHuoWuBeforeAT;
                    default:
                        return DefaultBeforeAT;
                }
            case 1:
                switch (skillid)
                {
                    default:
                        return DefaultBeforeAT;
                }
            default:
                return DefaultBeforeAT;
        }
    }

    static public Skill.Start GetStartFunction(int heroId, int skillId)
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
                        return DefaultStart;
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
                        return DefaultStart;
                }
            default:
                return DefaultStart;
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
                        return BackJumpUpdate;
                    case 3:
                        return HuoJingUpdate;
                    case 6:
                        return TianFengHuoWuUpdate;
                    default:
                        return DefalutUpdate;
                }
            case 1:
                switch (skillId)
                {
                    default:
                        return DefalutUpdate;
                }
            default:
                return DefalutUpdate;
        }
    }

    static public Skill.End GetEndFunction(int heroId, int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    default:
                        return DefalutEnd;
                }
            case 1:
                switch (skillId)
                {
                    default:
                        return DefalutEnd;
                }
            default:
                return DefalutEnd;
        }
    }

    static public Skill.Hit GetHitFunction(int heroId, int skillId)
    {
        switch (heroId)
        {
            case 0:
                switch (skillId)
                {
                    case 2:
                        return ShanXiHit;
                    case 3:
                        return XuanFengTuiHit;
                    case 4:
                        return HuoQiuHit;
                    case 6:
                        return TianFengHuoWuHit;
                    default:
                        return DefalutHit;
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
                    default:
                        return DefalutHit;
                }
            default:
                return DefalutHit;
        }
    }

    static void DefaultStart(Hero h)
    {

    }

    static void DefaultBeforeAT(Hero h)
    {

    }

    static void ShanXiBeforeAT(Hero h)
    {
        h._isFacingLeft = !h.isLeftOfOther();
        AudioSource.PlayClipAtPoint(SEController.clips[6], Vector3.zero);
    }

    static void TianFengHuoWuBeforeAT(Hero h)
    {
        h.Move(new Vector3(0.3f, 2.5f, 0), 0.4f);
        h.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    static void XuanFengTuiStart(Hero h)
    {
        float realDistance = 0;
        if (h._isFacingLeft)
            realDistance = h.transform.position.x + 8.18f < 3 ? h.transform.position.x + 8.18f : 3;
        else
            realDistance = 8.18f - h.transform.position.x < 3 ? 8.18f - h.transform.position.x : 3;

        h.Move(new Vector3(realDistance, 0, 0), 1.3f);
    }

    static void ShanXiStart(Hero h)
    {
        float realDistance = 0;
        if (h._isFacingLeft)
            realDistance = h.transform.position.x + 8.18f < 5 ? h.transform.position.x + 8.18f : 5;
        else
            realDistance = 8.18f - h.transform.position.x < 5 ? 8.18f - h.transform.position.x : 5;

        h.Move(new Vector3(realDistance, 0, 0), 0.01f);
    }

    static void JumpStart(Hero h)
    {
        h._nowState = Hero.state.still;
        h.Jump(new Vector2(0, h._jumpForce));
    }

    static void JumpUpdate(Hero h, float time)
    {
        h._nowState = Hero.state.jumping;
    }

    static void BackJumpStart(Hero h)
    {
        h._nowState = Hero.state.still;
        h.Jump(new Vector2((h._isFacingLeft ? 1 : -1) * h._backJumpForce * 1.5f, h._backJumpForce));
    }

    static void BackJumpUpdate(Hero h, float time)
    {
        h._nowState = Hero.state.jumping;
    }

    static void HuoQiuStart(Hero h)
    {
        AudioSource.PlayClipAtPoint(SEController.clips[0], Vector3.zero);
    }

    static void HuoYanZhangKongStart(Hero h)
    {
        GameObject[] fireBalls = GameObject.FindGameObjectsWithTag(Tags.fireball);
        foreach (GameObject fireball in fireBalls)
        {
            if ((h._isFacingLeft && fireball.transform.position.x < h.transform.position.x) || (!h._isFacingLeft && fireball.transform.position.x > h.transform.position.x))
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector3(h._isFacingLeft ? -5 : 5, 0, 0);
        }
    }

    static void ShanXiHit(Hero h)
    {
        if (GameManager.GetInstance().GetOtherHero(h)._nowSkill._isCombo && GameManager.GetInstance().GetOtherHero(h)._lastSkillName == "ShanXi")
        {
            h.StartDizzy(1500);
        }
        else
        {
            h.StartDizzy(200);
        }
        AudioSource.PlayClipAtPoint(SEController.clips[5], Vector3.zero);
    }

    static void XuanFengTuiHit(Hero h)
    {
        h.StartDizzy(200);
        AudioSource.PlayClipAtPoint(SEController.clips[4], Vector3.zero);
    }

    static void HuoQiuHit(Hero h)
    {
        h.StartDizzy(300);
        AudioSource.PlayClipAtPoint(SEController.clips[1], Vector3.zero);
    }


    static void TianFengHuoWuStart(Hero h)
    {
        AudioSource.PlayClipAtPoint(SEController.clips[2], Vector3.zero);
    }

    static void TianFengHuoWuHit(Hero h)
    {
        h.StartDizzy(200);
        AudioSource.PlayClipAtPoint(SEController.clips[1], Vector3.zero);
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
        if (h._nowState >= Hero.state.LastHalfAfterAT)
            h.GetComponent<Rigidbody2D>().gravityScale = 5;
    }

    static void HuoJingUpdate(Hero h, float time)
    {
        if (h._nowState == Hero.state.FirstHalfAfterAT)
        {
            h.GetComponent<Rigidbody2D>().gravityScale = 5;
            h.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    static void DefalutEnd(Hero h)
    {

    }

    static void DefalutHit(Hero h)
    {

    }

    static void EXHit(Hero h)
    {
        GameManager.GetInstance().StartEX(GameManager.GetInstance().GetOtherHero(h));
    }
}