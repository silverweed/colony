using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

namespace Colony.Events {

using Random = UnityEngine.Random;

public class EventSpawner : MonoBehaviour {
	
	public float MinEventDelay = 60;
	public float MaxEventDelay = 200;

	public Sprite BearSprite;
	public float BearTimeout = 180;
	public int BearMinHoneyStolen = 100;
	public int BearMaxHoneyStolen = 500;
	public int BearMinBeesKilled = 2;
	public int BearMaxBeesKilled = 8;
	public int BearSacrificedBees = 5;

	public Sprite AttackSprite;
	public float AttackTimeout = 100;
	public int AttackMinWaspsSpawned = 3;
	public int AttackMaxWaspsSpawned = 10;

	public Sprite SkunkSprite;
	public float SkunkTimeout = 60;
	public int SkunkMinHoneyStolen = 40;
	public int SkunkMaxHoneyStolen = 350;
	public int SkunkMinWaterStolen = 30;
	public int SkunkMaxWaterStolen = 250;
	public int SkunkMinBeesKilled = 1;
	public int SkunkMaxBeesKilled = 6;
	public int SkunkSacrificedBees = 4;

	public Sprite ToxicPollenSprite;
	public float ToxicPollenMinPercBeesInvolved = 0.1f;
	public float ToxicPollenMaxPercBeesInvolved = 0.5f;
	public float ToxicPollenMinLifespanDecreased = 0.2f;
	public float ToxicPollenMaxLifespanDecreased = 0.8f;

	public Sprite RainSprite;
	public int RainMinFlowersInvolved = 5;
	public int RainMaxFlowersInvolved = 20;
	public float RainMinPercPollenDecreased = 0.2f;
	public float RainMaxPercPollenDecreased = 0.9f;

	private float curEventDelay;
	private float curLevelCap = 1;
	private Event evt;
	private Event[] eventPool;

	void Start() {
		eventPool = new Event[] {
			new BearEvent(),
			new ToxicPollenEvent(),
			new RainEvent(),
			new AttackEvent(),
			new SkunkEvent()
		};

		evt = pickRandomEvent(1);
		Debug.Assert(evt != null, "picked event is null!");
		curEventDelay = Mathf.Clamp(Random.Range(MinEventDelay, MaxEventDelay) * evt.Level,
			MinEventDelay, MaxEventDelay);
	}

	void Update() {
		curEventDelay -= Time.deltaTime;
		if (curEventDelay <= 0) {
			EventManager.Instance.LaunchEvent(evt);
			evt = pickRandomEvent((int)curLevelCap);
			Debug.Assert(evt != null, "picked event is null!");
			curLevelCap += Random.Range(0.3f, 1f);
			curEventDelay = Mathf.Clamp(Random.Range(MinEventDelay, MaxEventDelay) * evt.Level,
				MinEventDelay, MaxEventDelay);
		}
	}

	private Event pickRandomEvent(int levelCap = int.MaxValue) {
		var e = eventPool.Where(evt => evt.Level <= levelCap)
			.OrderBy(x => Guid.NewGuid()).FirstOrDefault().Init();
		Debug.Assert(e != null, "Event is null in pickRandomEvent(" + levelCap + ")!");
		return (Event)Activator.CreateInstance(e.GetType());
	}
}

}