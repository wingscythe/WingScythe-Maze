﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterText : MonoBehaviour
{
    public GameObject player;
    public Text pointCounter;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pointCounter.text = player.GetComponent<PlayerController>().points.ToString();
    }
}
