using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

class XmlOperate
{

    static public String XmlFilePath;

    ///<summary>
    ///获得英雄的信息，由于暂时没有英雄类和技能类，只能返回null，然后把所有信息打印出来
    ///</summary>
    ///<param name="Name"></param>
    ///作者：韩璐璐
    public static HeroAttr GetHeroInformation(String name)
    {
        XmlFilePath = Application.dataPath + "\\Heros.xml";
        ///初始化一个xml实例
        XmlDocument myXmlDoc = new XmlDocument();
        ///加载xml文件（参数为xml文件的路径）
        myXmlDoc.Load(XmlFilePath);
        ///获得第一个姓名匹配的节点（SelectSingleNode）：此xml文件的根节点

        XmlNode rootNode = myXmlDoc.SelectSingleNode("Heros");
        ///分别获得该节点的InnerXml和OuterXml信息
        string innerXmlInfo = rootNode.InnerXml.ToString();
        string outerXmlInfo = rootNode.OuterXml.ToString();

        ///获得该节点的子节点（即：该节点的第一层子节点）
        XmlNodeList firstLevelNodeList = rootNode.ChildNodes;
        ///挨个比对每个英雄节点
        foreach (XmlNode node in firstLevelNodeList)
        {
            ///如果符合就获得该英雄的信息
            if (node.Attributes["Name"].InnerText == name)
            {
                int heroId = int.Parse(node.Attributes["heroId"].InnerText);
                float jump = float.Parse(node.Attributes["Jump"].InnerText);
                int move = int.Parse(node.Attributes["Move"].InnerText);
                float height = float.Parse(node.Attributes["Height"].InnerText);


                SkillTree skillTree = new SkillTree();
                List<InputReceiver.dir> dirList;
                ///获取该英雄的技能属性
                XmlNodeList secondLevelNodeList = node.ChildNodes;
                foreach (XmlNode secondNode in secondLevelNodeList)
                {
                    if (secondNode.Name == "Skills")
                    {
                        Skill skill = new Skill();


                        XmlNodeList skillList = secondNode.ChildNodes;
                        foreach (XmlNode skillNode in skillList)
                        {
                            skill = new Skill();
                            dirList = new List<InputReceiver.dir>();
                            XmlNodeList skillKeyList = skillNode.ChildNodes;
                            skill._skillName = skillNode.Attributes["SkillName"].InnerText;
                            
                            foreach (XmlNode skillKeyNode in skillKeyList)
                            {
                          
                                ///获得技能的各个属性
                                if (skillKeyNode.Name == "IsCreated")
                                {
                                    bool isCreated = (int.Parse(skillKeyNode.InnerText) == 0) ? false : true;
                                    skill._isCreator = isCreated;
                                }
                                if (skillKeyNode.Name == "offset")
                                {
                                    XmlNodeList offsetAttrList = skillKeyNode.ChildNodes;
                                    foreach (XmlNode offsetAttr in offsetAttrList)
                                    {
                                        int x = 0, y = 0;
                                        if (offsetAttr.Name == "x")
                                            x = int.Parse(offsetAttr.InnerText);
                                        if (offsetAttr.Name == "y")
                                            y = int.Parse(offsetAttr.InnerText);
                                        Vector3 vector = new Vector3(x, y);
                                    }
                                }
                                if (skillKeyNode.Name == "AddRage")
                                {
                                    int addRage = int.Parse(skillKeyNode.InnerText);
                                    skill._addRage = addRage;
                                }
                                if (skillKeyNode.Name == "HitAddRage")
                                {
                                    int hitaddRage = int.Parse(skillKeyNode.InnerText);
                                    skill._hitAddRage = hitaddRage;
                                }
                                if (skillKeyNode.Name == "aggressivity")
                                {
                                    int aggressivity = int.Parse(skillKeyNode.InnerText);
                                    skill._aggressivity = aggressivity;
                                }
                                if (skillKeyNode.Name == "skillId")
                                {
                                    int skillId = int.Parse(skillKeyNode.InnerText);
                                    skill._skillId = skillId;
                                }
                                if (skillKeyNode.Name == "time")
                                {
                                    float time = float.Parse(skillKeyNode.InnerText);
                                    skill._time = time;
                                }
                                if (skillKeyNode.Name == "BeforeAT")
                                {
                                    float beforeAT = float.Parse(skillKeyNode.InnerText);
                                    skill._beforeAT = beforeAT;
                                }
                                if (skillKeyNode.Name == "firstAfterAT")
                                {
                                    float firstAfterAT = float.Parse(skillKeyNode.InnerText);
                                    skill._afterATFirst = firstAfterAT;
                                }
                                if (skillKeyNode.Name == "lastAfterAT")
                                {
                                    float lastAfterAT = float.Parse(skillKeyNode.InnerText);
                                    skill._afterATLast = lastAfterAT;
                                }
                                if (skillKeyNode.Name == "existTime")
                                {
                                    float existTime = float.Parse(skillKeyNode.InnerText);
                                    skill._existTime = existTime;
                                }
                                if (skillKeyNode.Name == "isChild")
                                {
                                    bool isChild = (int.Parse(skillKeyNode.InnerText) == 0) ? false : true;
                                    skill._isChild = isChild;
                                }
                                if (skillKeyNode.Name == "tralls")
                                {
                                    XmlNodeList trallsList = skillKeyNode.ChildNodes;
                                    foreach (XmlNode trallNode in trallsList)
                                    {
                                        dirList.Add((InputReceiver.dir)(int.Parse(trallNode.InnerText)));
                                    }
                                }
                            }
                            skill.SetFunc();
                            ///把技能添加到技能树
                            skillTree.Add(dirList, skill);
                        }
                       
                    }
                }
                ///构造英雄
                HeroAttr hero = new HeroAttr(name, heroId, jump, move, height, skillTree);
                return hero;
            }
        }
        return null;
    }

}
