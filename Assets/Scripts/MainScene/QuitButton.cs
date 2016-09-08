using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

    public void OnButtonClick() {
        GameManager.GetInstance().QuitGame();
    }
}  
