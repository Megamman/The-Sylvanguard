using UnityEngine;
using System.Collections.Generic;

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
        SavedGame.Remove(MainStaticData.SelectedGame);

    }
}
