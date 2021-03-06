﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * 需求：
 * 能在场景内生成物体
 * 能管理两个玩家的信息
 * 能管理场景里的东西
 */


/*
 * 版本：V1.00 修改时间 2016.8.28 修改内容：管理玩家的信息
 * 版本：V2.00 修改时间 2016.8.30 修改内容：游戏设置，切换场景，实例化gameObject
 * 版本：V3.00 修改时间 2016.9.1 修改内容：能释放EX必杀技
 * 版本：V3.01 修改时间 2016.9.2 修改内容：能接收轨迹的输入
 * 版本：V4.00 修改时间 2016.9.2 修改内容：添加显示逻辑，用于客户端部分的显示，去掉其他的冗余操作
 */

class GameManager:MonoBehaviour{

    public GameObject[] _GestureManager;//轨迹管理器

    public string _nowScene;//当前场景的名字

    //游戏内统一数据
    static public int _maxRage = 1000;//最大怒气值
    static public int _defenseForce = 32; //防御力
    static public int _maxHP = 1024;//最大生命值
    static public float totalEXTime = 6;//EX释放时间

    //游戏设置数据
    public enum map { boat, seu };//游戏地图枚举
    public enum difficulty {none, easy, middle, difficult };//游戏难度枚举
    public enum mode { none, player, computer, story, tutorial, skillWatch };//游戏模式枚举
    [HideInInspector]
    public int _volume = -1;//音量大小，-1为无声音
    [HideInInspector]
    public bool _shock;//是否震动
    [HideInInspector]
    public mode _nowMode = mode.computer;//当前的游戏模式
    [HideInInspector]
    public int _roundNum = 3;//回合数
    [HideInInspector]
    public int _roundTime = -1;//回合时间
    [HideInInspector]
    public difficulty _diffi = difficulty.easy;//难度选择
    [HideInInspector]
    public map _nowMap = map.boat;//地图选择
    [HideInInspector]
    public int _HPNum = 1;//血量
    [HideInInspector]
    public int _heroesNum = 1;//每个玩家选择的角色数

    //单例
    static GameManager _instance;

    //角色管理
    static public PrefabManager _prefabManager;//预制资源管理器
    Player _playerLeft, _playerRight;//两个角色
    [HideInInspector]
    public int _controlPlayer = 1;//本次游戏控制的角色
    [HideInInspector]
    public Vector3 _positionLeft = new Vector3(-5, -2, 0);
    [HideInInspector]
    public Vector3 _positionRight = new Vector3(5, -2, 0);

    //EX必杀技释放管理
    bool _isEXing;//是否有角色正在释放EX必杀技
    int _EXId;//释放EX必杀技的角色ID
    int _EXLevel;//当前EX必杀技轨迹展示等级
    float _EXTime;//EX必杀技剩余时间
    int _nowGestureId;//正在展示的轨迹在所属等级的ID
    int _EXCount;//已经成功完成的EX轨迹次数
    Hero _EXHero;//释放EX必杀技的角色
    bool _isTimeFlow;//播放动画时导致计时暂停的状态位

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //最开始取得角色工厂引用
        _prefabManager = PrefabManager.instance;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (_isEXing&&_isTimeFlow)
        {
            _EXTime -= Time.deltaTime;
            if (_EXTime <= 0)
            {
                EXEnd();
            }
        }
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

    /// <summary>
    /// 按照角色ID获得相应角色
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public Player GetPlayer(int playerId)
    {
        if (playerId == _playerLeft._id)
        {
            return _playerLeft;
        }
        else if(playerId == _playerRight._id)
        {
            return _playerRight;
        }
        return null;
    }

    /// <summary>
    /// 设置玩家信息
    /// </summary>
    /// <param name="theLeft">位置处于左边的玩家，位置处于右边的玩家</param>
    /// <param name="theRight"></param>
    public void SetPlayers(Player theLeft,Player theRight)
    {
        _playerLeft = theLeft;
        _playerLeft.setHP(_HPNum);
        _playerLeft._isAI = false;

        _playerRight = theRight;
        _playerRight.setHP(_HPNum);
        _playerRight._isAI = (_nowMode == mode.computer);
    }

    /// <summary>
    /// 设置玩家信息（只能用于联机模式）
    /// </summary>
    /// <param name="player"></param>
    /// <param name="playerId"></param>
    public void SetPlayer(Player player, int playerId)
    {
        if (playerId == 1)
        {
            _playerLeft = player;
        }
        else
        {
            _playerRight = player;
        }
    }

