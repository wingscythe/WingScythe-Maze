using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public string sceneName;
    public Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton = this.GetComponent<Button>();
        playButton.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void onClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
