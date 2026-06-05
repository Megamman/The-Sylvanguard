using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDungean : MonoBehaviour, IStatsDataPersistence
{

    public int point = 1;
    public int Dungean;
    public Transform player;

    //MainStaticData.SpawnPosition

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void GoInDungean()
    {
        MainStaticData.SpawnPosition = point;


        MainStaticData.loadScreen.LoadScene(Dungean);
    }

    public void LoadStatsData(StatsData data)
    {
        return;
    }

    public void SaveStatsData(ref StatsData data)
    {
        data.TownPos = player.position;
    }
}
