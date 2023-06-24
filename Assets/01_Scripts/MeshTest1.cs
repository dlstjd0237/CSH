using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest1 : MonoBehaviour
{
    MeshFilter _meshFilter;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        Mesh mesh = new Mesh();

        _meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[3]; // ¡§¡°πˆ∆€, ¡§¡°ºŒ¿Ã¥ı
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3]; //¿Œµ¶Ω∫ πˆ∆€, «»ºøºŒ¿Ã¥ı

        vertices[0] = new Vector3(5, 0);
        vertices[1] = new Vector3(5, 5);
        vertices[2] = new Vector3(0, 0 );

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        _meshFilter.mesh = mesh;
    }

}
