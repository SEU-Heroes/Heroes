using UnityEngine;
using System.Collections;

/*
 * 需求：
 * 能按照角色ID取得角色
 * 按照角色ID获得技能物体
 */

/*
 * 版本 V1.0 能获得角色和技能物体
 */

class PrefabManager : MonoBehaviour {

    static public PrefabManager instance;

    public GameObject[] _heroesAI;//AI角色的预制资源
    public GameObject[] _heroes;//各种角色的预制资源
    public GameObject _GestureDisplay;//展示轨迹的预制资源

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 根据角色ID得到角色
    /// </summary>
    /// <param name="heroId"></param>
    /// <param name="AI">是否是带AI的角色</param>
    /// <returns>相应的角色</returns>
    /// 作者：胡皓然
    public GameObject GetHero(int heroId, bool AI)
    {
        if (AI)
        {
            return _heroesAI[heroId];
        }
        return _heroes[heroId];
    }

    /// <summary>
    /// 根据轨迹复杂等级获得一个随机的轨迹
    /// </summary>
    /// <param name="level">技能复杂等级</param>
    /// <returns>随机轨迹</returns>
    /// 作者：胡皓然
    public SkillGesture GetGesture(int level)
    {
        return GameManager.GetInstance()._GestureManager[level].GetComponent<GestureManager>().GetRandGesture();
    }
}
