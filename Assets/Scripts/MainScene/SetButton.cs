using UnityEngine;
using System.Collections;

public class SetButton : MonoBehaviour {

    public void OnButtonClick()
    {
        GameManager.GetInstance().ChangeScene("SetScene");
    }
}
