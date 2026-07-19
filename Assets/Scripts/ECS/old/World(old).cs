//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class World
//{
//    public List<Entity> entities = new List<Entity>();
//    public List<Entity> entitiesToAdd = new List<Entity>();
//    public List<Entity> entitiesToRemove = new List<Entity>();

//    public Dictionary<int, GameObject> entityViews = new Dictionary<int, GameObject>();

//    public Entity CreateEntity()
//    {
//        Entity entity = new Entity();
//        entitiesToAdd.Add(entity);
//        return entity;
//    }
//    public void DestroyEntity(Entity entity)
//    {
//        entitiesToRemove.Add(entity);
//    }

//    public IEnumerable<Entity> GetEntitiesWith<T1>()
//       where T1 : class
//    {
//        return entities.Where(entity => entity.HasComponent<T1>());
//    }
//    public IEnumerable<Entity> GetEntitiesWith<T1, T2>()
//        where T1 : class
//        where T2 : class
//    {
//        return entities.Where(entity => entity.HasComponent<T1>() && entity.HasComponent<T2>());
//    }
//    public IEnumerable<Entity> GetEntitiesWith<T1, T2, T3>()
//        where T1 : class
//        where T2 : class
//        where T3 : class
//    {
//        return entities.Where(entity => entity.HasComponent<T1>() && entity.HasComponent<T2>() && entity.HasComponent<T3>());
//    }
//    public IEnumerable<Entity> GetEntitiesWith<T1, T2, T3, T4>()
//        where T1 : class
//        where T2 : class
//        where T3 : class
//        where T4 : class
//    {
//        return entities.Where(entity => entity.HasComponent<T1>() && entity.HasComponent<T2>() && entity.HasComponent<T3>() && entity.HasComponent<T4>());
//    }
//    public IEnumerable<Entity> GetEntitiesWith<T1, T2, T3, T4, T5>()
//        where T1 : class
//        where T2 : class
//        where T3 : class
//        where T4 : class
//        where T5 : class
//    {
//        return entities.Where(entity => entity.HasComponent<T1>() && entity.HasComponent<T2>() && entity.HasComponent<T3>() && entity.HasComponent<T4>() && entity.HasComponent<T5>());
//    }
//    public void ApplyChanges()
//    {
//        foreach (var T in entitiesToAdd)
//        {
//            entities.Add(T);
//        }

//        foreach (var T in entitiesToRemove)
//        {
//            entities.Remove(T);
//        }

//        entitiesToAdd.Clear();
//        entitiesToRemove.Clear();
//    }
//}
