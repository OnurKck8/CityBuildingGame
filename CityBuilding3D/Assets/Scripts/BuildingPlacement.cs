using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;//Orada bir sey var mi?
    private bool currentlyBulldozering;//Su an silinmis mi

    private BuildingPreset curBuilidngPreset;

    private float indicatorUpdateTime = 0.05f;//Gecikme suresi veriyor ki. Ýslemci yorulmasin
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    public GameObject placementIndicator;
    public GameObject bulldozerIndicator;

    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        //if(City.Instance.money<preset.cost)
        //{
        //    return;
        //}

        currentlyPlacing = true;
        curBuilidngPreset = preset;
        placementIndicator.SetActive(true);
    }

    private void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
    }

    public void ToggleBulldoze()
    {
        currentlyBulldozering = !currentlyBulldozering;//Ne secildiyse diger duruma gec
        bulldozerIndicator.SetActive(currentlyBulldozering);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuildingPlacement();
        }

        if(Time.time-lastUpdateTime>indicatorUpdateTime)
        {
            lastUpdateTime = Time.time;

            curIndicatorPos = Selector.Instance.GetCurrentTilePos();//Tam yerlestirme

            if(currentlyPlacing)
            {
                placementIndicator.transform.position = curIndicatorPos;
            }
            else if(currentlyBulldozering)
            {
                bulldozerIndicator.transform.position = curIndicatorPos;
            }
        }

        if(Input.GetMouseButtonDown(0) && currentlyPlacing)//musaitse orasi
        {
            PlaceBuilding();
        }
        else if(Input.GetMouseButtonDown(0) && currentlyBulldozering)
        {
            Bulldoze();
        }
    }

    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuilidngPreset.prefab, curIndicatorPos, Quaternion.identity);
        City.Instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());
        CancelBuildingPlacement();
    }

    void Bulldoze()
    {
        Building buildingToDestroy = City.Instance.buildings.Find(x => x.transform.position == curIndicatorPos);
        if(buildingToDestroy!=null)
        {
            City.Instance.OnRemoveBuilding(buildingToDestroy);
        }
    }
}
