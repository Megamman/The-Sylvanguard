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
        StatsDataPersistenceManager.instance.SaveStatsData();
    }

    public static void LoadData()
    {
        //Debug.Log("Loading Data");
        StatsDataPersistenceManager.instance.LoadStatsData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
