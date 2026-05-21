using System.IO;
using System;
using UnityEngine;

public class GameDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public GameDataHandler(string _dataDirPath, string _dataFileName)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
    }


    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception)
            {
                throw;
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        if (data == null) { Debug.Log("No data found"); return; }

        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object inot Json
            string dataToStore = JsonUtility.ToJson(data, true);

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    public void DeleteSaveFile()
    {
        string fullPath = Path.Combine(dataDirPath, MainStaticData.SelectedGame);

        if (File.Exists(fullPath))
        {
            Debug.Log("Deleting file");
            GameData _data = MainStaticData.gameData;
            File.Delete(fullPath);
            _data.RemoveGameData();

        }
    }


}
