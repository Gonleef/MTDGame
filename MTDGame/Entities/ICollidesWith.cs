namespace MG
{
    public interface ICollidesWith<T> where T:IEntity
    {
        void Collide(T entity);
    }
}
