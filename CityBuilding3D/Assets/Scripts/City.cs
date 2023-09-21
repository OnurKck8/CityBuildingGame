using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MonoBehaviour
{
    public static City Instance;

    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public int maxPopulation;
    public int maxJobs;
    public int incomePerJob;//Is basi gelir

    public TextMeshProUGUI statsText;
    public List<Building> buildings = new List<Building>();

    private void Awake()
    {
        Instance = this;
    }
    public void OnPlaceBuilding(Building building)
    {
        money -= building.buildingPreset.cost;
        maxPopulation += building.buildingPreset.population;
        maxJobs += building.buildingPreset.jobs;
        buildings.Add(building);
        UpdateStatText();
    }
    public void OnRemoveBuilding(Building building)
    {
        maxPopulation -= building.buildingPreset.population;
        maxJobs -= building.buildingPreset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText();
    }


    private void UpdateStatText()
    {
        
    }
}
