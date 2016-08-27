using UnityEngine;
using System.Collections;

public class TestAnimation : MonoBehaviour {

    private Rigidbody2D rigidbody;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {
        // attack
        if (Input.GetKeyDown(KeyCode.J))
            anim.SetBool("normalAttack", true);
        if(Input.GetKeyUp(KeyCode.J))
            anim.SetBool("normalAttack", false);

        if (Input.GetKeyDown(KeyCode.K))
            anim.SetTrigger("xuanfengtui");

        // move
        float h = Input.GetAxis("Horizontal");
        if (h >= 0.001f)
        {
            rigidbody.velocity = new Vector2(5.0f, 0);
            transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        }
        else if (h <= -0.001f)
        {
            rigidbody.velocity = new Vector2(-5.0f, 0);
            transform.localScale = new Vector3(-1.7f, 1.7f, 1.7f);
        }
        else
            rigidbody.velocity = new Vector2(0, 0);
    }
}
