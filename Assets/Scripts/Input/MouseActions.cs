﻿using UnityEngine;
using System.Collections.Generic;
using InputEvents;

public class MouseActions : MonoBehaviour {

	private HashSet<Selectable> selectables = new HashSet<Selectable>();
	private HashSet<Moveable> moveables = new HashSet<Moveable>();
	
	private GameObject moveTarget;

	// Make this class a singleton
	public static MouseActions Instance { get; private set; }
	
	private MouseActions() {}
	
	void Awake() {
		if (Instance == null) {
			Instance = this;
			MouseMonitor.OnLeftClick += clickSelect;
			MouseMonitor.OnDrag += dragSelect;
			MouseMonitor.OnRightClick += moveSelectedUnits;
		} else {
			GameObject.Destroy(this);
		}
	}

	public void AddSelectable(Selectable obj) {
		selectables.Add(obj);
	}

	public void RemoveSelectable(Selectable obj) {
		selectables.Remove(obj);
	}

	public void AddMoveable(Moveable obj) {
		moveables.Add(obj);
	}

	public void RemoveMoveable(Moveable obj) {
		moveables.Remove(obj);
	}

	private void clickSelect(Click click) {
		if (!Input.GetKey(KeyCode.LeftShift))
			deselectAll();
		
		// Find out if some Selectable was hit by click
		RaycastHit2D hit = Physics2D.Raycast(click.pos, Vector2.zero);
		Selectable sel = hit.collider?.gameObject?.GetComponentInChildren<Selectable>();
		// If so, select it
		if (sel != null)
			sel.SelectToggle();
	}
	
	private void dragSelect(Drag drag) {
		Debug.Log($"drag select (rect = {drag.spanRect})");

		if (!Input.GetKey(KeyCode.LeftShift))
			deselectAll();

		foreach (Selectable obj in selectables) {
			var go = obj.gameObject;
			if (!go.GetComponent<Renderer>().isVisible) continue;
			if (drag.spanRect.Contains(go.transform.position))
				obj.Select();
		}
	}

	private void moveSelectedUnits(Click click) {
		// Draw a point on move target
		if (moveTarget != null)
			GameObject.Destroy(moveTarget);
		moveTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		moveTarget.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		moveTarget.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		// Move each selected moveable object
		int i = 0, max_i = 6;
		float radius = 1f, angle = 0f;
		foreach (Moveable obj in moveables) {
			Selectable sel = obj.gameObject.GetComponentInChildren<Selectable>();
			if (sel != null && sel.IsSelected) {
				// Dispose in circle around the target
				var pos = click.pos;
				if (++i > max_i) {
					max_i *= 2;
					radius += 1;
					i = 0;
				}
				angle = 2*Mathf.PI * i / max_i;
				pos.x += radius * Mathf.Cos(angle);
				pos.y += radius * Mathf.Sin(angle);
				Debug.Log($"obj: {obj}; angle={angle}, pos:{pos}");
				obj.Move(pos);
			}
		}
	}

	private void deselectAll() {
		foreach (Selectable s in selectables)
			s.Deselect();
	}
}
