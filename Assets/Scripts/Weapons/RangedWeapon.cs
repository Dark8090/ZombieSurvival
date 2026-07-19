using UnityEngine;

public class RangedWeapon : WeaponBase
{
    protected RangedWeaponData rangedWeaponData => WeaponData as RangedWeaponData; // безопасное приведение типов, оператор as пробует преобразовать weaponData в RangedWeaponData
    protected Transform target;
    public RangedWeaponData RangedWeaponData => rangedWeaponData;

    [SerializeField] private protected LayerMask targetLayer;
    private float nextFireTime = 0f;
    public override void Attack()
    {
        ShootBullet();
    }

    private void FindNearestTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, rangedWeaponData.AttackRange, targetLayer.value);
        target = null;
        float minDist = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                target = enemy.transform;
            }
        }
    }

    protected void TryAttack()
    {
        FindNearestTarget();
        // Если цель есть И пора стрелять — атакуем
        if (target != null && Time.time >= nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + 1f / rangedWeaponData.FireRate; // FireRate = выстрелов в секунду
        }
    }
    private void ShootBullet()
    {
        GameObject bulletObj = Instantiate(rangedWeaponData.BulletPrefab, transform.position, Quaternion.identity);

        //Направление
        Vector2 direction = (target.position - bulletObj.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Передача ссылки пуле
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.rangedWeapon = this;
        }
        Destroy(bulletObj, 1f);
    }


    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, rangedWeaponData.AttackRange);
    //}



    //// RAYCAST ATTACK
    //[Header("Ray")]

    //[SerializeField, Min(0)] private float distance = Mathf.Infinity;
    //[SerializeField, Min(0)] private int shotCount = 1;

    //[Header("Spread")]
    //[SerializeField] private bool useSpread = false;
    //[SerializeField, Min(0)] private float spreadfactor = 1f;


    //private Vector2 CalculateSpread()
    //{
    //    return new Vector2
    //    {
    //        x = Random.Range(-spreadfactor, spreadfactor),
    //        y = Random.Range(-spreadfactor, spreadfactor)
    //    };
    //}

    //private void PerformRaycast()
    //{
    //    Vector2 direction = useSpread ? Vector2.right + CalculateSpread() : Vector2.right;
    //    Vector2 origin = (Vector2)transform.position;

    //    RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask);
    //    Collider2D hitCollider = hit.collider;

    //    if (hitCollider != null) // Можно указать ~ перед layerMask для игнорирования слоев(инвертирование)
    //    {
    //        if (hitCollider.TryGetComponent(out IDamageable damageable))
    //        {
    //            damageable.ApplyDamage(rangedWeaponData.BaseDamage);
    //        }

    //    }
    //    else
    //    {
    //        print("Не попал");
    //    }
    //}


    //#if UNITY_EDITOR
    //    private void OnDrawGizmos()
    //    {
    //        return;
    //    }
    //    private void OnDrawGizmosSelected()
    //    {
    //        Vector2 origin = (Vector2)transform.position;
    //        Vector2 direction = Vector2.right;

    //        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask);

    //        if (hit)
    //        {
    //            DrawRay(origin, direction, hit.point, hit.distance, Color.red);
    //        }
    //        else
    //        {
    //            Vector3 hitPosition = origin + direction * distance;
    //            DrawRay(origin, direction, hitPosition, hit.distance, Color.green);

    //        }
    //    }

    //    private void DrawRay(Vector2 origin, Vector2 direction, Vector2 hitPosition, float distance, Color color)
    //    {
    //        const float hitPointRadious = 0.15f;

    //        Debug.DrawRay(origin, direction * distance, color);

    //        Gizmos.color = color;
    //        Gizmos.DrawSphere(hitPosition, hitPointRadious);

    //    }

    //#endif
}

