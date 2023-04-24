using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playOnce : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = this.GetComponent<AudioSource>();
        source.playOnAwake = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying) { return; }
        Destroy(this.gameObject);
    }
}
