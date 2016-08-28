using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class SkillTree
{
    SkillNode root;
    public SkillTree()
    {
        root = new SkillNode(InputReceiver.dir.down);
    }

    public void Add(List<InputReceiver.dir> input,Skill s)
    {
        SkillNode theRoot = root;
        for (int i = 0; i < input.Count; i++)
        {
            theRoot = theRoot.addChild(new SkillNode(input[i]));
        }
        theRoot.addChild(s);
    }

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