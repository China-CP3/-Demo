using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoint : MonoBehaviour
{
    public int maxWave;
    public int monsterNumOneWave;//ÿ���ж��ٸ���
    private int nowNum;//��ǰ����Ҫ�����Ĺ�������
    public List<int> mosterIds;//���id  ��ʾ��������Ķ��ֲ�ͬ�Ĺ���
    private int nowId;//��ǰ���Ĺ���id
    public float createOneOffsetTime;//�������ﴴ����ʱ����
    public float delayTimeWave;//ÿ��֮���ʱ����
    public float firstDelayTime;//��һ������֮ǰ�����׼��ʱ��
    private List<GameObject> monsterObjList;//��ǰ���غø�������Ԥ����
    // Start is called before the first frame update
    void Start()
    {
        GameLevelMgr.Instance.UpdateMaxWaveNum(maxWave);//���¹ؿ������г��ֵ����������ܺ�
        GameLevelMgr.Instance.AddMonsterPonit(this);//ÿ�����ֵ㶼���Լ���ӽ� �ؿ������� �����Ǳ��õ����ֵ�Ĳ���֮�������
        monsterObjList = new List<GameObject>(GameDataMgr.Instance.monsterInfoList.Count - 1);
        for (int i = 0; i < GameDataMgr.Instance.monsterInfoList.Count; i++)
        {
            monsterObjList.Add(Resources.Load<GameObject>(GameDataMgr.Instance.monsterInfoList[i].res));
            //Resources.Load<GameObject>(GameDataMgr.Instance.monsterInfoList[i].res);
        }
        Invoke("CreateWave", firstDelayTime);
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ����һ������
    /// </summary>
    private void CreateWave()
    {
        nowId=mosterIds[Random.Range(0, mosterIds.Count)];//0,count-1
        nowNum = monsterNumOneWave;
        maxWave--;
        CreateMonster();
        GameLevelMgr.Instance.ChangeNowWaveNum(1);
    }
    private void CreateMonster()
    {
        MonsterInfo info =GameDataMgr.Instance.monsterInfoList[nowId-1];
        //GameObject obj = Instantiate(Resources.Load<GameObject>(info.res),this.transform.position,Quaternion.identity);
        GameObject obj = Instantiate(monsterObjList[nowId - 1], this.transform.position, Quaternion.identity);
        MonsterObj monsterObj = obj.AddComponent<MonsterObj>();
        monsterObj.InitInfo(info);
        nowNum--;
        GameLevelMgr.Instance.AddMonsterToMonsterList(monsterObj);
        if (nowNum==0)//��ǰ�����ﴴ�����  ����������һ��  ���ڼ��ʱ���  ������һ��
        {
            if(maxWave>0)
            {
                Invoke("CreateWave",delayTimeWave);
            }
        }
        else
        {
            Invoke("CreateMonster", createOneOffsetTime);//���ʱ��� �����¸�����          
        }
    }
    public bool CheckOver()
    {
        return maxWave==0&&nowNum==00;//����Ϊ0�͵�ǰ�������贴���Ĺ������Ϊ0   ˵������ȫ����������
    }
}
