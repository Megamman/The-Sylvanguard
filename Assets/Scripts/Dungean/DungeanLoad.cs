using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeanLoad : MonoBehaviour
{
    public Scene scene;
    public int spawnPos;

    public void LoadDungean()
    {
        MainStaticData.SpawnPosition = spawnPos;
        SceneManager.LoadScene(scene.name);
    }
}
