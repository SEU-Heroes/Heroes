using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class Player_number : MonoBehaviour
{
    private static int _number1 = 1;
    private static int _number3 = 0;
    public Image backImage;
    public Sprite _1V1BG;
    public Sprite _3V3BG;
    public Image button1;
    public Image button3;
    public Sprite _1v1Button1;
    public Sprite _1v1Button2;
    public Sprite _3v3Button1;
    public Sprite _3v3Button2;
    public Text text;
    private string[] Instruct = { "在1V1模式下，双方只能选择一个角色进行PK，采用三局两胜制。在回合时间内血量为0的一方失败；若回合时间结束，则剩余血量多的一方在当前回合获胜.获胜两局的一方胜利，结束本次PK",
                                    "在3V3模式下，双方可选择3个角色进行PK，采用KOF对战模式，即当前回合获胜的一方在下一回合的角色状态和上一回合结束时一样，三个角色全部死亡的一方失败，结束本次PK"};

    void Start()
    {
        text.text = Instruct[0];

    }

    public void ClickReturn()
    {
        GameManager.GetInstance().ChangeScene("ModleSelectScene");
        // Application.LoadLevel("ModleSelectView");
    }

    public void Click1v1()
    {
        Debug.Log("1");
        if (_number1 == 1)
        {
            GameManager.GetInstance()._heroesNum = 1;
            if (GameManager.GetInstance()._nowMode == GameManager.mode.player)
            {
                GameManager.GetInstance().ChangeScene("NetWorkScene");
            }
            else if (GameManager.GetInstance()._nowMode == GameManager.mode.computer)
            {
                GameManager.GetInstance().ChangeScene("HeroSelectScene");
            }
            // Application.LoadLevel("NetWorkView");
        }
        else if (_number1 == 0)
        {
            backImage.sprite = _1V1BG;
            button1.sprite = _1v1Button1;
            button3.sprite = _3v3Button2;
            text.text = Instruct[0];
            _number1++;
            _number3 = 0;
        }
    }

    public void Click3v3()
    {
        Debug.Log("3");
        if (_number3 == 0)
        {
            backImage.sprite = _3V3BG;
            button1.sprite = _1v1Button2;
            button3.sprite = _3v3Button1;
            text.text = Instruct[1];
            _number1 = 0;
            _number3++;
        }
        else if (_number3 == 1)
        {
            GameManager.GetInstance()._heroesNum = 3;
            // Application.LoadLevel("NetWorkView");
            if (GameManager.GetInstance()._nowMode == GameManager.mode.player)
            {
                GameManager.GetInstance().ChangeScene("NetWorkScene");
            }
            else if (GameManager.GetInstance()._nowMode == GameManager.mode.computer)
            {
                GameManager.GetInstance().ChangeScene("HeroSelectScene");
            }
        }

    }
    public void NumberSureButton()
    {
        if (_number1 == 1)
        {
            GameManager.GetInstance()._heroesNum = 1; 

            if (GameManager.GetInstance()._nowMode == GameManager.mode.player)
            {
                GameManager.GetInstance().ChangeScene("NetWorkScene");
            }
            else if (GameManager.GetInstance()._nowMode == GameManager.mode.computer)
            {
                GameManager.GetInstance().ChangeScene("HeroSelectScene");
            }
            // Application.LoadLevel("NetWorkView");
        }
        else if (_number3 == 1)
        {
            GameManager.GetInstance()._heroesNum = 3;

            if (GameManager.GetInstance()._nowMode == GameManager.mode.player)
            {
                GameManager.GetInstance().ChangeScene("NetWorkScene");
            }
            else if (GameManager.GetInstance()._nowMode == GameManager.mode.computer)
            {
                GameManager.GetInstance().ChangeScene("HeroSelectScene");
            }
        }
    }
}
