﻿using UnityEngine;
using System.Collections;
using System.Linq;
using Colony.Input;
using Colony.Resources;

namespace Colony.UI {

public class LarvaButtonsCallbacks : MonoBehaviour {

	public void GrowWorkerBee() {
		growBee("WorkerBee");
	}

	public void GrowQueenBee() {
		growBee("QueenBee");
	}

	private void growBee(string beeType) {
		var sel = EntityManager.Instance.Larvae.Where(x => x.GetComponent<Selectable>().IsSelected);
		foreach (var larvaObj in sel) {
			if (!UIController.Instance.resourceManager.RequireResources(Costs.WorkerBee)) {
				// UI message
				return;
			}
			larvaObj.GetComponent<Larva>().StartGrowing(beeType);
		}
	}

}

}