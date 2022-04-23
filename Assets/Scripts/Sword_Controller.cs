using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controller : MonoBehaviour
{
    // Variables
    public GameObject playerSword;
    public int playerSwordDamage;

    public GameObject enemySword;
    public int enemySwordDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        playerSword = GameObject.FindGameObjectWithTag("PlayerSword");
        playerSword.GetComponent<BoxCollider>();
        
        enemySword = GameObject.FindGameObjectWithTag("EnemySword");
        enemySword.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
