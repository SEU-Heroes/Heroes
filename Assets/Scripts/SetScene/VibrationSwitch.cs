/*

* 音量控制按钮脚本

* @author 何明胜

* @Time 2016-9-2  16:34:00

*/

using UnityEngine;
using UnityEngine.UI;

public class VibrationSwitch : MonoBehaviour
{

    //创建开启和关闭的按钮图片变量
    public Image imageOn;
    public Image imageOff;

    //创建开关按钮
    public Button btn;

    //创建判断震动是否开启的变量
    public static bool switch_On = true;

    // Use this for initialization
    void Start()
    {
        //监听音乐按钮
        btn.onClick.AddListener(delegate ()
        {
            this.ClickEvent(btn);
        });
    }

    //按钮点击响应事件
    void ClickEvent(Button sender)
    {
        Debug.Log("Click");

        //判断按钮是否开启
        if (switch_On)
        {
            //更换开关图片
            btn.GetComponent<Image>().sprite = imageOff.sprite;

            GameManager.GetInstance()._shock = false;
            switch_On = false;
        }
        else
        {
            //更换开关图片
            btn.GetComponent<Image>().sprite = imageOn.sprite;

            GameManager.GetInstance()._shock = true;
            switch_On = true;
        }
    }
}
