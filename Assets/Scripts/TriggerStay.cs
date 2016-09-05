using UnityEngine;
using System.Collections;

// 暂时用不到这个类，不用管
class TriggerStay : MonoBehaviour
{

    public GameObject hitPrefab;

    [HideInInspector]
    public Skill skill;
    public float existTime;

    public void Start()
    {
        Invoke("Destroy", existTime);
        InvokeSkillGrowUp(0.2f);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.player02)
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

    void InvokeSkillGrowUp(float time)
    {
        Invoke("SkillGrowUp", time);
    }

    void SkillGrowUp()
    {
        GetComponent<Animator>().SetTrigger("GrowUp");
    }

}
