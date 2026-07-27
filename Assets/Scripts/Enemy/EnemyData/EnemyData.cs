using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("General")]
    public EnemyType EnemyType;
    public List<SpriteRenderer> Sprites;

    [Header("AttackSettings")]
    public float Damage;
    public float AttackSpeed;
    public float AttackDelay;
    public float AttackRange;
    public float StoppingDistance = 2f;

}
