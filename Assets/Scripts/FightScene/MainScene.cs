﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
  * 需求：
  * 玩家需要看到双方的血量及血量变化；
  * 玩家需要能看见游戏的倒计时；
  * 玩家需要看到怒气值的变化；
  * 扩展需求：
  * 这些变化应该伴随着一些特效。
  */

/* 
  * 版本 V1.00  修改时间2016.8.24 修改内容-新增倒计时
  * 版本 V1.01  修改时间2016.8.25 修改内容-新增血量变化特效
  * 版本 V1.02  修改时间2016.8.27 修改内容-新增SP条满时闪烁特效
  */

class MainScene : MonoBehaviour {

    static public MainScene _instance;

    /// <summary>
    /// EX必杀时的UI
    /// </summary>
    public GameObject _gestureDisplay;//用于展示轨迹
    public GameObject _maskBlack;//用来在释放EX必杀时慢慢使背景变黑
    public GameObject _maskRed;//用来在EX必杀使用成功时使全屏幕变红
    public GameObject _nowChanging;//正在改变透明度的物体
    
    private float totalHP; // 玩家HP总量
    private float totalSP; // 玩家SP总量

    // 玩家HP剩余值
    private float player1HP;
    private float player2HP;
    private float player1SP;
    private float player2SP;

    // 玩家胜负判断（为了判断平局所以必须加入这两个布尔值）
    private bool player1Lose;
    private bool player2Lose;

    // 每帧更新的变量，用作控制SP条特效
    private int tempPara;

    // 玩家剩余HP的UI显示
    public Slider player1HP1;
    public Slider player1HP2;
    public Slider player2HP1;
    public Slider player2HP2;
    public Slider player1SPSlider;
    public Slider player2SPSlider;

    //SP条供选择材质
    public Material material1;
    public Material material2;

    /// <summary>
    /// 在实例化的同时进行基本参数的设置，调用在Start()之前
    /// </summary>
    /// 作者：庄亦舟
    void Awake()
    {
        totalHP = GameManager._maxHP;
        totalSP = GameManager._maxRage;
        tempPara = 0;
        _instance = this;
    }

    /// <summary>
    /// 在物体实例化完成后立刻调用，以初始化相关变量
    /// </summary>
    /// 作者：庄亦舟
    void Start()
    {
        //初始设置EXUI不显示
        _gestureDisplay.GetComponent<Renderer>().enabled = false;
        _maskBlack.GetComponent<Renderer>().enabled = false;
        _maskRed.GetComponent<Renderer>().enabled = false;

        // 动态设置角色初始HP为最大值
        player1HP = player2HP = totalHP;
        player1HP1.maxValue = player1HP2.maxValue = player2HP1.maxValue = player2HP2.maxValue = totalHP;
        player1HP1.value = player1HP2.value = player2HP1.value = player2HP2.value = totalHP;
        
        // 动态设置角色SP最大值以及初始化为0
        player1SP = player2SP = 0;
        player1SPSlider.maxValue = player2SPSlider.maxValue = totalSP;
        player1SPSlider.value = player2SPSlider.value = 0;
    }

	/// <summary>
    /// 每帧调用进行游戏更新
    /// </summary>
    /// 作者：庄亦舟
	void Update ()
    {
        // 每帧更新变量tempPara，来控制SP条满时的动画提示
        tempPara = tempPara > 60 ? 0 : tempPara + 1;
        if (player1SP == totalSP)
        {
            if (tempPara > 30)
                GameObject.Find("Fill1").GetComponent<Image>().material = material1;
            else
                GameObject.Find("Fill1").GetComponent<Image>().material = material2;
        }
        if (player2SP == totalSP)
        {
            if (tempPara > 30)
                GameObject.Find("Fill2").GetComponent<Image>().material = material1;
            else
                GameObject.Find("Fill2").GetComponent<Image>().material = material2;
        }
    }

