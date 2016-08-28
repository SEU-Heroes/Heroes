using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Player{
    const int heroNum = 3;

    HeroAttr[] heroes;

    Hero nowHero;

    int nowHeroNum = 0;

    public Player()
    {
        heroes = new HeroAttr[3];
    }

    public void setHeroAttr(HeroAttr ha0,HeroAttr ha1,HeroAttr ha2)
    {
        heroes[0] = ha0;
        heroes[1] = ha1;
        heroes[2] = ha2;
    }

    public Hero getHero()
    {
        return nowHero;
    }

    public void Instantiate(Vector3 position,Quaternion q)
    {
        nowHero = GameManager.getInstance().Instantiate(GameManager.factory.getHero(heroes[nowHeroNum].heroId), position, q).GetComponent<Hero>(); 
        nowHero.attr = heroes[nowHeroNum];
    }

    public void handInput(List<InputReceiver.dir> input)
    {
        nowHero.handInput(input);
    }

    public void handDirection(InputReceiver.joyDir dir)
    {
        nowHero.handDirection(dir);
    }

    public void touchStay(List<InputReceiver.dir> input)
    {
        nowHero.touchStay(input);
    }
}
