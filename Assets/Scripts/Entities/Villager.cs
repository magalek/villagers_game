namespace Entities
{
    public class Villager : Entity
    {
        public float gatheringSpeed = 1;
    }

    public interface IProcessable
    {
        void Process();
    }
}