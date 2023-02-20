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
    [SerializeField] private List<FloorController> floors = new List<FloorController>();
    [SerializeField] GameObject brickPrefabs;
    private List<GameObject[,]> blocks = new List<GameObject[,]>();
    private List<GameObject[]> bricks = new List<GameObject[]>();
    private List<List<Vector3>> l_pos = new List<List<Vector3>>();
    // Ki?m soát s? ng??i ch?i trong game
    [SerializeField] int playerNumber;

    public int PlayerNumber { get => playerNumber; set => playerNumber = value; }
    public List<FloorController> Floors { get => floors; set => floors = value; }

    private void Start()
    {
        GetInstance();

        for (int i = 0; i < floors.Count; i++)
        {
            l_pos.Add(new List<Vector3>());
            SetSpawnPos(i);
        }
    }

    void SetSpawnPos(int floor)
    {
        // Add cac vi tri spawn cho g?ch
        for (int i = 0; i < floors[floor].Height; i++)
        {
            for (int j = 0; j < floors[floor].Width; j++)
            {
                l_pos[floor].Add(floors[floor].SpawnPos.transform.position + new Vector3(i - (int)floors[floor].Height / 2, 0.1f, j - (int)floors[floor].Width / 2));
            }
        }
    }

    // Spawn random pos for new color
    public void OnInit(int floor, Color c)
    {
        while (true)
        {
            if (floors[floor].isMax(c))
            {
                return;
            }
            if (l_pos[floor].Count == 0)
            {
                return;
            }
            int index = Random.Range(0, l_pos[floor].Count);
            GameObject go = GameObjectPool.GetInstance().GetGameObject(l_pos[floor][index]);
            go.GetComponent<Renderer>().material.color = c;
            go.transform.SetParent(floors[floor].transform);
            go.GetComponent<BrickController>().NumFloor = floor;
            floors[floor].DictColorPos[c].Add(go.transform.position);
            floors[floor].DictColor[c] += 1;
            l_pos[floor].RemoveAt(index);
        }
    }

    public IEnumerator SpawnBrick(int floor, Color color, Vector3 pos)
    {
        floors[floor].DictColor[color] -= 1;
        floors[floor].DictColorPos[color].Remove(pos);
        yield return new WaitForSeconds(5f);
        int index;
        List<Color> temp = new List<Color>();
        // Add color brick not in limit
        foreach (Color c in floors[floor].Colors)
        {
            if (!floors[floor].isMax(c))
            {
                temp.Add(c);
            }
        }
        // spawn random 1 color form list color not limit
        if (temp.Count > 0)
        {
            index = Random.Range(0, temp.Count);
            GameObject go = GameObjectPool.GetInstance().GetGameObject(pos);
            go.GetComponent<Renderer>().material.color = temp[index];
            go.GetComponent<BrickController>().NumFloor = floor;
            floors[floor].DictColor[temp[index]] += 1;
            floors[floor].DictColorPos[temp[index]].Add(go.transform.position);
        }
    }
}
