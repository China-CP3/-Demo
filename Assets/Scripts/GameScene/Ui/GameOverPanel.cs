using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverPanel : BasePanel
{
    public Text txtWinOrLose;
    public Text txtMoneyNum;
    public Button btnSure;
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UiMgr.Instance.HidePanel<GameOverPanel>();
            UiMgr.Instance.HidePanel<GamePanel>();
            //�л�����
            SceneManager.LoadScene("BeginScene");
            GameLevelMgr.Instance.ClearInfo();
        });
    }
    public void InitInfo(int money,bool isWin)
    {
        txtWinOrLose.text = isWin ? "���ʤ����" : "�ź��ܱ���";
        txtMoneyNum.text = money.ToString();
        GameDataMgr.Instance.playerData.GoldNum+=money;
        GameDataMgr.Instance.SavePlayerData();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState=CursorLockMode.None;//������Ϸ�������� �������
    }
}
