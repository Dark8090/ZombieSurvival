using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private CharacterBase characterBase;


    [Header("Stats")]
    [SerializeField] private float MaxHealth;
    [SerializeField] private float CurrentHealth;
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

    public float CurrentHealthPlayer
    {
        get
        {
            return CurrentHealth;
        }
        set
        {
            CurrentHealth = value;
            //UIManager.Instance.UpdateHealthSlider(CurrentHealth, MaxHealth); //TODO: Реализовать отображение игрока и изменять его
        }
    }


    private int currentExperience = 0;
    public int maxExperience = 100;

    private int currentLevel = 1;
    private int maxLevel = 50;


    
    private void Start()
    {
        characterBase = GetComponent<CharacterBase>();
        MaxHealth = characterBase.CharacterData.MaxHealth;
        CurrentHealth = MaxHealth;
        RegenerationHealth = characterBase.CharacterData.RegenerationHealth;
        Armor = characterBase.CharacterData.Armor;
        MoveSpeed = characterBase.CharacterData.MoveSpeed;


        UIManager.Instance.UpdateSlider(currentExperience, maxExperience, currentLevel);
    }

    private void Update()
    {

        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += RegenerationHealth * Time.deltaTime;
            CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
        }



        TestDamage(10);
    }


    private void TestDamage(float value)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentHealth -= value;
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
    }
    public void RecalculateStats()
    {
        foreach (var item in InventorySystem.Instance.PassiveItemsList)
        {
            if (item == null) continue;
            MaxHealth = item.GetMaxHealthBonus();
            RegenerationHealth = item.GetRegenerationHealthBonus();
            Debug.Log(item.GetRegenerationHealthBonus());
            Armor = item.GetArmorBonus();
            MoveSpeed = item.GetMoveSpeedBonus();
        }


        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
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

                //GameManager.Instance.LevelUpBox.GetComponent<LevelUpSystem>().LevelUp();
                GameManager.Instance.LevelUpBox.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        UIManager.Instance.UpdateSlider(currentExperience, maxExperience, currentLevel);
        
        

    }

   
}
