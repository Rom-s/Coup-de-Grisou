using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Light playerLight;

    public SpriteRenderer minerSprite;

    private Vector3 lastPosition;

    private CharacterController _controller;

    public Look spriteLooker;

    [SerializeField]
    private float Speed = 1;

    private Vector3 _velocity = Vector3.zero;

    private float Gravity = Physics.gravity.y;

    private RaycastHit Hit;
    [SerializeField] private float _miningDistance = 1.5f;

    public bool oxygenChange = false;
    private float oxygenBar = 100;
    [SerializeField] private float oxygenBarMax = 100;
    [SerializeField] private float oxygenLoss = 30;
    [SerializeField] private float oxygenGain = 5;

    private float CoeffHorX = 1;
    private float CoeffHorZ = 0;
    private float CoeffVerX = 0;
    private float CoeffVerZ = 1;

    private GazLevel[] _gazLevels;

    public ParticleSystem particles;

    //Sounds:
    private FootStepAudioController _footStepAudioController;
    private PiocheAudioController _piocheAudioController;
    private PiouAudioController _piouAudioController;
    private VoiceAudioController _voiceAudioController;


    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        oxygenBar = oxygenBarMax;
        _gazLevels = FindObjectsOfType<GazLevel>();

        _footStepAudioController = GetComponentInChildren<FootStepAudioController>();
        _piocheAudioController = GetComponentInChildren<PiocheAudioController>();
        _piouAudioController = GetComponentInChildren<PiouAudioController>();
        _voiceAudioController = GetComponentInChildren<VoiceAudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition = transform.position;
        particles.Stop();
        //gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //gameObject.transform.Translate(new Vector3(0.5f * Input.GetAxis("Horizontal"), 0, 0.5f * Input.GetAxis("Vertical")));
        Vector3 move = new Vector3(CoeffHorX * Input.GetAxis("Horizontal") + CoeffVerX * Input.GetAxis("Vertical"), 0, CoeffHorZ * Input.GetAxis("Horizontal") + CoeffVerZ * Input.GetAxis("Vertical"));

        _controller.Move(move * Time.deltaTime * Speed);

        if(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") > 0)
        {
            minerSprite.flipX = false;
        }
        else if(Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") < 0)
        {
            minerSprite.flipX = true;

        }

        /* ce sera pour orienter le personnage.*/
        if (move != Vector3.zero)
            transform.forward = move;
        
        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        if (_controller.isGrounded && _velocity.y < 0)
            _velocity.y = 0f;

        if (/*move == Vector3.zero*/ transform.position == lastPosition)
        {
            playerAnimator.SetBool("IsRunning", false);

            if (oxygenChange)
            {
                oxygenBar -= oxygenLoss * Time.deltaTime;
                if (oxygenBar <= 0)
                {
                    Debug.Log("C'est perdu");
                    SceneManager.LoadScene("GameOverScene");
                }
            }

        }
        else
        {
            _footStepAudioController.PlayOne();
            playerAnimator.SetBool("IsRunning", true);
            if (oxygenChange)
            {
                oxygenBar += oxygenGain * Time.deltaTime;
                if (oxygenBar >= oxygenBarMax)
                {
                    oxygenBar = oxygenBarMax;
                }
            }
        }

        if (oxygenBar <= 30)
        {
            _voiceAudioController.PlayOne();
        }
        
        /* ce sera pour orienter le personnage.*/
        if (move != Vector3.zero)
            transform.forward = move;
        
        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        GazLevel gazLevel = GetCurrentGazLevel();

        Debug.Log(oxygenBar);

        playerLight.intensity = (oxygenBar / 100) * 2;

        
        if(spriteLooker != null)
        {
            spriteLooker.LookCamera();
        }

        if (gazLevel)
        {
            if (gazLevel.GazRate > 100)
            {
                Debug.Log("Boum !");
                SceneManager.LoadScene("GameOverScene");
            }

            if (gazLevel.GazRate >= 80)
            {
                _piouAudioController.PlayHighPanicked();
                playerAnimator.SetBool("Level2", true);
            }
            else if (gazLevel.GazRate >= 60)
            {
                _piouAudioController.PlayLowPanicked();
            }
            else if (gazLevel.GazRate < 80)
            {
                playerAnimator.SetBool("Level2", false);
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

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Mine") && lastPosition == transform.position)
        {

                if (other.tag == "Coal")
                {
                    playerAnimator.SetTrigger("IsMining");
                    particles.Play();
                    _piocheAudioController.PlayOne();

                    GazLevel gazLevel = GetCurrentGazLevel();
                    gazLevel.IncreaseGazLevel();
                    if (gazLevel.GazRate > 100)
                    {
                        Debug.Log("Boum !");
                        SceneManager.LoadScene("GameOverScene");
                    }
                    other.gameObject.GetComponent<CoalBlock>().MineBlock();
                }
                else
                {
                    Debug.Log("not coal : " + Hit.point);
                }
        }
    }
}

