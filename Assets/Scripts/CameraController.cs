﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField] private float mapScale;

    [SerializeField] private float yOffset;

    [SerializeField] private float rotationTime;

    private float _currentRotationTime;
    private bool _isRotating;
    private int _rotationFactor;
    
    private bool _locked;

    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(mapScale, player.transform.position.y + yOffset , -mapScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRotating)
        {
            float deltaTime = Time.deltaTime;
            if (_currentRotationTime + Time.deltaTime > rotationTime)
            {
                deltaTime = rotationTime - _currentRotationTime;
                _isRotating = false;
            }
            else
            {
                _currentRotationTime += Time.deltaTime;
            }
            transform.RotateAround(Vector3.zero, Vector3.up, _rotationFactor * deltaTime * 90 / rotationTime);
        }
        transform.position = new Vector3(transform.position.x, player.transform.position.y + yOffset , transform.position.z);
        
        if (Input.GetAxis("CameraHorizontal")==0 && _locked)
        {
            _locked = false;
            return;
        }

        if (Input.GetAxis("CameraHorizontal")>0 && !_locked && !_isRotating)
        {
            RotateRight();
            _locked = true;
        }
        
        if (Input.GetAxis("CameraHorizontal")<0 && !_locked && !_isRotating)
        {
            RotateLeft();
            _locked = true;
        }
    }

    private void RotateLeft()
    {
        //transform.RotateAround(Vector3.zero,Vector3.up, 90f);
        _playerController.RotateLeft();
        _rotationFactor = 1;
        _isRotating = true;
        _currentRotationTime = 0;
    }

    private void RotateRight()
    {

        //transform.RotateAround(Vector3.zero, Vector3.up, -90f);
        _playerController.RotateRight();
        _rotationFactor = -1;
        _isRotating = true;
        _currentRotationTime = 0;
    }
}
