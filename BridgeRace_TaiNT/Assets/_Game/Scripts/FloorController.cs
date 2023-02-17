using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorController : MonoBehaviour
{
    [SerializeField] float height, width;
    [SerializeField] Transform spawnPos;
    private List<Color> colors = new List<Color>();
    private int maxBrickNum;
    Dictionary<Color, int> dictColor = new Dictionary<Color, int>();

    public float Height { get => height; set => height = value; }
    public float Width { get => width; set => width = value; }
    public List<Color> Colors { get => colors; set => colors = value; }
    public Dictionary<Color, int> DictColor { get => dictColor; set => dictColor = value; }
    public Transform SpawnPos { get => spawnPos; set => spawnPos = value; }

    private void Start()
    {
        try
        {
            maxBrickNum = (int)(height * width / SpawnManager.GetInstance().PlayerNumber);
        }
        catch 
        {
            Debug.Log("Error Math");
        }
    }
    public bool isMax(int index)
    {
        if (!dictColor.ContainsKey(Colors[index]))
        {
            dictColor[Colors[index]] = 0;
            return false;
        }
        if (dictColor[Colors[index]] >= maxBrickNum)
        {
            return true;
        }
        else return false;
    }
    public bool isMax(Color c)
    {
        if (!dictColor.ContainsKey(c))
        {
            dictColor[c] = 0;
            return false;
        }
        if (dictColor[c] >= maxBrickNum)
        {
            return true;
        }
        else return false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Enter");
            colors.Add(collision.gameObject.GetComponent<Renderer>().material.color);
            SpawnManager.GetInstance().OnInit(0,collision.gameObject.GetComponent<Renderer>().material.color);
        }
        
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        colors.Remove(collision.gameObject.GetComponent<Renderer>().material.color);
    //    }
    //}
}

