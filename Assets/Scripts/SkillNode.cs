using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 需求：
 * 添加子技能
 * 添加子结点
 * 得到子技能
 * 得到子节点
 */

/*
 * 版本 V1.0 能添加查找子节点和子技能
 */

class SkillNode
{
    InputReceiver.dir theDir;//该结点代表的一段轨迹
    List<SkillNode> child;//子节点
    Skill skill;//子技能

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

    /// <summary>
    /// 查找子节点
    /// </summary>
    /// <param name="d">子节点的轨迹方向</param>
    /// <returns>有相应子节点就返回相应子节点，没有就返回null</returns>
    /// 作者：胡皓然
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


    /// <summary>
    /// 得到子技能
    /// </summary>
    /// <returns>返回子技能，没有就返回null</returns>
    /// 作者：胡皓然
    public Skill getSkill()
    {
        return skill;
    }

    /// <summary>
    /// 确认该结点下是否有某个子节点
    /// </summary>
    /// <param name="d">子节点方向</param>
    /// <returns>有相应子节点就返回子节点下标，没有就返回-1</returns>
    /// 作者：胡皓然
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