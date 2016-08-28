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
 */

class SkillTree
{
    SkillNode root;
    public SkillTree()
    {
        root = new SkillNode(InputReceiver.dir.down);
    }

    /// <summary>
    /// 添加技能
    /// </summary>
    /// <param name="input">技能轨迹序列</param>
    /// <param name="s">技能</param>
    /// 作者：胡皓然
    public void Add(List<InputReceiver.dir> input,Skill s)
    {
        SkillNode theRoot = root;
        for (int i = 0; i < input.Count; i++)
        {
            theRoot = theRoot.addChild(new SkillNode(input[i]));
        }
        theRoot.addChild(s);
    }

    /// <summary>
    /// 查找某个技能
    /// </summary>
    /// <param name="input">技能的序列</param>
    /// <returns>找到了就返回该技能，否则返回null</returns>
    /// 作者：胡皓然
    public Skill checkSkill(List<InputReceiver.dir> input)
    {
        SkillNode theLeaf = root;
        for (int i = 0; i < input.Count; i++)
        {
            theLeaf = theLeaf.getChild(input[i]);
            if (theLeaf == null)
            {
                return null;
            }
        }
        return theLeaf.getSkill();
    }
}