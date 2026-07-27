using Arch.Buffer;
using Arch.Core;
using Arch.System;
using UnityEngine;
using UnityEngine.UIElements;

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
    [All(typeof(Velocity), typeof(TransformRef), typeof(Rigidbody2DRef), typeof(SpriteRendererRef), typeof(EnemyTag))] // это вообще что то с чем то (непон)
    //[Any(typeof(EnemyTag), typeof(PlayerTag))] // либо такой аттрибут
    public void MovementEnemy([Data] in float deltaTime, ref Velocity velocity, ref TransformRef transformRef, ref Rigidbody2DRef rigidbody2DRef, ref SpriteRendererRef spriteRendererRef, ref EnemyTag enemyTag)
    {

        float distance = Vector2.Distance(GameManager.Instance.PlayerTransform.position, transformRef.Value.position);

        if (distance > enemyTag.enemyData.StoppingDistance)
        {
            Vector2 direction = (GameManager.Instance.PlayerTransform.position - transformRef.Value.position).normalized;
            rigidbody2DRef.Value.linearVelocity = direction * velocity.Speed; // применяем скорость к RigidBody2D
            transformRef.Value.position = rigidbody2DRef.Value.position; // обновляем трансформ
        }
        else
        {
            rigidbody2DRef.Value.linearVelocity = Vector2.zero;
        }



        if (GameManager.Instance.PlayerTransform.position.x < transformRef.Value.position.x)
        {
            if (spriteRendererRef.Value == null)
            {
                Debug.Log("null");
            }
            else
            {
                spriteRendererRef.Value.flipX = false;
            }
        }
        else if (GameManager.Instance.PlayerTransform.position.x > transformRef.Value.position.x)
        {
            if (spriteRendererRef.Value == null)
            {
                Debug.Log("null");
            }
            else
            {
                spriteRendererRef.Value.flipX = true;
            }
        }




        //if (Random.value < 0.01f)
        //{
        //    velocity.Direction = Random.insideUnitCircle.normalized;
        //}


    }
}

public partial class KillSystem : BaseSystem<World, float>
{
    public KillSystem(World world) : base(world) { }
    private float timer = 0f;
    public override void Initialize()
    {
        base.Initialize();
        timer = float.MaxValue;
    }


    [Query]
    //[All(typeof(GameObjectRef), typeof(Health))]
    public void Kill(ref GameObjectRef goRef, ref TransformRef transformRef, ref Health health, ref EnemyTag enemyTag, Entity entity) //TODO: добавить компонент Transform и проверять дистанцию через него
    {
        float distance = Vector2.Distance(GameManager.Instance.PlayerTransform.position, transformRef.Value.position);

        if (distance < enemyTag.enemyData.AttackRange)
        {
            if (timer >= enemyTag.enemyData.AttackDelay)
            {
                GameManager.Instance.PlayerStats.CurrentHealthPlayer -= enemyTag.enemyData.Damage;
                Debug.Log(GameManager.Instance.PlayerStats.CurrentHealthPlayer);
                timer = 0f;

                if (GameManager.Instance.PlayerStats.CurrentHealthPlayer <= 0)
                {
                    //Object.Destroy(GameManager.Instance.PlayerGameObject); //TODO: Реализовать смерть игрока

                }

            }
            else
            {
                timer += Time.deltaTime;
            }

        }
        else
        {
            timer = float.MaxValue;
        }
        
        
        

        if (health.CurrentHP <= 0f)
        {
            if (goRef.Value != null)
            {
                GameManager.Instance.UnregisterEntity(goRef.Value);
                Object.Destroy(goRef.Value);
            }

            World.Destroy(entity);
        }
    }

}




