using System;

namespace LDJam45.Game
{
	public interface ICard
	{
		Guid OwnerId { get; }
		Guid ID { get; }
	}
}