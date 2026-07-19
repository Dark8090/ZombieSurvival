using Arch.Buffer;
using Arch.Core;
using Arch.System;
using UnityEngine;

/* Есть следующие аттрибуты:
 * [All] - сущность должна иметь все компоненты
 * [Any] - сущность должна иметь хотя бы один компонент
 * [None] - сущность НЕ должна иметь компонент
 * 
 */


public partial class MovementSystem : BaseSystem<World, float>
{
    public MovementSystem(World world) : base(world) { }

    [Query]
    [All(typeof(Velocity), typeof(TransformRef), typeof(Rigidbody2DRef), typeof(EnemyTag))] // это вообще что то с чем то (непон)
    //[Any(typeof(EnemyTag), typeof(PlayerTag))] // либо такой аттрибут
    public void MovementEnemy([Data] in float deltaTime, ref Velocity velocity, ref TransformRef transformRef, ref Rigidbody2DRef rigidbody2DRef)
    {
        Debug.Log("Я  бегаю");

        if (Random.value < 0.01f)
        {
            velocity.Direction = Random.insideUnitCircle.normalized;
        }

        rigidbody2DRef.Value.linearVelocity = velocity.Direction * velocity.Speed; // применяем скорость к RigidBody2D
        transformRef.Value.position = rigidbody2DRef.Value.position; // обновляем трансформ
    }
}

public partial class KillSystem : BaseSystem<World, float>
{
    public KillSystem(World world) : base(world) { }


    [Query]
    //[All(typeof(GameObjectRef), typeof(Health))]
    public void Kill(ref GameObjectRef goRef, ref Health health, Entity entity)
    {
        Debug.Log("Я работаю");
        if (health.CurrentHP <= 0f)
        {
            if (goRef.Value != null)
            {
                Debug.Log("Я убил!");

                GameManager.Instance.UnregisterEntity(goRef.Value);
                Object.Destroy(goRef.Value);
            }

            World.Destroy(entity);
        }
    }
}


