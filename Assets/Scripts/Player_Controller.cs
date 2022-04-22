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
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        bookSpawnPoint = GameObject.FindGameObjectWithTag("SpawnBullet").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerAttacks();
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

    private void PlayerAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ArmAttack");
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("BookAttack");
            Instantiate(bookPrefab, bookSpawnPoint.transform.position, bookSpawnPoint.transform.rotation);
        }
    }

    #endregion
}
