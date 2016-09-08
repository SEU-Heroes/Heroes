using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

/*
 * demand:
 * player can start the game by press down the sure button
 * can deliver the information of heroes player has chosen
 */

public class HeroSureButton : MonoBehaviour {

	//an instance of Selectable
	Selectable sel;

	// Use this for initialization
	void Start ()
	{
		//find the selectable script
		sel = GameObject.Find ("EventSystem").GetComponent<Selectable> ();              
	}

	public void OnClick(){
		// change the scene

		//return the chosed heroes' name
		List<string> list = sel.ReturnList();

        if(GameManager.GetInstance()._nowMode == GameManager.mode.computer)
        {
            Player player = new Player(1);
            player.SetHeroNum(GameManager.GetInstance()._heroesNum);
            if(GameManager.GetInstance()._heroesNum == 1)
            {
                player.SetHeroAttr(XmlOperate.GetHeroInformation(list[0]));
            }
            else if(GameManager.GetInstance()._heroesNum == 3)
            {
                player.SetHeroAttr(XmlOperate.GetHeroInformation(list[0]),XmlOperate.GetHeroInformation(list[1]),XmlOperate.GetHeroInformation(list[2]));
            }
            Player computer = new Player(2);
            computer.SetHeroNum(GameManager.GetInstance()._heroesNum);
            computer.SetRandHero();
            GameManager.GetInstance().SetPlayers(player, computer);
            GameManager.GetInstance()._controlPlayer = 1;
            GameManager.GetInstance().ChangeScene("UserSetScene");
        }
        else if (GameManager.GetInstance()._nowMode == GameManager.mode.player)
        {
            Player player = new Player(GameManager.GetInstance()._controlPlayer);
            player.SetHeroNum(GameManager.GetInstance()._heroesNum);
            if (GameManager.GetInstance()._heroesNum == 1)
            {
                player.SetHeroAttr(XmlOperate.GetHeroInformation(list[0]));
                //通知联网部分英雄选择
            }
            else if (GameManager.GetInstance()._heroesNum == 3)
            {
                player.SetHeroAttr(XmlOperate.GetHeroInformation(list[0]), XmlOperate.GetHeroInformation(list[1]), XmlOperate.GetHeroInformation(list[2]));
                //通知联网部分英雄选择
            }
            GameManager.GetInstance().ChangeScene("WaitScene");
        }          
	}
}
