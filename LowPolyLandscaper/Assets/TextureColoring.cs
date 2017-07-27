using UnityEngine;

[ExecuteInEditMode]	
public class TextureColoring : MonoBehaviour {

	//You can remove these if you use pure function calls
	public Vector3 XYZ;
	public Renderer Renderer;

	//Minimum bounds in XYZ space
	public Vector3 XYZMinimum = Vector3.zero;
	public Vector3 XYZMaximum = Vector3.zero;
	
	//This material exists as long as the script is attached as component
	private Material Material;

	//Create material
	void Awake() {
		Material = new Material(Shader.Find("Standard"));
	}

	//Clean up material
	void OnDestroy() {
		DestroyImmediate(Material);
		Resources.UnloadUnusedAssets();
	}

	//Maybe not required for you?
	void Update() {
		if(Renderer == null) {
			return;
		}
		XYZ = transform.position;
		OnValidate();
		transform.position = XYZ;
		Modify(XYZ, Renderer);
	}

	//Maybe also not required for you?
	void OnValidate() {
		XYZ.x = Mathf.Clamp(XYZ.x, XYZMinimum.x, XYZMaximum.x);
		XYZ.y = Mathf.Clamp(XYZ.y, XYZMinimum.y, XYZMaximum.y);
		XYZ.z = Mathf.Clamp(XYZ.z, XYZMinimum.z, XYZMaximum.z);
	}

	//You can call this
	public void Modify(Vector3 xyz, Renderer renderer) {
		Modify(xyz.x, xyz.y, xyz.z, renderer);
	}

	//Or this
	public void Modify(float x, float y, float z, Renderer renderer) {
		Color color = new Color(
			Normalise(x, XYZMinimum.x, XYZMaximum.x, 0f, 1f),
			Normalise(y, XYZMinimum.y, XYZMaximum.y, 0f, 1f),
			Normalise(z, XYZMinimum.z, XYZMaximum.z, 0f, 1f)
			);
		Material.color = color;
		renderer.material = Material;
	}

	//Normalisation :)
	private float Normalise(float value, float valueMin, float valueMax, float resultMin, float resultMax) {
		if(valueMax-valueMin != 0f) {
			return (value-valueMin)/(valueMax-valueMin)*(resultMax-resultMin) + resultMin;
		} else {
			//Debug.Log("Not possible to normalize");
			return value;
		}
	}
	
}
