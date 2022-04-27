using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth_Controller : MonoBehaviour
{
    // Variables
    public int maxHealth;
    public int actualHealth;
    public bool isCoroutineDieActive;
    public Slider bossHealthBarSlider;

    
    [SerializeField] private Boss_Controller _bossController;
    
    void Awake()
    {
        maxHealth = 400;
        actualHealth = maxHealth;

        _bossController = GetComponent<Boss_Controller>();
    }
    public void EnemyDamaged(int damageValue)
    {
        if (_bossController.isLunged)
        {
            actualHealth -= damageValue; 
            bossHealthBarSlider.value = actualHealth;


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
    
    
    public void StartBossLungedState()
    {
        StartCoroutine(Coroutine_LoseHealth());
    }
    

    IEnumerator Coroutine_LoseHealth()
    {
        if (_bossController.isLunged && !isCoroutineDieActive)
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
