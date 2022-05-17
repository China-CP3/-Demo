using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour//�����಻�ܱ�new  ֻ�ܼ̳и�����ʹ��
{
    private CanvasGroup canvasGroup;//������弰������������͸����
    private float alphaSpeed = 10f;//���뵭�����ٶ�
    public bool isShow=false;//�жϵ�ǰ�ǵ��뻹�ǵ���
    private UnityAction hideCallBack=null;
    //awake��start��ִ�� Ϊ�˱�����start���Ҳ���canvasgroup public���������ط�������� û�б�Ҫ ������protected  
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup==null)
        {
            canvasGroup=this.gameObject.AddComponent<CanvasGroup>();//�������˸��ö���������canvasgroup ��������� �����Ǿ�õ�����null  ��Ϊû��Ӹ����
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()//�麯������������д Ҳ���Բ���д
    {
        Init();
    }
    /// <summary>
    /// �����������ʼ�� ע��ؼ��¼� �������������ʵ��
    /// </summary>
    public abstract void Init();
    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }
    public virtual void HideMe(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hideCallBack = callBack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isShow && canvasGroup.alpha < 1)//������ʱ, ���alpha͸����<1 ��һֱ�ۼ� �ӵ�1λ��
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if (!isShow && canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hideCallBack?.Invoke();
            }
        }
        ////��������ʾ״̬ʱ ���͸���� ��Ϊ1  �ͻ᲻ͣ�ļӵ�1 �ӵ�1 ���� ��ֹͣ�仯��
        ////����
        //if (isShow && canvasGroup.alpha != 1)
        //{
        //    canvasGroup.alpha += alphaSpeed * Time.deltaTime;
        //    if (canvasGroup.alpha >= 1)
        //        canvasGroup.alpha = 1;
        //}
        ////����
        //else if (!isShow && canvasGroup.alpha != 0)
        //{
        //    canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
        //    if (canvasGroup.alpha <= 0)
        //    {
        //        canvasGroup.alpha = 0;
        //        //����� ͸���ȵ�����ɺ� ��ȥִ�е�һЩ�߼�
        //        hideCallBack?.Invoke();
        //    }

        //}
    }
 
}
