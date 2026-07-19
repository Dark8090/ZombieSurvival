using UnityEngine;

public class DamagableTest : MonoBehaviour, IDamageable
{
    public void ApplyDamage(float damage)
    {
        print($"Нанесен урон объекту - {gameObject.name}, в размере - {damage}");
    }

    public void Die()
    {
        print($"DEAD");
    }
}
