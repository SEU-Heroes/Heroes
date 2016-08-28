using UnityEngine;
using System.Collections;

/*
  * 需求：相机随着角色移动
  */

/*
  * 版本 V1.00 修改时间2016.8.26 修改内容-能够平滑跟踪角色并改变可视大小
  */

class FollowPlayers : MonoBehaviour {

    private Camera camera;

    [HideInInspector]
    public Transform player1;
    [HideInInspector]
    public Transform player2;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Start()
    {/*
        player1 = GameManager.GetInstance().GetMainPlayer().GetHero().gameObject.transform;
        player2 = GameManager.GetInstance().GetOtherPlayer().GetHero().gameObject.transform;*/
    }

    /// <summary>
    /// 每帧调用，进行Camera跟随角色移动的操作
    /// 保证相机处于角色中心，可视范围随角色移动而平滑变化
    /// 另外也要确保背景外部不能被摄像机拍到
    /// </summary>
    /// 作者：庄亦舟
    void Update ()
    {
        // 计算相机可视范围并限制最低大小
        // size = 0.23 * x + 1.12
        camera.orthographicSize = 0.23f * (Mathf.Abs(player1.position.x - player2.position.x)) + 1.12f;
        camera.orthographicSize = camera.orthographicSize < 2.5f ? 2.5f : camera.orthographicSize;

        // 平滑移动相机位置
        // 下述位置由 
        // x = (p1.x + p2.x) / 2;
        // y = camera.size * 3 / 5  - 3;
        // 得出
        transform.position = 
            Vector3.Lerp(
            transform.position,
            new Vector3(
            (player1.position.x + player2.position.x) / 2, // x
            (0.6f * camera.orthographicSize - 3), // y
            -10), // z
            0.05f);

        // 防止相机拍到地图外的区域
        float size = camera.orthographicSize;
        if (transform.position.y - size < -5)
            transform.position = new Vector3(transform.position.x, -5 + size, -10);
        if (transform.position.x - size * 16 / 9 < -8.5f)
            transform.position = new Vector3(-8.5f + size * 16 / 9, transform.position.y, -10);
        if (transform.position.x + size * 16 / 9 > 8.5f)
            transform.position = new Vector3(8.5f - size * 16 / 9, transform.position.y, -10);
    }

    public void setPlayer(Hero player)
    {
        if (player1 != null)
        {
            player2 = player.gameObject.transform;
        }else{
            player1 = player.gameObject.transform;
        }
    }
}
