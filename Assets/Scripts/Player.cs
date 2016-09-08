using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 需求：
 * 按照模式管理一个或三个角色
 */

/*
 * 版本 V1.0 能管理三个角色
 */

class Player{
    public int _id;//角色在场景中的id，有1和2

    public int _maxHP;//当前设置的最大生命值

    int _heroNum = 3;//角色数量

    int _dieChance = 3;//玩家还可以死亡的次数

    HeroAttr[] _heroes;//角色属性列表

    Hero _nowHero;//目前的角色

    int _nowHeroNum = 0;//目前是第几个角色

    [HideInInspector]
    public bool _isAI;//这个角色是否是AI

    public Player(int id)
    {
        _id = id;
        _heroes = new HeroAttr[_heroNum];
    }

    /// <summary>
    /// 加入三个角色属性
    /// </summary>
    /// <param name="ha0"></param>
    /// <param name="ha1"></param>
    /// <param name="ha2"></param>
    /// 作者：胡皓然
    public void SetHeroAttr(HeroAttr ha0,HeroAttr ha1,HeroAttr ha2)
    {
        _heroes[0] = ha0;
        _heroes[1] = ha1;
        _heroes[2] = ha2;
        _heroNum = 3;
        _dieChance = 3;
    }

    /// <summary>
    /// 加入一个角色属性
    /// </summary>
    /// <param name="ha"></param>
    /// 作者：胡皓然
    public void SetHeroAttr(HeroAttr ha)
    {
        _heroes[0] = ha;
        _heroNum = 1;
        _dieChance = 2;
    }

    /// <summary>
    /// 得到目前的角色
    /// </summary>
    /// <returns></returns>
    /// 作者：胡皓然
    public Hero GetHero()
    {
        return _nowHero;
    }

    /// <summary>
    /// 实例化当前的角色
    /// </summary>
    /// <param name="position">实例化的位置</param>
    /// <param name="q">实例化时的旋转</param>
    /// 作者：胡皓然
    public void Instantiate(Vector3 position,Quaternion q)
    {
        Debug.Log(position);
        _nowHero = GameManager.GetInstance().Instantiate(GameManager._prefabManager.GetHero(_heroes[_nowHeroNum]._heroId, _isAI), position, q).GetComponent<Hero>();
        _nowHero._id = _id;
        Camera.main.GetComponent<FollowPlayers>().setPlayer(_nowHero,_id);
        _nowHero._attr = _heroes[_nowHeroNum];
    }

    //传递方向输入
    public void HandDirection(InputReceiver.joyDir dir)
    {
        _nowHero.HandDirection(dir);
    }

    /// <summary>
    /// 该玩家的当前角色死亡
    /// </summary>
    /// <param name="position">下一个角色要生成的位置</param>
    public void HeroDie(Vector3 position)
    {
        _dieChance--;
        if (_dieChance > 0)
        {
            if (_heroNum == 3)
            {
                _nowHeroNum++;
            }
            _nowHero.HeroDie();
            //MainScene._instance.SetHeroInfo(this);
            //MainScene._instance.NewRound();
        }
        else
        {
            GameManager.GetInstance().GameOver(this);
        }
    }

    public void setHP(int num)
    {
        _maxHP = num * GameManager._maxHP;
    }

    /// <summary>
    /// 根据角色数量随机设置角色
    /// </summary>
    public void SetRandHero()
    {
        if (_heroNum == 1)
        {
            _heroes[0] = XmlOperate.GetHeroInformation("JiXiaoke");
        }
        else if (_heroNum == 3)
        {
            _heroes[0] = XmlOperate.GetHeroInformation("JiXiaoke");
            _heroes[1] = XmlOperate.GetHeroInformation("JiXiaoke");
            _heroes[2] = XmlOperate.GetHeroInformation("JiXiaoke");
        }
    }

    /// <summary>
    /// 设置角色数量
    /// </summary>
    /// <param name="num"></param>
    public void SetHeroNum(int num)
    {
        _heroNum = num;
        if (_heroNum == 3)
        {
            _dieChance = 3;
        }
        else
        {
            _dieChance = 2;
        }
    }
}
