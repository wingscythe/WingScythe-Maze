using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonToggle : MonoBehaviour
{
    public KeyCode key;
    // public Canvas canvas;
    public GameObject GUI1;
    public GameObject GUI2;
    bool GUI1OnDisplay = true;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        GUI1.SetActive(true);
        GUI2.SetActive(false);
        button = this.GetComponent<Button>();
        // button.interactable = true;
        // button.enabled = false;
        button.onClick.AddListener(toggle);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.GetAxisRaw("Settings"));
        if (Input.GetKeyDown(key))
        {
            toggle();
        }
        
    }

    void toggle()
    {
        if (!GUI1OnDisplay)
        {
            swapGUIImages(GUI2, GUI1);
        }
        else
        {
            swapGUIImages(GUI1, GUI2);
        }
    }

    void swapGUIImages(GameObject onDisplayGUI, GameObject offDisplayGUI)
    {
        closeGUI(onDisplayGUI);
        openGUI(offDisplayGUI);
        GUI1OnDisplay = !GUI1OnDisplay;
        Debug.Log(GUI1OnDisplay);
    }

    void openGUI(GameObject GUI) 
    {
        GUI.SetActive(true);   
    }

    void closeGUI(GameObject GUI)
    {
        GUI.SetActive(false);
    }
}
