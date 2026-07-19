using Arch.Buffer;
using Arch.Core;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public RangedWeapon rangedWeapon;
    public float speed = 10f;
    //public float damage = 0f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.GameObjectToEntity.TryGetValue(collision.gameObject, out Entity targetEntity)) // проверка, есть ли объект в Entity словаре
        {
            Debug.Log("Зашел №1");
            return;
        }
        if (!GameManager.Instance.World.Has<EnemyTag>(targetEntity)) // проверяем, враг ли наш объект-энтити
        {
            Debug.Log("Зашел №2");

            return;
        }
        if (!GameManager.Instance.World.Has<Health>(targetEntity)) // проверяем, есть ли здоровье у нашего объект-энтити
        {
            Debug.Log("Зашел №3");

            return;
        }
        
        ref var health = ref GameManager.Instance.World.Get<Health>(targetEntity);
        health.CurrentHP -= rangedWeapon.GetDamage();

        if (health.CurrentHP < 0)
        {
            CommandBuffer commandBuffer = new CommandBuffer(); 
            commandBuffer.Destroy(targetEntity);
            commandBuffer.Playback(GameManager.Instance.World);
            commandBuffer.Dispose();
        }

        Destroy(gameObject);

    }
}
