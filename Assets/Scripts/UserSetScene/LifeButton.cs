using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeButton : MonoBehaviour {

    public Text TimeShowText;
    public string[] lifes = { "一", "二", "三","四","五"};
    public int _LifeNum = 0;
    static public LifeButton _instance;

    void Start()
    {
        _instance = this;
    }

    //点击右键则顺序更换显示血量条数
    public void RightClick()
    {
        _LifeNum = (_LifeNum + 1) % 5;
        TimeShowText.text = lifes[_LifeNum];
        
    }
    //点击左键则逆序更换显示血量条数
    public void LeftClick() {
        _LifeNum = (_LifeNum +4) % 5;
        TimeShowText.text = lifes[_LifeNum];
    }
}
