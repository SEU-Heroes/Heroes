using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {


	/// <summary>
	/// Raises the click event.
	/// </summary>
	/// writer:Liu Yueqi
	public void OnClick()
	{
		//return to the last scene
        //人机模式下返回到模式选择界面
        if ((ModeSelect.firstMode + 2) % 5 == 2)
        {
            GameManager.GetInstance().ChangeScene("ModleSelectScene");
            //Application.LoadLevel("ModleSelectView");
        }
         //局域网模式下返回到局域网对战方式界面
        else if ((ModeSelect.firstMode + 2) % 5 == 3)
        {
            GameManager.GetInstance().ChangeScene("NetWorkScene");
        }
		//Debug.Log("return to the last scene.");
	}
	
}
