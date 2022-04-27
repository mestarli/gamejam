using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Book_Controller : MonoBehaviour
{
    // Variables
    [Header("Scripts")]
    [SerializeField] private Enemies_Controller _enemiesController;
    [SerializeField] private EnemiesHealth_Controller _enemiesHealthController;
    [SerializeField] private Player_Controller _playerController;
   
    
    [Header("Booleans")]
    [Space(15)]
    public bool isCoroutineLungedActive;

    [Header("Daño hacia enemigo")]
    [Space(15)]
    public int damageEnemy = 10;
    

    private void Awake()
    {
        // Recuperación de scripts mediante tags
        _enemiesController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemies_Controller>();
        _enemiesHealthController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemiesHealth_Controller>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    void Update()
    {
        // Llamada del método que instancia libros
        ParaboleInstantiate();
    }

    // Método que sigue una parabola e instancia un libro
    private void ParaboleInstantiate()
    {
        // Si se pulsa el botón derecho del ratón, se ejecuta el if
        if (Input.GetMouseButtonDown(1))
        {
            // Si la variable booleana isBookInstantiated, se ejecuta el 2º if
            if (_playerController.isBookInstantiated)
            {
                // Se obtiene el componente Parabola_Controller de tipo Script y se accede al método encargado de
                // seguir la parabola mediante unos puntos determinados
                GetComponent<Parabola_Controller>().FollowParabola();
            }
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // Si se produce una collision con un gameObject con tag Enemy, 
        if (other.gameObject.tag == "Enemy")
        {
            // Activación de las variables booleans isLunged y isCoroutineLungedActive
            _enemiesController.isLunged = true;
            isCoroutineLungedActive = true;
            
            // Se llama al método que se encarga de restar vida al enemigo
            _enemiesHealthController.EnemyDamaged(damageEnemy);
            
            // Si la variable boolean isCoroutineLungedActive, se producen 2 eventos
            if (isCoroutineLungedActive)
            {
                // Se llama al método que le produce el daño al enemigo
                _enemiesHealthController.StartLungedState();
                
                // Se llama a la coroutine Coroutine_LungedActive()
                StartCoroutine(Coroutine_LungedActive());
            }
        }
        
        // Si el gameObject tiene el tag Ground, 
        else if (other.gameObject.tag == "Ground")
        {
            //gameObject.SetActive(false);
        }
    }

    // Coroutine para desactivar la variable boolean isLunged y llamada de otra coroutine
    IEnumerator Coroutine_LungedActive()
    {
        yield return new WaitForSeconds(2f);
     
        // La variable boolean isLunged se pone a false
        _enemiesController.isLunged = false;

        // Se llama a la coroutine Coroutine:LungedNotActive()
        StartCoroutine(Coroutine_LungedNotActive());
    }

    // Coroutine encargada de desactivar la variable boolean isCoroutineLungedActive
    IEnumerator Coroutine_LungedNotActive()
    {
        // Se pone la variable boolean isCoroutineLungedActive a false
        isCoroutineLungedActive = false;
        yield return new WaitForSeconds(1f);
    }
}
