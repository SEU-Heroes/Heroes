using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 需求：
 * 能完成移动跳跃操作
 * 能处理玩家的输入
 * 能释放技能
 */

/*
 * 版本 V1.0 能处理玩家输入并释放技能或者移动跳跃
 */

class Hero : MonoBehaviour {

    public int _id;//玩家ID

    public GameObject[] _skillCreator;//角色技能的生成物体

    //角色状态枚举
    public enum state { still, jumping, floating, dizzy, falling, blocking, moving, BeforeAT, FirstHalfAfterAT, LastHalfAfterAT, unControlable, acting };

    //角色当前状态
    public state _nowState = state.unControlable;//角色目前的状态
    [HideInInspector]
    public HeroAttr _attr;//角色属性
    [HideInInspector]
    public bool _isFacingLeft = false;//角色是否朝向左边
    Skill _nowSkill;//正在释放的技能
    bool isJumping;//是否正在跳跃

    //角色固定属性
    public float _jumpForce;//跳跃力
    public float _moveSpeed;//角色移动速度
    public float _backJumpForce;//后跳力

    //算法所需变量（不用管）
    Vector3 _aimPosition;//本次移动的目的地
    float _moveTime;//本次移动需要的时间
    float _speed;//本次移动需要的速度
    float _actTime;//本次动作已经用掉的时间
    float _scaleX;//角色的X轴缩放值

    //所需引用
    Animator _anim;//角色的动画控制器
    Transform _groundCheck;//角色脚底的射线检测器

	// Use this for initialization
	void Start () {
        _anim = gameObject.GetComponent<Animator>();
        _groundCheck = transform.Find("groundCheck");
        //取得角色的X轴缩放值
        _scaleX = transform.localScale.x;
	}

    void Update()
    {
        //判断是否在空中，并在落地瞬间将状态改为静止
        if (!(isJumping = CheckJump()))
        {
            if (_nowState == state.jumping)
            {
                Debug.Log("111");
                _nowState = state.still;
            }
        }

        //PC机上操作检测
        if (Input.GetKeyDown(KeyCode.D))
        {
            HandSkill(_attr._skills.FindSkillByName("XuanFengTui"));
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            HandSkill(_attr._skills.FindSkillById(0));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            HandSkill(_attr._skills.FindSkillByName("BackJump"));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            HandSkill(_attr._skills.FindSkillByName("ShanXi"));
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            HandSkill(_attr._skills.FindSkillByName("HuoQiu"));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            HandSkill(_attr._skills.FindSkillByName("HuoYanZhangKong"));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            HandSkill(_attr._skills.FindSkillByName("TianFengHuoWu"));
        }
    }

    void FixedUpdate()
    {
        //每一帧检测角色的朝向
        TurnBack();
        //如果角色正在移动，改变角色的移动状态
        if (_moveTime != 0)
        {
            _moveTime -= Time.deltaTime;
            if (_moveTime < 0)
                _moveTime = 0;
            Move();
        }
        //放技能时每一帧改变角色状态
        if (_nowState == state.acting || _nowState == state.BeforeAT || _nowState == state.LastHalfAfterAT || _nowState == state.FirstHalfAfterAT)
        {
            Debug.Log(_nowState);
            _actTime += Time.deltaTime;
            _nowSkill._update(this, _actTime);
        }
    }

    /// <summary>
    /// 尝试释放一个技能
    /// </summary>
    /// <param name="skill"></param>
    public void HandSkill(Skill skill)
    {
        //判断是否能释放技能
        int skillable = IsSkillable();
        if (skillable == 0)
        {
            return;
        }
        else
        {
            if(skillable == 2)
            {
                _nowSkill._end(this);
            }
            StartSkill(skill);
        }
    }

    /// <summary>
    /// 设置角色的朝向
    /// </summary>
    /// <param name="isLeft"></param>
    /// 作者：胡皓然
    public void SetFacing(bool isLeft)
    {
        _isFacingLeft = isLeft;
    }

