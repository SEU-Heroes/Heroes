using UnityEngine;
using System.Collections;

class SkillScheduler{
    Skill.Start getStartFunction(int skillTag)
    {
        switch (skillTag)
        {
            case 1:
                return ShanXiStart;
            default:
                return null;
        }
    }

    Skill.Update getUpdateFunction(int skillTag)
    {
        switch (skillTag)
        {
            case 1:
                return ShanXiUpdate;
            default:
                return null;
        }
    }

    Skill.End getEndFunction(int skillTag)
    {
        switch (skillTag)
        {
            case 1:
                return ShanXiEnd;
            default:
                return null;
        }
    }

    void ShanXiStart(Hero h)
    {
        h.move(GameManager.getInstance().distanceStander,0);
        h.gameObject.GetComponent<Animator>().SetTrigger("ShanXi");
    }

    void ShanXiUpdate(Hero h, float time)
    {

    }

    void ShanXiEnd(Hero h)
    {

    }
}