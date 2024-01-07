using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataService 
{
    string relativePath = "/save.json";
    public void SaveData(GameData gameData)
    {
      
        string path = Application.persistentDataPath + relativePath;
        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(path, jsonData);
    }

    public GameData LoadGameData()
    {
        string path = Application.persistentDataPath + relativePath;
     
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(jsonData);
        }

  
        return null;
    }

    public void DeleteSaveFile()
    {
    
        string path = Application.persistentDataPath + relativePath;

        if (File.Exists(path))
        {
            File.Delete(path);
        }
   
    }

    public bool CheckFileExist()
    {
        string path = Application.persistentDataPath + relativePath;
        if (File.Exists(path))
        {
            return true;
        }
        return false;
    }
}
