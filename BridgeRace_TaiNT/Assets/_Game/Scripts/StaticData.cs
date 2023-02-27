using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData :GOSingleton<StaticData>
{
    public List<FloorController> floorLevel1 = new List<FloorController>();
    public Transform WinposLv1;
    public List<FloorController> floorLevel2 = new List<FloorController>();
    public Transform WinposLv2;
    public List<List<FloorController>> l_floorLevel = new List<List<FloorController>>();
    public List<Transform> l_winPos = new List<Transform>();
    private void Start()
    {
        l_floorLevel.Add(floorLevel1);
        l_floorLevel.Add(floorLevel2);
        l_winPos.Add(WinposLv1);
        l_winPos.Add(WinposLv2);
    }
}
