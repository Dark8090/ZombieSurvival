//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//interface IComponent
//{

//}
//public class PositionComponent : IComponent
//{
//    public Transform Transform { get; set; }
//    public Vector3 position
//    {
//        get
//        {
//            return Transform.position;
//        }
//        set
//        {
//            Transform.position = value;
//        }
//    }


//    public override string ToString()
//    {
//        return $"X = {position.x}, Y = {position.y}";
//    }
//}
//public class VelocityComponent : IComponent
//{
//    public float Speed { get; set; } = 9f;
//    public Rigidbody2D Rigidbody2D { get; set; }
//    public Vector2 velocity
//    {
//        get
//        {
//            return Rigidbody2D.linearVelocity;
//        }
//        set
//        {
//            Rigidbody2D.linearVelocity = value;
//        }
//    }
//    public override string ToString()
//    {
//        return $"DeltaX = {velocity.x}, DeltaY = {velocity.y}";
//    }
//}

//public class BulletComponent : IComponent
//{
//    public float Speed { get; set; } = 5f;
//    public float LifeTime { get; set; } = 3f;
//    public float TimeAlive { get; set; } = 0f;
//    public Vector2 direction;
//    public Vector2 position;
//}
//public class HealthComponent : IComponent
//{
//    public int CurrentHP { get; set; }
//    public int MaxHP { get; set; }
//    public override string ToString()
//    {
//        return $"CurrentHP = {CurrentHP}, MaxHP = {MaxHP}";
//    }
//    public void TakeDamage(int damage)
//    {
//        if (CurrentHP - damage > 0)
//        {
//            CurrentHP -= damage;
//        }
//        else
//        {
//            CurrentHP = 0;
//        }


//    }
//}

////public class InputComponent : IComponent
////{
////    public float InputX => Input.GetAxisRaw("Horizontal");
////    public float InputY => Input.GetAxisRaw("Vertical");
////}
//public class ControllerComponent : IComponent
//{
//    public TypeEntity TypeEntity;
//}
//public class PlayerTagComponent : IComponent
//{

//}
//public class EnemyTagComponent : IComponent
//{

//}
//public class StateComponent : IComponent
//{
//    public State State;
//    public Vector3 PatrolTarget;
//    public float DetectionRange = 20f;
//    public float AttackRange = 10f;
//    public float AttackCooldown = 2f;
//    public float LastAttackTime = 0f;
//}
//public enum TypeEntity
//{
//    Player,
//    Bot
//}
//public enum State
//{
//    Inactivity,
//    Patrol,
//    Chase,
//    Attack
//}