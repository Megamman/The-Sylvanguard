using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineSettings : MonoBehaviour
{
    public PlayableDirector m_Director;
    PlayerMovement controls;

    bool Pause;


    private void Awake()
    {        
        controls = new PlayerMovement();
    }
    private void OnEnable()
        { if (controls != null) controls.Enable(); }

    private void OnDisable()
    { if (controls != null) controls.Disable(); }

    private void Start() {
        controls.Enable();
        // m_Director = gameObject.GetComponent<PlayableDirector>();
    }

    void Update()
    {
        controls.Main.Interect.performed += ctx => Resume();

    }

    public void PauseTimeline()
    {
        m_Director.Pause();
        Debug.Log("Pause Video");
    }

    void Resume()
    {
        m_Director.Resume();
        Debug.Log("Resmune Video");
    }
}
