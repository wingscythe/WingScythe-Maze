using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterText : MonoBehaviour
{
    public GameObject player;
    public Text pointCounter;
    public GameObject winnerText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int points = player.GetComponent<PlayerController>().points;
        pointCounter.text = points.ToString();
        if (points >= 3)
        {
            winnerText.SetActive(true);
            winnerText.GetComponent<Text>().text = "Congratulations! You got the most toppings!\nWinner";
        }
    }
}
