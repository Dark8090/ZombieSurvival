using Arch.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UIElements;

public class MeleeWeapon : WeaponBase
{
    protected MeleeWeaponData meleeWeaponData => WeaponData as MeleeWeaponData;

    [Header("General")]
    [SerializeField] protected bool canAttack = true;
    [SerializeField] protected LayerMask searchLayerMask;
    [SerializeField] protected Transform playerTransform;

    [Header("OrbitalWeapon")]
    [SerializeField] protected bool isOrbitalWeapon = false;
    [SerializeField] protected float radious; // Радиус круга
    [SerializeField] protected float rotationSpeed; // скорость вращения
    [SerializeField] protected float appearDelay; // задержка перед первым появлением
    [SerializeField] protected float activeDuration; // сколько секунд объект активен
    [SerializeField] protected float inactiveDuration; // сколько секунд объект неактивен между циклами


    //[SerializeField] private LayerMask obstacleLayerMask;

    //[Header("OverlapArea")]
    //[SerializeField] private Transform overlapStartPoint;
    //[SerializeField] private OverlapType overlapType;
    //[SerializeField] private Vector2 offset;
    //[SerializeField] private Vector2 boxSize = Vector2.one;
    //[SerializeField, Min(0f)] private float circleRadious = 1f;

    //[Header("Obstacles")]
    //[SerializeField] private bool considerObstacle; // для того, чтобы не наносить урон сквозь стены/других врагов

    //private readonly List<Collider2D> overlapResult = new List<Collider2D>(); // МБ Изменить на list 
    //[SerializeField] private Collider2D[] hits;
    //private int overlapResultsCount;
    //private bool canAttack = true;
    //private float lastAttackTime;


    //[Header("Gizmos")]
    //[SerializeField] private DrawGizmoType drawGizmoType;
    //[SerializeField] private Color gizmoColor = Color.coral;


    //public override void Attack()
    //{
    //    if (!canAttack && Time.time >= lastAttackTime + meleeWeaponData.AttackSpeed)
    //    {
    //        canAttack = true;
    //    }

    //    if (TryFindEnemies() && canAttack) // canAttack для таймера по attackSpeed из meleeWeaponData
    //    {
    //        TryAttackEnemies();
    //    }
    //}




    //public override void Attack()
    //{
    //    GetDamage();
    //}

    
    
}


//private bool TryFindEnemies()
//{

//    Vector3 position = overlapStartPoint.TransformPoint(offset); // Принимает точку в локальных координатах (например, localPosition) и возвращает её мировые координаты.
//                                                                 // Это полезно, чтобы узнать, куда указывает что-то, находящееся внутри объекта, в общем пространстве.

//    overlapResultsCount = overlapType switch // само switch выражение
//    {
//        OverlapType.Circle => OverlapCircle(position),
//        OverlapType.Box => OverlapBox(position),

//        _ => throw new ArgumentOutOfRangeException(nameof(overlapType)) // "_" - это аналог default для switch ВЫРАЖЕНИЙ 
//    };
//    return overlapResultsCount > 0;
//}

// OverlapBoxNonAlloc метод, который не создает новый массив, а записывает результаты в заранее выделенные массив(overlapResult)
// Обычный OverlapBox метод каждый раз при нахождении будет создавать новый массив.
//private int OverlapBox(Vector2 position)
//{
//    hits = null;
//    hits = Physics2D.OverlapBoxAll(position, boxSize, overlapStartPoint != null ? overlapStartPoint.eulerAngles.z : 0f, searchLayerMask.value);
//    overlapResult.Clear(); // очищаем лист для нового поиска врагов
//    overlapResult.AddRange(hits);

//    foreach (Collider2D hit in hits)
//        print(hit.name);

//    return overlapResult.Count;
//}

//private int OverlapCircle(Vector2 position)
//{
//    hits = null;
//    hits = Physics2D.OverlapCircleAll(position, circleRadious, searchLayerMask.value);
//    overlapResult.Clear(); // очищаем лист для нового поиска врагов
//    overlapResult.AddRange(hits);

//    print(overlapResult.Count);
//    print(hits.Length);
//    return overlapResult.Count;
//}

//private void TryAttackEnemies()
//{
//    for (int i = 0; i < overlapResultsCount; i++)
//    {
//        if (overlapResult[i].TryGetComponent(out IDamageable damageable) == false) // проверка на наличие интерфейса получения урона
//        {
//            continue;
//        }

//        if (considerObstacle)
//        {
//            Vector3 startPointPositon = overlapStartPoint.position;
//            Vector3 colliderPosition = overlapResult[i].transform.position;
//            bool hasObstacle = Physics.Linecast(startPointPositon, colliderPosition, obstacleLayerMask.value);

//            if (hasObstacle)
//            {
//                continue;
//            }

//        }

//        print("Урон");
//        //damageable.ApplyDamage(meleeWeaponData.BaseDamage);
//    }
//}


//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {
//        TryDrawGizmos(DrawGizmoType.Selected);

//    }
//    private void OnDrawGizmosSelected()
//    {
//        TryDrawGizmos(DrawGizmoType.Selected);
//    }

//    private void TryDrawGizmos(DrawGizmoType gizmoType)
//    {
//        if (drawGizmoType != gizmoType)
//        {
//            return;
//        }

//        if (overlapStartPoint == null)
//        {
//            return;
//        }

//        Gizmos.matrix = overlapStartPoint.localToWorldMatrix;
//        Gizmos.color = gizmoColor;

//        switch (overlapType)
//        {
//            case OverlapType.Box:
//                Gizmos.DrawCube(offset, boxSize);
//                break;
//            case OverlapType.Circle:
//                DrawCircleGizmo(offset, circleRadious, gizmoColor);
//                break;
//            default:
//                throw new ArgumentOutOfRangeException(nameof(overlapType));

//        }
//    }

//    private void DrawCircleGizmo(Vector3 center, float radius, Color color, int segments = 32)
//    {
//        Gizmos.color = color;
//        Vector3 prevPoint = Vector3.zero;
//        for (int i = 0; i <= segments; i++)
//        {
//            float angle = i * (360f / segments) * Mathf.Deg2Rad;
//            Vector3 point = new Vector3(
//                center.x + Mathf.Cos(angle) * radius,
//                center.y + Mathf.Sin(angle) * radius,
//                center.z
//            );

//            if (i > 0)
//                Gizmos.DrawLine(prevPoint, point);

//            prevPoint = point;
//        }
//    }
//#endif
//}


//enum DrawGizmoType
//{
//    Selected,
//    Always
//}
