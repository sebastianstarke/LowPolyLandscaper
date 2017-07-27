using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class LowPolyLandscaper : MonoBehaviour {
	
	public ToolType ToolType = ToolType.Normal;
	public float ToolSize = 25f;
	public float ToolStrength = 250f;
	public Color ToolColor = Color.white;
	
	public Vector3 MousePosition = Vector3.zero;

	public TerrainSystem TerrainSystem = null;

	//Terrain Generator
	public int Seed = 1;
	public float Scale = 25f;
	public int Octaves = 10;
	public float Persistance = 0.25f;
	public float Lacunarity = 3.0f;
	public float FalloffStrength = 1.0f;
	public float FalloffRamp = 3.0f;
	public float FalloffRange = 2.0f;
	public Vector2 Offset = Vector2.zero;
	public float HeightMultiplier = 25f;
	public AnimationCurve HeightCurve = new AnimationCurve();

    void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0f,1f,1f,0.75f);
		Gizmos.DrawSphere(MousePosition, ToolSize);
    }

	void Awake() {
		if(TerrainSystem == null) {
			TerrainSystem = ScriptableObject.CreateInstance<TerrainSystem>().Initialise(this);
		}
	}

	void Update() {
		if(TerrainSystem == null) {
			TerrainSystem = ScriptableObject.CreateInstance<TerrainSystem>().Initialise(this);
		}
		TerrainSystem.Update();
	}

	void OnDestroy() {
		#if UNITY_EDITOR
		if((EditorApplication.isPlayingOrWillChangePlaymode || !Application.isPlaying) && (!EditorApplication.isPlayingOrWillChangePlaymode || Application.isPlaying)) {
			DestroyImmediate(TerrainSystem.Terrain.gameObject);
		}
		#endif
	}

}