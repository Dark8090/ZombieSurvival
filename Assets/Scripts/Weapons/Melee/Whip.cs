using Arch.Core;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MeleeWeapon
{
    private SpriteRenderer spriteRenderer;
    private Collider2D[] findObjects;
    private float timer = 0f;
    private float delayTimer = 0f;
    private bool isAttack = false;
    //флаг, что атака уже выполнена
    private bool attackExecuted = false;

    //TODO: Для будущего улучшения, чтобы оружие атаковало влево/вправо.
    //private bool rightDirection = true;
    //private bool leftDirection = false;

    [SerializeField] private Vector2 sizeOverlap;
    [SerializeField] private Vector2 offsetOverlap;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
    }

    private void Update()
    {
        CheckAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.GameObjectToEntity.TryGetValue(collision.gameObject, out var entityTarget)) return; // проверка, есть ли объект в Entity словаре
        if (!GameManager.Instance.World.Has<Health, EnemyTag>(entityTarget)) return; // проверяем, враг ли наш объект-энтити и есть ли у него здоровье

        ref var healthEntity = ref GameManager.Instance.World.Get<Health>(entityTarget);
        healthEntity.CurrentHP -= GetDamage();
    }

    //private void Attack()
    //{
    //    if (attackExecuted) return;
    //    attackExecuted = true;

    //    findObjects = Physics2D.OverlapCapsuleAll((Vector2)transform.position + offsetOverlap, sizeOverlap, CapsuleDirection2D.Horizontal, 0f);

    //    if (findObjects != null)
    //    {
    //        foreach (Collider2D col in findObjects)
    //        {
    //            print($"Проверяю {col.gameObject.name}");

    //            if (((1 << col.gameObject.layer) & searchLayerMask) != 0)
    //            {
    //                //if (col.TryGetComponent<IDamageable>(out IDamageable damagable))
    //                //{
    //                //    //damagable.ApplyDamage(GetDamage());
    //                //}

    //                Destroy(col.gameObject);
    //            }
    //        }
    //    }
    //}
    private void CheckAttack()
    {
        if (!isAttack)
        {
            SetVisible(false);
            timer += Time.deltaTime;
            if (timer >= meleeWeaponData.ReloadTime)
            {
                isAttack = true;
                attackExecuted = false;
                delayTimer = 0f;
                SetVisible(true);

                Attack();
            }
        }
        if (isAttack)
        {
            SetVisible(true);
            delayTimer += Time.deltaTime;

            if (delayTimer >= 2f)
            {
                SetVisible(false);
                timer = 0f;
                isAttack = false;
            }
        }

    }
    private void SetVisible(bool visible) 
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = visible;
            gameObject.GetComponent<BoxCollider2D>().enabled = visible;
        }
    }
    private void OnDrawGizmosSelected()
    {
        // Параметры из вашего кода
        Vector2 offset = offsetOverlap;
        Vector2 size = sizeOverlap;

        // Вычисляем мировой центр капсулы
        Vector2 worldCenter = (Vector2)transform.position + offset;

        Gizmos.color = Color.yellow;

        // Рисуем капсулу вручную, так как Gizmos.DrawCapsule нет в стандартном API
        float radius = size.y * 0.5f; // Радиус равен половине высоты (для горизонтальной капсулы)
        float length = Mathf.Max(0, size.x - size.y); // Длина прямой части

        Vector2 leftEnd = worldCenter + Vector2.left * (length * 0.5f);
        Vector2 rightEnd = worldCenter + Vector2.right * (length * 0.5f);

        // Рисуем два круга по краям
        Gizmos.DrawWireSphere(leftEnd, radius);
        Gizmos.DrawWireSphere(rightEnd, radius);

        // Соединяем их линиями сверху и снизу
        Gizmos.DrawLine(leftEnd + Vector2.up * radius, rightEnd + Vector2.up * radius);
        Gizmos.DrawLine(leftEnd + Vector2.down * radius, rightEnd + Vector2.down * radius);
    }
}
