using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;

    public Text txtInfo;
    public Image imgScene;

    private SceneInfo nowSceneInfo;
    private int nowIndex;
    public override void Init()
    {

        btnLeft.onClick.AddListener(() =>
        {
            if(nowIndex==0)
            {
                nowIndex = GameDataMgr.Instance.sceneInfoList.Count - 1;
            }
            else
            {
                nowIndex--;
            }
            ChangeScene();
        });
        btnRight.onClick.AddListener(() =>
        {
            if (nowIndex == GameDataMgr.Instance.sceneInfoList.Count - 1)
            {
                nowIndex = 0;
            }
            else
            {
                nowIndex++;
            }
            ChangeScene();
        });
        btnStart.onClick.AddListener(() =>
        {
            UiMgr.Instance.HidePanel<ChooseScenePanel>();
            //����Ϸ����
            AsyncOperation ao=SceneManager.LoadSceneAsync(nowSceneInfo.sceneName);//��֤�ڳ�����ȫ������Ϻ���ȥ��ʼ���¸������������п��ܻ��ڳ���û������ʱ��ȥ���¸������Ķ���ͻᱨ��
            ao.completed += (obj) =>
            {
                GameLevelMgr.Instance.InitInfo(nowSceneInfo);
            };           
        });
        btnBack.onClick.AddListener(() =>
        {
            UiMgr.Instance.HidePanel<ChooseScenePanel>();
            UiMgr.Instance.ShowPanel<ChooseHeroPanel>();
        });
        ChangeScene();
    }
    public void ChangeScene()
    {
        nowSceneInfo = GameDataMgr.Instance.sceneInfoList[nowIndex];
        imgScene.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);
        txtInfo.text = "����:\n" + nowSceneInfo.name +"\n"+"������"+"\n" + nowSceneInfo.tips;
    }
}
