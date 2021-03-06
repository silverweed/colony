﻿using Colony.Resources;
using System;
using UnityEngine;
using Colony.UI;

namespace Colony.Tasks.BasicTasks
{
    public class Deposit : Task
    {
        private BeeLoad load;
        private HiveWarehouse targetHive;

        public Deposit(GameObject agent, GameObject targetBeehive) : base(agent, TaskType.Deposit)
        {
            targetHive = targetBeehive.GetComponent<HiveWarehouse>();
            load = agent.GetComponent<BeeLoad>();
        }

        public override void Activate()
        {
            status = Status.Active;
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            targetHive.AddResources(load.Load);
            load.Clear();
            // FIXME: this is not very nice: it'd be better if UIController
            	// updated by itself (maybe via an event?)
            if (agent.GetComponent<Selectable>().IsSelected)
                UIController.Instance.SetBeeLoadText(agent);
            status = Status.Completed;

            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
