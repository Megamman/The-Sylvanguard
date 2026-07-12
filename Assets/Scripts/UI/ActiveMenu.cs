using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveMenu : MonoBehaviour, IStatsDataPersistence
{
    public GameObject Menu;
    public GameObject Curser;
    public Transform player;

    public GameObject HowToPlayMenu;

    Scene scene;

    private PlayerMovement controls;
    bool openMenu = false;
    private void Awake()
    {
        controls = new PlayerMovement();
        controls.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Menu.SetActive(false);
        Curser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //press ESC to open menu
        controls.Main.OpenMenu.performed += ctx => CheckMenu();
    }

    void CheckMenu()
    {
        if (!openMenu)
        {
            OpenMenu();
            openMenu = true;
            Menu.SetActive(false);
        } else
        {
            ContinueGame();
            openMenu = false;
            Menu.SetActive(false);
        }
    }

    public void OpenHowToPlay()
    {
        HowToPlayMenu.SetActive(true);
        Menu.SetActive(false );
    }
    public void CloseHowToPlay()
    {
        HowToPlayMenu.SetActive(false);
        Menu.SetActive(true);
    }

    public void OpenMenu()
    {
        MainStaticData.HoldMovement = false;
        //Menu.SetActive(true);
        if (Menu != null)
        {
            Menu.SetActive(true);
        }
        else
        {
            // This logs a clean error message instead of crashing your input system
            Debug.LogError("🔴 ActiveMenu Error: The menu GameObject was destroyed elsewhere in the project! Cannot open it.", this);
        }
        Curser.SetActive(true);
        openMenu = true;
    }

    public void ContinueGame()
    {
        MainStaticData.HoldMovement = true;
        Menu.SetActive(false);
        Curser.SetActive(false);
        openMenu = false;
    }

    public void ToSkillTree()
    {
        //Save Position
        DungeanSaveData.SaveData();
        //Load Skill tree
        //SceneManager.LoadScene("SkillTree");
        MainStaticData.loadScreen.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseGame()
    {
        //Save Position
        DungeanSaveData.LoadData();
        //quit
        Application.Quit();
    }

    public void LoadStatsData(StatsData data)
    {
        if (scene.name == ("Town"))
        {
            player.position = data.TownPos;
            //Debug.Log("Loading Town Location " + data.TownPos);
        }
    }

    public void SaveStatsData(ref StatsData data)
    {
        if (scene.name == ("Town"))
        {
            data.TownPos = player.position;

            Debug.Log("Saving Town Location " + data.TownPos);
        }
    }
}
