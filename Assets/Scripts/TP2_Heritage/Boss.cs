using System;
using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float shootInterval = 2f;

    [SerializeField]
    private float shootingRange = 10f;

    [SerializeField]
    private bool canShot = true;


    protected override void Update()
    {
        if (player.gameObject.TryGetComponent<TP1_Encapsulation.Correction.PlayerCharacter>(out var playerCharacter))
        {
            if (!playerCharacter.IsDead)
            {
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance < shootingRange)
                {
                    if (canShot)
                        Shoot();
                }

                else if (distance < detectionRange)
                {


                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;

                }
            }
        }

    }


    private void Shoot()
    {
        if (bullet != null && player != null)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            Projectile projectile = newBullet.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.Target = player;
            }
            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShot = false;
        yield return new WaitForSeconds(shootInterval);
        canShot = true;
    }

    protected override void OnCollisionEnter(Collision collision)
    {

    }

}
