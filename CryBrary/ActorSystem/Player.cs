﻿namespace CryEngine
{
	/// <summary>
	/// WIP Player class.
	/// </summary>
    public class BasePlayer : StaticEntity
    {
        public BasePlayer() { }

        /// <summary>
        /// Initializes the player.
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="channelId"></param>
		public void InternalSpawn(EntityId entityId, int channelId)
        {
			SpawnCommon(entityId);
           // Id = entityId;
            ChannelId = channelId;
			//MonoEntity = true;
			//Spawned = true;

			//InitPhysics();

			OnSpawn();
        }

        public int ChannelId { get; set; }
		public float Health { get { return ActorSystem._GetPlayerHealth(Id); } set { ActorSystem._SetPlayerHealth(Id, value); } }
		public float MaxHealth { get { return ActorSystem._GetPlayerMaxHealth(Id); } set { ActorSystem._SetPlayerMaxHealth(Id, value); } }

        public bool IsDead() { return Health <= 0; }
    }
}
