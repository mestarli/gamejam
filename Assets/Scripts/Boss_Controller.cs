using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Boss_Controller : MonoBehaviour

{
    // Variables
    [SerializeField] private BossHealth_Controller _bossHealthController;
    [SerializeField] private PlayerHealth_Controller _playerHealthController;
    [SerializeField] private Player_Controller _playerController;
    [SerializeField] private Animator anim;
    
    public bool isLunged;
    public int enemyDamage = 15;

    public float rutine = 5;
    public float chronometer;
    public Quaternion angle;
    public float grade = 4;
    public GameObject target;

    public GameObject daga;
    public Transform dagaSpawn;

    public GameObject roseSpawn;

    void Awake()
    {
        anim = GetComponent<Animator>();

        _bossHealthController = GetComponent<BossHealth_Controller>();
        daga = GameObject.FindGameObjectWithTag("Daga");
        daga.SetActive(false);
        
        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth_Controller>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
    }

    void Update()
    {
        if (isLunged)
        {
            anim.SetBool("Damage", true);
        }

        if (_bossHealthController.actualHealth <= 0 && !_bossHealthController.isCoroutineDieActive)
        {
            anim.SetBool("Damage", false);

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
    
    // Método para atacar con los puños
    public void EnemyPunch()
    {
        if (daga == true)
        {
            anim.SetBool("Attack", true);
            // Llamada del método encargado de restarle vida al enemigo
            _bossHealthController.EnemyDamaged(enemyDamage);
            
            // Se llama a la coroutine de la daga
            StartCoroutine(Coroutine_PunchAttack());
            StartCoroutine(Coroutine_DagaIsInstantiated());
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            daga.SetActive(true);
            _playerHealthController.PlayerDamaged(enemyDamage);
            _playerHealthController.PlayerDamaged(enemyDamage);
            _playerController.isDamaged = true;
            _bossHealthController.bossHealthBarSlider.value = _bossHealthController.actualHealth;
            
            anim.SetBool("Attack", false);
        }
    }
    
    // Coroutine para activar y desactivar la boolean para lanzar el libro
    IEnumerator Coroutine_DagaIsInstantiated()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject libro = Instantiate(daga, dagaSpawn.position, Quaternion.identity);
        libro.GetComponent<Rigidbody>().AddForce(dagaSpawn.forward * 400);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Attack", false);
    }
    
    // Coroutine para desactivar la daga y se pueda volver a realizar el ataque
    IEnumerator Coroutine_PunchAttack()
    {
        yield return new WaitForSeconds(2f);
        daga.SetActive(false);
    }
    
    #endregion

    IEnumerator Coroutine_DieAnim()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Instantiate(roseSpawn, transform.position, roseSpawn.transform.rotation);
    }
}
