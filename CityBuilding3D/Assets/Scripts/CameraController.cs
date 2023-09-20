using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    [Header("Move")]
    public float moveSpeed;

    [Header("Rotate")]
    public float rotateSpeed;
    public float minXRot;
    public float maxXRot;
    private float curXRot;

    [Header("Zoom")]
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    private float curZoom;

  
    private void Start()
    {
        cam = Camera.main;

        curZoom = cam.transform.localPosition.y;
        curXRot = -50;
    }

    private void Update()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, 20, 50);//curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);

        cam.transform.localPosition = Vector3.up * curZoom;

        if(Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);//curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + (x + rotateSpeed), 0.0f);
        }

        //Movement
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;//Yukari dogru giderken Y de hareket etme
        forward.Normalize();

        Vector3 right = cam.transform.right;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();

        dir *= moveSpeed * Time.deltaTime;

        transform.position += dir;

    }
}
