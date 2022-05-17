using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    private GameObject towerObj=null;//��Ԥ����
    public TowerInfo nowTowerInfo=null;//������
    public List<int> chooseIds;//���Խ����3����id
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(nowTowerInfo!=null&&nowTowerInfo.next==0)//�������Ѿ����� ������ʾ������������
        {
            return;
        }
        UiMgr.Instance.GetPanel<GamePanel>().UpdateTowerPoint(this);
    }
    private void OnTriggerExit(Collider other)
    {
        UiMgr.Instance.GetPanel<GamePanel>().UpdateTowerPoint(null);

    }
    public void CreateTower(int id)
    {
        TowerInfo info=GameDataMgr.Instance.towerInfoList[id-1];
        if(info.money>GameLevelMgr.Instance.playerObejct.money)//������
        {
            return;
        }
        else//�������
        {
            GameLevelMgr.Instance.playerObejct.AddMoney(-1*info.money);
            if(towerObj!=null)
            {
                Destroy(towerObj);
                towerObj = null;
            }
            towerObj= Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
            towerObj.GetComponent<TowerObj>().InitInfo(info);
            nowTowerInfo = info;
            if(info.next!=0)
            {
                UiMgr.Instance.GetPanel<GamePanel>().UpdateTowerPoint(this);//��������ť
            }
            else
            {
                UiMgr.Instance.GetPanel<GamePanel>().UpdateTowerPoint(null);
            }
        }

    }
}
