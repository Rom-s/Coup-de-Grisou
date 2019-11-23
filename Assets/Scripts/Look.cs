using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public GameObject cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LookCamera()
    {
        transform.LookAt(cameraObject.transform.position,Vector3.up);
    }
}
