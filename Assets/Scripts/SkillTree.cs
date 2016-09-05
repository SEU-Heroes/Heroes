using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * 能查找某个技能
 * 能添加技能
 */

/*
 * 版本 V1.0 能添加技能，能查找技能
 * 版本 V2.0 从树状的存储换为了List
 */

class SkillTree
{
    List<Skill> skills;

    public SkillTree()
    {
        skills = new List<Skill>();
    }

    /// <summary>
    /// 添加技能
    /// </summary>
    /// <param name="input">技能轨迹序列</param>
    /// <param name="s">技能</param>
    /// 作者：胡皓然
    public void Add(Skill s)
    {
        if (!CheckSkill(s))
        {
            skills.Add(s);
        }
    }

    /// <summary>
    /// 查找某个技能
    /// </summary>
    /// <param name="input">技能的序列</param>
    /// <returns>找到了就返回该技能，否则返回null</returns>
    /// 作者：胡皓然
    public bool CheckSkill(Skill s)
    {
        foreach (Skill skill in skills)
        {
            if (skill._skillName == s._skillName)
            {
                return true;
            }
        }
        return false;
    }

    public Skill FindSkillById(int id)
    {
        foreach (Skill skill in skills)
        {
            if (skill._skillId == id)
            {
                return skill;
            }
        }
        return null;
    }

    public Skill FindSkillByName(string name)
    {
        foreach (Skill skill in skills)
        {
            if (skill._skillName == name)
            {
                return skill;
            }
        }
        return null;
    }
}