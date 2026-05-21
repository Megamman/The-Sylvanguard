using System.IO;
using System;
using UnityEngine;

public class StatsDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = MainStaticData.SelectedGame;

    public StatsDataHandler(string _dataDirPath)
    {
        this.dataDirPath = _dataDirPath;
    }

    public StatsData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        StatsData loadedData = null;

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
                loadedData = JsonUtility.FromJson<StatsData>(dataToLoad);
            }
            catch (Exception)
            {
                throw;
            }
        }

        return loadedData;
    }

    public void Save(StatsData data)
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

    //public void DeleteSaveFile() // Delete Save File set in the name save file
    //{
    //    string fullPath = Path.Combine(dataDirPath, dataFileName);

    //    if (File.Exists(fullPath))
    //    {
    //        Debug.Log("Deleting file");
    //        StatsData _file = MainStaticData.statsData;
    //        File.Delete(fullPath);
    //        _file.RemoveData();

    //    }
    //}
}