    /// <summary>
    /// 处理方向输入
    /// </summary>
    /// <param name="dir">输入的方向</param>
    /// 作者：胡皓然
    public void HandDirection(InputReceiver.joyDir dir)
    {
        if (dir == InputReceiver.joyDir.down)
        {
            //防御部分
            if(IsDefensable())
            {
                Defense();
            }
        }
        else if (dir == InputReceiver.joyDir.none || dir == InputReceiver.joyDir.up)
        {
            //无操作部分
            Stay();
        }
        else
        {
            //移动部分
            if (IsMoveable())
            {
                _nowState = state.moving;
                GetComponent<Rigidbody2D>().velocity = new Vector2(InputReceiver.joyDir.left == dir ? -_moveSpeed : _moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                _isFacingLeft = InputReceiver.joyDir.left == dir ? true : false;
            }
        }
    }

    /// <summary>
    /// 处理当玩家的手指在某一点停留时间过长的情况
    /// </summary>
    /// <param name="input">玩家目前已经输入的序列</param>
    /// 作者：胡皓然
    public void TouchStay(List<InputReceiver.dir> input)
    {

    }

    /// <summary>
    /// 游戏开始时设置角色状态
    /// </summary>
    /// 作者：胡皓然
    public void StartGame()
    {
        _nowState = state.still;
    }

    /// <summary>
    /// 释放技能时角色移动
    /// </summary>
    /// <param name="distance">移动距离</param>
    /// <param name="time">所需时间</param>
    /// 作者：胡皓然
    public void Move(Vector3 destination, float time)
    {
        _moveTime = time;
        _speed = destination.magnitude / time;
        if (_isFacingLeft)
        {
            _aimPosition = new Vector3(transform.localPosition.x - destination.x, transform.localPosition.y + destination.y, transform.localPosition.z);
        }
        else
        {
            _aimPosition = new Vector3(transform.localPosition.x + destination.x, transform.localPosition.y + destination.y, transform.localPosition.z);
        }
    }

    //跳跃
    public void Jump(Vector2 jumpDir)
    {
        if (IsJumpable())
        {
            GetComponent<Rigidbody2D>().AddForce(jumpDir);
            _nowState = state.jumping;
        }
    }

    /// <summary>
    /// 技能攻击到敌人后增加怒气值
    /// </summary>
    /// <param name="h">攻击到的角色</param>
    /// 作者：胡皓然
    public void Hit(Hero h)
    {
        RageAdd(_nowSkill._addRage);
    }

    /// <summary>
    /// 设置角色眩晕（硬直）
    /// </summary>
    /// <param name="dizzyTimeInMs">硬直时间，以毫秒（ms）计</param>
    public void StartDizzy(float dizzyTimeInMs)
    {
        if (_nowState == state.acting || _nowState == state.BeforeAT || _nowState == state.FirstHalfAfterAT || _nowState == state.LastHalfAfterAT)
        {
            Destroy(_nowSkill._instantiation);
            _nowSkill._end(this);
        }
        _nowState = state.dizzy;
        _anim.SetBool("Dizzy", true);
        Invoke("CancelDizzyState", dizzyTimeInMs / 1000f);
    }


    /// <summary>
    /// 技能释放完成，由动画事件调用
    /// </summary>
    /// 作者：胡皓然
    public void skillEnd()
    {
        _nowSkill._end(this);
    }

    /// <summary>
    /// 血量减少
    /// </summary>
    /// <param name="num"></param>
    /// 作者：胡皓然
    public void HPReduce(int num)
    {
        int realReduce = num;
        if (_nowState == state.blocking)
        {
            realReduce = num - GameManager._defenseForce;
        }
        _attr._HP -= realReduce;
        GameManager.GetInstance().HPReduce(_id, num);
    }

    /// <summary>
    /// 气力值增加
    /// </summary>
    /// <param name="addRage"></param>
    /// 作者：胡皓然
    public void RageAdd(int addRage)
    {
        int count;
        _attr._Rage += addRage;
        if (_attr._Rage > GameManager._maxRage)
        {
            count = _attr._Rage - GameManager._maxRage;
            _attr._Rage = GameManager._maxRage;
            if (_attr._fullRage >= 3)
            {
                _attr._fullRage = 3;
            }
            else
            {
                _attr._fullRage++;
                _attr._Rage = count;
            }
        }
        MainScene._instance.SPAdd(this, addRage);
    }

    /// <summary>
    /// 角色死掉，删除场景上的该角色
    /// </summary>
    public void HeroDie()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 开始释放技能
    /// </summary>
    /// <param name="skill">需要释放的技能</param>
    /// 作者：胡皓然
    void StartSkill(Skill skill)
    {
        _nowSkill = skill;
        skill._start(this);
        _actTime = 0;
    }

    // 将角色的状态从眩晕（硬直）中恢复
    void CancelDizzyState()
    {
        _anim.SetBool("Dizzy", false);
        _nowState = state.still;
    }

    /// <summary>
    /// 判断当前是否能释放技能
    /// </summary>
    /// <returns>0：不能释放 1：能正常释放 2：能连击释放</returns>
    public int IsSkillable()
    {
        if (_nowState == state.still || _nowState == state.moving || _nowState == state.blocking||_nowState == state.jumping)
        {
            return 1;
        }
        else if (_nowState == state.FirstHalfAfterAT)
        {
            return 2;
        }
        return 0;
    }

    //判断是否可以移动
    public bool IsMoveable()
    {
        if (_nowState == state.still || _nowState == state.moving || _nowState == state.blocking)
            return true;
        return false;
    }

    //判断是否可以跳跃
    bool IsJumpable()
    {
        if (_nowState == state.still || _nowState == state.moving || _nowState == state.blocking)
        {
            return true;
        }
        return true;
    }

    //判断是否可以防御
    bool IsDefensable()
    {
        if (_nowState == state.still || _nowState == state.moving || _nowState == state.blocking || _nowState == state.FirstHalfAfterAT)
            return true;
        return false;
    }

    //判断是否可以被攻击
    bool Hitable()
    {
        if (_nowState == state.still || _nowState == state.jumping || _nowState == state.moving || _nowState == state.blocking || _nowState == state.FirstHalfAfterAT || _nowState == state.floating || _nowState == state.dizzy || _nowState == state.BeforeAT || _nowState == state.LastHalfAfterAT || _nowState == state.acting)
            return true;
        return false;
    }

    /// <summary>
    /// 技能移动的算法辅助函数
    /// </summary>
    /// 作者：胡皓然
    void Move()
    {
        transform.localPosition = new Vector3
            (Mathf.Lerp(transform.localPosition.x, _aimPosition.x, _speed * Time.deltaTime),
            Mathf.Lerp(transform.localPosition.y, _aimPosition.y, _speed * Time.deltaTime),
            transform.localPosition.z);
    }

    //停止角色左右移动
    void Stay()
    {
        if (_nowState == state.moving)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    }

    //防御
    void Defense()
    {
        _nowState = state.blocking;
        _anim.SetBool("defense", true);
    }

    //判断是否在跳跃
    bool CheckJump()
    {
        return !Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    //按照当前朝向设置角色UI朝向
    void TurnBack()
    {
        if (_isFacingLeft)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
    }
}