using Arch.Core;
using System.Collections.Generic;
using UnityEngine;

public class Pike : MeleeWeapon
{
    private MouseFollow mouseFollow;

    [Header("Настройки")]
    public float speed = 10f;       // Скорость полета


    // Внутренние переменные
    private Vector3 targetPosition; // Куда летим (запоминается раз и навсегда)
    private bool isMovingToTarget = false;
    private bool isReturning = false;
    private SpriteRenderer spriteRenderer;
    private float timer = 0f;



    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mouseFollow = playerTransform.GetComponent<MouseFollow>();
        transform.position = playerTransform.position;



        SetVisible(false);
    }
    private void FixedUpdate()
    {
        if (!isMovingToTarget && !isReturning)
        {
            SetVisible(false);
            timer += Time.fixedDeltaTime;
            if (timer >= meleeWeaponData.ReloadTime)
            {
                ShootToCursor();
                timer = 0f;
                SetVisible(true);
            }
        }
        else if (isMovingToTarget)
        {
            MoveToTarget();
        }
        else if (isReturning)
        {
            ReturnToPlayer();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.GameObjectToEntity.TryGetValue(collision.gameObject, out var entityTarget)) return; // проверка, есть ли объект в Entity словаре
        if (!GameManager.Instance.World.Has<Health, EnemyTag>(entityTarget)) return; // проверяем, враг ли наш объект-энтити и есть ли у него здоровье

        ref var healthEntity = ref GameManager.Instance.World.Get<Health>(entityTarget);
        healthEntity.CurrentHP -= GetDamage();
    }

    private void ShootToCursor()
    {
        transform.position = playerTransform.position;

        Vector3 mouseWorldPos = mouseFollow.MouseTracker();

        // Запоминаем точку попадания
        targetPosition = mouseWorldPos;
        targetPosition.z = 0f;

        isMovingToTarget = true;
        isReturning = false;
        LookAtPosition(targetPosition);

    }
    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        LookAtPosition(targetPosition);

        if (Vector3.Distance(playerTransform.position, transform.position) >= meleeWeaponData.AttackRange ||
            Vector3.Distance(transform.position, targetPosition) <= 0.5f)
        {
            isMovingToTarget = false;
            isReturning = true;
        }

    }

    private void ReturnToPlayer()
    {
        Vector3 playerPos = playerTransform.position;
        playerPos.z = 0; // Фиксируем Z для 2D

        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * 1.5f * Time.fixedDeltaTime);
        LookAtPosition(playerPos);
        if (Vector3.Distance(transform.position, playerPos) < 0.3f)
        {
            isReturning = false;
        }
    }
    private void LookAtPosition(Vector3 target)
    {
        Vector2 direction = (Vector2)target - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    private void SetVisible(bool visible)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = visible;
            gameObject.GetComponent<BoxCollider2D>().enabled = visible;
        }
    }
    
}
