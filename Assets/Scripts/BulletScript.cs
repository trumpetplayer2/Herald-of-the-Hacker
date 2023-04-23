using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime = .5f;
    private void Awake()
    {
        Destroy(this.gameObject, lifeTime);
    }
}
