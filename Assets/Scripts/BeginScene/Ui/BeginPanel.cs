using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnAbout;
    public Button btnQuit;
    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            //�����������ת����  ��ʾѡ�����
            UiMgr.Instance.HidePanel<BeginPanel>();
            Camera.main.GetComponent<BeginCameraAnimator>().TurnLeft(() => 
            { 
                UiMgr.Instance.ShowPanel<ChooseHeroPanel>();    
            });
        });
        btnSetting.onClick.AddListener(() =>
        {
            //���������
            UiMgr.Instance.ShowPanel<SettingPanel>();
        });
        btnAbout.onClick.AddListener(() =>
        {
            //��˵�����
            UiMgr.Instance.ShowPanel<AboutPanel>();
        });
        btnQuit.onClick.AddListener(() =>
        {
            
            //�˳���Ϸ
            Application.Quit();//��������Ч  �༭ʱ��Ч
        });
    }
}
