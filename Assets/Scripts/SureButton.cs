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

public class SureButton : MonoBehaviour {

	//an instance of Selectable
	Selectable sel;

	// Use this for initialization
	void Start ()
	{
		//find the selectable script
		sel = GameObject.Find ("EventSystem").GetComponent<Selectable> ();              
	}

	public void OnClick(){
		//return the chosed heroes' name
		var names = sel.ReturnList();

        //for test
        names = new List<string>();
        names.Add("JiXiaoke");
        names.Add("JiXiaoke");
        names.Add("JiXiaoke");

        // change the scene
        GameManager.GetInstance()._controlPlayer = 1;
        GameManager.GetInstance().StartFightScene(names,names);
	}

	// Update is called once per frame
	void Update () {

	}
}
