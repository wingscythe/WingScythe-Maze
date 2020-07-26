using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MazeCreation : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 pos; 
    public int len;
    public int x;
    public int y;
      // Start is called before the first frame update
    void Start()
    {
        createGrid();
    }

    void createGrid()
    {
        pos = new Vector3( (-x / 2) + len / 2, 0.0f, (-y/ 2) + len / 2);
        Vector3 newpos = pos;
        GameObject temp; 
        for(int i = 0; i< y; i++)
        {
            for(int j = 0; j<= x; j++)
            {
                pos = new Vector3(pos.x + (len * j) - len/2 , 0.0f, pos.z + len* i - len/2);
                temp = Instantiate(prefab, newpos, Quaternion.identity);
                temp.transform.SetParent(this.transform);
            }
        }

        for (int i = 0; i <= y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                pos = new Vector3(pos.x + len * j - len/2, 0.0f, pos.z + len * i - len/2);
                temp = Instantiate(prefab, newpos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                temp.transform.SetParent(this.transform);
            }
        }
    }
}
