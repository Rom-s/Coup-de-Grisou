using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerController playerController;

    [SerializeField] private float mapScale;

    [SerializeField] private float yOffset;
    
    private bool locked;

    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(mapScale, player.transform.position.y + yOffset , -mapScale);
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + yOffset , transform.position.z);
        
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
        transform.RotateAround(Vector3.zero,Vector3.up, 90f);
        playerController.RotateLeft();
    }

    private void RotateRight()
    {
        playerController.RotateRight();
        transform.RotateAround(Vector3.zero, Vector3.up, -90f);
    }
}
