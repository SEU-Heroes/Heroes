﻿using UnityEngine;
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
    const int _heroNum = 3;//角色数量

    HeroAttr[] _heroes;//角色属性列表

    Hero _nowHero;//目前的角色

    int _nowHeroNum = 0;//目前是第几个角色

    public Player()
    {
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
        if (GameManager._factory == null)
        {
            Debug.Log("111");
        }
        _nowHero = GameManager.GetInstance().Instantiate(GameManager._factory.GetHero(_heroes[_nowHeroNum]._heroId), position, q).GetComponent<Hero>();
        Camera.main.GetComponent<FollowPlayers>().setPlayer(_nowHero);
        _nowHero._attr = _heroes[_nowHeroNum];
    }

    //传递轨迹输入
    public void HandInput(List<InputReceiver.dir> input)
    {
        _nowHero.HandInput(input);
    }

    //传递方向输入
    public void HandDirection(InputReceiver.joyDir dir)
    {
        _nowHero.HandDirection(dir);
    }

    //传递手指停留操作
    public void TouchStay(List<InputReceiver.dir> input)
    {
        _nowHero.TouchStay(input);
    }
}
