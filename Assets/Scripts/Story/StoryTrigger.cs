using UnityEngine;

public class StoryTrigger : MonoBehaviour
{

    public GameObject StartStory;

    public void TriggerStory()
    {
        StartStory.SetActive(true);
        Debug.Log ("Activate Story");
        //activate a Gameobject to start the script that will start the story
    }
 
}

