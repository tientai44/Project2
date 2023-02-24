using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData :GOSingleton<StaticData>
{
    public List<FloorController> floorLevel1 = new List<FloorController>();
    public List<FloorController> floorLevel2 = new List<FloorController>();

}
