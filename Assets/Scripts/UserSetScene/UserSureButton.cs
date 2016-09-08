using UnityEngine;
using System.Collections;

public class UserSureButton : MonoBehaviour
{


    public void OnButtonClick()
    {
        //血量条数量
        GameManager.GetInstance()._HPNum = (LifeButton._instance._LifeNum + 1);
        //回合时间
        GameManager.GetInstance()._roundTime = (TimeButton._instance._TimeNumber);
        //各种模式下的难度
        GameManager.GetInstance()._diffi =  GetLevel();
        //地图选择
        MapSelect();
        //点击进入战斗场景
        GameManager.GetInstance().ChangeScene("BattleScene-boat");
    }

    /// <summary>
    /// 当处于人机对战模式下确定游戏当前模式难度
    /// </summary>
    /// <returns></returns>
    GameManager.difficulty GetLevel()
    {
        if ((ModeSelect.firstMode + 2) % 5==2)
        {
            switch (LevelButton._instance._levelNum + 1)
            {
                case 1:
                    return GameManager.difficulty.easy;
                case 2:
                    return GameManager.difficulty.middle;
                case 3:
                    return GameManager.difficulty.difficult;
                default:
                    return GameManager.difficulty.none;

            }
        }
        else
        {
            return GameManager.difficulty.none;
        }
    }
    private void MapSelect() {
        if (ImageChange._instance._BGPicture == 0)
        {
            GameManager.GetInstance()._nowMap = GameManager.map.boat;
        }
        else {
            GameManager.GetInstance()._nowMap = GameManager.map.seu;
        }
    }
}