    /// <summary>
    /// 改变角色的HPUI（减少时）
    /// </summary>
    /// <param name="h">要减少HP的角色对象</param>
    /// <param name="num">要减少的HP数值</param>
    /// 作者：胡皓然
    public void HPReduce(int playerId,int num)
    {
        MainScene._instance.HPReducce(playerId, num);
    }

    /// <summary>
    /// 根据传入的一个Hero得到场景中的另一个Hero
    /// </summary>
    /// <param name="h"></param>
    /// <returns>另一个Hero</returns>
    public Hero GetOtherHero(Hero h)
    {
        if (_playerLeft.GetHero() == h)
            return _playerRight.GetHero();
        return _playerLeft.GetHero();
    }

    /// <summary>
    /// 在切换到战斗场景时调用
    /// </summary>
    /// <param name="names">战斗的角色名列表，Player1在前，Player2在后，人机对战只需要玩家的角色名</param>
    /// 作者：胡皓然
    public void StartFightScene(List<string> playerLeftHeroes,List<string> playerRightHeroes)
    {
        _playerLeft = new Player(1);
        _playerLeft.SetHeroAttr(XmlOperate.GetHeroInformation(playerLeftHeroes[0]), XmlOperate.GetHeroInformation(playerLeftHeroes[1]), XmlOperate.GetHeroInformation(playerLeftHeroes[2]));

        _playerRight = new Player(2);
        _playerRight.SetHeroAttr(XmlOperate.GetHeroInformation(playerRightHeroes[0]), XmlOperate.GetHeroInformation(playerRightHeroes[1]), XmlOperate.GetHeroInformation(playerRightHeroes[2]));
        
        ChangeScene("BattleScene-boat");
    }

    /// <summary>
    /// 实例化当前已设置好的玩家角色
    /// </summary>
    /// 作者：胡皓然
    public void InstantiatePlayers()
    {
        _playerLeft.Instantiate(_positionLeft, Quaternion.identity);
        _playerRight.Instantiate(_positionRight, Quaternion.identity);
        _playerRight.GetHero()._isFacingLeft = true;
    }

    /// <summary>
    /// 有角色死亡
    /// </summary>
    /// <param name="id">死亡角色的ID</param>
    /// 作者：胡皓然
    public void HeroDie(int id)
    {
        if (id == 1)
        {
            _playerLeft.HeroDie(_positionLeft);
            _playerLeft.Instantiate(_positionLeft, Quaternion.identity);
        }
        else
        {
            _playerRight.HeroDie(_positionRight);
            _playerRight.Instantiate(_positionRight, Quaternion.identity);
            _playerRight.GetHero()._isFacingLeft = true;
        }
    }

    /// <summary>
    /// 处理一个轨迹
    /// </summary>
    /// <param name="gesture"></param>
    /// 作者：胡皓然
    public void HandleGesture(string gestureName,float match)
    {
        if (_isEXing&&_EXHero._id == _controlPlayer)
        {
            GestureMatch(gestureName, match);
        }
//         else if (gestureName == "EX")
//         {
//             if (GetPlayer(_controlPlayer).GetHero().IsSkillable() != 0)
//             {
//                 StartEX(GetPlayer(_controlPlayer).GetHero());
//             }
//         }
        else
        {
            Skill theSkill;
            if ((theSkill = GetPlayer(_controlPlayer).GetHero()._attr._skills.FindSkillByName(gestureName)) != null)
            {
                GetPlayer(_controlPlayer).GetHero().HandSkill(theSkill);
            }
        }
    }

    /// <summary>
    /// 联机模式下接收对方传来的操作
    /// </summary>
    /// <param name="gesture"></param>
    /// <param name="match"></param>
    public void OtherControl(string gestureName, int match)
    {
        Skill theSkill;
        if ((theSkill = GetPlayer(_controlPlayer).GetHero()._attr._skills.FindSkillByName(gestureName)) != null)
        {
            GameManager.GetInstance().GetOtherHero(GetPlayer(_controlPlayer).GetHero()).HandSkill(theSkill);
        }
    }

