using System.Collections.Generic;

namespace TP5
{
    public abstract class Equipment : Collectible, IEquipable
    {
        public abstract void ActivatePassiveEffects();
        public abstract void DeactivatePassiveEffects();
    }
}