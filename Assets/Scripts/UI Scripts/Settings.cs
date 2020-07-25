using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Settings : MonoBehaviour
{
    [Header("Settings")]
    public string keyCode;

    public Canvas canvas;
    public GameObject GUI;
    bool onDisplay = false;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        GUI.SetActive(false);
        button = this.GetComponent<Button>();
        // button.interactable = true;
        // button.enabled = false;
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode)) {
            OnClick();
        }
        
    }

    void OnClick()
    {
        if (!onDisplay)
        {
            openGUI();
        }
        else
        {
            closeGUI();
        }
    }

    void openGUI() 
    {
        onDisplay = true;
        GUI.SetActive(true);
        
    }

    void closeGUI()
    {
        onDisplay = false;
        GUI.SetActive(false);
    }
}
