using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObj : MonoBehaviour
{
    //�������ƶ�
    //�ƶ� Ѱ·���
    //�������� �˺����
    //����
    //����
    private Animator animator;
    private NavMeshAgent agent;
    private MonsterInfo monsterInfo;
    private int hp;
    public bool isDead = false;
    private RectTransform rect;
    private float frontTime=0;
    private void Awake()
    {
        agent= GetComponent<NavMeshAgent>();
        animator= GetComponent<Animator>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        animator.SetBool("Run",agent.velocity!=Vector3.zero);
        if(Vector3.Distance(this.transform.position,MainTowerObj.Instance.transform.position)<5&&Time.time- frontTime>=monsterInfo.atkOffset)
        {
            frontTime = Time.time;
            animator.SetTrigger("Atk");
        }
    }
    /// <summary>
    /// ��ʼ����ʬ���ϵĲ���  ��json��Ľ�ʬ��������ֵ
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(MonsterInfo info)
    {
        monsterInfo = info;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
        hp=info.hp;
        agent.speed = info.moveSpeed;
        agent.angularSpeed = info.roundSpeed;
        agent.acceleration = info.roundSpeed;

    }
    public void Wound(int value)
    {
        if(isDead)
        {
            return;
        }
        hp -=value;
        animator.SetTrigger("Wound");
        if(hp<=0&& !isDead)
        {
            //����
            Dead();
        }
        else
        {
            GameDataMgr.Instance.PlaySound("Music/Wound");
        }
    }
    public void Dead()
    {
        isDead=true;
        agent.isStopped=true;
        agent.enabled = false;
        animator.SetBool("Dead",true);
        //������Ч
        GameDataMgr.Instance.PlaySound("Music/dead");
        //��ɫ�������
        GameLevelMgr.Instance.playerObejct.AddMoney(100);
    }
    public void DeadEvent()
    {        
        GameLevelMgr.Instance.RemoveMonsterFromMonsterList(this);
        if (GameLevelMgr.Instance.CheckOverAll())
        {
            UiMgr.Instance.ShowPanel<GameOverPanel>().InitInfo(20, true);
        }
        Destroy(gameObject);       
    }
    public void BornOver()
    {
        //����������� ���ƶ�
        agent.SetDestination(MainTowerObj.Instance.transform.position);
        animator.SetBool("Run",true);
    }
    public void AtkEvent()
    {
        GameDataMgr.Instance.PlaySound("Music/Eat");
        Collider[] colliders = Physics.OverlapSphere(this.transform.position+transform.forward+this.transform.up,1,1<<LayerMask.NameToLayer("MainTower"));
        for (int i = 0; i < colliders.Length; i++)
        {
            if(MainTowerObj.Instance.gameObject==colliders[i].gameObject)
            {
                MainTowerObj.Instance.Wound(monsterInfo.atk);
            }
        }
    }
}
