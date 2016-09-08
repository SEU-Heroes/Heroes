/*
 * 模式选择界面
 * 
 * 作者：何明胜
 * 
 * 日期：2016-9-2 10：20：00
 *
*/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BtnListener : MonoBehaviour {

    //技能列表按钮数组
    public Button[] btnS;
    //创建按钮List
   // List<string> btnsName = new List<string>();

    // 将按钮名称添加到List<string>，并监听每一个按钮的点击事件
    void Start () {
        // List<string> btnsName = new List<string>();
        //for (int i = 0; i < btnS.Length; i++)
        //{
        //    btnsName.Add(btnS[i].name);
        //}

        for (int i = 0; i < btnS.Length; i++)
        {
            Button btnTemp = btnS[i];
            //btnsName.Add(btn[i].name);
            btnS[i].onClick.AddListener(delegate ()
            {
                this.OnClick(btnTemp);
            });
        }

        //foreach (string btnName in btnsName)
        //{
        //    GameObject btnObj = GameObject.Find(btnName);
        //    Button btn = btnObj.GetComponent<Button>();
        //    btn.onClick.AddListener(delegate ()
        //    {
        //        this.OnClick(btn);
        //    });
        //}

    }

    //按钮点击事件的判断及响应操作
    private void OnClick(Button sender)
    {
       
        for (int i = 0; i < btnS.Length; i++)
        {
            if (sender == btnS[i])
            {
              
                GameObject TextObj = GameObject.Find("SkillsInstructionsContent");
                Text tex = TextObj.GetComponent<Text>();

                if(RoleSkills.ruanOrJi == 1)
                {
                   
                    tex.text = "Ruanskills["+(i+1)+"]软小贱技能说明";
                    //此处加技能具体说明、技能手势、动画。
                }
                else if(RoleSkills.ruanOrJi == 2)
                {
                  
                    tex.text = "Jiskills["+(i+1)+"]季小可技能说明";
                    //此处加技能具体说明、技能手势、动画。
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
