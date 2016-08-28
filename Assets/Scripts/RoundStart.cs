using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;

public class RoundStart : MonoBehaviour {

    public Text countDownText;
    public Text readyText;

    private int leftTime;
    private bool startCountingDown = false;

    void Awake()
    {
        
    }

    void Start()
    {
        //测试时添加一个角色
        HeroAttr ha = new HeroAttr();
        ha.heroId = 0;
        ha.skills = new SkillTree();

        List<InputReceiver.dir> input = new List<InputReceiver.dir>();
        input.Add(InputReceiver.dir.right);

        Skill xuanFengTui = new Skill(0, 0);
        xuanFengTui.skillName = "XuanFengTui";
        xuanFengTui.aggressivity = 60;
        ha.skills.Add(input, xuanFengTui);

        List<InputReceiver.dir> input1 = new List<InputReceiver.dir>();
        input1.Add(InputReceiver.dir.up);
        Skill jump = new Skill(0, 1);
        jump.skillName = "Jump";
        jump.aggressivity = 0;
        ha.skills.Add(input1, jump);

        Player p = new Player();
        p.setHeroAttr(ha, ha, ha);
        GameManager.getInstance().setMainPlayer(p);
        GameManager.getInstance().getMainPlayer().Instantiate(new Vector3(-3, -2, 0), Quaternion.identity);
        Player p2 = new Player();
        p2.setHeroAttr(ha, ha, ha);
        GameManager.getInstance().setOtherPlayer(p2);
        GameManager.getInstance().getOtherPlayer().Instantiate(new Vector3(3, -2, 0), Quaternion.identity);
        p2.getHero().isFacingLeft = true;


        // 开启倒计时
        leftTime = 60;
        InvokeRepeating("DoCountDown", 0, 1);

        // 控制回合开始动画
        Destroy(readyText, 4.5f);
        readyText.text = "Round" + GameController.GetInstance().roundNumber;
        Invoke("RoundToReady", 2);
        Invoke("ReadyToFight", 3.5f);
    }

    void Update()
    {

    }

    // 开始计时器倒计时
    public void DoCountDown()
    {
        if (startCountingDown && leftTime > 0)
        {
            leftTime -= 1;
            countDownText.text = leftTime.ToString();
        }
        if(leftTime == 0)
        {
            // Do something judging the winner
        }
    }

    // 下面两个函数用来控制每回合开始的动画
    public void RoundToReady()
    {
        readyText.text = "Ready?";
    }
    public void ReadyToFight()
    {
        GameManager.getInstance().getMainPlayer().getHero().StartGame();

        // 调整字体颜色和斜体，开始倒计时
        readyText.color = new Color(200, 0, 0);
        readyText.fontStyle = FontStyle.Italic;
        readyText.text = "Fight!";
        startCountingDown = true;
    }
}
