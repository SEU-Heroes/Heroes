using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public void OnButtonClick()
    {
        GameManager.GetInstance().ChangeScene("ModleSelectScene");
    }
}
