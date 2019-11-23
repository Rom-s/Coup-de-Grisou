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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 1))
            {
                if(Hit.collider.tag == "Coal")
                {
                    Hit.collider.gameObject.GetComponent<CoalBlock>().MineBlock();
                }
            }
        }
    }
}
