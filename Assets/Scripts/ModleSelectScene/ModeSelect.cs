/*

* 模式选择脚本

* @author 何明胜

* @Time 2016-9-3  16:08:00

*/

using UnityEngine;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour {
    //创建模式按钮
    public Button[] btnArray;

    //创建模式名称数组
    public static string[] mode = new string[] { "新手教程", "技能回顾", "人机对战", "局域网对战", "剧情模式" };

    //创建模式说明数组
    public static string[] model_instructions = new string[] { "在该模式下，您能够很快的熟悉游戏操作依以便于获得更好的游戏体验和操作分数",
                                                               "在该模式下，您能够回顾每个角色的每个技能，忘记手势时都可以来这里回顾", 
                                                               "在该模式下，您能够选择不同难度的AI进行战斗，对于提升操作熟练度很有帮助", 
                                                               "在该模式下，您能够和处于同一局域网下的玩家进行匹配对战，可以选择自由匹配和创建房间匹配",
                                                               "在该模式下，您可以在不同的剧情背景下进行战斗" };

    //模式列表第一个模式的序号
    public static int firstMode = 0;

    //创建模式说明文本
    public Text instructionsText;

	// Use this for initialization
	void Start () {
        for(int i=0; i<btnArray.Length; i++)
        {
            //初始化模式列表
            GameObject TextObj = GameObject.Find("Text" + (1 + i));
            Text text = TextObj.GetComponent<Text>();
            text.text = mode[i + firstMode];

            //初始化模式说明面板
            modelInstructions();

            //监听五个模式按钮
            Button tempBtn = btnArray[i];
            btnArray[i].onClick.AddListener(delegate ()
            {
                this.OnClick(tempBtn);
            });
        }

    }

    //监听事件响应
    private void OnClick(Button sender)
    {
        //判断点击了哪个按钮
        for(int i=0; i<btnArray.Length; i++)
        {
            if(sender == btnArray[i])
            {
                //调用处理点击事件的函数
                ChangeMode(i);
                modelInstructions();
            }
        }
    }

    //处理点击事件函数
    public void ChangeMode(int clickBtn)
    {
        //根据点击模式按钮的不同执行不同操作
        if (clickBtn == 2)
        {//点击五个按钮中间一个，确认选择        
            ModleSureButton.ClickEvent();
        }
        else
        {
            //点击五个按钮上面两个，五个模式向下顺序循环移动一次
            //点击五个按钮下面两个，五个模式向上顺序循环移动一次
            if (clickBtn > -1 && clickBtn < 2)
            {//向下移动，第一个模式序号减1（如为0，则变成4，依次循环）
                firstMode = (firstMode + 4) % 5;
            }
            else if (clickBtn > 2 && clickBtn < 5)
            {//向下移动，第一个模式序号加1（如为4，则变成0，依次循环）
                firstMode++;
            }

            //执行模式名称的变换
            for (int i = 0; i < btnArray.Length; i++)
            {
                GameObject TextObj = GameObject.Find("Text" + (i + 1));
                Text tex = TextObj.GetComponent<Text>();
                tex.text = mode[((firstMode + i) % 5)];
            }
        }
    } 

    //更新模式说明面板
    public void modelInstructions()
    {
        int between = (firstMode + 2) % 5;
        instructionsText.text = model_instructions[between];
    }
}
