/*

* 模式选择确认按钮

* @author 何明胜

* @Time 2016-9-3  16:18:00

*/

using UnityEngine;
using UnityEngine.UI;

public class ModleSureButton : MonoBehaviour {

    //创建按钮
    public Button button;

    // Use this for initialization
   public void Start()
    {
        //监听按钮
        button.onClick.AddListener(ClickEvent);
    }

    //按钮点击事件响应
   public static void ClickEvent()
    {
        //根据选择，进入响应模式
        switch ((ModeSelect.firstMode+2)%5)
        {
            case 0:
                Debug.Log("进入新手教程");
                GameManager.GetInstance()._nowMode = GameManager.mode.tutorial;
                //GameManager.GetInstance().ChangeScene("StudyScene");
                break;
            case 1:
                Debug.Log("进入技能回顾");
                GameManager.GetInstance()._nowMode = GameManager.mode.skillWatch;
                GameManager.GetInstance().ChangeScene("SkillsReviewScene");
                break;
            case 2:
                GameManager.GetInstance()._nowMode = GameManager.mode.computer;
                Debug.Log("进入人机对战");
                GameManager.GetInstance().ChangeScene("PlayerNumberScene");
                break;
            case 3:
                Debug.Log("进入局域网对战");
                GameManager.GetInstance()._nowMode = GameManager.mode.player;
                //GameManager.GetInstance().ChangeScene("PlayerNumberScene");
                break;
            case 4:
                Debug.Log("进入剧情模式");
                GameManager.GetInstance()._nowMode = GameManager.mode.story;
               // GameManager.GetInstance().ChangeScene("NumberScene");
                break;
            default:
                break;
        }
    }

}
