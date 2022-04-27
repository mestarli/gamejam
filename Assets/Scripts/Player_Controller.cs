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
    [Header("Componentes")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;

    [Header("Variables")]
    [Space(15)]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 200.0f;

    private float x, y;

    [Header("Boolean Libro")]
    [Space(15)]
    public bool isBookInstantiated;

    [Header("Variables daño player")]
    [Space(15)]
    public bool isDamaged;
    public GameObject playerSword;
    public int playerSwordDamage = 15;
    
    [SerializeField] private EnemiesHealth_Controller _enemiesHealthController;
    [SerializeField] private Enemies_Controller _enemiesController;
    [SerializeField] private GameObject book;
    
    void Awake()
    {
        // Recuperación del Animator y del rigidbody
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Se recoge y desactiva el GameObject mediante el tag PlayerSword
        playerSword = GameObject.FindGameObjectWithTag("PlayerSword");
        playerSword.SetActive(false);
        
        // Se recoge el componente EnemiesHealth_Controller mediante el tag Enemy
    }

    void Update()
    {
        // Llamada de los métodos de movimiento, ataque con espada y ataque con libro
        PlayerMovement();
        PlayerRun();
        PlayerSwordSlash();
        PlayerStun();
    }

    #region Movement

    private void PlayerMovement()
    {
        // Funciones
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
        // Animaciones player
        anim.SetFloat("Speed_X", x);
        anim.SetFloat("Speed_Y", y);
        
        // Movimiento player
        transform.Translate(0, 0, y * Time.deltaTime * speed);
        
        // Rotación player
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
    }

    private void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
            Debug.Log("Holi" + speed);
        }

        else
        {
            speed = 5.0f;
            Debug.Log("Holi" + speed);
        }
    }

    #endregion

    #region Attacks

    // Método para atacar con la espada
    public void PlayerSwordSlash()
    {
        // Si se pulsa el botón izquierdo del ratón y el collider de la espada esta activo, se ejecutará el if
        if (Input.GetMouseButtonDown(0) && playerSword == true)
        {
            // Llamada del método encargado de restarle vida al enemigo
            _enemiesHealthController.EnemyDamaged(playerSwordDamage);
            
            // Se llama a la coroutine de la espada
            StartCoroutine(Coroutine_SwordSlashAttack());
        }
    }
    
    // Método para atacar con el libro
    public void PlayerStun()
    {
        // Si se pulsa el botón derecho del raton y la variable booleana isBookInstantiated esta a false,
        //podremos lanzar otro libro
        if (Input.GetMouseButtonDown(1) && !isBookInstantiated)
        {
            // Se llama a la coroutine del libro
            StartCoroutine(Coroutine_BookIsInstantiated());
            book.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Si el player entra en contacto con el enemigo se activa el collider de la espada
        if (other.gameObject.tag == "Enemy")
        {
            playerSword.SetActive(true);
            _enemiesHealthController = other.gameObject.GetComponent<EnemiesHealth_Controller>();
            _enemiesController = other.gameObject.GetComponent<Enemies_Controller>();
            _enemiesHealthController.EnemyDamaged(playerSwordDamage);
            _enemiesController.isLunged = true;
            _enemiesHealthController.enemyHealthBarSlider.value = _enemiesHealthController.actualHealth;
        }
    }

    // Coroutine para desactivar el collider de la espada y se pueda volver a realizar el ataque con la espada
    IEnumerator Coroutine_SwordSlashAttack()
    {
        yield return new WaitForSeconds(2f);
        playerSword.SetActive(false);
    }
    
    // Coroutine para activar y desactivar la boolean para lanzar el libro
    IEnumerator Coroutine_BookIsInstantiated()
    {
        isBookInstantiated = true;
        yield return new WaitForSeconds(2f);
        isBookInstantiated = false;
    }

    #endregion
}
