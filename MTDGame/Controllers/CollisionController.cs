using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MG
{
    public class CollisionController
    {
        private readonly Dictionary<Tuple<Type, Type>, Action<IEntity, IEntity>> possibleCollisions =
            new Dictionary<Tuple<Type, Type>, Action<IEntity, IEntity>>();

        private readonly Dictionary<Type, List<IEntity>> entitiesByType = new Dictionary<Type, List<IEntity>>();

        public CollisionController()
        {
            RegisterCollidablesInAssembly(Assembly.GetExecutingAssembly());
        }
        public void Add(params IEntity[] entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        private void Add(IEntity entity)
        {
            List<IEntity> list;
            if (!entitiesByType.TryGetValue(entity.GetType(), out list))
            {
                list = new List<IEntity>();
                entitiesByType.Add(entity.GetType(), list);
            }
            list.Add(entity);
        }

        public void Remove(IEntity entity)
        {
            List<IEntity> list;
            if (!entitiesByType.TryGetValue(entity.GetType(), out list))
            {
                return;
            }
            list.Remove(entity);
        }

        public void RegisterCollision<T1, T2>(Action<T1, T2> collisionHandler)
            where T1 : IEntity where T2 : IEntity
        {
            possibleCollisions.Add(
                new Tuple<Type, Type>(typeof(T1), typeof(T2)),
                (entity1, entity2) => collisionHandler((T1)entity1, (T2)entity2)
            );
        }

        public void RegisterCollidable<T1, T2>()
            where T1 : IEntity, ICollidesWith<T2> where T2 : IEntity
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
                List<IEntity> active, passive;
                if (!entitiesByType.TryGetValue(types.Key.Item1, out active))
                    continue;
                if (!entitiesByType.TryGetValue(types.Key.Item2, out passive))
                    continue;
                var collisionAction = types.Value;
                foreach (var activeEntity in active)
                {
                    foreach (var passiveEntity in passive)
                    {
						if(activeEntity.Box.Intersects(passiveEntity.Box))
                        	collisionAction(activeEntity, passiveEntity);
                    }
                }
            }
        }
    }
}
