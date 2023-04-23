using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject Bullet;
    public LayerMask layer;
    public float range = 100;
    public float damage = 1;
    public float bulletForce = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.getIsPaused()) { return; }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        GameObject tempBullet = Instantiate(Bullet, Vector3.zero, Quaternion.identity);
        LineRenderer shotLine = tempBullet.GetComponent<LineRenderer>();
        shotLine.SetPosition(0, transform.position);


        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range, layer))
        {
            
            shotLine.SetPosition(1, hit.point);
            if(hit.collider.gameObject.GetComponent<EnemyBase>() != null)
            {
                EnemyBase enemy = hit.collider.gameObject.GetComponent<EnemyBase>();
                enemy.hit(damage);
            }
            if(hit.collider.tag == "Box")
            {
                //Add velocity to box
                Rigidbody box = hit.collider.GetComponent<Rigidbody>();
                box.AddForceAtPosition(playerCam.transform.forward * bulletForce, hit.point);
            }
        }
        else
        {
            //GameObject tempBullet = Instantiate(Bullet, transform);
            //LineRenderer shotLine = tempBullet.GetComponent<LineRenderer>();
            //shotLine.SetPosition(0, shotLine.transform.localPosition);
            shotLine.SetPosition(1, playerCam.transform.forward * range);
        }
    }
}
