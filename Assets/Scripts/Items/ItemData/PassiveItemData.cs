using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItem", menuName = "Items/Create Passive Item Data")]
public class PassiveItemData : ItemData
{
    [Header("Settings Passive Item ")]
    public int Level;
    public int MaxLevel;
    [Tooltip("Макс хп")]
    public float MaxHealth;
    [Tooltip("Регенерация хп")]
    public float RegenerationHealth;
    [Tooltip("Броня, поглощает X урона")]
    public float Armor;
    [Tooltip("Скорость передвижения")]
    public float MoveSpeed;
    [Tooltip("мощь/сила, которая увеличивает урон от любого оружия")]
    public float Might;
    //[Tooltip("модификатор зоны для всех атак")]
    //public float Area;
    //[Tooltip("скорость атаки")]
    //public float AttackSpeed;
    //[Tooltip("длительность действия атак/оружий")]
    //public float Duration;
    //[Tooltip("количество снарядов для оружия")]
    //public float Amount;
    //[Tooltip("модификатор длительности между атаками ")]
    //public float Cooldawn;
    //[Tooltip("Изменяет вероятность выпадения определенных предметов, например,\r\nвероятность выпадения большинства предметов и вероятность того,\r\nчто сундуки с сокровищами будут более высокого качества.")]
    //public float Luck;
    //[Tooltip("изменяет количество опыта, получаемого за сбор самоцветов опыта")]
    //public float Growth;
    //[Tooltip("изменяет количество золота, получаемого за сбор объектов с монетами")]
    //public float Greed;
    //[Tooltip("модификатор для врагов, изменяет скорость, здоровье, количество и частоту появления")]
    //public float Curse;
    //[Tooltip("Определяет радиус, в пределах которого собираются самоцветы опыта и предметы для подбора")]
    //public float Magnet;
    //[Tooltip("Определяет количество дополнительных жизней, которыми располагает игрок.")]
    //public float Revival;
    //[Tooltip("количество попыток для рерола левел-ап наград/сундуков")]
    //public float Reroll;
    //[Tooltip("количество скипов для рерола левел-ап наград/сундуков")]
    //public float Skip;
    //[Tooltip("количество банов для рерола левел-ап наград/сундуков")]
    //public float Banish;

}
