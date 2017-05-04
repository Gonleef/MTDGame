namespace MG
{
    public interface IItem
    {
        void Activate();
        IEntity Owner { get; set; }
    }
}