using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/*
 * 需求：
 * 能在场景内生成物体
 * 能管理两个玩家的信息
 * 能管理场景里的东西
 */


/*
 * 版本：V1.00 修改时间 2016.8.28 修改内容：管理玩家的信息
 */
class GameManager:MonoBehaviour{

    //游戏内统一数据
    static public int _distanceStander;//距离标准值

    //游戏模式管理
    public enum mode {none, player, computer, story,tutorial,skillWatch};//游戏模式枚举
    mode _nowMode = mode.computer;//当前的游戏模式

    //单例
    static GameManager _instance;

    //角色管理
    static public HeroFactory _factory;//角色工厂
    Player _player1, _player2;//两个角色

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //最开始取得角色工厂引用
        _factory = HeroFactory.instance;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 单例模式
    /// </summary>
    /// <returns>单例</returns>
    /// 作者：胡皓然
    static public GameManager GetInstance()
    {
        return _instance;
    }

    /// <summary>
    /// 在场景内实例化一个物体
    /// </summary>
    /// <param name="go">需要实例化的物体引用</param>
    /// <param name="position">实例化物体的位置</param>
    /// <param name="q">实例化物体的</param>
    /// <returns></returns>
    /// 作者：胡皓然
    public GameObject Instantiate(GameObject go, Vector3 position, Quaternion q)
    {
        return (GameObject)Camera.Instantiate(go, position, q);
    }

    //得到玩家的对象
    public Player GetMainPlayer()
    {
        return _player1;
    }

    //得到敌方的对象
    public Player GetOtherPlayer()
    {
        return _player2;
    }

    public void SetMainPlayer(Player p)
    {
        _player1 = p;
    }

    public void SetOtherPlayer(Player p)
    {
        _player2 = p;
    }

    //设置模式
    public void SetMode(mode m)
    {
        _nowMode = m;
    }

    /// <summary>
    /// 改变角色的HPUI（减少时）
    /// </summary>
    /// <param name="h">要减少HP的角色对象</param>
    /// <param name="num">要减少的HP数值</param>
    /// 作者：胡皓然
    public void HPReduce(Hero hero,int num)
    {
        if (hero == _player1.GetHero())
        {
            MainScene._instance.HPReducce(1, num);
        }
        else
        {
            MainScene._instance.HPReducce(2, num);
        }
    }

    /// <summary>
    /// 在切换到战斗场景时调用
    /// </summary>
    /// <param name="names">战斗的角色名列表，Player1在前，Player2在后，人机对战只需要玩家的角色名</param>
    /// 作者：胡皓然
    public void StartFightScene(List<string> names)
    {
        setPlayer(names[0], names[1], names[2]);
        ChangeScene("BattleScene-boat");
    }

    /// <summary>
    /// 实例化当前已设置好的玩家角色
    /// </summary>
    /// 作者：胡皓然
    public void InstantiatePlayers()
    {
        _player1.Instantiate(new Vector3(-5, -2, 0), Quaternion.identity);
        _player2.Instantiate(new Vector3(5, -2, 0), Quaternion.identity);
        _player2.GetHero()._isFacingLeft = true;
    }

    /// <summary>
    /// 设置人机的角色，玩家选择三个，电脑与玩家相同
    /// </summary>
    /// <param name="heroName1"></param>
    /// <param name="heroName2"></param>
    /// <param name="heroName3"></param>
    /// 作者：胡皓然
    void setPlayer(string heroName1,string heroName2,string heroName3)
    {
        _player1 = new Player();
        _player1.SetHeroAttr(XmlOperate.GetHeroInformation(heroName1),XmlOperate.GetHeroInformation(heroName2),XmlOperate.GetHeroInformation(heroName3));
        _player2 = new Player();
        _player2.SetHeroAttr(XmlOperate.GetHeroInformation(heroName1), XmlOperate.GetHeroInformation(heroName2), XmlOperate.GetHeroInformation(heroName3));
    }

    /// <summary>
    /// 改变当前场景
    /// </summary>
    /// <param name="sceneName">要转到的场景名字</param>
    /// 作者：胡皓然
    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    
}
