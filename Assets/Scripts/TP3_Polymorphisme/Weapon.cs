using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    [SerializeField] protected int Damages;

    public void Attck()
    {
        OnAttack();
    }
    protected abstract void OnAttack();

    protected virtual void InflictDamages(int amount , TP3_Polymorphisme.Enemy enemy)
    {
        enemy.TakeDamage(amount);
    }
}
