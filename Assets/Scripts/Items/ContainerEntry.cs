namespace Items
{
    public class ContainerEntry
    {
        public Item item;
        public int amount;

        public ContainerEntry(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }
}