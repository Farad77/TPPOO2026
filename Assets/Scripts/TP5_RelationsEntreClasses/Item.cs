namespace TP5
{
    public abstract class Item : Collectible, IUsable
    {
        public abstract void Use();

        protected void DestroyItem()
        {
            if (Owner.TryGetComponent<Inventory>(out Inventory inventory)) 
            { 
                inventory.RemoveItem(this);
            }
        }
    }
}