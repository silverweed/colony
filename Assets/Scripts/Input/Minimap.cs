﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace Colony.Input {

public class Minimap : MonoBehaviour, IPointerClickHandler {

	public Camera MMCamera;
	public Texture MMTexture;

	private Boundaries bounds;

	void Start() {
		bounds = GameObject.FindWithTag("Ground").GetComponent<Boundaries>();
	}

	public void OnPointerClick(PointerEventData data) {
		moveCameraToClick(data.position);
	}

	private void moveCameraToClick(Vector3 pos) {
		// Convert main screen coordinates to minimap relative
		// screen coordinates: x_rel_mm = x_orig - minimap.x
		Vector3 mmPos = pos - (Vector3)gameObject.transform.position;
		float mmRatio = 2f * MMCamera.orthographicSize / MMTexture.width;

		// Don't go past world boundaries
		var newPos = Camera.main.transform.position + mmPos * mmRatio;
		if (newPos.x < -bounds.worldSize / 2f || newPos.x > bounds.worldSize / 2f
		    || newPos.y < -bounds.worldSize / 2f || newPos.y > bounds.worldSize / 2f)
			return;
		
		Camera.main.transform.Translate(mmPos * mmRatio);
	}
}

}