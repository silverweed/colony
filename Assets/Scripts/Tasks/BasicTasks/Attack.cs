﻿using Colony.Tasks.BasicTasks;
using Colony.Behaviour;
using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class Attack : Task
    {
        private GameObject enemy;
        private SteeringBehaviour steeringBehaviour;
        private Stats stats;
        private Life enemyLife;
        private float remainingCooldownTime;

        public Attack(GameObject agent, GameObject enemy) : base(agent, TaskType.Attack)
        {
            this.enemy = enemy;
            steeringBehaviour = agent.GetComponent<SteeringBehaviour>();
            stats = agent.GetComponent<Stats>();
            enemyLife = enemy.GetComponent<Life>();
            remainingCooldownTime = 0.0f;
        }

        public override void Activate()
        {
            status = Status.Active;

            steeringBehaviour.StartPursuit(enemy);
            steeringBehaviour.StopAlignment();
            steeringBehaviour.StopSeparation();
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();
            remainingCooldownTime -= Time.deltaTime;

            if (enemy == null)
            {
                status = Status.Completed;
            }
            else
            {
                Vector2 toEnemy = enemy.transform.position - agent.transform.position;
                if (remainingCooldownTime <= 0 && toEnemy.sqrMagnitude < stats.AttackRange * stats.AttackRange)
                {
                    enemyLife.Decrease(stats.Damage);
                    remainingCooldownTime = stats.CooldownTime;
                }
            }

            return status;
        }

        public override void Terminate()
        {
            steeringBehaviour.StopPursuit();
            steeringBehaviour.StartAlignment();
            steeringBehaviour.StartSeparation();
        }
    }
}