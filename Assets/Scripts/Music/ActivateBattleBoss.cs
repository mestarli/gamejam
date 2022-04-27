using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBattleBoss : MonoBehaviour
{
    [SerializeField] private AudioSource MainTheme;
    [SerializeField] private AudioSource BossTheme;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!BossTheme.isPlaying)
            {
                MainTheme.Stop();
                BossTheme.Play();
            }
        }
    }
}
