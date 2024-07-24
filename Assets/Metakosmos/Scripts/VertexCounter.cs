using UnityEngine;
using TMPro;

public class VertexCounter : MonoBehaviour
{
    public TextMeshProUGUI vertexCountText;

    void Start()
    {
        int totalVertices = 0;
        MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh != null)
            {
                totalVertices += meshFilter.sharedMesh.vertexCount;
            }
        }

        Debug.Log("Total Vertices in Scene: " + totalVertices);

        if (vertexCountText != null)
        {
            vertexCountText.text = "Total Vertices: " + totalVertices;
        }
    }
}
