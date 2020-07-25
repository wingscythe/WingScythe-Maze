using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Settings")]
    public string keyCode;

    public Canvas canvas;
    GameObject hud;
    // Start is called before the first frame update
    void Start()
    {
        hud.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode)) {
            onClick();
        }
    }

    void onClick() 
    {
        hud.SetActive(true);
    }
}
