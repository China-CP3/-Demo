using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public enum JsonType
{
    JsonUtlity,
    LitJson,
}
public class JsonMgr 
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;
    private JsonMgr() { }
    
    public void SaveData(object obj,string fileName,JsonType type=JsonType.LitJson)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".Json";
        string jsonData = "";
        switch (type)
        {
            case JsonType.JsonUtlity:
                jsonData = JsonUtility.ToJson(obj);
                break;
            case JsonType.LitJson:
                jsonData = LitJson.JsonMapper.ToJson(obj);
                break;
        }
        File.WriteAllText(path, jsonData);
        Debug.Log(path);
    }
    public T LoadData<T>(string fileName,JsonType type=JsonType.LitJson) where T:new ()
    {
        //�������Ϸһ��ʼ��Ĭ���ļ��� ���ж�������û������ 
        string path= Application.streamingAssetsPath + "/" + fileName + ".Json";
        //���������Ĭ���ļ� �ʹӶ�д�ļ�����ȥ��ȡ
        if(File.Exists(path)==false)
           path = Application.persistentDataPath + "/" + fileName + ".Json";
        //�����д�ļ���Ҳû�� ����Ĭ��ֵ
        if(!File.Exists(path))
           return new T();
        string jsonData = File.ReadAllText(path);
        T data = new T();
        switch (type)
        {
            case JsonType.JsonUtlity:
                data= JsonUtility.FromJson<T>(jsonData);
                break;
            case JsonType.LitJson:
                data= LitJson.JsonMapper.ToObject<T>(jsonData);
                break;
        }
        return data;
    }
}
