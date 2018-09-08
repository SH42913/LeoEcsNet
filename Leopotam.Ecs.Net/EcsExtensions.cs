﻿namespace Leopotam.Ecs.Net
{
    public static class EcsExtensions
    {
        public static void SendComponentToNetwork<TComponent>(this EcsWorld ecsWorld, int entity) 
            where TComponent : class, new()
        {
            ecsWorld.CreateEntityWith(out PrepareComponentToSendEvent<TComponent> prepare);
            prepare.LocalEntityUid = entity;
            prepare.ComponentFlags = 0;
        }
        
        public static void SendRemovedComponentToNetwork<TComponent>(this EcsWorld ecsWorld, int entity) 
            where TComponent : class, new()
        {
            ecsWorld.CreateEntityWith(out PrepareComponentToSendEvent<TComponent> prepare);
            prepare.LocalEntityUid = entity;
            prepare.ComponentFlags = EcsNetComponentFlags.WAS_REMOVED;
        }
        
        public static TEvent SendEventToNetwork<TEvent>(this EcsWorld ecsWorld) 
            where TEvent : class, new()
        {
            int entity = ecsWorld.CreateEntityWith(out TEvent newEvent);
            ecsWorld.CreateEntityWith(out PrepareComponentToSendEvent<TEvent> prepare);
            prepare.LocalEntityUid = entity;
            prepare.ComponentFlags = EcsNetComponentFlags.IS_EVENT;

            return newEvent;
        }

        public static void RemoveAllEntities(this EcsFilter filter)
        {
            var world = filter.GetWorld();
            for (var i = 0; i < filter.EntitiesCount; i++) {
                world.RemoveEntity (filter.Entities[i]);
            }
        }
    }
}