using UnityEngine;
using System.Collections;

class SkillGesture{

    public Sprite _gestureSprite;
    public int _id;
    public int _level;

    public SkillGesture(Sprite gestureSprite, int id, int level)
    {
        _gestureSprite = gestureSprite;
        _id = id;
        _level = level;
    }
}
