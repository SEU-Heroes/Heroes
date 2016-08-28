using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/*
 * demand:
 * player can see which hero he/she has chosen
 * player can cancel the hero he/she has chosen
 * make the hero choseable after it has been cancelled
 */

public class Selected : MonoBehaviour {

	//the sprite of the default
	Sprite none; 

	//an instance of selectable
	Selectable sel;  

	//stores the name of buttons in the second row
	List<string> btnNames = new List<string> ();   

	//stores the name of buttons in the first row which carrys the hero can be chosen 
	List<string> heroes = new List<string>();                           

	// Use this for initialization 
	void Start () 
	{

		//get the sprite of default
		GameObject obj = GameObject.Find ("origin");
		none = obj.GetComponent<Image> ().sprite;                       

		btnNames.Add ("chose1");
		btnNames.Add ("chose2");
		btnNames.Add ("chose3");

		heroes.Add ("hero1");
		heroes.Add ("hero2");
		heroes.Add ("hero3");
		heroes.Add ("hero4");
		heroes.Add ("hero5");

		//get the script of selectable
		sel = GameObject.Find ("EventSystem").GetComponent<Selectable> ();             
	}

	/// <summary>
	/// Trigger something when button is pressed	
	/// </summary>
	/// <param name="btn">Button.</param>
	/// writer:Liu Yueqi
	public void OnClick(Button btn)
	{                  
		//find the order of the pressed button 
		int number = btnNames.IndexOf (btn.name); 

		//find which hero has been cancelled
		foreach (string btnName in heroes) 
		{                                           
			GameObject obj = GameObject.Find(btnName);
			if(btn.GetComponent<Image>().sprite.Equals(obj.GetComponent<Image>().sprite))
			{
				//make the cancelled hero unchosen
				sel.ReturnList().Remove (obj.GetComponent<Button> ().name);  

				//make the cancelled hero whiter since it can be chosen again
				obj.gameObject.GetComponent<Image>().color = Color.white;             
			}
		}

		//change the image of the pressed button into the default image
		btn.GetComponent<Image> ().sprite = none; 

		//decrease the number of the chosed hero
		sel.DecreaseNumber(); 

		//set the order of the button which can carry a hero again
		if (sel.number > number)
		{
			sel.SetOrder (number);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
