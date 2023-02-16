using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

public class SpawnManager : GOSingleton<SpawnManager>
{
    //Ki?m soát các Floor
    [SerializeField] private List<FloorController> floors = new List<FloorController> ();
    [SerializeField] GameObject brickPrefabs;
    [SerializeField] private int height, width;
    private List<GameObject[,]> blocks = new List<GameObject[,]>();
    private List<GameObject[]> bricks = new List<GameObject[]>();
    private List<Vector3> l_pos = new List<Vector3>();
    // Ki?m soát s? ng??i ch?i trong game
    [SerializeField] int playerNumber;

    public int PlayerNumber { get => playerNumber; set => playerNumber = value; }

    private void Start()
    {
        GetInstance();
        for (int i = 0; i < floors[0].Height; i++)
        {
            for (int j = 0; j < floors[0].Width; j++)
            {
                l_pos.Add(floors[0].SpawnPos.transform.position + new Vector3(i - (int)floors[0].Height / 2, 0.1f, j - (int)floors[0].Width / 2));
                //int index;
                //do
                //{
                //    index = Random.Range(0, floors[0].Colors.Count);
                //}
                //while (floors[0].isMax(index));

                ////blocks[0][i, j] = Instantiate(blockPrefabs[index], floors[0].position + new Vector3(i - (int)height / 2, 0.1f, j - (int)width / 2), blockPrefabs[index].transform.rotation, floors[0]);
                //GameObject go= GameObjectPool.GetInstance().GetGameObject(floors[0].transform.position + new Vector3(i - (int)height / 2, 0.1f, j - (int)width / 2));
                //go.GetComponent<Renderer>().material.color = floors[0].Colors[index];
                //go.transform.SetParent(floors[0].transform);
                //go.GetComponent<BrickController>().NumFloor = 0;
                //floors[0].DictColor[floors[0].Colors[index]] += 1;
            }
        }
        //colors.Add(Color.blue);
        //colors.Add(Color.yellow);
        //blocks.Add(new GameObject[height,width]);

    }

    public void OnInit(int floor)
    {
        
        foreach(Color c in floors[floor].Colors)
        {
            while (!floors[floor].isMax(c))
            {
                if (l_pos.Count == 0)
                {
                    return;
                }
                int index = Random.Range(0, l_pos.Count);
                GameObject go = GameObjectPool.GetInstance().GetGameObject(l_pos[index]);
                go.GetComponent<Renderer>().material.color = c;
                go.transform.SetParent(floors[0].transform);
                go.GetComponent<BrickController>().NumFloor = 0;
                floors[0].DictColor[c] += 1;
                l_pos.RemoveAt(index);

            }
        }

    }

    public IEnumerator SpawnBrick(int floor,Color color,Vector3 pos) {
        Debug.Log("Spawn");
        floors[0].DictColor[color] -= 1;
        yield return new WaitForSeconds(5f);
        int index;
        do {
            index = Random.Range(0, floors[floor].Colors.Count);
        }
        while (floors[floor].isMax(index));
        //Instantiate(blockPrefabs[index], pos, blockPrefabs[index].transform.rotation, floors[0]);
        GameObject go = GameObjectPool.GetInstance().GetGameObject(pos);
        go.GetComponent<Renderer>().material.color = floors[0].Colors[index];
        floors[0].DictColor[floors[0].Colors[index]] += 1;
    }
}
