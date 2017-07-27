using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	public float Velocity = 5f;
	public float AngularVelocity = 5f;
	public float ZoomVelocity = 10;

	public float Sensitivity = 1f;

	private Vector2 MousePosition;
	private Vector2 LastMousePosition;
	private Vector3 DeltaRotation;

	private Quaternion ZeroRotation;

	void Awake() {
		ZeroRotation = transform.rotation;
	}

	void Start() {
		MousePosition = GetNormalizedMousePosition();
		LastMousePosition = GetNormalizedMousePosition();
	}

	void LateUpdate() {
		MousePosition = GetNormalizedMousePosition();

		if(EventSystem.current != null) {
			if(EventSystem.current.currentSelectedGameObject != null) {
				LastMousePosition = MousePosition;
				return;
			}
		}

		//Translation
		Vector3 direction = Vector3.zero;
		if(Input.GetKey(KeyCode.A)) {
			direction.x -= 1f;
		}
		if(Input.GetKey(KeyCode.D)) {
			direction.x += 1f;
		}
		if(Input.GetKey(KeyCode.W)) {
			direction.z += 1f;
		}
		if(Input.GetKey(KeyCode.S)) {
			direction.z -= 1f;
		}
		transform.position += Velocity*Sensitivity*Time.deltaTime*(transform.rotation*direction);
		//Zoom
		if(Input.mouseScrollDelta.y != 0) {
			transform.position += ZoomVelocity*Sensitivity*Time.deltaTime*Input.mouseScrollDelta.y*transform.forward;
		}
		//Rotation
		MousePosition = GetNormalizedMousePosition();
		if(Input.GetMouseButton(0)) {
			DeltaRotation += 1000f*AngularVelocity*Sensitivity*Time.deltaTime*new Vector3(GetNormalizedDeltaMousePosition().x, GetNormalizedDeltaMousePosition().y, 0f);
			transform.rotation = ZeroRotation * Quaternion.Euler(-DeltaRotation.y, DeltaRotation.x, 0f);
		}

		LastMousePosition = MousePosition;
	}

	private Vector2 GetNormalizedMousePosition() {
		Vector2 ViewPortPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		return new Vector2(ViewPortPosition.x, ViewPortPosition.y);
	}

	private Vector2 GetNormalizedDeltaMousePosition() {
		return MousePosition - LastMousePosition;
	}
}
