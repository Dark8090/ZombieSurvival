using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private CharacterBase characterBase;


    [Header("Stats")]
    [SerializeField] private float MaxHealth;
    [SerializeField] private float RegenerationHealth;
    [SerializeField] private float Armor;
    [SerializeField] private float MoveSpeed;
    //public float Might;
    //public float Area;
    //public float AttackSpeed;
    //public float Duration;
    //public float Amount;
    //public float Cooldawn;
    //public float Luck;
    //public float Growth;
    //public float Greed;
    //public float Curse;
    //public float Magnet;
    //public float Revival;
    //public float Reroll;
    //public float Skip;
    //public float Banish;


    private float currentHealth;

    private int currentExperience = 0;
    public int maxExperience = 100;

    private int currentLevel = 1;
    private int maxLevel = 50;


    
    private void Start()
    {
        characterBase = GetComponent<CharacterBase>();
        MaxHealth = characterBase.CharacterData.MaxHealth;
        RegenerationHealth = characterBase.CharacterData.RegenerationHealth;
        Armor = characterBase.CharacterData.Armor;
        MoveSpeed = characterBase.CharacterData.MoveSpeed;

        currentHealth = MaxHealth;

        UIManager.Instance.UpdateSlider(currentExperience, maxExperience, currentLevel);
    }

    private void Update()
    {

        if (currentHealth < MaxHealth)
        {
            currentHealth += RegenerationHealth * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, MaxHealth);
        }



        TestDamage(10);
    }


    private void TestDamage(float value)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= value;
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(MaxHealth, currentHealth + amount);
    }
    public void RecalculateStats()
    {
        foreach (var item in InventorySystem.Instance.PassiveItemsList)
        {
            if (item == null) continue;
            MaxHealth += item.GetMaxHealthBonus();
            RegenerationHealth += item.GetRegenerationHealthBonus();
            Armor += item.GetArmorBonus();
            MoveSpeed += item.GetMoveSpeedBonus();
        }


        currentHealth = Mathf.Min(currentHealth, MaxHealth);
    }

    public void AddExperience(int count)
    {
        if (currentExperience + count < maxExperience)
        {
            currentExperience += count;
        }
        else
        {
            if (currentLevel + 1 <= maxLevel)
            {
                currentLevel += 1;
                currentExperience = 0;
                maxExperience += 50;
            }
        }
        UIManager.Instance.UpdateSlider(currentExperience, maxExperience, currentLevel);
        
        

    }
}
