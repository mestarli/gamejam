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
    
    public bool isLunged;
    public int enemyDamage = 10;

    public float rutine = 5;
    public float chronometer;
    public Quaternion angle;
    public float grade = 4;
    public GameObject target;

    public GameObject enemyPunch;

    public GameObject roseSpawn;

    void Awake()
    {
        anim = GetComponent<Animator>();

        _enemiesHealthController = GetComponent<EnemiesHealth_Controller>();
        enemyPunch = GameObject.FindGameObjectWithTag("EnemyPunch");
        enemyPunch.SetActive(false);
        
        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth_Controller>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    void Update()
    {
        if (isLunged)
        {
            anim.SetBool("Punched", true);
        }

        if (_enemiesHealthController.actualHealth <= 0 && !_enemiesHealthController.isCoroutineDieActive)
        {
            anim.SetBool("Punched", false);

            StartCoroutine(Coroutine_DieAnim());
        }
        
        EnemyMovement();
        EnemyPunch();
    }

    #region Enemy Movement

    private void EnemyMovement()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            anim.SetBool("Walk", false);

            chronometer += 1 * Time.deltaTime;

            if (chronometer >= 4)
            {
                rutine = Random.Range(0f, 2f);
                chronometer = 0;

                switch (rutine)
                {
                    case 0:
                        anim.SetBool("Walk", true);
                        break;
                    
                    case 1:
                        grade = Random.Range(0f, 360f);
                        angle = Quaternion.Euler(0, grade, 0);
                        rutine++;
                        break;
                    
                    case 2:
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                        anim.SetBool("Walk", true);
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
    
    // M??todo para atacar con los pu??os
    public void EnemyPunch()
    {
        if (enemyPunch == true)
        {
            anim.SetBool("Attack", true);
            // Llamada del m??todo encargado de restarle vida al enemigo
            _enemiesHealthController.EnemyDamaged(enemyDamage);
            
            // Se llama a la coroutine de la espada
            StartCoroutine(Coroutine_PunchAttack());
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyPunch.SetActive(true);
            _playerHealthController.PlayerDamaged(enemyDamage);
            _playerHealthController.PlayerDamaged(enemyDamage);
            _playerController.isDamaged = true;
            _enemiesHealthController.enemyHealthBarSlider.value = _enemiesHealthController.actualHealth;
            
            anim.SetBool("Attack", false);
        }
    }
    
    // Coroutine para desactivar el collider del p??o y se pueda volver a realizar el ataque
    IEnumerator Coroutine_PunchAttack()
    {
        yield return new WaitForSeconds(2f);
        enemyPunch.SetActive(false);
    }
    
    #endregion

    IEnumerator Coroutine_DieAnim()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Instantiate(roseSpawn, transform.position, roseSpawn.transform.rotation);
    }
}
