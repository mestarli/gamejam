using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_Controller : MonoBehaviour
{
    // Variables
    public int maxHealth;
    public int actualHealth;
    public bool isCoroutineDieActive;

    [SerializeField] private Player_Controller _playerController;

    void Awake()
    {
        maxHealth = 100;
        actualHealth = maxHealth;

        _playerController = GetComponent<Player_Controller>();
    }

    public void PlayerDamaged(int damageValue)
    {
        if (_playerController.isDamaged)
        {
            actualHealth -= damageValue;
            //GameManager.Instance.playerHealthBarSlider.value = actualHealth;

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

    public void StartDamagedState()
    {
        StartCoroutine(Coroutine_LoseHealth());
    }

    IEnumerator Coroutine_LoseHealth()
    {
        if (_playerController.isDamaged && !isCoroutineDieActive)
        {
            PlayerDamaged(1);
            yield return new WaitForSeconds(1);
        }
    }
    
    IEnumerator Coroutine_Die()
    {
        yield return new WaitForSeconds(2.0f);
        isCoroutineDieActive = true;
    }
}
