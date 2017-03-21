using System;
using Microsoft.Xna.Framework;

namespace MG
{
	public interface IEntity
	{
		Rectangle Box { get; set; }

		void Collide(IEntity entity);
	}
}