    /// <summary>
    /// HP血条及内部HP变量减少的效果
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num"></param>
    /// 作者：庄亦舟
    public void HPReducce(int id, int num)
    {
        // 两名玩家分别的掉血处理，用id控制玩家1或2
        if(id == 1) { 
            player1HP -= num;
            player1HP2.value = player1HP;
            StartCoroutine(HPReduceAnimation(player1HP, player1HP1)); // 开线程处理血条减少动画
        }
        else {
            player2HP -= num;
            player2HP2.value = player2HP;
            StartCoroutine(HPReduceAnimation(player2HP, player2HP1));
        }

        // 受伤后增加角色SP，值为受伤值/2
        SPAdd(id, num / 2);

        // 判断是否有角色HP<=0，若有则进行胜负判断及后续操作
        if (player1HP <= 0)
            player1Lose = true;
        if (player2HP <= 0)
            player2Lose = true;

        if(player1Lose && player2Lose)
        {
            // 平局操作
            GameManager.GetInstance().HeroDie(1);
            GameManager.GetInstance().HeroDie(2);
        }
        else if(player1Lose)
        {
            // 玩家2获胜
            GameManager.GetInstance().HeroDie(1);
        }
        else if(player2Lose)
        {
            GameManager.GetInstance().HeroDie(2);
        }

    }

    /// <summary>
    /// 显示EX必杀技的UI
    /// </summary>
    public void ShowEXUI()
    {
        _maskBlack.GetComponent<Renderer>().enabled = true;
        ColorFade(_maskBlack, Color.black, new Color(Color.black.r, Color.black.g, Color.black.b, 1), 1.5f);
        _gestureDisplay.GetComponent<Renderer>().enabled = true;
    }

    /// <summary>
    /// EX必杀的全屏闪烁
    /// </summary>
    public void Twinkle()
    {
        _maskRed.GetComponent<Renderer>().enabled = true;
        Invoke("RedHide", 1f);
    }

    /// <summary>
    /// 隐藏红色特效
    /// </summary>
    void RedHide()
    {
        _maskRed.GetComponent<Renderer>().enabled = false;
    }

    /// <summary>
    /// 隐藏EX必杀技的UI
    /// </summary>
    public void HideEXUI()
    {
        _gestureDisplay.GetComponent<Image>().enabled = false;
        _maskBlack.GetComponent<Image>().enabled = false;
        _maskRed.GetComponent<Image>().enabled = false;
    }

    /// <summary>
    /// 利用线程暂停2帧来达到血条缓慢减少的特效
    /// </summary>
    /// <param name="targetHP"></param>
    /// <param name="HPSlider"></param>
    /// <returns></returns>
    /// 作者：庄亦舟
    IEnumerator HPReduceAnimation(float targetHP, Slider HPSlider)
    {
        float fromHP = HPSlider.value;
        // 暂停帧来达到血条缓慢减少效果
        for (int i = 0; i < 8; i++)
        {
            yield return 1; 
            HPSlider.value = HPSlider.value - (fromHP - targetHP) / 8;
            yield return 1;
        }

        // 处理帧延迟达到的血量误差
        if(HPSlider.value > targetHP)
            HPSlider.value = targetHP;
    }

    /// <summary>
    /// 进行不同角色SP的增加，通过id来控制具体角色
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num"></param>
    /// 作者：庄亦舟
    public void SPAdd(int id, int num)
    {
        if (id == 1)
        {
            player1SP += num;
            player1SPSlider.value += num;
        }
        else
        {
            player2SP += num;
            player2SPSlider.value += num;
        }
    }

    public void SPAdd(Hero hero, int num)
    {
        if (hero._id == 1)
        {
            player1SP += num;
            player1SPSlider.value += num;
        }
        else
        {
            player2SP += num;
            player2SPSlider.value += num;
        }
    }

    public void WinAnim()
    {

    }

    public void LoseAnim()
    {

    }

    /// <summary>
    /// 使物体的颜色渐变
    /// </summary>
    /// <param name="gameObject">要渐变的物体</param>
    /// <param name="from">起始颜色</param>
    /// <param name="end">结束颜色</param>
    /// <param name="time">渐变时间</param>
    void ColorFade(GameObject gameObject, Color from, Color end, float time)
    {

    }
}
