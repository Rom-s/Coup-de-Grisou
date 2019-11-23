using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public GameObject cameraObject;

    private float rotationX;
    private float rotationZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LookCamera()
    {
        rotationX = transform.rotation.x;
        rotationZ = transform.rotation.z;
        transform.LookAt(cameraObject.transform.position,Vector3.up);
        transform.rotation = new Quaternion(rotationX, transform.rotation.y, rotationZ, transform.rotation.w);
    }
}
