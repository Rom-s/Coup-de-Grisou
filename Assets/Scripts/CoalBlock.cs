using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBlock : MonoBehaviour
{
    private float BlockLife = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BlockLife == 0)
        {
            Debug.Log("Block Destroyed");
            ++ScoreManager.Instance.score;
            GameObject.Destroy(gameObject);
        }
    }

    public void MineBlock()
    {
        BlockLife -= 1;
        Debug.Log("MiningBlock");
    }
}
