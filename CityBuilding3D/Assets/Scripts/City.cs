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

    void Start()
    {
        UpdateStatText();
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
        statsText.text = string.Format("Day: {0} Money: {1} Population: {2} / {3} Jobs: {4} / {5} Food: {6}", 
            new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood }); ;
    }

    public void EndTurn()
    {
        day++;
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();

        UpdateStatText();

    }
    void CalculateMoney()
    {
        money += curJobs * incomePerJob;
        foreach(Building building in buildings)
        {
            money -= building.buildingPreset.costPerTurn;
        }
    }
    void CalculatePopulation()
    {
        if(curFood>=curPopulation && curPopulation<maxPopulation)
        {
            curFood -= curPopulation / 4;
            curPopulation = Mathf.Min(curPopulation+(curFood/4), maxPopulation);
        }
        else if(curFood<curPopulation)
        {
            curPopulation = curFood;
        }

    }
    void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);

    }
    void CalculateFood()
    {
        curFood = 0;

        foreach(Building building in buildings)
        {
            curFood += building.buildingPreset.food;
        }
    }
    
}
