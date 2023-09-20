using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="Building Preset",menuName ="New Building Preset")]
public class BuildingPreset : ScriptableObject
{
    public int cost;//maliyet
    public int costPerTurn;//donusum maliyeti
    public GameObject prefab;

    public int population;
    public int jobs;
    public int food;
}
