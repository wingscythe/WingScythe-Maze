using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

public class Maze : MonoBehaviour
{

    public GameObject prefab;
    public GameObject Fruit1;
    public GameObject Fruit2;
    public GameObject Fruit3;
    public GameObject Fruit4;
    public GameObject Cake;
    public GameObject seederObj;
    public PUN2_RoomController seeder;


    public int width;

    public int height;

    public Tile[] tiles;

    public List<Edge> edges;

    public int EdgeIndex = 0;

    public void Start()
    {
        seeder = seederObj.GetComponent<PUN2_RoomController>();
        int seed = getSeed();
        Random.InitState(seed); 
        tiles = new Tile[width * height];

        for (int i = 0; i < width * height; i++)
        {
            tiles[i] = new Tile();
        }

        edges = new List<Edge>();

        spawnLeftRightBoundaries();
        spawnUpDownBoundaries();
        spawnInnerEdgesLeftRight();
        spawnInnerEdgesUpDown();

       
        StartCoroutine(removeEdgeCoroutine());
        StartCoroutine(waiter());
        

    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10f);
        removeMiddle();
        makeEntrances();
    }

    public int getSeed()
    {
        return seeder.getSeed();
    }
    public void spawnLeftRightBoundaries()
    {
        for (int z = 1; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                if (x == 0 || x == width)
                {
                    
                    GameObject temp = Instantiate(prefab, new Vector3((x * 5), 0, (z * 5)), Quaternion.Euler(0, 90, 0));
                    temp.transform.SetParent(this.transform);
                }

            }
        }

    }

    public void spawnUpDownBoundaries()
    {
        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (z == 0 || z == height)
                {
                    GameObject temp = Instantiate(prefab, new Vector3((x * 5) + 2.5f, 0, (z * 5) + 2.5f), Quaternion.Euler(0, 0, 0));
                    temp.transform.SetParent(this.transform);
                }

            }
        }

    }

    public void spawnInnerEdgesLeftRight()
    {
        EdgeIndex = 0;

        for (int z = 1; z <= height; z++)
        {
            for (int x = 1; x < width; x++)
            {
                GameObject go = (GameObject)Instantiate(prefab, new Vector3((x * 5), 0, (z * 5)), Quaternion.Euler(0, 90, 0));
                go.transform.SetParent(this.transform);

                Edge edge = go.AddComponent<Edge>() as Edge;

                go.GetComponent<Edge>().tiles = new Tile[2];
                go.GetComponent<Edge>().tiles[0] = tiles[EdgeIndex];
                go.GetComponent<Edge>().tiles[1] = tiles[EdgeIndex + 1];



                edges.Add(go.GetComponent<Edge>());

                EdgeIndex++;

            }
            EdgeIndex++;
        }
    }

    public void spawnInnerEdgesUpDown()
    {
        EdgeIndex = 0;

        for (int z = 1; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject go = (GameObject)Instantiate(prefab, new Vector3((x * 5) + 2.5f, 0, (z * 5) + 2.5f), Quaternion.Euler(0, 0, 0));
                go.transform.SetParent(this.transform);

                Edge edge = go.AddComponent<Edge>() as Edge;

                go.GetComponent<Edge>().tiles = new Tile[2];
                go.GetComponent<Edge>().tiles[0] = tiles[EdgeIndex];
                go.GetComponent<Edge>().tiles[1] = tiles[EdgeIndex + 1];

                edges.Add(go.GetComponent<Edge>());

                EdgeIndex++;

            }

        }
    }


    public void removeEdges()
    { 
        int randInt = UnityEngine.Random.Range(0, edges.Count);

        Edge randomEdge = edges[randInt];
        

        edges.RemoveAt(randInt);

        if (Tile.getHighestParent(randomEdge.tiles[0]) == Tile.getHighestParent(randomEdge.tiles[1]))
        {
            if (Tile.getHighestParent(randomEdge.tiles[0]) == null && Tile.getHighestParent(randomEdge.tiles[1]) == null)
            {
                randomEdge.tiles[0].parent = randomEdge.tiles[1];

                randomEdge.disableEdge();
        
            }
        }

        else
        {
            if (Tile.getHighestParent(randomEdge.tiles[0]) == null && Tile.getHighestParent(randomEdge.tiles[1]) == null)
            {
                Tile.getHighestParent(randomEdge.tiles[0]).parent = randomEdge.tiles[1];
                randomEdge.disableEdge();
            }

            else
            {
                Tile.getHighestParent(randomEdge.tiles[1]).parent = randomEdge.tiles[0];
                randomEdge.disableEdge();
            }
        }





    }

    public IEnumerator removeEdgeCoroutine()
    {
        int loopNum = edges.Count;

        for (int i = 0; i < loopNum; i++)
        {
            removeEdges();
            yield return new WaitForSeconds(0f);
        }
    }

    public void removeMiddle()
    {
        
        Vector3 mid = new Vector3((float) (5*width) / 2, 0.0f,(float) (5*height) / 2);
        Collider[] hitColliders = Physics.OverlapBox(mid, transform.localScale / 4);
        int i = 0;
        while (i < hitColliders.Length )
        {
            if (hitColliders[i].gameObject.tag != "Player")
            {
                Destroy(hitColliders[i].gameObject);
                i++;
            }
        }
        GameObject temp = Instantiate(Cake, mid, Quaternion.identity);
        temp.transform.SetParent(this.transform);
    }

    public void makeEntrances()
    {
        Vector3 top = new Vector3((5.0f * width) / 2.0f, 0.0f, (5.0f* height));
        Vector3 mid = new Vector3((5.0f * width) / 2.0f, 0.0f, 0.0f );
        Vector3 left = new Vector3(0.0f, 0.0f, (5.0f * height) / 2.0f);
        Vector3 right = new Vector3((5.0f*width), 0.0f, (5.0f * height) / 2.0f);
        Collider[] hitColliders = Physics.OverlapBox(mid, transform.localScale / 8);
        Collider[] hitColliders2 = Physics.OverlapBox(right, transform.localScale / 8);
        Collider[] hitColliders3 = Physics.OverlapBox(left, transform.localScale / 8);
        Collider[] hitColliders4 = Physics.OverlapBox(top, transform.localScale / 8);
        int i = 0;
        while (i < hitColliders.Length && i< hitColliders2.Length && i < hitColliders3.Length && i < hitColliders4.Length)
        {
            if (hitColliders[i].gameObject.tag != "Player" && hitColliders2[i].gameObject.tag != "Player" && hitColliders3[i].gameObject.tag != "Player" && hitColliders4[i].gameObject.tag != "Player")
            {
                Destroy(hitColliders[i].gameObject);
                Destroy(hitColliders2[i].gameObject);
                Destroy(hitColliders3[i].gameObject);
                Destroy(hitColliders4[i].gameObject);
            }
            i++;
        }

    }

}

[System.Serializable]
public class Tile
{

    public Tile parent;

    public static Tile getHighestParent(Tile tile)
    {



        if (tile.parent == null)
        {
            return tile;
        }

        else
        {
            return getHighestParent(tile.parent);
        }


    }

}


