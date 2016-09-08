using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    public Text TimeShowText;
    public string[] Level = { "简单", "普通", "困难" };
    public int _levelNum = 0;
    static public LevelButton _instance;
    

    void Start()
    {
        _instance = this;
        TimeText();
    }
    /// <summary>
    /// 人机对战模式下可选择不同难度并且显示
    /// 点击右键则顺序更换显示难度等级
    /// 其他模式下选择但显示为空，并且GmaeManger设置为nnone
    /// </summary>
    public void RightClick()
    {
        _levelNum = (_levelNum + 1) % 3;
        TimeText();

    } /// <summary>
    /// 人机对战模式下可选择不同难度并且显示
    /// 点击左键则逆序更换显示难度等级
    /// 其他模式下选择但显示为空，并且GmaeManger设置为nnone
    /// </summary>
    public void LeftClick()
    {
        _levelNum = (_levelNum + 2) % 3;
        TimeText();
        //将level参数传递给Gamemanager
    }
    private void TimeText() 
    {
        if ((ModeSelect.firstMode + 2) % 5 == 2)
        {
            TimeShowText.text = Level[_levelNum];
        }
        else {
            TimeShowText.text = "  ";
        }
    }
}
