using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Controller : MonoBehaviour
{
    // Variables
    public BoxCollider playerSword;
    public int playerSwordDamage = 3;

    public BoxCollider enemySword;
    public int enemySwordDamage = 3;

    [SerializeField] private PlayerHealth_Controller _playerHealthController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerSword = GetComponent<BoxCollider>();
        playerSword.enabled = false;

        

        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        

       
    }
}
