using UnityEngine;
using System.Collections;

public class FireBallAnimController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("GrowUp", 0.2f);
	}

    void GrowUp()
    {
        GetComponent<Animator>().SetTrigger("GrowUp");
    }

}
