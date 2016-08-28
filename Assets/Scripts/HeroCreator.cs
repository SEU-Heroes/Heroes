using UnityEngine;
using System.Collections;

public class HeroCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.GetInstance().InstantiatePlayers();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
