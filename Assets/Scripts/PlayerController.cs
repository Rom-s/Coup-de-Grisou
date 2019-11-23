using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float Speed = 1;

    private Vector3 _velocity = Vector3.zero;

    private float Gravity = Physics.gravity.y;

    private RaycastHit Hit;

    private float CoeffHorX = 1;
    private float CoeffHorZ = 0;
    private float CoeffVerX = 0;
    private float CoeffVerZ = 1;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //gameObject.transform.Translate(new Vector3(0.5f * Input.GetAxis("Horizontal"), 0, 0.5f * Input.GetAxis("Vertical")));
        Vector3 move = new Vector3(CoeffHorX * Input.GetAxis("Horizontal") + CoeffVerX * Input.GetAxis("Vertical"), 0, CoeffHorZ * Input.GetAxis("Horizontal") + CoeffVerZ * Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * Speed);
        /* ce sera pour orienter le personnage.*/
        if (move != Vector3.zero)
            transform.forward = move;
        
        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, transform.forward, out Hit, 1))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.yellow);
                if (Hit.collider.tag == "Coal")
                {
                    Debug.Log("coal hitted : " + Hit.point);
                    Hit.collider.gameObject.GetComponent<CoalBlock>().MineBlock();
                }
                else
                {
                    Debug.Log("not coal : " + Hit.point);
                }
            }
            else
            {
                Debug.Log("no hit");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
        }
    }

    public void RotateRight()
    {
        if(CoeffHorZ == 0)
        {
            
            if (CoeffHorX == 1)
            {
                
                CoeffHorX = 0;
                CoeffHorZ = 1;
                CoeffVerX = -1;
                CoeffVerZ = 0;
}
            else
            {
                
                CoeffHorX = 0;
                CoeffHorZ = -1;
                CoeffVerX = 1;
                CoeffVerZ = 0;
            }
        }
        else
        {
            
            if (CoeffHorZ == 1)
            {
                
                CoeffHorX = -1;
                CoeffHorZ = 0;
                CoeffVerX = 0;
                CoeffVerZ = -1;
            }
            else
            {
                
                CoeffHorX = 1;
                CoeffHorZ = 0;
                CoeffVerX = 0;
                CoeffVerZ = 1;
            }
        }
    }

    public void RotateLeft()
    {
        if (CoeffHorZ == 0)
        {
            Debug.Log("Plop");
            if (CoeffHorX == 1)
            {
                Debug.Log("1");
                CoeffHorX = 0;
                CoeffHorZ = -1;
                CoeffVerX = 1;
                CoeffVerZ = 0;
            }
            else
            {
                Debug.Log("2");
                CoeffHorX = 0;
                CoeffHorZ = 1;
                CoeffVerX = -1;
                CoeffVerZ = 0;
            }
        }
        else
        {
            Debug.Log("Plip");
            if (CoeffHorZ == 1)
            {
                Debug.Log("3");
                CoeffHorX = 1;
                CoeffHorZ = 0;
                CoeffVerX = 0;
                CoeffVerZ = 1;
            }
            else
            {
                Debug.Log("4");
                CoeffHorX = -1;
                CoeffHorZ = 0;
                CoeffVerX = 0;
                CoeffVerZ = -1;
            }
        }
    }
}
