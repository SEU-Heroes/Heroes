using UnityEngine;
using System.Collections;

public class BackBtuoon : MonoBehaviour {
    //点击返回角色选择界面
    public void OnButtonClick()
    {
        GameManager.GetInstance().ChangeScene("HeroSelectScene");
    }
}
