using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHealth_Controller : MonoBehaviour
{
    // Variables
    public int maxHealth;
    public int actualHealth;
    public bool isCoroutineDieActive;

    [SerializeField] private Enemies_Controller _enemiesController;
    
    void Awake()
    {
        maxHealth = 100;
        actualHealth = maxHealth;

        _enemiesController = GetComponent<Enemies_Controller>();
    }
    public void EnemyDamaged(int damageValue)
    {
        if (_enemiesController.isLunged)
        {
            actualHealth -= damageValue;
            //GameManager.Instance.enemyHealthBarSlider.value = actualHealth;


            if (actualHealth <= 0)
            {
                isCoroutineDieActive = false;

                if (!isCoroutineDieActive)
                {
                    StartCoroutine(Coroutine_Die());
                }
            }
        }
    }
    
    
    public void StartLungedState()
    {
        StartCoroutine(Coroutine_LoseHealth());
    }
    

    IEnumerator Coroutine_LoseHealth()
    {
        if (_enemiesController.isLunged && !isCoroutineDieActive)
        {
            EnemyDamaged(1);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Coroutine_Die()
    {
        yield return new WaitForSeconds(2.0f);
        isCoroutineDieActive = true;
    }
}
