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

    [SerializeField] private Transform bookSpawnPoint;
    [SerializeField] private GameObject bookPrefab;
    public bool isBookInstantiated;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        bookSpawnPoint = GameObject.FindGameObjectWithTag("SpawnBullet").GetComponent<Transform>();
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
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Coroutine_BookIsInstantiated());
            
            if (isBookInstantiated)
            {
                Instantiate(bookPrefab, bookSpawnPoint.transform.position, bookSpawnPoint.transform.rotation);
                StartCoroutine(Coroutine_BookIsNotInstantiated());
            }
        }
    }

    IEnumerator Coroutine_BookIsInstantiated()
    {
        yield return new WaitForSeconds(0.2f);
        isBookInstantiated = true;
    }
    
    IEnumerator Coroutine_BookIsNotInstantiated()
    {
        yield return new WaitForSeconds(2f);
        isBookInstantiated = false;
    }

    #endregion
}
