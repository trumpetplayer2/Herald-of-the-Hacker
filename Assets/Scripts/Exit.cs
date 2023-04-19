using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public AudioSource sfx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(sfx != null)
        {
            sfx.PlayOneShot(sfx.clip);
        }
        GameManager.instance.finishLevel();
    }
}
