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
                ///获取该英雄的技能属性
                XmlNodeList secondLevelNodeList = node.ChildNodes;
                foreach (XmlNode secondNode in secondLevelNodeList)
                {
                    if (secondNode.Name == "Skills")
                    {
                        Skill skill = new Skill();


                        XmlNodeList skillList = secondNode.ChildNodes;
                        List<InputReceiver.dir> dirList=new List<InputReceiver.dir>();
                        foreach (XmlNode skillNode in skillList)
                        {
                            ///获得技能的各个属性
                            if (skillNode.Name == "IsCreated")
                            {
                                bool isCreated = (int.Parse(skillNode.Value) == 0) ? false : true;
                                skill._isCreator = isCreated;
                            }
                            if (skillNode.Name == "offset")
                            {
                                XmlNodeList offsetNodeList = skillNode.ChildNodes;
                                int x = 0 , y = 0;
                                foreach (XmlNode offsetNode in offsetNodeList)
                                {
                                    if (offsetNode.Name == "x")
                                    {
                                        x = int.Parse(offsetNode.Value);
                                    }
                                    if (offsetNode.Name == "y")
                                    {
                                        y = int.Parse(offsetNode.Value);
                                    }
                                }
                                Vector3 vector = new Vector3(x, y, 0);
                                skill._offset = vector;
                            }
                            if (skillNode.Name == "AddRage")
                            {
                                int addRage = int.Parse(skillNode.Value);
                                skill._AddRage = addRage;
                            }
                            if (skillNode.Name == "aggressivity")
                            {
                                int aggressivity = int.Parse(skillNode.Value);
                                skill._aggressivity = aggressivity;
                            }
                            if (skillNode.Name == "skillId")
                            {
                                int skillId = int.Parse(skillNode.Value);
                                skill._skillId = skillId;
                            }
                            if (skillNode.Name == "BeforeAT")
                            {
                                int beforeAT = int.Parse(skillNode.Value);
                                skill._BeforeAT = beforeAT;
                            }
                            if (skillNode.Name == "firstAfterAT")
                            {
                                int afterAT = int.Parse(skillNode.Value);
                                skill._AfterATFirst = afterAT;
                            }
                            if (skillNode.Name == "lastAfterAT")
                            {
                                int afterAT = int.Parse(skillNode.Value);
                                skill._AfterATLast = afterAT;
                            }

                            if (skillNode.Name == "isChild")
                            {
                                bool isChild = (int.Parse(skillNode.Value) == 0) ? false : true;
                                skill._isChild = isChild;
                            }
                            if (skillNode.Name == "time")
                            {
                                int time = int.Parse(skillNode.Value);
                                skill._Time= time;
                            }
                            if (skillNode.Name == "tralls")
                            {
                                XmlNodeList trallNodeList = skillNode.ChildNodes;
                                foreach (XmlNode trallNode in trallNodeList)
                                {
                                    InputReceiver.dir dir = (InputReceiver.dir)(int.Parse(trallNode.Value));
                                    dirList.Add(dir);
                                }
                            }
                        }
                        ///把技能添加到技能树
                        skillTree.Add(dirList, skill);
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
