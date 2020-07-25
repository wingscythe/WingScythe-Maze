using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header(Settings)]
    public Keycode key;

    public Canvas canvas;
    object hud;
    // Start is called before the first frame update
    void Start()
    {
        hud = canvas.GetComponent(SettingsUI);
        hud.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.getKeyCode(key)) {
            onClick();
        }
    }

    void onClick() 
    {
        hud.SetActive(true);
    }
}
