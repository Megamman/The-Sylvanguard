using System.Collections.Generic;
using UnityEngine;

public class GameSaveData : MonoBehaviour
{



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadData();
    }

    public static void SaveData()
    {
        Debug.Log("Saving Data");
        GameDataPersistenceManager.instance.SaveFileData();
    }

    public static void LoadData()
    {
        //Debug.Log("Loading Data");
        GameDataPersistenceManager.instance.LoadFileData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
