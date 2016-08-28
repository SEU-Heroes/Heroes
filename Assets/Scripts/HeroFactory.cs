using UnityEngine;
using System.Collections;

public class HeroFactory : MonoBehaviour {

    public GameObject[] heroes;
    public GameObject[] skillCreator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject getHero(int heroId)
    {
        return heroes[heroId];
    }

    public GameObject getSkillObject(int heroId, int SkillId)
    {
        return skillCreator[SkillId];
    }
}
