using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Book_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private Enemies_Controller _enemiesController;
    [SerializeField] private EnemiesHealth_Controller _enemiesHealthController;
    [SerializeField] private Player_Controller _playerController;
    
    public bool isCoroutineLungedActive;

    public int damageEnemy = 2;
    

    private void Awake()
    {
        _enemiesController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemies_Controller>();
        _enemiesHealthController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemiesHealth_Controller>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    void Update()
    {
        ParaboleInstantiate();
    }

    private void ParaboleInstantiate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_playerController.isBookInstantiated)
            {
                GetComponent<Parabola_Controller>().FollowParabola();
            }
        }
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemiesController.isLunged = true;
            isCoroutineLungedActive = true;
            
            _enemiesHealthController.EnemyDamaged(damageEnemy);
            
            if (isCoroutineLungedActive)
            {
                _enemiesHealthController.StartLungedState();
                StartCoroutine(Coroutine_LungedActive());
            }
        }
        
        else if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject, 2.5f);
        }
    }

    IEnumerator Coroutine_LungedActive()
    {
        yield return new WaitForSeconds(2f);
        _enemiesController.isLunged = false;

        StartCoroutine(Coroutine_LungedNotActive());
    }

    IEnumerator Coroutine_LungedNotActive()
    {
        isCoroutineLungedActive = false;
        yield return new WaitForSeconds(1f);
    }
}
