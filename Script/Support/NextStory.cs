using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStory : MonoBehaviour
{
    // public AudioSource theMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStoryClick()
    {
        SceneManager.LoadScene("StorySceneTwo");
    }
}
