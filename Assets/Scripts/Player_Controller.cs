using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]

public class Player_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 200.0f;

    private float x, y;

    public bool isBookInstantiated;

    public bool isDamaged;
    public GameObject playerSword;
    public int playerSwordDamage = 3;
    
    [SerializeField] private EnemiesHealth_Controller _enemiesHealthController;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        playerSword = GameObject.FindGameObjectWithTag("PlayerSword");
        playerSword.SetActive(false);
        
        _enemiesHealthController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemiesHealth_Controller>();
    }

    void Update()
    {
        PlayerMovement();
        PlayerSwordSlash();
        PlayerStun();
    }

    #region Movement

    private void PlayerMovement()
    {
        // Funciones
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
        anim.SetFloat("Speed_X", x);
        anim.SetFloat("Speed_Y", y);
        
        // Movimiento player
        transform.Translate(0, 0, y * Time.deltaTime * speed);
        
        // Rotación player
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
    }

    #endregion

    #region Attacks

    public void PlayerSwordSlash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ArmAttack");
            
        }
    }
    
    public void PlayerStun()
    {
        if (Input.GetMouseButtonDown(1) && !isBookInstantiated)
        {
            StartCoroutine(Coroutine_BookIsInstantiated());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerSword.SetActive(true);
            
            if (Input.GetMouseButtonDown(0) && playerSword == true)
            {
                _enemiesHealthController.EnemyDamaged(playerSwordDamage);
            }
        }
    }

    IEnumerator Coroutine_BookIsInstantiated()
    {
        isBookInstantiated = true;
        yield return new WaitForSeconds(2f);
        isBookInstantiated = false;
    }

    #endregion
}
