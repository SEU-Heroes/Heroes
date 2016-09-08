/*

* 设置界面的返回按钮

* @author 何明胜

* @Time 2016-9-1  20:34:00

*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoleSkills : MonoBehaviour
{
    //创建软小贱和季小可角色按钮
    public Button btnRuan;
    public Button btnJi;

    //技能列表按钮数组
    public GameObject[] btn;
    // public Image[] image;

    //把角色技能名称加入数组
    public string[] Ruanskills = new string[] { "软小贱1", "软小贱2", "软小贱3", "软小贱4", "软小贱5", "软小贱6" };
    public string[] Jiskills   = new string[] { "季小可1", "季小可2", "季小可3", "季小可4", "季小可5", "季小可6" };

    //创建按钮List
    List<string> btnsName = new List<string>();

    //创建判断当前显示角色的int值
    public static int ruanOrJi = 1;

    // Use this for initialization
    public void Start()
    {
        //进入界面默认选择软小贱角色，季小可角色为灰色
        btnJi.gameObject.GetComponent<Image>().color = Color.grey;

        //把技能按钮的名称添加到btnsName
        for (int i =0; i< btn.Length; i++)
        {
            btnsName.Add(btn[i].name);
        }

        //技能按钮初始化为软小贱技能
        for (int i = 0; i < btnsName.Count; i++)
        {
            //将右侧技能列表的名称改成响应角色的技能
            GameObject TextObj = GameObject.Find("TextSkill" + (1 + i));
            Text tex = TextObj.GetComponent<Text>();
            tex.GetComponent<Text>().text = Ruanskills[i];
        }

        //监听软小贱角色按钮
        btnRuan.onClick.AddListener(delegate ()
        {
            this.OnClick(btnRuan);
        });

        //监听季小可角色按钮
        btnJi.onClick.AddListener(delegate ()
        {
            this.OnClick(btnJi);
        });
    }
    
    //按钮点击事件响应
    public void OnClick(Button sender)
    {
        GameObject TextObj = GameObject.Find("SkillsInstructionsContent");
        Text tex = TextObj.GetComponent<Text>();
        tex.text = "尚未选择技能";

        //判断按下的是软小贱还是季小可
        if (sender == btnRuan)
        {
            //选中的按钮为白色，未选中的为灰色
            btnRuan.gameObject.GetComponent<Image>().color = Color.white;
            btnJi.gameObject.GetComponent<Image>().color = Color.grey;
    
            ruanOrJi = 1;

            for (int i = 0; i < btnsName.Count; i++)
            {
                //GameObject btnObj = GameObject.Find(btnsName[i]);
                // Button btn = btnObj.GetComponent<Button>();
                // btn.GetComponent<Image>().sprite = image[i].sprite;

                //将右侧技能列表的名称改成响应角色的技能
                TextObj = GameObject.Find("TextSkill" + (1 + i));
                tex = TextObj.GetComponent<Text>();
                tex.text = Ruanskills[i];
            }
        }
        else
        {
            //选中的按钮为白色，未选中的为灰色
            btnRuan.gameObject.GetComponent<Image>().color = Color.grey;
            btnJi.gameObject.GetComponent<Image>().color = Color.white;

           
            ruanOrJi = 2;

            //将右侧技能列表的名称改成响应角色的技能
            for (int i = 0; i < btnsName.Count; i++)
            { 
                TextObj = GameObject.Find("TextSkill" + (1 + i));
                tex = TextObj.GetComponent<Text>();
                tex.text = Jiskills[i];
            }
        }
 
    }

    // Update is called once per frame
    void Update()
    {

    }
}