using UnityEngine;

public class MainMenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GameSelection;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject HowTOPlayMenu;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ToMainMenu();
    }

    public void ToMainMenu()
    {
        MainStaticData.SelectedGame = null;
        MainMenu.SetActive(true);
        GameSelection.SetActive(false);
        SettingsMenu.SetActive(false);
        HowTOPlayMenu.SetActive(false);
    }

    public void ToGameMenu()
    {
        MainMenu.SetActive(false);
        GameSelection.SetActive(true);
        SettingsMenu.SetActive(false);
        HowTOPlayMenu.SetActive(false);

    }

    public void HowToPlay()
    {
        MainMenu.SetActive(false);
        GameSelection.SetActive(false);
        SettingsMenu.SetActive(false);
        HowTOPlayMenu.SetActive(true);
    }

    public void ToSettings()
    {
        MainMenu.SetActive(false);
        GameSelection.SetActive(false);
        SettingsMenu.SetActive(false);
        HowTOPlayMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
