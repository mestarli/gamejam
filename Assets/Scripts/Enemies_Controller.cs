using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public float rutine;
    public float chronometer;
    public Quaternion angle;
    public float grade;
    public GameObject target;

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
        
        EnemyMovement();
    }

    #region Enemy Movement

    private void EnemyMovement()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            anim.SetBool("walk", false);

            chronometer += 1 * Time.deltaTime;

            if (chronometer >= 4)
            {
                rutine = Random.Range(0f, 2f);
                chronometer = 0;

                switch (rutine)
                {
                    case 0:
                        anim.SetBool("walk", true);
                        break;
                    
                    case 1:
                        grade = Random.Range(0f, 360f);
                        angle = Quaternion.Euler(0, grade, 0);
                        rutine++;
                        break;
                    
                    case 2:
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                        anim.SetBool("walk", true);
                        break;
                }
            }
            
            else
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,2);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            }
        }
    }

    #endregion
    
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
