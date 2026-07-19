using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{   
    private float damage;
    private float duration;
    private float tickRate = 0.5f; // Урон каждые 0.5 сек

    private List<GameObject> enemiesInside = new List<GameObject>();
    private Coroutine damageCoroutine;

    // Настройка при создании
    public void Setup(float damage, float radius, float duration)
    {
        this.damage = damage;
        this.duration = duration;

        // Настраиваем размер 2D коллайдера
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col != null) col.radius = radius;

        // Таймер жизни зоны
        Destroy(gameObject, duration);

        // Запуск нанесения урона
        damageCoroutine = StartCoroutine(DamageTick());
    }

    // 2D Триггер: Вход врага
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Убедитесь, что у врагов стоит тег "Enemy"
        {
            if (!enemiesInside.Contains(other.gameObject))
            {
                enemiesInside.Add(other.gameObject);
            }
        }
    }

    // 2D Триггер: Выход врага
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInside.Remove(other.gameObject);
        }
    }

    // Тик урона
    private IEnumerator DamageTick()
    {
        WaitForSeconds wait = new WaitForSeconds(tickRate);

        while (true)
        {
            yield return wait;

            for (int i = enemiesInside.Count - 1; i >= 0; i--)
            {
                // Проверка на случай, если враг умер и объект уничтожен
                if (enemiesInside[i] == null)
                {
                    enemiesInside.RemoveAt(i);
                    continue;
                }
                if (enemiesInside[i].TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damage);

                }
                Destroy(enemiesInside[i].gameObject);

                // Нанесение урона
                //EnemyHealth enemyHealth = enemiesInside[i].GetComponent<EnemyHealth>();
                //if (enemyHealth != null)
                //{
                //    enemyHealth.TakeDamage(damage);
                //}
            }
        }
    }

    private void OnDestroy()
    {
        if (damageCoroutine != null) StopCoroutine(damageCoroutine);
    }
}
