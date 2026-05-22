using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDungean : MonoBehaviour
{

    public int point = 1;
    public string Dungean;

    //MainStaticData.SpawnPosition

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void GoInDungean()
    {
        MainStaticData.SpawnPosition = point;

        SceneManager.LoadScene(Dungean);
    }

}
