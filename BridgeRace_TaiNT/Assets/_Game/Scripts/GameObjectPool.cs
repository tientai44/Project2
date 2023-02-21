using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : GOSingleton<GameObjectPool>
{
    [SerializeField]private GameObject Prefabs;
    [SerializeField]private  List<GameObject> pools = new List<GameObject>();

    public GameObject GetGameObject(Vector3 pos)
    {
        if (pools.Count == 0)
        {
            GameObject go = Instantiate(Prefabs, pos, Prefabs.transform.rotation);
            go.SetActive(true);
            return go;
        }
        else
        {
            GameObject go = pools[0];
            go.SetActive(true);
            go.transform.position = pos;
            pools.RemoveAt(0);
            return go;
        }
    }

    public void ReturnGameObject(GameObject go)
    {
        go.transform.rotation = Prefabs.transform.rotation;
        pools.Add(go);
        go.SetActive(false);
    }

}

