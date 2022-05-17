using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMgr
{
    private static UiMgr instance=new UiMgr();
    public static UiMgr Instance => instance;
    //�洢��ʾ�е�ui��� ÿ�δ�ĳ����� �ͼ�������ֵ�
    //�ر����ʱ ֱ�ӻ�ȡ�ֵ��е�ĳ�������йر�
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    private GameObject canvas;//��Ϊ���ĸ�����
    private UiMgr()
    {
        canvas = GameObject.Instantiate(Resources.Load<GameObject>("Ui/Canvas"));
        GameObject.DontDestroyOnLoad(canvas);//�л�����ʱ���Ƴ�
    }
    public T ShowPanel<T>() where T:BasePanel
    {
        //��֤T�����ֺ����Ԥ���������һ�� �����Ϳ���ͨ��T�����ֵõ���Ӧ��Ԥ����
        string panelName = typeof(T).Name;
        if(panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;//����ת���� ����Լ��basepanel  ��ôһ���ܹ�ת��  ��Ϊ����basepanel����������
        }
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("Ui/"+panelName));
        panelObj.transform.SetParent(canvas.transform, false);//�´򿪵�ui�������Ϊcanvas�������� false��ʾ��������ı���
        T panel= panelObj.GetComponent<T>();//�õ����Ԥ�������ϵĽű� �Ȼ�ر�����ʱ�� ����ͨ������ű�ɾ��������gameobject �Ϳ��Թر������
        panelDic.Add(panelName,panel);//���Ѿ��򿪵������ӵ��ֵ���  ÿ�δ�ǰ���ж��ֵ�����û�� �Ƿ��Ѵ�
        panel.ShowMe();//����ʱ���߼�  ����������   uimgrֻ����򿪹ر���� 
        return panel;
    }
    public void HidePanel<T>(bool isFade=true) where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)//�Ƿ���Ҫ���뵭�� Ĭ����Ҫ
            {
                panelDic[panelName].HideMe(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);//�ű������Ķ��� Ҳ�������
                    panelDic.Remove(panelName);
                });
            }
            else
            {  
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
           
        }
    }
    public T GetPanel<T>() where T:BasePanel
    {
        string panelName= typeof(T).Name;
        if(panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }
}
