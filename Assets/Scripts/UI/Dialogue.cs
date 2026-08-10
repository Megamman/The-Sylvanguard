using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using System;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public PlayableDirector m_Director;
    public TextMeshProUGUI textMesh;
    PlayerMovement controls;
    bool isPause;

    public TMP_Text Text;
    public TMP_Text Name1;
    public TMP_Text Name2;

    public Image Persion1;
    public Image Persion2;
    public Sprite s_Persion1;
    public Sprite s_Persion2;

    int i = 0;

    public DialogueContent[] dialogueBox;


    private void Awake()
    {
        controls = new PlayerMovement();
    }
    private void OnEnable()
    { if (controls != null) controls.Enable(); }

    private void OnDisable()
    { if (controls != null) controls.Disable(); }

    private void Start()
    {
        controls.Enable();


    }

    // Update is called once per frame
    void Update()
    {
        Persion1.sprite = s_Persion1;
        Persion2.sprite = s_Persion2;

        if(isPause)
        {
            controls.Main.Interect.performed += ctx => GetDialogue();
        }

        ShowDialogue();
    }

    void ShowDialogue()
    {
        if (i >= dialogueBox.Length || i < 0)
        { return; }
            string currentText = dialogueBox[i].dailogue.ToString();

        string newText = currentText.Replace("{name}", MainStaticData.SelectedGame);

        textMesh.text = newText.ToString();
        
    }

    void GetDialogue()
    {
        if (i <= dialogueBox.Length)
        {
            i++;

        }
        else { ResumeTimeline(); }

    }


    public void PauseTimeline()
    {
        m_Director.Pause();
        Debug.Log("Pause Video");
        isPause = true;
    }

    void ResumeTimeline()
    {
        m_Director.Resume();
        Debug.Log("Resmune Video");
        isPause = false;
        
    }

    [Serializable]
    public class DialogueContent 
    {
        public string dailogue;
        public bool isPersion2;
    }


}
