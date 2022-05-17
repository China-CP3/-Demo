using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePanel : BasePanel
{
    private bool checkInput=false;
    public GameObject objSetting;
    public GameObject objBot;

    public bool IsSettingBtnOpen;

    public Image imgSettingBk;
    public Image imgHp;

    public Text txtHp;
    public Text txtWave;
    public Text txtGoldNum;

    public Button btnSetting;
    public Button btnStop;
    public Button btnContinue;
    public Button btnQuit;
    public Button btnCancel;
    
    public List<BtnTower> towerBtns=new List<BtnTower>();//�������Ľű�

    public TowerPoint nowSelTowerPoint;

    public override void Init()
    {
        Cursor.lockState = CursorLockMode.Confined;//һ��ʼ��������� ��Ȼ�����ܵ�������
        imgSettingBk.gameObject.SetActive(false);
        objSetting.gameObject.SetActive(false);
        //objBot.gameObject.SetActive(false);

        btnSetting.onClick.AddListener(() =>
        {
            if(IsSettingBtnOpen)
            {
                imgSettingBk.gameObject.SetActive(false);
                objSetting.gameObject.SetActive(false);
                IsSettingBtnOpen = false;
            }
            else
            {              
                imgSettingBk.gameObject.SetActive(true);
                objSetting.gameObject.SetActive(true);
                IsSettingBtnOpen = true;
            }
        });
        btnStop.onClick.AddListener(() =>
        {
            Time.timeScale = 0;
        });
        btnContinue.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
        });
        btnQuit.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            UiMgr.Instance.HidePanel<GamePanel>();
            GameLevelMgr.Instance.ClearInfo();
            SceneManager.LoadScene("BeginScene");
        });
        btnCancel.onClick.AddListener(() =>
        {
            imgSettingBk.gameObject.SetActive(false);
            objSetting.gameObject.SetActive(false);
            IsSettingBtnOpen = false;
        });
    }
    /// <summary>
    /// ����Ѫ��
    /// </summary>
    /// <param name="hp"></param>
    public void UpdateTowerHp(int hp)
    {
        imgHp.fillAmount = hp*0.01f;
        txtHp.text = hp.ToString();

    }
    /// <summary>
    /// ���µ�ǰ�����������
    /// </summary>
    /// <param name="nowNum"></param>
    /// <param name="maxNum"></param>
    public void UpdateWaveNum(int nowNum,int maxNum)
    {
        txtWave.text= nowNum+"/"+maxNum;

    }
    /// <summary>
    /// ���½��
    /// </summary>
    public void UpdateMoney(int money)
    {
        txtGoldNum.text= money.ToString();
    }
    /// <summary>
    /// ���µ�ǰѡ�е�����������һЩ�仯
    /// </summary>
    public void UpdateTowerPoint(TowerPoint towerPoint)
    {
        if(towerPoint==null)
        {
            objBot.gameObject.SetActive(false);
            checkInput = false;
        }
        else
        {
            checkInput = true;
            objBot.gameObject.SetActive(true);
            //�������������Ϣ ���������ϵ���ʾ����
            nowSelTowerPoint = towerPoint;
            if (nowSelTowerPoint.nowTowerInfo == null)//������Ϊ�� ˵����������㻹û����
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(true);
                    towerBtns[i].InitInfo(nowSelTowerPoint.chooseIds[i], "���ּ�" + (i + 1));
                }
            }
            else
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(false);
                }
                towerBtns[1].gameObject.SetActive(true);
                towerBtns[1].InitInfo(towerPoint.nowTowerInfo.next, "�ո��");
            }
        }
        
    }
    private void Update()
    {
        base.Update();
        if (!checkInput)
            return;
        //������  �������� ����
        if (nowSelTowerPoint.nowTowerInfo==null)//info�Ǵ�json��ȡ��������  ���������ﹺ���  ��������Ϊ��  ���˲Ų�Ϊ��   Ϊ��˵����û�����
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIds[0]);//chooseIds������д����id 1,4,7 ��json���ȡlist�е�1,4,7����������
            }else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIds[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.chooseIds[2]);
            }
        }
        else//˵������� ��������� 
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreateTower(nowSelTowerPoint.nowTowerInfo.next);
            }
        }
    }
}
