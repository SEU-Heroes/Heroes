using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

/*
 * demand:
 * there are five heroes provided to player for chose
 * player can chose new hero again after he/she cancels a hero
 * the max number of heroes player can chose is three
 * player can not chose the hero which he/she has chosen
 * deliver the hero has been chosen to the selected.cs
 * expanded demand:
 * there are some way to mark the chosed hero
 */

public class Selectable : MonoBehaviour {

	//the name of the button in the second row which carrys the chosed hero
	private string[] choseName = { "chose1", "chose2", "chose3" };   

	//a list stores the name of the button in the first row which has been chosen
	public List<string>  chosedName = new List<string>();               

	//the number of chosed hero
	public int number = 0;     

	//the order of the button in the second row which can carry a hero
	public int order = 0; 

	// Use this for initialization
	public void Start () 
	{
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
	///Set the order of the button in the second row which can carry a new hero
	/// </summary>
	/// <param name="newOrder"></param>
	/// writer:Liu Yueqi
	public void SetOrder(int newOrder)
	{                                  
		order = newOrder;
	}

	/// <summary>
	/// Return the list<string> chosedName
	/// </summary>
	/// <returns>The list.</returns>
	/// writer:Liu Yueqi
	public List<string> ReturnList(){                                        
		return chosedName;
	}

	/// <summary>
	/// Trigger some events when button is pressed.
	/// </summary>
	/// <param name="btn"></param>
	/// writer:Liu Yueqi
	public void OnClick(Button btn)
	{      
		//the max number of heroes can chose is three 
		if (number < 3) {       
			// get the name of the pressed button
			string name = btn.name;  

			//whether the hero has been chosen
			if (!chosedName.Contains (name))
			{
				//find the button which can carry a hero
				GameObject obj = GameObject.Find (choseName [order]);  

				//change the image of the button in the second row into the image of chosed hero
				obj.GetComponent<Image> ().sprite = btn.GetComponent<Image> ().sprite;

				//make the chosed hero darker
				btn.gameObject.GetComponent<Image>().color = GameObject.Find("origin").GetComponent<Image>().color;  

				//the number of chosed hero has increased
				number++;

				//the order of the button which can carry a new hero has increased
				order++;  

				//the hero can not be chosen again
				chosedName.Add (name);                                                      
			}
			//the hero can not be chosen again
			else
			{                                                     
				;}
		}
		//the number of heroes can chose can not greater than three
		else 
		{;                                                        
		}
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
