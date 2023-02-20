using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorController : MonoBehaviour
{
    [SerializeField] float height, width;
    [SerializeField] Transform spawnPos;
    [SerializeField] int indexFloor;
    private List<Color> colors = new List<Color>();
    private int maxBrickNum;
    Dictionary<Color, int> dictColor = new Dictionary<Color, int>();
    Dictionary<Color, List<Vector3>> dictColorPos = new Dictionary<Color, List<Vector3>>();
    public float Height { get => height; set => height = value; }
    public float Width { get => width; set => width = value; }
    public List<Color> Colors { get => colors; set => colors = value; }
    public Dictionary<Color, int> DictColor { get => dictColor; set => dictColor = value; }
    public Transform SpawnPos { get => spawnPos; set => spawnPos = value; }
    public Dictionary<Color, List<Vector3>> DictColorPos { get => dictColorPos; set => dictColorPos = value; }
    public int IndexFloor { get => indexFloor; set => indexFloor = value; }

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
        //if (!dictColor.ContainsKey(Colors[index]))
        //{
        //    dictColor[Colors[index]] = 0;
        //    return false;
        //}
        return (dictColor[Colors[index]] >= maxBrickNum);
    }

    public bool isMax(Color c)
    {
        //if (!dictColor.ContainsKey(c))
        //{
        //    dictColor[c] = 0;
        //    return false;
        //}
        if (dictColor[c] >= maxBrickNum)
        {
            return true;
        }
        else return false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    Color c = collision.gameObject.GetComponent<Renderer>().material.color;
        //    if (!colors.Contains(c))
        //    {
        //        colors.Add(c);
        //        dictColor[c] = 0;
        //        dictColorPos[c] = new List<Vector3>();
        //        collision.gameObject.GetComponent<CharacterController>().CurrentFloor = this;
        //        SpawnManager.GetInstance().OnInit(indexFloor, c);

        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Color c = other.gameObject.GetComponent<Renderer>().material.color;
            if (!colors.Contains(c))
            {
                colors.Add(c);
                dictColor[c] = 0;
                dictColorPos[c] = new List<Vector3>();
                other.gameObject.GetComponent<CharacterController>().CurrentFloor = this;
                SpawnManager.GetInstance().OnInit(indexFloor, c);

            }
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

