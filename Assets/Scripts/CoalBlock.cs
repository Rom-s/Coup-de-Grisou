using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBlock : MonoBehaviour
{
    [SerializeField] private GameObject popAudioSource;
    
    private float BlockLife = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BlockLife <= 0)
        {
            ++ScoreManager.Instance.score;
            Instantiate(popAudioSource, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void MineBlock()
    {
        BlockLife -= 1;
        Debug.Log("Block life = " + BlockLife);
    }
}
