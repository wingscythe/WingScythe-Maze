using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneWayButton : MonoBehaviour
{
    public KeyCode key;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(ContextSwitch);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            ContextSwitch();
        }
    }

    void ContextSwitch()
    {
        
    }
}
