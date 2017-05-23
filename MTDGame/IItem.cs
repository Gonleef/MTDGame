namespace MG
{
    public interface IItem
    {
        void Activate();
        IComponentEntity Owner { get; set; }
    }
}