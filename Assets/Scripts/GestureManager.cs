using UnityEngine;
using System.Collections;

class GestureManager : MonoBehaviour {

    public Sprite[] _gestures;
    public int level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public SkillGesture GetRandGesture()
    {
        int id = Random.Range(0, _gestures.Length);
        return new SkillGesture(_gestures[id], id, level);
    }
}
