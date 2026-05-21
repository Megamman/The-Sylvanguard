using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameNameData : MonoBehaviour
{
    //public StatsDataPersistenceManager statsFile;
    public string PlayerName;
    public TMP_Text Title;

    void Start()
    {
        Title.text = PlayerName;
        Debug.Log(PlayerName);
    }

    public void GetButtonPress()
    {
        MainStaticData.SelectedGame = PlayerName;
    }

    private void OnDestroy()
    {
        Debug.Log("Item is Destroid " + PlayerName);
    }
}
