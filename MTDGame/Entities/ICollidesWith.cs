namespace MG
{
    public interface ICollidesWith<T> where T:IComponentEntity
    {
        void Collide(T entity);
    }
}
