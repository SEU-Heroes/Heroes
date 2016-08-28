using UnityEngine;
using System.Collections;

public class DestroyNormalHit : MonoBehaviour {

	/// <summary>
    /// 被攻击效果出现后1秒从场景移除，节省内存
    /// </summary>uop啊
	void Start () {
        Destroy(gameObject, 1f);
	}

}
