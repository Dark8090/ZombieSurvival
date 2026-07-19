using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public RangedWeapon rangedWeapon; // Ссылка на оружие
    public GameObject explosionAreaPrefab; // Префаб лужи (Zone)

    [Header("Параметры пули")]
    public float speed = 6f;

    private Vector3 targetPosition;
    private bool hasTarget = false;

    // Установка точки назначения
    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        targetPosition.z = 0; // Фиксируем Z
        hasTarget = true;
    }

    void Update()
    {
        if (!hasTarget) return;

        // Движение в 2D пространстве
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Если долетели (расстояние почти 0)
        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionAreaPrefab != null)
        {
            // Создаем зону урона на земле
            GameObject zoneObj = Instantiate(explosionAreaPrefab, transform.position, Quaternion.identity);

            // Настраиваем зону
            DamageZone zoneScript = zoneObj.GetComponent<DamageZone>();
            if (zoneScript != null)
            {
                //float damage = rangedWeapon != null ? rangedWeapon.GetDamage() : 10;
                zoneScript.Setup(rangedWeapon.GetDamage(), rangedWeapon.RangedWeaponData.RadiousZone, rangedWeapon.RangedWeaponData.ZoneDuration); // Урон, Радиус, Время жизни
            }
        }

        // Можно добавить партиклы взрыва здесь

        Destroy(gameObject); // Удаляем саму пулю
    }
}
