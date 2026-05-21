using UnityEngine;

public interface IGameDataPersistence
{
    void LoadGameData(GameData data);
    void SaveGameData(ref GameData data);
}
