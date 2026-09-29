using UnityEngine;


public class WeaponData : ScriptableObject
{
    [Header("General")]
    public int ID;
    public string WeaponName;
    public string Description;
    public Sprite Sprite;
    public GameObject WeaponPrefab;
    public WeaponType WeaponType;

    [Header("Parametres")]
    public int Level = 1;
    public int MaxLevel = 5;
    public float BaseDamage; // базовый дамаг
    [Min(0.1f)] public float FireRate; // скорость атаки (выстрелы в секунду)
    public float ReloadTime;
    [Range(0, 100)] public float CriticalChance; // шанс крита
    public float CriticalMultiplier; // множитель крита (1.0f = x1 урон, 2.0f = x2 урон)


    [Header("Evolution")] //TODO: —делать так, чтобы в эволюционном предмете не было блока Evolution (разбить ScriptableObject на несколько)
    public bool CanEvolution;
    public PassiveItemData RequiredPassiveItemToEvolution;
    public WeaponData EvolutionWeapon;




    [Header("Visualities")]
    public AudioClip AttackSound; // «вук выстрела/атаки
    public AudioClip HitSound; // «вук при попадании
    public AudioClip ReloadSound; // «вук перезар€дки
    public ParticleSystem ParticleSystem; // Ёффекты при попадании
}





