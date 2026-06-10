using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

[System.Serializable]
public class GameData
{
    public List<string> SavedGame;

    public GameData()
    {
        SavedGame = new List<string>();
    }

    public void RemoveGameData()
    {
        if(SavedGame.Contains(MainStaticData.SelectedGame))
        {Debug.Log("Game found");}else {Debug.Log("Game not found");}

        SavedGame.Remove(MainStaticData.SelectedGame);
        GameDataPersistenceManager.instance.SaveFileData();
        MainStaticData.SelectedGame = null;

    }
}
