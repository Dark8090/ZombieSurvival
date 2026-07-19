//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Entity1
//{
//    private static int id = 0;
//    public int ID { get; }

//    private Dictionary<Type, object> components = new Dictionary<Type, object>();

//    public Entity()
//    {
//        ID = id++;
//    }

//    public Entity AddComponent<T>(T component)
//    {
//        components[typeof(T)] = component;
//        return this;
//    }

//    public T? GetComponent<T>() where T : class
//    {
//        components.TryGetValue(typeof(T), out var component);
//        return component as T;
//    }

//    public bool HasComponent<T>() where T : class
//    {
//        return components.ContainsKey(typeof(T));
//    }

//    public void RemoveComponent<T>() where T : class
//    {
//        components.Remove(typeof(T));
//    }

//    public override string ToString()
//    {
//        return $"Entity ID: {ID}, Components: {string.Join(", ", components.Values)}";
//    }
//}
