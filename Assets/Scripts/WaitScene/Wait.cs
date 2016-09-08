using UnityEngine;
using System.Collections;

public class Wait : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject ui = GameObject.Find("TextTips");
        ui.GetComponent<GUIText>().text = "";
        GameObject.Find("TextTips").GetComponent<GUIText>().text = "";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
