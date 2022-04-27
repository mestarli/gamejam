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
    [SerializeField] private Player_Controller _playerController;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    
    public bool isLunged;
    public int enemyDamage = 10;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        _enemiesHealthController = GetComponent<EnemiesHealth_Controller>();
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

    #region DamageToPlayer
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerHealthController.PlayerDamaged(enemyDamage);
            _playerHealthController = other.gameObject.GetComponent<PlayerHealth_Controller>();
            _playerController = other.gameObject.GetComponent<Player_Controller>();
            _playerHealthController.PlayerDamaged(enemyDamage);
            _playerController.isDamaged = true;
            _enemiesHealthController.enemyHealthBarSlider.value = _enemiesHealthController.actualHealth;
        }
    }
    
    #endregion

    IEnumerator Coroutine_DieAnim()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
