using UnityEngine;
using System.Collections;

public class CheckTowards : MonoBehaviour {

    static public bool turnToLeft = false;

    void Awake()
    {
        GetComponent<Hero>()._attr = XmlOperate.GetHeroInformation("JiXiaoke");
    }

	// Update is called once per frame
	void Update () {
	}

    static public bool IsAtLeft(Transform source, Transform target)
    {
        if (source.position.x > target.position.x)
            return false;
        return true;
    }
}
