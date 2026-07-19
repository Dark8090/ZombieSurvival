using UnityEngine;

public class Knife : MeleeWeapon
{
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private float timer = 0f;
    private float totalCycleTimer => appearDelay + activeDuration + inactiveDuration;



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        SetVisible(false);
    }
    private void Update()
    {
        if (isOrbitalWeapon)
        {
            if (playerTransform == null) return;

            timer += Time.deltaTime;

            // Нормализуем таймер в пределах одного цикла
            float cycleTime = timer % totalCycleTimer; // в cycleTime попадает остаток от деления, например, 0 % 5, таймер идет, получается 1.5 % 5 = 3.5(таймер находится в текущем времени цикла) 

            //Определяем текущую фазу
            if (cycleTime < appearDelay)
            {
                
                // Фаза 1, еще не появился
                SetVisible(false);
            }
            else if (cycleTime < appearDelay + activeDuration)
            {
                // Фаза 2, крутимся
                SetVisible(true);
                

                //Вычисляем, сколько времени прошло с начала активной фазы
                float activeTime = cycleTime - appearDelay;

                // Угол поворота(в радианах)
                float angle = rotationSpeed * activeTime;
                float radians = angle * Mathf.Deg2Rad;

                //Позиция по кругу игрока
                Vector3 offset = new Vector3(
                    Mathf.Cos(radians) * radious,
                    Mathf.Sin(radians) * radious,
                    0f
                );
                transform.position = playerTransform.position + offset;
            }
            else
            {
                // Фаза 3, неактивен(ожидание)
                
                SetVisible(false);
            }
        }


    }
    void SetVisible(bool visible)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = visible;
        }
        if (collider != null)
        {
            collider.enabled = visible;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAttack && ((1 << collision.gameObject.layer) & searchLayerMask) != 0)
        {
            print($"Нанесен урон объекте - {collision.gameObject.name}, в размере {GetDamage()}");
            Destroy(collision.gameObject);
        }
    }
}
