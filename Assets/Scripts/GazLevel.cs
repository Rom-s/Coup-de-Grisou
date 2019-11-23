using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazLevel : MonoBehaviour
{
    [SerializeField] private float increaseStep;

    [SerializeField] private float decreaseSpeed;

    public float GazRate { get; private set; }

    public bool ActiveLevel { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        GazRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GazRate = Mathf.Max(0, GazRate - decreaseSpeed * Time.deltaTime);
    }

    public void IncreaseGazLevel()
    {
        GazRate += increaseStep;
    }

    private void OnTriggerEnter(Collider other)
    {
        ActiveLevel = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ActiveLevel = false;
    }
}
