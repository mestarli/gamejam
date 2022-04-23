using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Enemies_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private EnemiesHealth_Controller _enemiesHealthController;
    [SerializeField] private PlayerHealth_Controller _playerHealthController;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    
    public bool isLunged;
    public GameObject enemySword;
    public int enemySwordDamage = 3;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        _enemiesHealthController = GetComponent<EnemiesHealth_Controller>();
        enemySword = GameObject.FindGameObjectWithTag("EnemySword");
        enemySword.SetActive(false);
    }

    void Update()
    {
        if (isLunged)
        {
            anim.SetBool("Die", false);
            anim.SetBool("Punched", true);
        }

        if (_enemiesHealthController.actualHealth <= 0 && !_enemiesHealthController.isCoroutineDieActive)
        {
            anim.SetBool("Die", true);
            anim.SetBool("Punched", false);

            StartCoroutine(Coroutine_DieAnim());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            { 
                enemySword.SetActive(true);
                _playerHealthController.PlayerDamaged(enemySwordDamage);
            }
        }
    }

    IEnumerator Coroutine_DieAnim()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
