using UnityEngine;
using System.Collections;

class TianFengHuoWuBorner : MonoBehaviour {

    public GameObject prefab;
    [HideInInspector]
    public Skill skill;
    public Hero hero;
    public float existTime;

    public float _speed = 5;

    private int num = 6;

    void Start()
    {
        Destroy(gameObject, 2.0f);
        Invoke("StartBorning", 0.6f);
    }

    void StartBorning()
    {
        StartCoroutine(BornCollider());
    }

    IEnumerator BornCollider()
    {
        for(int i = 0; i < num; i++) {
            yield return new WaitForSeconds(0.1f);
            GameObject go = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
            go.transform.Rotate(new Vector3(0, 0, (hero._isFacingLeft?-1:1) * i * 12 + 45)); 
            go.GetComponent<Rigidbody2D>().velocity = new Vector3((hero._isFacingLeft?-1:1) * 1.7f / 5 * i, -1).normalized * _speed;
            go.GetComponent<TianFengHuoWuSkill>()._skill = skill;
            go.GetComponent<TianFengHuoWuSkill>()._hero = hero;
        }
    }

}
