using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth_Controller : MonoBehaviour
{
    // Variables
    public int maxHealth;
    public int actualHealth;
    public bool isCoroutineDieActive;

    [SerializeField] private Player_Controller _playerController;

    void Awake()
    {
        maxHealth = 300;
        actualHealth = maxHealth;

        _playerController = GetComponent<Player_Controller>();
    }

    public void PlayerDamaged(int damageValue)
    {
        if (_playerController.isDamaged)
        {
            actualHealth -= damageValue;
            GameManager.Instance.playerHealthBarSlider.value = actualHealth;

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

    IEnumerator Coroutine_Die()
    {
        yield return new WaitForSeconds(2.0f);
        isCoroutineDieActive = true;
        SceneManager.LoadScene("GameOver");
    }
}
