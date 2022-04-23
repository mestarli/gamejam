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
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    
    public bool isLunged;
    
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

    IEnumerator Coroutine_DieAnim()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
