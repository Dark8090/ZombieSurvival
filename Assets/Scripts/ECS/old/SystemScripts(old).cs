//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.AI;


//public class BotControllerSystem
//{
//    private Transform player;
//    private Vector3 playerPosition;
//    private GameObject bulletPrefab;

//    private bool isPlayerAlive = false;
//    //private List<Entity> entities = new List<Entity>();

//    //public void Init(World world)
//    //{

//    //}
//    public BotControllerSystem(Transform player)
//    {
//        this.player = player;
//    }
//    public void Update(World world)
//    {
//        //isPlayerAlive = false;


//        //foreach (var entity in world.GetEntitiesWith<ControllerComponent>())
//        //{
//        //    ControllerComponent controllerComponent = entity.GetComponent<ControllerComponent>();

//        //    if (controllerComponent.TypeEntity == TypeEntity.Player)
//        //    {
//        //        playerEntity = entity;
//        //        PositionComponent playerPositionComponent = playerEntity.GetComponent<PositionComponent>();
//        //        playerPosition = playerPositionComponent.position;
//        //        isPlayerAlive = true;

//        //    }
//        //}

//        foreach (var entity in world.GetEntitiesWith<PositionComponent>())
//        {
//            //ControllerComponent controllerComponent = entity.GetComponent<ControllerComponent>();

//            PositionComponent positionComponentBot = entity.GetComponent<PositionComponent>();

//            positionComponentBot.position = Vector3.MoveTowards(positionComponentBot.position, player.position, 3f);



//            //VelocityComponent velocityComponentBot = entity.GetComponent<VelocityComponent>();
//            //StateComponent stateComponentBot = entity.GetComponent<StateComponent>();

//            //if (!isPlayerAlive)
//            //{
//            //    if (stateComponentBot.State != State.Patrol && stateComponentBot.State != State.Inactivity)
//            //    {
//            //        stateComponentBot.State = State.Patrol;
//            //        stateComponentBot.PatrolTarget = positionComponentBot.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0f, UnityEngine.Random.Range(-15f, 15f));

//            //    }

//            //    HandlePatrol(positionComponentBot, stateComponentBot);
//            //    continue;
//            //}



//            //float distanceToPlayer = Vector3.Distance(positionComponentBot.position, playerPosition);

//            //switch (stateComponentBot.State)
//            //{
//            //    case State.Inactivity:
//            //    case State.Patrol:
//            //        if (distanceToPlayer < stateComponentBot.DetectionRange)
//            //        {
//            //            stateComponentBot.State = State.Chase;
//            //        }
//            //        else
//            //        {
//            //            HandlePatrol(positionComponentBot, stateComponentBot);
//            //        }
//            //        break;

//            //    case State.Chase:
//            //        if (distanceToPlayer > stateComponentBot.DetectionRange * 1.5f) // ����� ������ ������
//            //        {
//            //            stateComponentBot.State = State.Patrol;
//            //        }
//            //        else if (distanceToPlayer <= stateComponentBot.AttackRange)
//            //        {
//            //            stateComponentBot.State = State.Attack;

//            //        }
//            //        else
//            //        {
//            //            HandleChase(positionComponentBot, velocityComponentBot,  playerPosition);
//            //        }
//            //        break;

//            //    case State.Attack:
//            //        if (distanceToPlayer > stateComponentBot.AttackRange)
//            //        {
//            //            stateComponentBot.State = State.Chase;
//            //        }
//            //        else
//            //        {
//            //            HandleAttack(stateComponentBot, playerPosition, positionComponentBot.position);
//            //        }
//            //        break;
//            //}

//            //Vector3 pos = positionComponentBot.position;
//            //pos.y = 0f;
//            //positionComponentBot.position = pos;


//        }
//    }

//    private void HandlePatrol(PositionComponent pos, StateComponent state)
//    {

//        // ���� �������� ����� ��������������, �������� �����
//        if (Vector3.Distance(pos.position, state.PatrolTarget) < 1f)
//        {
//            state.PatrolTarget = pos.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0f, UnityEngine.Random.Range(-15f, 15f));
//            Debug.Log("���� - " + state.PatrolTarget);
//        }



//    }

//    private void HandleChase(PositionComponent pos, VelocityComponent vel, Vector3 targetPos)
//    {


//    }

//    private void HandleAttack(StateComponent state, Vector3 playerPos, Vector3 botPosition)
//    {


//        if (Time.time >= state.LastAttackTime + state.AttackCooldown)
//        {
//            Vector3 direction = (playerPos - botPosition).normalized;


//            GameObject bulletObject = GameObject.Instantiate(bulletPrefab, botPosition, Quaternion.LookRotation(direction));



//            Rigidbody2D rigidbody = bulletObject.GetComponent<Rigidbody2D>();


//            if (rigidbody != null)
//            {
//                rigidbody.linearVelocity = direction * 5f;
//            }


//            GameObject.Destroy(bulletObject, 2f);

//            state.LastAttackTime = Time.time;
//        }
//    }

//}
////public class PlayerControllerSystem
////{
////    public void Update(World world)
////    {
////        foreach (var entity in world.GetEntitiesWith<PositionComponent, VelocityComponent, ControllerComponent>())
////        {
////            ControllerComponent controllerComponent = entity.GetComponent<ControllerComponent>();

////            if (controllerComponent.TypeEntity != TypeEntity.Player)
////            {
////                continue;
////            }

////            PositionComponent positionComponent = entity.GetComponent<PositionComponent>();
////            VelocityComponent velocityComponent = entity.GetComponent<VelocityComponent>();
////            InputComponent inputComponent = entity.GetComponent<InputComponent>();

////            if (velocityComponent.Rigidbody != null)
////            {
////                velocityComponent.velocity = new Vector3(inputComponent.InputX * velocityComponent.Speed, velocityComponent.velocity.y, inputComponent.InputY * velocityComponent.Speed);
////            }


////            Vector3 pos = positionComponent.position;
////            pos.y = 0f;
////            positionComponent.position = pos;

////        }
////    }
////}

////public class DamageSystem
////{

////    public void Update(World world)
////    {
////        foreach (var entity in world.GetEntitiesWith<HealthComponent, ControllerComponent, PositionComponent>())
////        {
////            HealthComponent healthComponent = entity.GetComponent<HealthComponent>();
////            ControllerComponent controllerComponent = entity.GetComponent<ControllerComponent>();
////            PositionComponent positionComponent = entity.GetComponent<PositionComponent>();

////            if (controllerComponent.TypeEntity == TypeEntity.Player && Input.GetKeyDown(KeyCode.Space))
////            {
////                healthComponent.TakeDamage(10);

////                Debug.Log(healthComponent.CurrentHP);
////            }
////        }
////    }
////}





