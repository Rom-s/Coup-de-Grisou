using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float mapScale;
    
    private bool locked;

    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(mapScale, player.transform.position.y , -mapScale);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y , transform.position.z);
        
        if (Input.GetAxis("CameraHorizontal")==0 && locked)
        {
            locked = false;
            return;
        }

        if (Input.GetAxis("CameraHorizontal")>0 && !locked)
        {
            RotateRight();
            locked = true;
        }
        
        if (Input.GetAxis("CameraHorizontal")<0 && !locked)
        {
            RotateLeft();
            locked = true;
        }
    }

    private void RotateLeft()
    {
        transform.RotateAround(Vector3.zero,Vector3.up, -90f);
    }

    private void RotateRight()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 90f);
    }
}
