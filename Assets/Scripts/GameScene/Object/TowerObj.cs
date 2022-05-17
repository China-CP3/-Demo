using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObj : MonoBehaviour
{
    public Transform head;
    public Transform gunPoint;
    private float roundSpeed = 50f;
    private TowerInfo info;
    private MonsterObj targetObj;
    private float nowTime;//�������ʱ��
    private Vector3 monsterPos;
    private List<MonsterObj> targetObjList;

    public void InitInfo(TowerInfo info)
    {
        this.info = info;
    }
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(info.type==1)
        {
            //���ܹ��������  ��Ŀ��  Ŀ������  ������������
            if(targetObj==null|| targetObj.isDead||Vector3.Distance(transform.position,targetObj.transform.position) > info.atkRange)
            {
                targetObj = GameLevelMgr.Instance.FindMonster(this.transform.position,info.atkRange);
            }
            if (targetObj == null)
                return;
            monsterPos=targetObj.transform.position;
            monsterPos.y=head.position.y;//���������������Ľŵ� ��˫��yֵһ��
            head.rotation = Quaternion.Slerp(head.rotation,Quaternion.LookRotation(monsterPos-head.transform.position),roundSpeed*Time.deltaTime);
            if(Vector3.Angle(head.forward, monsterPos - head.position)<20&&Time.time-nowTime>=info.offsetTime)//������forward�ͽ�ʬ��ȥ�������������ļн�    �������ʱ��
            {
                nowTime = Time.time;
                targetObj.Wound(info.atk);
                GameDataMgr.Instance.PlaySound("Music/Tower");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff),gunPoint.position,gunPoint.rotation);
                Destroy(effObj,0.3f);
            }
        }
        else
        {
            targetObjList = GameLevelMgr.Instance.FindMonstersAll(this.transform.position,info.atkRange);
            if(targetObjList.Count>0&&Time.time-nowTime>=info.offsetTime)
            {
                nowTime = Time.time;
                GameDataMgr.Instance.PlaySound("Music/Gun");
                GameObject effObj = Instantiate(Resources.Load<GameObject>(info.eff), this.transform.position, this.transform.rotation);
                Destroy(effObj, 0.3f);
                for (int i = 0; i < targetObjList.Count; i++)
                {
                    
                    targetObjList[i].Wound(info.atk);
                }
            }
        }
    }
}
