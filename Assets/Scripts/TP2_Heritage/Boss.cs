using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    private bool isTargettingPlayer = false;
    private Transform target;

    override protected void Update()
    {
        if (!isTargettingPlayer)
            base.Update();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTargettingPlayer)
        {
            isTargettingPlayer = true;

            StartCoroutine(ChargeAttackRoutine());
        }
    }

    private IEnumerator ChargeAttackRoutine()
    {
        target = player;

        yield return new WaitForSeconds(2f);

        float chargeDuration = 5f;
        float elapsedTime = 0f;

        // Rush in straight line towards the player for 3 seconds
        while (elapsedTime < chargeDuration)
        {
            Debug.Log("Charging towards player...");
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * 2f * Time.deltaTime;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isTargettingPlayer = false;
    }
}
