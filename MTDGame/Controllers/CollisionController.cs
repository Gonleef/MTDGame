using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MG
{
    public class CollisionController
    {
        private readonly Dictionary<Tuple<Type, Type>, Action<IComponentEntity, IComponentEntity>> possibleCollisions =
            new Dictionary<Tuple<Type, Type>, Action<IComponentEntity, IComponentEntity>>();

        private readonly Dictionary<Type, List<IComponentEntity>> entitiesByType = new Dictionary<Type, List<IComponentEntity>>();

        public CollisionController()
        {
            RegisterCollidablesInAssembly(Assembly.GetExecutingAssembly());
        }
        public void Add(params IComponentEntity[] entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        private void Add(IComponentEntity entity)
        {
            List<IComponentEntity> list;
            if (!entitiesByType.TryGetValue(entity.GetType(), out list))
            {
                list = new List<IComponentEntity>();
                entitiesByType.Add(entity.GetType(), list);
            }
            list.Add(entity);
        }

        public void Remove(IComponentEntity entity)
        {
            List<IComponentEntity> list;
            if (!entitiesByType.TryGetValue(entity.GetType(), out list))
            {
                return;
            }
            list.Remove(entity);
        }

        public void RegisterCollision<T1, T2>(Action<T1, T2> collisionHandler)
            where T1 : IComponentEntity where T2 : IComponentEntity
        {
            possibleCollisions.Add(
                new Tuple<Type, Type>(typeof(T1), typeof(T2)),
                (entity1, entity2) => collisionHandler((T1)entity1, (T2)entity2)
            );
        }

        public void RegisterCollidable<T1, T2>()
            where T1 : IComponentEntity, ICollidesWith<T2> where T2 : IComponentEntity
        {
            possibleCollisions.Add(
                new Tuple<Type, Type>(typeof(T1), typeof(T2)),
                (entity1, entity2) => ((T1)entity1).Collide((T2)entity2)
            );
        }

        public void RegisterCollidablesInAssembly(Assembly assembly)
        {
            var registerMethod = GetType().GetMethod("RegisterCollidable");
            var collisionRegistrations = assembly.GetTypes()
                .SelectMany(
                    type => type.GetInterfaces()
                        .Where(x => x.IsGenericType)
                        .Where(x => x.GetGenericTypeDefinition() == typeof(ICollidesWith<>))
                        .Select(x => registerMethod.MakeGenericMethod(type, x.GetGenericArguments()[0]))
                );
            foreach (var register in collisionRegistrations)
            {
                register.Invoke(this, null);
            }
        }

        public void Update()
        {
            foreach (var types in possibleCollisions)
            {
                List<IComponentEntity> active, passive;
                if (!entitiesByType.TryGetValue(types.Key.Item1, out active))
                    continue;
                if (!entitiesByType.TryGetValue(types.Key.Item2, out passive))
                    continue;
                var collisionAction = types.Value;
                foreach (var activeEntity in active)
                {
                    foreach (var passiveEntity in passive)
                    {
						if(activeEntity.GetComponent<Collidable>().Box.Intersects(passiveEntity.GetComponent<Collidable>().Box))
                        	collisionAction(activeEntity, passiveEntity);
                    }
                }
            }
        }
    }
}
