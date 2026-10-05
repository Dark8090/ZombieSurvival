using Arch.Core;
using Arch.Core.Extensions;
using Arch.System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // test

    [SerializeField] private CharacterBase _characterBase;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject greenGemPrefab;
    [SerializeField] private Sprite spriteEnemy;
    [SerializeField] private List<EnemyData> EnemyDatas;
    [SerializeField] private GameObject levelUpBox;
    [SerializeField] private GameObject weaponItemBox;
    [SerializeField] private GameObject passiveItemBox;

    public Transform PlayerTransform
    {
        get
        {
            if (playerObject != null)
            {
                return playerObject.transform;
            }
            else
            {
                return null;
            }
        }
    }
    public GameObject PlayerGameObject { get => playerObject; }
    public GameObject GreenGemObject { get => greenGemPrefab; }
    public GameObject LevelUpBox { get => levelUpBox; }
    public GameObject PassiveItemBox { get => passiveItemBox; }
    public GameObject WeaponItemBox { get => weaponItemBox; }

    public PlayerStats PlayerStats => playerStats;
    public CharacterBase CharacterBase { get => _characterBase; set => value = _characterBase; }

    public World World { get; private set; }
    private Group<float> systems;

    public Dictionary<GameObject, Entity> GameObjectToEntity { get; } = new();
    private void Awake()
    {
        Instance = this;
        World = World.Create();
    }
    private void Start()
    {
        //var player = World.Create( // создание Энтити и добавление на нее компонентов
        //    new Position(),
        //    new Velocity(),
        //    new PlayerTag(),
        //    new GameObjectRef());


        //player.Add<Health, Position>(); // Пример добавления компонентов на сущность
        //World.Add(player, new Health(), new Position()); // Пример добавления компонентов на сущность
        //player.Remove<Health, Position>(); // Пример удаления компонентов с сущности

        //LevelUp();

        systems = new Group<float>( // создаем группу систем
            "GameSystems",
            new MovementSystem(World),
            new KillSystem(World));

        systems.Initialize(); // инициализируем группу систем

        for (int i = 0; i < 0; i++)
        {
            SpawnEnemy($"Enemy_{i}", Random.insideUnitCircle * 5f);
        }



    }
    private void Update()
    {
        systems.Update(Time.deltaTime); // запускаем все системы в группе
        var countEntites = new QueryDescription().WithAll<TransformRef>();

        //print(World.CountEntities(countEntites));
    }


    private void LevelUp()
    {
        LevelUpBox.SetActive(true);
    }
    private void SpawnEnemy(string name, Vector2 startPos)
    {
        var go = new GameObject();
        go.name = name;
        go.transform.position = startPos;
        go.layer = 3;

        var rb = go.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        //var triggerCollider = go.AddComponent<CircleCollider2D>();
        //triggerCollider.isTrigger = true;
        //triggerCollider.radius = 0.6f;

        var physicCollider = go.AddComponent<CircleCollider2D>();


        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        //spriteRenderer.sprite = spriteEnemy;


        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomSpeed = Random.Range(1f, 3f);


        var entity = World.Create(
            new Position { Value = startPos },
            new Velocity { Direction = randomDirection, Speed = randomSpeed },
            new Rigidbody2DRef { Value = rb },
            new TransformRef { Value = go.transform },
            new GameObjectRef { Value = go },
            new EnemyTag { enemyData = EnemyDatas[0] },
            new SpriteRendererRef { Value = spriteRenderer},
            new AnimationState { Timer = 0f, FrameIndex = 0},
            new Health { CurrentHP = 100, MaxHealth = 100 });

        //var sprite = World.Get<SpriteRendererRef>(entity);
        //spriteRenderer.sprite = EnemyDatas[0].Sprites[0];

        //var enemyData = World.Get<EnemyTag>(entity);
        RegisterEntity(entity, go);
    }



    public void RegisterEntity(Entity entity, GameObject gameObject)
    {
        if (gameObject != null && !GameObjectToEntity.ContainsKey(gameObject))
        {
            GameObjectToEntity[gameObject] = entity;
        }
    }
    public void UnregisterEntity(GameObject gameObject)
    {
        if (gameObject != null && GameObjectToEntity.TryGetValue(gameObject, out Entity entity))
        {
            GameObjectToEntity.Remove(gameObject);
        }
    }

    private void OnDestroy() // при уничтожении GameObject на котором висит GameManager - уничтожаем группу систем и мир
    {
        systems?.Dispose();
        World?.Dispose();
    }


    ///

    public void AddExperience(int experience)
    {
        playerStats.AddExperience(experience);
    }
}
