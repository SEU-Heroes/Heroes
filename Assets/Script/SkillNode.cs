using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SkillNode
{
    InputReceiver.dir theDir;
    List<SkillNode> child;
    Skill skill;

    public SkillNode(InputReceiver.dir d)
    {
        theDir = d;
        child = new List<SkillNode>();
        skill = null;
    }

    public SkillNode addChild(SkillNode n)
    {
        int count = checkChild(n.theDir);
        if (count == -1)
        {
            child.Add(n);
            return n;
        }
        return child[count];
    }

    public bool addChild(Skill s)
    {
        if (skill == null)
        {
            skill = s;
            return true;
        }
        else
        {
            return false;
        }
    }

    public SkillNode getChild(InputReceiver.dir d)
    {
        if (checkChild(d) == -1)
        {
            return null;
        }
        else
        {
            return child[checkChild(d)];
        }
    }

    public Skill getSkill()
    {
        return skill;
    }

    int checkChild(InputReceiver.dir d)
    {
        for (int i = 0; i < child.Count; i++)
        {
            if (child[i].theDir == d)
            {
                return i;
            }
        }
        return -1;
    }
}