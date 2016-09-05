using UnityEngine;
using System.Collections;

class TianFengHuoWuSkill : MonoBehaviour {

    public GameObject hitPrefab;
    [HideInInspector]
    public Skill _skill;
    public Hero _hero;
    public float existTime;

    private string selfTag;
    private string enemyTag;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        selfTag = _hero.tag;
        enemyTag = selfTag == Tags.player01 ? Tags.player02 : Tags.player01;
        if (collision.gameObject.tag == enemyTag || collision.gameObject.tag == Tags.boundary)
        {
            if(collision.gameObject.tag == enemyTag) { 
                Vector3 randomPos = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
                Instantiate(hitPrefab, collision.transform.position + randomPos, collision.transform.rotation);
                _skill._hit(collision.gameObject.GetComponent<Hero>());
            }
            GetComponent<Animator>().SetTrigger("Dead");
            Destroy(gameObject, 0.3f);
        }
    }

}
