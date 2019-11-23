using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    private CharacterController _controller;

    public Look spriteLooker;

    [SerializeField]
    private float Speed = 1;

    private Vector3 _velocity = Vector3.zero;

    private float Gravity = Physics.gravity.y;

    private RaycastHit Hit;

    private float oxygenBar = 100;
    [SerializeField] private float oxygenBarMax = 100;
    [SerializeField] private float oxygenLoss = 5;
    [SerializeField] private float oxygenGain = 5;

    private float CoeffHorX = 1;
    private float CoeffHorZ = 0;
    private float CoeffVerX = 0;
    private float CoeffVerZ = 1;

    private GazLevel[] _gazLevels;

    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        oxygenBar = oxygenBarMax;
        _gazLevels = FindObjectsOfType<GazLevel>();
        Debug.Log(_gazLevels.Length);
    }

    // Update is called once per frame
    void Update()
    {
        particles.Stop();
        //gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //gameObject.transform.Translate(new Vector3(0.5f * Input.GetAxis("Horizontal"), 0, 0.5f * Input.GetAxis("Vertical")));
        Vector3 move = new Vector3(CoeffHorX * Input.GetAxis("Horizontal") + CoeffVerX * Input.GetAxis("Vertical"), 0, CoeffHorZ * Input.GetAxis("Horizontal") + CoeffVerZ * Input.GetAxis("Vertical"));

        _controller.Move(move * Time.deltaTime * Speed);

        if(move == Vector3.zero)
        {
            oxygenBar -= oxygenLoss * Time.deltaTime;
            if(oxygenBar <= 0)
            {
                Debug.Log("C'est perdu");
                SceneManager.LoadScene("GameOverScene");
            }
        }
        else
        {
            oxygenBar += oxygenGain * Time.deltaTime;
            if(oxygenBar >= oxygenBarMax)
            {
                oxygenBar = oxygenBarMax;
            }
        }
        /* ce sera pour orienter le personnage.*/
        if (move != Vector3.zero)
            transform.forward = move;
        
        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        if (Input.GetButtonDown("Mine"))
        {
            if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), transform.forward, out Hit, 1))
            {
                if (Hit.collider.tag == "Coal")
                {
                    particles.Play();

                    GazLevel gazLevel = GetCurrentGazLevel();
                    gazLevel.IncreaseGazLevel();
                    if (gazLevel.GazRate > 100)
                    {
                        Debug.Log("Boum !");
                        SceneManager.LoadScene("GameOverScene");
                    }
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
            }
        }
        if(spriteLooker != null)
        {
            spriteLooker.LookCamera();
        }
        //Debug.Log("Oxygen = " + oxygenBar);
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
            if (CoeffHorX == 1)
            {

                CoeffHorX = 0;
                CoeffHorZ = -1;
                CoeffVerX = 1;
                CoeffVerZ = 0;
            }
            else
            {

                CoeffHorX = 0;
                CoeffHorZ = 1;
                CoeffVerX = -1;
                CoeffVerZ = 0;
            }
        }
        else
        {
            if (CoeffHorZ == 1)
            {

                CoeffHorX = 1;
                CoeffHorZ = 0;
                CoeffVerX = 0;
                CoeffVerZ = 1;
            }
            else
            {

                CoeffHorX = -1;
                CoeffHorZ = 0;
                CoeffVerX = 0;
                CoeffVerZ = -1;
            }
        }
    }

    GazLevel GetCurrentGazLevel()
    {
        foreach (var gl in _gazLevels)
        {
            if (gl.ActiveLevel)
                return gl;
        }

        return null;
    }
}
