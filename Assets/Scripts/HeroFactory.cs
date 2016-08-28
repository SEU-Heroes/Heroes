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

public class HeroFactory : MonoBehaviour {

    static public HeroFactory instance;

    public GameObject[] _heroes;
    public GameObject[] _skillCreator;

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
    /// <returns>相应的角色</returns>
    /// 作者：胡皓然
    public GameObject GetHero(int heroId)
    {
        var temp = _heroes[heroId];
        return _heroes[heroId];
    }

    /// <summary>
    /// 根绝角色ID和技能ID得到技能物体
    /// </summary>
    /// <param name="heroId"></param>
    /// <param name="SkillId"></param>
    /// <returns>技能物体</returns>
    /// 作者：胡皓然
    public GameObject GetSkillObject(int heroId, int SkillId)
    {
        return _skillCreator[SkillId];
    }
}
