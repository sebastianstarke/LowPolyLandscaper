using UnityEngine;

public static class Utility {
	public static float Normalise(float value, float valueMin, float valueMax, float resultMin, float resultMax) {
		if(valueMax-valueMin != 0f) {
			return (value-valueMin)/(valueMax-valueMin)*(resultMax-resultMin) + resultMin;
		} else {
			return 0f;
		}
	}

	public static void FlatShading(ref Vector3[] vertices, ref int[] triangles, ref Vector2[] uvs) {
		Vector3[] flatVertices = new Vector3[triangles.Length];
		Vector2[] flatUVs = new Vector2[triangles.Length];
		for(int i=0; i<triangles.Length; i++) {
			flatVertices[i] = vertices[triangles[i]];
			flatUVs[i] = uvs[triangles[i]];
			triangles[i] = i;
		}
		vertices = flatVertices;
		uvs = flatUVs;
	}

	public static Mesh[] CreateMeshes(Vector3[] vertices, int[] triangles, Vector2[] uvs, int chunkSize) {
		Mesh[] meshes = new Mesh[Mathf.CeilToInt((float)vertices.Length / (float)chunkSize)];
		for(int i=0; i<meshes.Length; i++) {
			int elements = Mathf.Min(chunkSize, vertices.Length-i*chunkSize);
			Mesh mesh = new Mesh();
			Vector3[] meshVertices = new Vector3[elements];
			int[] meshTriangles = new int[elements];
			Vector2[] meshUVs = new Vector2[elements];
			for(int j=0; j<elements; j++) {
				meshVertices[j] = vertices[i*chunkSize+j];
				meshTriangles[j] = triangles[i*chunkSize+j] % chunkSize;
				meshUVs[j] = uvs[i*chunkSize+j];
			}
			mesh.vertices = meshVertices;
			mesh.triangles = meshTriangles;
			mesh.uv = meshUVs;
			mesh.RecalculateNormals();
			meshes[i] = mesh;
		}
		return meshes;
	}

    public static T[] RangeSubset<T>(this T[] array, int startIndex, int length) {
        T[] subset = new T[length];
        System.Array.Copy(array, startIndex, subset, 0, length);
        return subset;
    }

 	public static Texture2D CreateTexture(Color[] colorMap, int dimX, int dimY, FilterMode filterMode) {
		Texture2D texture = new Texture2D(dimX, dimY);
		texture.filterMode = filterMode;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels(colorMap);
		texture.Apply();
		return texture;
	}
}