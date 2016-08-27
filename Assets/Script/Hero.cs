using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Hero : MonoBehaviour {

    public enum state { still, jumping, floating, dizzy, falling, blocking, moving, BeforeAT, FirstHalfAfterAT, LastHalfAfterAT, unControlable, acting };

    [HideInInspector]
    public HeroAttr attr;

    [HideInInspector]
    public bool isFacingLeft = false;

    float moveTime;
    Vector3 aimPosition;
    float speed;
    public float moveSpeed;
    float actTime;

    state nowState = state.unControlable;

    Skill nowSkill;

    Animator anim;

    float scaleX;

	// Use this for initialization
	void Start () {
        isFacingLeft = false;
        anim = gameObject.GetComponent<Animator>();
        scaleX = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            handDirection(InputReceiver.joyDir.left);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            handDirection(InputReceiver.joyDir.down);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            handDirection(InputReceiver.joyDir.right);
        }
        else
        {
            stay();
        }
	}

    void FixedUpdate()
    {
        turnBack();
        if (moveTime != 0)
        {
            moveTime -= Time.deltaTime;
            if (moveTime < 0)
                moveTime = 0;
            move();
        }

        if (nowState == state.acting)
        {
            actTime += Time.deltaTime;
            nowSkill.update(this, actTime);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    public void handInput(List<InputReceiver.dir> input)
    {
        if (isSkillable())
        {
            Skill skill = checkSkill(input);
            if (skill != null)
            {
                startSkill(skill);
                actTime = 0;
            }
        }
    }

    public void setFacing(bool isLeft)
    {
        isFacingLeft = isLeft;
    }

    public void handDirection(InputReceiver.joyDir dir)
    {
        if (dir == InputReceiver.joyDir.down)
        {
            if(isDefensable())
            {
                defense();
            }
        }
        else if (dir == InputReceiver.joyDir.none || dir == InputReceiver.joyDir.up)
        {
            stay();
        }
        else
        {
            if (isMoveable())
            {
                nowState = state.moving;
                GetComponent<Rigidbody2D>().velocity = new Vector2(InputReceiver.joyDir.left == dir ? -moveSpeed : moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                isFacingLeft = InputReceiver.joyDir.left == dir ? true : false;
            }
        }
    }

    public void touchStay(List<InputReceiver.dir> input)
    {

    }

    public void StartGame()
    {
        nowState = state.still;
    }

    void startSkill(Skill skill)
    {
        GameObject o = GameManager.getInstance().Instantiate(GameManager.factory.getSkillObject(attr.heroId, skill.skillId), transform.localPosition + skill.offset, Quaternion.identity);
        o.transform.Rotate(isFacingLeft ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0));
        if (!skill.isCreator)
        {
            o.transform.parent = transform;
        }
        nowSkill = skill;
        skill.useHero = this;
        skill.start(this);
    }

    Skill checkSkill(List<InputReceiver.dir> input)
    {
        return attr.skills.checkSkill(input);
    }

    bool isSkillable()
    {
        if (nowState == state.still || nowState == state.jumping || nowState == state.moving || nowState == state.blocking || nowState == state.FirstHalfAfterAT)
            return true;
        return false;
    }

    bool isMoveable()
    {
        if (nowState == state.still || nowState == state.jumping || nowState == state.moving || nowState == state.blocking)
            return true;
        return false;
    }

    bool isDefensable()
    {
        if (nowState == state.still || nowState == state.moving || nowState == state.blocking || nowState == state.FirstHalfAfterAT)
            return true;
        return false;
    }

    bool Hitable()
    {
        if (nowState == state.still || nowState == state.jumping || nowState == state.moving || nowState == state.blocking || nowState == state.FirstHalfAfterAT || nowState == state.floating || nowState == state.dizzy || nowState == state.BeforeAT || nowState == state.LastHalfAfterAT || nowState == state.acting)
            return true;
        return false;
    }

    void move()
    {
        transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, aimPosition.x, speed * Time.deltaTime), transform.localPosition.y, transform.localPosition.z);
    }

    void stay()
    {
        if(nowState == state.moving)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    }

    public void move(int distance, float time)
    {
        moveTime = time;
        speed = distance / time;
        if (isFacingLeft)
        {
            aimPosition = new Vector3(transform.localPosition.x - distance, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            aimPosition = new Vector3(transform.localPosition.x + distance, transform.localPosition.y, transform.localPosition.z);
        }
    }

    public void hit(Hero h)
    {
        RageAdd(nowSkill.AddRage);
    }

    public void actEnd()
    {
        nowSkill.end(this);
    }

    void RageAdd(int addRage)
    {
        int count;
        attr.Rage += addRage;
        if (attr.Rage > HeroAttr.maxRage)
        {
            count = attr.Rage - HeroAttr.maxRage;
            attr.Rage = HeroAttr.maxRage;
            if (attr.fullRage >= 3)
            {
                attr.fullRage = 3;
            }
            else
            {
                attr.fullRage++;
                attr.Rage = count;
            }
        }
    }

    void turnBack()
    {
        if(isFacingLeft)
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    void defense()
    {
        nowState = state.blocking;
        anim.SetBool("defense", true);
    }
}