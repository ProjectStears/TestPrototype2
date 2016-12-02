using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerData {

    public long Id;
    public string Name;
    public float Latitude;
    public float Longitude;
    public float Rotation;

    //Note: Module offset Z: -0.04
    public List<ModuleData> Modules;

    public GameObject GameObjectReference;
}
