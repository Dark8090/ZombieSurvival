using Arch.Core;
using UnityEngine;

public struct Position
{
    public Vector2 Value;
}

public struct Velocity
{
    public Vector2 Direction;
    public float Speed;
}
public struct Health
{
    public float MaxHealth;
    public float CurrentHP;
}



// Мосты к Unity
public struct TransformRef
{
    public Transform Value;
}

public struct Rigidbody2DRef
{
    public Rigidbody2D Value;
}

public struct GameObjectRef
{
    public GameObject Value;
}
public struct SpriteRendererRef
{
    public SpriteRenderer Value;
}

// Теги
public struct PlayerTag { }
public struct EnemyTag
{
    public EnemyType EnemyType;
}


public enum EnemyType
{
    Common,
    Ranged
}