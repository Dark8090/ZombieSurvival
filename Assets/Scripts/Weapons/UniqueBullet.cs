using Arch.Buffer;
using Arch.Core;
using UnityEngine;

public class UniqueBullet : MonoBehaviour
{
    public UniqueWeapon UniqueWeapon;
    public float Speed = 5f;

    private Vector2 direction;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = direction * Speed;
        //transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.GameObjectToEntity.TryGetValue(collision.gameObject, out Entity targetEntity)) return; // проверка, есть ли объект в Entity словаре
        if (!GameManager.Instance.World.Has<EnemyTag, Health>(targetEntity)) return; // проверяем, враг ли наш объект-энтити и есть ли у него здоровье

        ref var health = ref GameManager.Instance.World.Get<Health>(targetEntity);
        health.CurrentHP -= UniqueWeapon.GetDamage();

        Destroy(gameObject); // уничтожаем пулю при попадании
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }
}







