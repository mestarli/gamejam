using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBattleBoss : MonoBehaviour
{
    [SerializeField] private AudioSource MainTheme;
    [SerializeField] private AudioSource BossTheme;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!MainTheme.isPlaying)
            {
                MainTheme.Play();
                BossTheme.Stop();
            }
            
        }
    }
}
