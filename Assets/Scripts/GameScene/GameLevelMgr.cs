using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr
{
    //����Ϸ����ʱ ��̬�������
    //�ж���Ϸ�Ƿ����
    //��ʼ��������Ѫ��
    private static GameLevelMgr instance=new GameLevelMgr();
    public static GameLevelMgr Instance => instance;
    public PlayerObejct playerObejct;
    private List<MonsterPoint> monsterPoints=new List<MonsterPoint>();
    private int nowWaveNum = 0;//��ǰ���ж��ٲ�����
    private int maxWaveNum=0;//�ܹ����ٲ�����
    //public int nowMonsterSavedNum=0;//��ǰ�����л��ŵĹ�������
    private List<MonsterObj> monstersList = new List<MonsterObj>();//�����еĹ�����
    private GameLevelMgr()
    {

    }
    /// <summary>
    /// ��ʼ����ɫ��Ϣ
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(SceneInfo info)
    {
        UiMgr.Instance.ShowPanel<GamePanel>();
        RoleInfo roleInfo = GameDataMgr.Instance.nowRoleChoose;//�õ���ɫ��Ϣ
        Transform heroBornPos = GameObject.Find("HeroBornPos").transform;
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res),heroBornPos.position,heroBornPos.rotation);
        playerObejct= heroObj.GetComponent<PlayerObejct>();//�õ���ɫ���ϵ�playerobj�ű� Ȼ���ʼ��
        playerObejct.InitPlayerInfo(roleInfo.atk, info.money);//�ӽ�ɫ��Ϣ�õ����������ӳ�����Ϣ�õ���ǰ�����ĳ�ʼ���
        
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);

        //��ʼ��������Ѫ��
        MainTowerObj.Instance.UpdateHp(info.towerHp);
    }
    //�ж���Ϸ�Ƿ���� 
    //ͨ���жϳ������Ƿ�ȫ�����ﴴ����� ���ҳ������Ѵ����Ĺ���ȫ������


    public void AddMonsterPonit(MonsterPoint Point)
    {
        
        monsterPoints.Add(Point);
    }
    /// <summary>
    /// ����һ���ж��ٲ���
    /// </summary>
    /// <param name="num"></param>
    public void UpdateMaxWaveNum(int num)
    {
        maxWaveNum += num;
        nowWaveNum = maxWaveNum;
        UiMgr.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum,maxWaveNum);
    }
    public void ChangeNowWaveNum(int num)
    {
        nowWaveNum -= num;
        UiMgr.Instance.GetPanel<GamePanel>().UpdateWaveNum(nowWaveNum, maxWaveNum);
    }
    /// <summary>
    /// �ж����г��ֵ��ʣ�²����͵�ǰ���贴����������Ƿ�Ϊ0
    /// </summary>
    /// <returns></returns>
    public bool CheckOverAll()
    {
        for (int i = 0; i < monsterPoints.Count; i++)
        {
            if(!monsterPoints[i].CheckOver())
            {
                return false;
            }
        }
        if(monstersList.Count>0)
        {
            return false;
        }
        return true;
    }
    ///// <summary>
    ///// ���������ÿ����1������  �����л��ŵĹ���������+1
    ///// </summary>
    ///// <param name="num"></param>
    //public void ChangeMonsterSavedNum(int num)
    //{
    //    nowMonsterSavedNum += num;
    //}
    public void AddMonsterToMonsterList(MonsterObj obj)
    {
        monstersList.Add(obj);
    }
    public void RemoveMonsterFromMonsterList(MonsterObj obj)
    {
        monstersList.Remove(obj);
    }
    
    public MonsterObj FindMonster(Vector3 pos,int range)
    {
        for (int i = 0; i < monstersList.Count; i++)
        {
            if (monstersList[i] .isDead==false&& Vector3.Distance(pos, monstersList[i].transform.position) <= range)
            {
                return monstersList[i];
            }
        }
        return null;
    }
    public List<MonsterObj> FindMonstersAll(Vector3 pos, int range)
    {
        List<MonsterObj> list=new List<MonsterObj>();
        for (int i = 0; i < monstersList.Count; i++)
        {
            if(monstersList[i].isDead==false&&Vector3.Distance(pos,monstersList[i].transform.position)<=range)
            {
                list.Add(monstersList[i]);
            }
        }
        return list;
    }
    /// <summary>
    /// ��յ�ǰ�ؿ���¼������  ����Ӱ���´ν���ùؿ�
    /// </summary>
    public void ClearInfo()
    {
        monsterPoints.Clear();
        monstersList.Clear();
        nowWaveNum =0;
        playerObejct = null;
    }
}
