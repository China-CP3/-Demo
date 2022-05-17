using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObj : MonoBehaviour
{
    //�ܵ��˺� ����Ѫ��
    //����ʬ��ȡ��λ��
    //
    // Start is called before the first frame update
    private int hp;
    private int maxHp;
    private bool isDead;
    private static MainTowerObj instance;
    public static MainTowerObj Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void UpdateHp(int hp,int maxHp)
    //{
    //    this.hp = hp;
    //    this.maxHp = maxHp;
    //    UiMgr.Instance.GetPanel<GamePanel>().UpdateTowerHp(hp);
    //}
    public void UpdateHp(int hp)
    {
        this.hp = hp;
        UiMgr.Instance.GetPanel<GamePanel>().UpdateTowerHp(this.hp);
    }
    public void Wound(int value)
    {
        if(isDead)
        {
            return;
        }
        hp-=value;
        if(hp<=0)
        {
            hp = 0;
            isDead = true;
            GameOverPanel gameOverPanel=  UiMgr.Instance.ShowPanel<GameOverPanel>();

            gameOverPanel.InitInfo(5, false);//��Ϸʧ�ܺ������
        }
        //UpdateHp(hp,maxHp);
        UpdateHp(hp);
    }
    /// <summary>
    /// ��������ʱ��ɾ���Լ�
    /// </summary>
    private void OnDestroy()
    {
        instance = null;
    }
}
