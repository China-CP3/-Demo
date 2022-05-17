using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance=new GameDataMgr();
    public static GameDataMgr Instance => instance;
    public MusicData musicData;   
    public PlayerData playerData;
    public RoleInfo nowRoleChoose;//�ѵ�ǰ��ɫ��Ϣ��¼���� �������Ϸ�󴴽�
    public List<SceneInfo> sceneInfoList;
    public List<RoleInfo> roleInfoList;
    public List<MonsterInfo> monsterInfoList;
    public List<TowerInfo> towerInfoList;

    private GameDataMgr()
    {
        //ClearPlayerData();
        
        musicData = JsonMgr.Instance.LoadData<MusicData>("MusicData");
        roleInfoList = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");//��ɫ���� ������ id Ԥ����·��
        playerData = JsonMgr.Instance.LoadData<PlayerData>("PlayerData");//������� ����ӵ�ж��ٽ�� �ѹ�����Щ��ɫ��id
        sceneInfoList = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        monsterInfoList = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
        towerInfoList = JsonMgr.Instance.LoadData<List<TowerInfo>>("TowerInfo");
        
    }
    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
    public void ClearPlayerData()
    {
        PlayerData data = new PlayerData();
        JsonMgr.Instance.SaveData(data, "PlayerData");
    }
    public void PlaySound(string resName)
    {
        GameObject obj=new GameObject();
        AudioSource audio =obj.AddComponent<AudioSource>();
        audio.clip=Resources.Load<AudioClip>(resName);
        audio.volume = musicData.soundValue;
        audio.mute = !musicData.soundOpen;
        audio.Play();
        GameObject.Destroy(obj,3);
    }
}
