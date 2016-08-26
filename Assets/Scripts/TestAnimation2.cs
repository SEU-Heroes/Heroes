using UnityEngine;
using System.Collections;

public class TestAnimation2 : MonoBehaviour {

    private Rigidbody2D rigidbody;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move
        float h = Input.GetAxis("Horizontal2");
        if (h >= 0.001f)
        {
            anim.SetBool("move", true);
            rigidbody.velocity = new Vector2(5.0f, 0);
            transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        }
        else if (h <= -0.001f)
        {
            anim.SetBool("move", true);
            rigidbody.velocity = new Vector2(-5.0f, 0);
            transform.localScale = new Vector3(-1.7f, 1.7f, 1.7f);
        }
        else
        {
            anim.SetBool("move", false);
            rigidbody.velocity = new Vector2(0, 0);
        }

        // attack
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            anim.SetBool("move", false);
            anim.SetTrigger("shoudao");
        }

    }
}
