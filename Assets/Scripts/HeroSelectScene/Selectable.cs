using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

/*
 * demand:
 * there are five heroes provided to player for chose
 * the max number of heroes player can chose is three
 * the chosed hero is brighter
 * the unchosed hero is darker
 * expanded demand:
 * some ways to store the information of chosed hero
 * version V1.00 modify time 2016.09.03 modified contain-control the hero to be chosed and unchosed
 */

public class Selectable : MonoBehaviour {

    //the max number player can choose
    public int _maxHeroNum;

	//a list stores the name of the hero which has been chosen
	public List<string>  chosedName = new List<string>(); 

	//a list stores the name of heroes 
	public List<string> heroesName = new List<string> ();

	//the number of chosed hero
	public int number = 0;

	//the origin color which is gray
	Color origin;

	// Use this for initialization
	public void Start () 
	{
        _maxHeroNum = GameManager.GetInstance()._heroesNum;

		heroesName.Add ("hero1");
		heroesName.Add ("hero2");
		heroesName.Add ("hero3");
		heroesName.Add ("hero4");
		heroesName.Add ("hero5");

		GameObject obj = GameObject.Find ("origin");
		origin = obj.GetComponent<Image> ().color;
	}

	/// <summary>
	/// Decrease the number
	/// </summary>
	/// <c></c>
	/// writer:Liu Yueqi
	public void DecreaseNumber()
	{                                              
		number--;	
	}
		
	/// <summary>
	/// Return the list<string> chosedName
	/// </summary>
	/// <returns>The list.</returns>
	/// writer:Liu Yueqi
	public List<string> ReturnList(){

        //用作测试
        List<string> list = new List<string>();
        list.Add("JiXiaoke");
        if (GameManager.GetInstance()._heroesNum == 3)
        {
            list.Add("JiXiaoke");
            list.Add("JiXiaoke");
        }
        return list;    

		return chosedName;
	}

	/// <summary>
	/// Trigger some events when button is pressed.
	/// </summary>
	/// <param name="btn"></param>
	/// writer:Liu Yueqi
	public void OnClick(Button btn)
	{      
		//get the name of the preesed button
		string name = btn.name;  

		//the max number of heroes can chose is three 
		if (number < _maxHeroNum) {       

			//whether the hero has been chosen
			if (!chosedName.Contains (name))
			{                       
				//make the chosed hero brighter
				btn.gameObject.GetComponent<Image>().color = Color.white;  

				//the number of chosed hero has increased
				number++;

				//stores the name of the pressed button in the list in order to mark the chosed hero
				chosedName.Add (name);
            }
			//player can cancel the hero if the hero has been chosen
			else
			{   
				//make the hero darker which means the hero can be chosed again
				btn.gameObject.GetComponent<Image> ().color = origin;

				//decrease the number of chosed number
				DecreaseNumber ();

				//the hero is not be chosed anymore
				chosedName.Remove (name);
				}
		}
		//the number of heroes can chose can not greater than three
		else 
		{
			//player can cancel the hero when the number of chosed hero reaches the maxmum
			if (chosedName.Contains (name)) {
				btn.gameObject.GetComponent<Image> ().color = origin;
				number--;
				chosedName.Remove (name);
			}
			//player can not chose more hero since the number of chosed hero is three
			else
			{
				;}
		}
	}
}