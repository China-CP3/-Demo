using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObejct : MonoBehaviour
{
    //��ʼ���������
    //վ�����¶׵��л�
    //����������ͬ�Ĵ��� ����ǹ
    //��Ǯ�仯
    public Transform gunPoint;//ǹ�������
    public int atk;
    public int money;
    private float roundSpeed = 100f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("HSpeed",Input.GetAxis("Horizontal"));
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.F))
            animator.SetTrigger("RollTrigger");
        if (Input.GetKeyDown(KeyCode.Mouse0))
            animator.SetTrigger("FireTrigger");

        this.transform.Rotate(Vector3.up,Input.GetAxis("Mouse X")*roundSpeed*Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1,1);
        }else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetLayerWeight(1, 0);
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Time.timeScale = 0f;
        //    UiMgr.Instance.ShowPanel<GamePanel>().btnSetting.gameObject.SetActive(true);
        //}
    }
    /// <summary>
    /// ��ʼ���������
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="money"></param>
    public void InitPlayerInfo(int atk,int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();
    }/// <summary>
     /// �����˺����
     /// </summary>
    public void KnifeEvent()
    {
       GameDataMgr.Instance.PlaySound("Music/Knife");
       Collider[] colliders= Physics.OverlapSphere(this.transform.position+this.transform.forward+transform.up,1,1<<LayerMask.NameToLayer("Monster"));
       for(int i=0;i<colliders.Length;i++)
       {
            //�ý�ʬ����
          MonsterObj monster= colliders[i].gameObject.GetComponent<MonsterObj>();
          if(monster!=null&&!monster.isDead)
          {
             monster.Wound(atk);
             break;
          }
       }
    }
    public void ShootEvent()
    {
       GameDataMgr.Instance.PlaySound("Music/Gun");
       RaycastHit[] hits = Physics.RaycastAll(new Ray(gunPoint.position, this.transform.forward), 1000, 1 << LayerMask.NameToLayer("Monster"));
       for (int i = 0; i < hits.Length; i++)
       {
            //�ý�ʬ���� ������Ч
            MonsterObj monster = hits[i].collider.gameObject.GetComponent<MonsterObj>();
            if (monster != null&&!monster.isDead)
            {
                monster.Wound(atk);
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowRoleChoose.hitEff));
                effObj.transform.position = hits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(hits[i].normal);
                Destroy(effObj,3);
                break;
            }
        }
    }
    public void UpdateMoney()
    {
        UiMgr.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }
    public void AddMoney(int n)
    {
        money += n;
        UpdateMoney();
    }
}
