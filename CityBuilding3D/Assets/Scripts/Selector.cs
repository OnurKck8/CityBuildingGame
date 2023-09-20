using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private Camera cam;
    public static Selector Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cam = Camera.main;
    }

    //zemine erismek icin
    public Vector3 GetCurrentTilePos()
    {
        if(EventSystem.current.IsPointerOverGameObject())//UI'a degdiyse-Canvasa dokundaysa
        {
            return new Vector3(0,-99,0);//Bir sey yapma
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);//Hayali bir plane olusturduk.

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayOut = 0.0f;

        if(plane.Raycast(ray,out rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut)-new Vector3(0.05f,0.0f,0.5f);//Hafif offset yapmak icin
            newPos = new Vector3(Mathf.CeilToInt(newPos.x),0.0f, Mathf.CeilToInt(newPos.z));//Uste tamamlamak icin. 2.99=>3.00 yap
            return newPos;
        }

        return new Vector3(0,-99,0);
    }
}
