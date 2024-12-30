using System.Collections.Generic;
using UnityEngine;

public class Trail2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _trailMesh;
    [SerializeField]
    private GameObject _upL;
    [SerializeField]
    private GameObject _downL;
    [SerializeField]
    private GameObject _upR;
    [SerializeField]
    private GameObject _downR;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector3 _previusUpL;
    private Vector3 _previusUpR;
    private Vector3 _previusDownL;
    private Vector3 _previusDownR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _vertices = new Vector3[4];
        _triangles = new int[12];

        _vertices[0] = _upL.transform.position;
        _vertices[1] = _downL.transform.position;
        _vertices[2] = _upR.transform.position;
        _vertices[3] = _downR.transform.position;
        _triangles[0] = 3;
        _triangles[1] = 1;
        _triangles[2] = 0;
        _triangles[3] = 0;
        _triangles[4] = 2;
        _triangles[5] = 3;
    
        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;
        _previusDownR = _downR.transform.position;
        _previusDownL = _downL.transform.position;

        _mesh = new Mesh();
        _trailMesh.GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(_upL.transform.position, _previusUpL) <= 1 ||
            Vector3.Distance(_upR.transform.position, _previusUpR) <= 1)
        {
            return;
        }

        // Define vertices and triangles for the new mesh segment
        _vertices = new Vector3[8];
        _triangles = new int[36];

        _vertices[0] = _previusUpL;
        _vertices[1] = _previusDownL;
        _vertices[2] = _previusUpR;
        _vertices[3] = _previusDownR;
        _vertices[4] = _upL.transform.position;
        _vertices[5] = _downL.transform.position;
        _vertices[6] = _upR.transform.position;
        _vertices[7] = _downR.transform.position;

        // Define triangles (counter-clockwise order for each face)
        _triangles[0] = 0; _triangles[1] = 1; _triangles[2] = 5;
        _triangles[3] = 0; _triangles[4] = 5; _triangles[5] = 4;
        _triangles[6] = 2; _triangles[7] = 6; _triangles[8] = 7;
        _triangles[9] = 2; _triangles[10] = 7;_triangles[11] = 3;
        _triangles[15] = 0; _triangles[16] = 6; _triangles[17] = 2;
        _triangles[18] = 1; _triangles[19] = 3; _triangles[20] = 7;
        _triangles[21] = 1; _triangles[22] = 7; _triangles[23] = 5;
        _triangles[24] = 4; _triangles[25] = 5; _triangles[26] = 7;
        _triangles[27] = 4; _triangles[28] = 7; _triangles[29] = 6;
        _triangles[12] = 0; _triangles[13] = 4; _triangles[14] = 6;
        _triangles[30] = 0; _triangles[31] = 2; _triangles[32] = 3;
        _triangles[33] = 0; _triangles[34] = 3; _triangles[35] = 1;

        // Create a new child object under _trailMesh
        GameObject segmentObject = new GameObject("TrailSegment");
        segmentObject.transform.SetParent(_trailMesh.transform);
        segmentObject.transform.localPosition = Vector3.zero;

        // Add a MeshFilter and MeshRenderer
        MeshFilter meshFilter = segmentObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = segmentObject.AddComponent<MeshRenderer>();
        segmentObject.AddComponent<Crash>();

        // Create and assign the mesh
        Mesh segmentMesh = new Mesh();
        segmentMesh.vertices = _vertices;
        segmentMesh.triangles = _triangles;
        segmentMesh.RecalculateNormals(); // Recalculate normals for proper shading
        meshFilter.mesh = segmentMesh;

        // Optional: Assign a material to the MeshRenderer
        meshRenderer.material = _trailMesh.GetComponent<MeshRenderer>().material; // Replace with your desired material

        // Add a BoxCollider and configure it
        BoxCollider boxCollider = segmentObject.AddComponent<BoxCollider>();

        // Calculate the bounds for the BoxCollider
        Vector3 min = _vertices[0];
        Vector3 max = _vertices[0];
        foreach (var vertex in _vertices)
        {
            min = Vector3.Min(min, vertex);
            max = Vector3.Max(max, vertex);
        }

        boxCollider.center = (min + max) / 2f;
        boxCollider.size = max - min;
        boxCollider.isTrigger = true;

        // Update the previous positions for the next segment
        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;
        _previusDownL = _downL.transform.position;
        _previusDownR = _downR.transform.position;
    }

}
