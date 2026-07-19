using UnityEngine;

public class Potion : RangedWeapon
{
    private float NextFireTime = 0f;

    private void Update()
    {
        TryAttack();
    }
    public override void Attack()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        // 1. Ищем врагов в радиусе 
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, rangedWeaponData.AttackRange, targetLayer);

        if (enemies.Length == 0) return;

        // 2. Выбираем случайного врага
        int randomIndex = Random.Range(0, enemies.Length);

        // 3. ЗАПОМИНАЕМ позицию в момент выстрела
        Vector3 targetPos = enemies[randomIndex].transform.position;

        // 4. Создаем пулю
        GameObject bulletObj = Instantiate(rangedWeaponData.BulletPrefab, transform.position, Quaternion.identity);

        // 5. Поворачиваем пулю лицом к цели (только по оси Z в 2D)
        Vector2 direction = (targetPos - bulletObj.transform.position).normalized;

        // Защита от деления на ноль, если цель точно под игроком
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletObj.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // 6. Передаем координаты цели в скрипт пули
        BulletProjectile bulletScript = bulletObj.GetComponent<BulletProjectile>();
        if (bulletScript != null)
        {
            bulletScript.rangedWeapon = this;
            bulletScript.SetTargetPosition(targetPos); // Передаем точку, а не объект!
        }

        // Страховка: удалить пулю, если она летит слишком долго
        Destroy(bulletObj, 2f);
    }

    private new void TryAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, rangedWeaponData.AttackRange, targetLayer);

        if (enemies.Length > 0 && Time.time >= NextFireTime)
        {
            print("Атакую");
            Attack();
            NextFireTime = Time.time + 1f / rangedWeaponData.FireRate;
        }
    }
    

}
