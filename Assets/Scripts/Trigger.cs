using UnityEngine;
using System.Collections;

class Trigger : MonoBehaviour {

    public GameObject hitPrefab;
    [HideInInspector]
    public Skill skill;
    public Hero hero;
    public float existTime;

    private string selfTag;
    private string enemyTag;

    public void Start()
    {
        Invoke("Destroy", existTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        selfTag = hero.tag;
        enemyTag = selfTag == Tags.player01 ? Tags.player02 : Tags.player01;
        if (collision.gameObject.tag == enemyTag)
        {
            Vector3 randomPos = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            Instantiate(hitPrefab, collision.transform.position + randomPos, collision.transform.rotation);
            skill._hit(collision.gameObject.GetComponent<Hero>());
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

}