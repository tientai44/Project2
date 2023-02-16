using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SpawnManager : GOSingleton<SpawnManager>
{
    //Ki?m soát các Floor
    [SerializeField] private List<FloorController> floors = new List<FloorController> ();
    [SerializeField] GameObject brickPrefabs;
    [SerializeField] private int height, width;
    private List<GameObject[,]> blocks = new List<GameObject[,]>();
    private List<GameObject[]> bricks = new List<GameObject[]>();
    // Ki?m soát s? ng??i ch?i trong game
    [SerializeField] int playerNumber;

    public int PlayerNumber { get => playerNumber; set => playerNumber = value; }

    private void Start()
    {
        GetInstance();
        //colors.Add(Color.blue);
        //colors.Add(Color.yellow);
        //blocks.Add(new GameObject[height,width]);
        OnInit ();

    }

    private void OnInit()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int index;
                do
                {
                    index = Random.Range(0, floors[0].Colors.Count);
                }
                while (floors[0].isMax(index) == false);

                //blocks[0][i, j] = Instantiate(blockPrefabs[index], floors[0].position + new Vector3(i - (int)height / 2, 0.1f, j - (int)width / 2), blockPrefabs[index].transform.rotation, floors[0]);
                GameObject go= GameObjectPool.GetInstance().GetGameObject(floors[0].transform.position + new Vector3(i - (int)height / 2, 0.1f, j - (int)width / 2));
                go.GetComponent<Renderer>().material.color = floors[0].Colors[index];
                go.transform.SetParent(floors[0].transform);
                go.GetComponent<BrickController>().NumFloor = 0;
                floors[0].DictColor[floors[0].Colors[index]] += 1;
            }
        }
    }

    public IEnumerator SpawnBrick(int floor,Vector3 pos) {
        Debug.Log("Spawn");
        yield return new WaitForSeconds(5f);
        int index;
        do
        {
            index = Random.Range(0, floors[0].Colors.Count);
        }
        while (floors[0].isMax(index) == false);
        //Instantiate(blockPrefabs[index], pos, blockPrefabs[index].transform.rotation, floors[0]);
        GameObject go = GameObjectPool.GetInstance().GetGameObject(pos);
        go.GetComponent<Renderer>().material.color = floors[0].Colors[index];
        floors[0].DictColor[floors[0].Colors[index]] += 1;
    }
}
