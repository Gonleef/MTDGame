using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MTDGame.Components;

namespace MG
{
	public class Bullet : IComponentEntity, ICollidesWith<Building>, ICollidesWith<Enemy>, ICollidesWith<ShootingEnemy>,
        ICollidesWith<BombEnemy>, ICollidesWith<Player>
    {
		private Texture2D texture;
		public Vector2 Speed { get; private set; }
		public bool Alive { get; set; }
        public Type Owner { get; private set; }
        private int Damage;

	    public Dictionary<Type, IComponent> Components { get; private set; }
	    public T GetComponent<T>()
		    where T:IComponent
	    {
		    if (HasComponent())
		    {
			    return (T)Components[typeof(T)];
		    }
		    return default(T);
	    }

	    public bool HasComponent()
	    {
		    return true;
	    }

		public Bullet(Vector2 position, Vector2 speed, Type owner, int damage)
		{
            Owner = owner;
			Speed = speed;
			texture = TextureLoader.Bullet;
			Components = new Dictionary<Type, IComponent>
			{
				{Type.GetType("MG.Position"), new Position(this, position)},
				{Type.GetType("MG.Collidable"), new Collidable(this, position, TextureLoader.Bullet)},
				{Type.GetType("MG.Transform"), new Transform(this, 0)},
				{Type.GetType("MG.Movement"), new Movement(this, speed)},
				{Type.GetType("MG.Visible"), new Visible(this, TextureLoader.Bullet)}
			};
			Alive = true;
            Damage = damage;
		}

        public void DistanceDestroy()
        {
            if (Vector2.Distance(Game1.mainPlayer.GetComponent<Position>().position, this.GetComponent<Position>().position) > 1000)
                Destroy();
        }

		public void Collide(IComponentEntity entity)
		{
            Destroy();
		}
        public void Collide(Building entity)
        {
            Destroy();
        }
        public void Collide (Player entity)
        {
	        if (Owner != typeof(Player))
	        {
		        entity.GetComponent<Health>().GetDamage(Damage);
		        Destroy();
	        }
        }

        public void Collide(Enemy entity)
        {
            if (Owner != typeof(Enemy))
            {
                entity.GetComponent<Health>().GetDamage(Damage);
                Destroy();
            }
        }

        public void Collide(ShootingEnemy entity)
        {
            if (Owner != typeof(ShootingEnemy))
            {
                entity.GetComponent<Health>().GetDamage(Damage);
                Destroy();
            }
        }

        public void Collide(BombEnemy entity)
        {
            if (Owner != typeof(BombEnemy))
            {
                entity.GetComponent<Health>().GetDamage(Damage);
                Destroy();
            }
        }

        public void Update(GameTime gameTime)
		{
			GetComponent<Movement>().Move(new Vector2(1,1));
			GetComponent<Collidable>().Update();
            DistanceDestroy();
		}

		public void Destroy()
		{
			Alive = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(GetComponent<Visible>().Texture, GetComponent<Position>().position, null, Color.White);
		}
	}
}
