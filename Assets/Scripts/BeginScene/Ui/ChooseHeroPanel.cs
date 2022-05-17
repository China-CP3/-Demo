using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ChooseHeroPanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnUnLock;
    public Button btnStart;
    public Button btnBack;
    public Text txtGoldNum;
    public Text txtUnLock;
    public Text txtName;

    private Transform heroPos;
    private GameObject heroObj;
    private int nowIndex;
    private RoleInfo nowRoleData;
    public override void Init()
    {
        txtGoldNum.text = GameDataMgr.Instance.playerData.GoldNum.ToString();
        heroPos = GameObject.Find("HeroPos").transform;
        ChangeHero();//����ѡ�ǳ����н�ɫģ��

        btnBack.onClick.AddListener(() =>
        {
            UiMgr.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main.GetComponent<BeginCameraAnimator>().TurnRight(() =>
            {
                UiMgr.Instance.ShowPanel<BeginPanel>();
            });
        });
        btnStart.onClick.AddListener(() =>
        {
            //��¼��ǰѡ��Ľ�ɫ ����ѡ�ǽ���
            GameDataMgr.Instance.nowRoleChoose = nowRoleData;
            UiMgr.Instance.HidePanel<ChooseHeroPanel>();
            UiMgr.Instance.ShowPanel<ChooseScenePanel>();
        });
        btnLeft.onClick.AddListener(() =>
        {
            if (nowIndex == 0)
                nowIndex = GameDataMgr.Instance.roleInfoList.Count - 1;
            else
            {
                nowIndex--;
            }
            ChangeHero();
        });
        btnRight.onClick.AddListener(() =>
        {
            if (nowIndex == GameDataMgr.Instance.roleInfoList.Count - 1)
                nowIndex = 0;
            else
            {
                nowIndex++;
            }
            ChangeHero();
        });
        btnUnLock.onClick.AddListener(() =>
        {
            PlayerData data = GameDataMgr.Instance.playerData;
            if (data.GoldNum>=nowRoleData.lockMoney)
            {
                data.GoldNum -= nowRoleData.lockMoney;
                txtGoldNum.text = data.GoldNum.ToString();
                data.boughtHero.Add(nowRoleData.id);
                btnStart.gameObject.SetActive(true);
                btnUnLock.gameObject.SetActive(false); 
                GameDataMgr.Instance.SavePlayerData();
                //��ʾ���  ��ʾ����ɹ�
                UiMgr.Instance.ShowPanel<TipPanel>().ChangeInfo("����ɹ�");
            }
            else
            {
                UiMgr.Instance.ShowPanel<TipPanel>().ChangeInfo("��Ҳ���");
            }

        });
    }
    private void ChangeHero()
    {
        if(heroObj!=null)//�жϳ������Ƿ��Ѿ��н�ɫģ��
        {
            Destroy(heroObj);
            heroObj = null;
        }
        nowRoleData = GameDataMgr.Instance.roleInfoList[nowIndex];//��ɫ���������
        heroObj = Instantiate(Resources.Load<GameObject>(nowRoleData.res),heroPos.position,heroPos.rotation);//���ؽ�ɫģ��
        Destroy(heroObj.GetComponent<PlayerObejct>());//ѡ�ǽ���ʱ ��ʱ����Ҫ����ű� �����������������ת���й���
        txtName.text = nowRoleData.tips;
        UpDateLockBtn();
    }
    private void UpDateLockBtn()
    {
        //����ý�ɫδ���� ����ʾ����ť�������ؿ�ʼ��Ϸ��ť
        if(nowRoleData.lockMoney>0&&!GameDataMgr.Instance.playerData.boughtHero.Contains(nowRoleData.id))
        {
            btnUnLock.gameObject.SetActive(true);
            txtUnLock.text = "�۸�"+nowRoleData.lockMoney+"���";
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnUnLock.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }
    public override void HideMe(UnityAction callBack)
    {
        base.HideMe(callBack);
        if(heroObj!=null)
        {
            DestroyImmediate(heroObj);
            heroObj = null;
        }
    }
}
