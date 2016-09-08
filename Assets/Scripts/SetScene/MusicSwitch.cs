/*

* 音量控制按钮脚本

* @author 何明胜

* @Time 2016-9-2  16:34:00

*/

using UnityEngine;
using UnityEngine.UI;

public class MusicSwitch : MonoBehaviour
{
    //创建开启和关闭的按钮图片变量
    public Image imageOn;
    public Image imageOff;

    //创建开关按钮
    public Button btn;

    //创建判断音乐是否开启的变量
    public static bool switch_On = true;

    //创建滑动条
    public Slider slider;

    //音量
    public static float musicVolume = 50;

    //音乐显示文本框
    public Text textVolume;

    // Use this for initialization
    void Start()
    {
        //初始化滑条值为上次结束时的值
        slider.value = musicVolume;

        //监听音乐按钮
        btn.onClick.AddListener(delegate ()
        {
            this.ClickEvent(btn);
        });
    }

    //按钮点击响应事件
    void ClickEvent(Button sender)
    {
        //判断按钮是否开启
        if (switch_On)
        {
            //更换开关图片
            btn.GetComponent<Image>().sprite = imageOff.sprite;
            GameManager.GetInstance()._volume = -1;
            switch_On = false;

            //音乐按钮关闭，滑条不可滑动
            slider.interactable = false;
        }
        else
        {
            Debug.Log("Click2");
            //更换开关图片
            btn.GetComponent<Image>().sprite = imageOn.sprite;

            GameManager.GetInstance()._volume = (int)MusicSwitch.musicVolume;
            switch_On = true;

            //音乐按钮开启，滑条可滑动
            slider.interactable = true;
        }
    }
       
    // Update is called once per frame
    void Update()
    {
        musicVolume = slider.value;
        textVolume.text = musicVolume + "%";
    }
}