    /// <summary>
    /// 使主角色释放技能
    /// </summary>
    /// <param name="playerId">技能释放者的玩家ID</param>
    /// <param name="skillName">技能名字</param>
    /// <param name="match">技能手势匹配程度</param>
    /// 作者：胡皓然
    public void StartSkill(int playerId,string skillName, float match)
    {
        Player thePlayer = (playerId == 1 ? _playerLeft : _playerRight);
        thePlayer.GetHero().HandSkill(thePlayer.GetHero()._attr._skills.FindSkillByName(skillName));
    }

    /// <summary>
    /// EX复杂度提高
    /// </summary>
    /// 作者：胡皓然
    public void EXLevelUp()
    {
        if (_EXLevel < 5)
        {
            _EXLevel++;
        }
    }

    /// <summary>
    /// 开始EX必杀技
    /// </summary>
    /// <param name="hero">释放EX必杀技的角色</param>
    /// 作者：胡皓然
    public void StartEX(Hero hero)
    {
        _isTimeFlow = true;
        _EXHero = hero;
        _isEXing = true;
        _EXLevel = 0;
        _EXId = hero._id;
        _EXTime = totalEXTime;
        _EXCount = 0;
        MainScene._instance.ShowEXUI();
        InstantiateGesture(_EXLevel);
        _playerLeft.GetHero()._nowState = Hero.state.unControlable;
        _playerRight.GetHero()._nowState = Hero.state.unControlable;
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <param name="hero">胜利的角色</param>
    /// 作者：胡皓然
    public void GameOver(Player player)
    {
        if (player._id == _controlPlayer)
        {
            MainScene._instance.WinAnim();
        }
        else
        {
            MainScene._instance.LoseAnim();
        }
    }

    /// <summary>
    /// 根据当前的模式选择返回到哪一个界面
    /// </summary>
    /// 作者：胡皓然
    public void ReturnScene()
    {

    }


    /// <summary>
    /// 结束EX动画
    /// </summary>
    /// 作者：胡皓然
    public void EndEXAnim()
    {
        InstantiateGesture(_EXLevel);
        _isTimeFlow = true;
    }

    /// <summary>
    /// 正常关闭游戏时的处理
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 开始EX必杀技的动画
    /// </summary>
    /// 作者：胡皓然
    void StartEXAnim()
    {
        _isTimeFlow = false;
        _EXHero.gameObject.GetComponent<Animator>().SetTrigger("EX" + _EXLevel);
    }

    /// <summary>
    /// EX模式下处理匹配的手势
    /// </summary>
    /// <param name="id">最佳匹配轨迹的ID</param>
    /// 作者：胡皓然
    void GestureMatch(string gestureName,float match)
    {
        //手势匹配成功
        if (GetGestureId(gestureName) == _nowGestureId)
        {
            StartEXAnim();
            EXLevelUp();

            _EXCount++;

            //处理匹配度得分
        }
    }

    /// <summary>
    /// 在屏幕上展示一个轨迹
    /// </summary>
    /// <param name="level">轨迹复杂度等级</param>
    /// 作者：胡皓然
    void InstantiateGesture(int level)
    {
        //实例化一个相应等级的轨迹展示预制资源
        var gesture = _GestureManager[level].GetComponent<GestureManager>().GetRandGesture();
        MainScene._instance._gestureDisplay.GetComponent<Image>().sprite = gesture._gestureSprite;
        _nowGestureId = gesture._id;
    }

    /// <summary>
    /// 改变当前场景
    /// </summary>
    /// <param name="sceneName">要转到的场景名字</param>
    /// 作者：胡皓然
    public void ChangeScene(string sceneName)
    {
        _nowScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 通过匹配到的轨迹名字来获得轨迹在其level中的id
    /// </summary>
    /// <param name="gestureName">轨迹名字</param>
    /// <returns>轨迹ID</returns>
    /// 作者：胡皓然
    int GetGestureId(string gestureName)
    {
        //根据轨迹的名字得到相应的序号
        return 0;
    }

    /// <summary>
    /// 结束EX必杀技
    /// </summary>
    /// 作者：胡皓然
    void EXEnd()
    {
        _isEXing = false;
        _playerLeft.GetHero()._nowState = Hero.state.still;
        _playerRight.GetHero()._nowState = Hero.state.still;
    }

    /// <summary>
    /// 计算本次EX必杀技的伤害量
    /// </summary>
    /// <returns></returns>
    /// 作者：胡皓然
    int CalculateEXHurt()
    {
        return 300 + _EXCount * 100;
    }
}