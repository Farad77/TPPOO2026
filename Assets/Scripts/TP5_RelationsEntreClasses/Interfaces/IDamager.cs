using UnityEngine;

namespace TP5
{
    public interface IDamager
    {
        public void InflictDamageOnTarget(GameObject target, int damage);
    }
}
