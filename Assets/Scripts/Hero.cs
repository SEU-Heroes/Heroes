using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Hero : MonoBehaviour {

    public enum state { still, jumping, floating, dizzy, falling, blocking, moving, BeforeAT, FirstHalfAfterAT, LastHalfAfterAT, unControlable, acting };

    [HideInInspector]
    public HeroAttr attr;

    [HideInInspector]
    public bool isFacingLeft = false;

    public float jumpForce;

    float moveTime;
    Vector3 aimPosition;
    float speed;
    public float moveSpeed;
    float actTime;

    public state nowState = state.unControlable;

    Skill nowSkill;

    Animator anim;

    float scaleX;

    Transform groundCheck;

    bool isJumping;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        scaleX = transform.localScale.x;
        groundCheck = transform.Find("groundCheck");
	}

    void Update()
    {
        Debug.Log(nowState);
        if (!(isJumping = checkJump()))
        {
            if (nowState == state.jumping)
            {
                nowState = state.still;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            List<InputReceiver.dir> list = new List<InputReceiver.dir>();
            list.Add(InputReceiver.dir.right);
            handInput(list);
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            List<InputReceiver.dir> list = new List<InputReceiver.dir>();
            list.Add(InputReceiver.dir.up);
            handInput(list);
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
        int skillable = isSkillable();
        if (skillable == 0)
        {
            return;
        }
        else
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
        o.GetComponent<Trigger>().skill = skill;
        nowSkill = skill;
        skill.start(this);
    }

    Skill checkSkill(List<InputReceiver.dir> input)
    {
        return attr.skills.checkSkill(input);
    }

    /// <summary>
    /// 判断当前是否能释放技能
    /// </summary>
    /// <returns>0：不能释放 1：能正常释放 2：能连击释放</returns>
    int isSkillable()
    {
        if (nowState == state.still || nowState == state.jumping || nowState == state.moving || nowState == state.blocking )
        {
            return 1;
        }
        else if(nowState == state.FirstHalfAfterAT)
        {
            return 2;
        }
        return 0;
    }

    bool isMoveable()
    {
        if (nowState == state.still || nowState == state.jumping || nowState == state.moving || nowState == state.blocking)
            return true;
        return false;
    }

    bool isJumpable()
    {
        if (nowState == state.still || nowState == state.moving || nowState == state.blocking)
        {
            return true;
        }
        return true;
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

    bool checkJump()
    {
        return !Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void Jump()
    {
        if (isJumpable())
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            nowState = state.jumping;
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

    public void HPReduce(int num)
    {
        if (nowState == state.blocking)
        {
            int realReduce = num - HeroAttr.defenseForce;
        }
        attr.HP -= num;
        GameManager.getInstance().HPReduce(this, num);
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

    public void skillEnd()
    {
        nowSkill.end(this);
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