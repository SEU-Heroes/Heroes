using UnityEngine;
using System.Collections;

class Trigger : MonoBehaviour {

    public GameObject hitPrefab;
    [HideInInspector]
    public Skill skill;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.player02)
        {
            skill.hit(collision.gameObject.GetComponent<Hero>());
            Vector3 randomPos = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            Instantiate(hitPrefab, collision.transform.position + randomPos, collision.transform.rotation);
        }
    }

}
