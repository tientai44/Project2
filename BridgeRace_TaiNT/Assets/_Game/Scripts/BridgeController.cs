using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private Transform startPos;
    [SerializeField] private GameObject bridgeBrickPrefab;
    [SerializeField] private float bridgeBrickLength;
    private List<GameObject> bricks = new List<GameObject>();
    private Vector3 offset = new Vector3(0, 0, 1f);
    private void Start()
    {
        for(int i = 0; i < length; i++)
        bricks.Add(Instantiate(bridgeBrickPrefab, startPos.position + bricks.Count * offset, transform.rotation));
    }
    
    private void AddBrick(Color c)
    {
        bricks.Add(Instantiate(bridgeBrickPrefab, startPos.position + bricks.Count*bridgeBrickLength*new Vector3(0,0,1),bridgeBrickPrefab.transform.rotation,transform));
        bricks[bricks.Count - 1].GetComponent<Renderer>().material.color = c;
    }

}
