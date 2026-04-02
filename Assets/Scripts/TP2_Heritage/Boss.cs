using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss")]
    [SerializeField] private GameObject spawn;
    private Coroutine spawnCoroutine;
    public Boss(int Health = 200, int Damage = 20, float Speed = 4f, float DetectionRange = 20f) : base(Health, Damage, Speed, DetectionRange){}

    [SerializeField] private bool canSpawn;

    protected override void Start()
    {
        base.Start();
        canSpawn = true;
    }


    protected override void SetupBehaviour()
    {
        base.SetupBehaviour();
        enemyBehavior += SpawnEnemy;
    }


    private void SpawnEnemy()
    {
        if (canSpawn & spawnCoroutine == null & spawn != null)
        {
            spawnCoroutine = StartCoroutine(nameof(Spawn));
        }
    }

    private IEnumerator Spawn()
    {
        canSpawn = false;
        Instantiate(spawn, player.transform.position + player.transform.forward * 20, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        canSpawn = true;
        spawnCoroutine = null;
    }
}
