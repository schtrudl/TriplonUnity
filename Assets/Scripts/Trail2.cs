/*
 *  Trail2 deluje na nacin, da na podlagi prejsnih tock in novih ob dovolj velikem premiku generiramo TrailSegment,
 *  ki je detachan child cikla, ob vsakem dovolj velikem premiku se ustvari segment, kateremu dodamo mesh,
 *  s tockami in ze vnaprej prirpavljenimi indexi, doda se mesh collider, ki se zaradi lepe oblike enostavno pretvori v convex
 */

using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class Trail2 : MonoBehaviour
{
    [SerializeField]
    private Material _variant;
    [SerializeField]
    private GameObject _upL;
    [SerializeField]
    private GameObject _downL;
    [SerializeField]
    private GameObject _upR;
    [SerializeField]
    private GameObject _downR;

    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector3 _previusUpL;
    private Vector3 _previusUpR;
    private Vector3 _previusDownL;
    private Vector3 _previusDownR;
    private double _dist = 1.2;
    private GameObject _trail;
    private bool _first = true;
    private Material _material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _trail = GameObject.Find("TrailOrganizer");
        if (_trail == null)
        {
            _trail = new GameObject("TrailOrganizer");
            _trail.transform.position = Vector3.zero;
        }
        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;
        _previusDownR = _downR.transform.position;
        _previusDownL = _downL.transform.position;

        // Define triangles (counter-clockwise order for each face)
        _triangles = new int[30];
        // Left Side (Connect Front and Back on Left)
        _triangles[0] = 6; _triangles[1] = 4; _triangles[2] = 0; // Triangle 1
        _triangles[3] = 0; _triangles[4] = 2; _triangles[5] = 6; // Triangle 2

        // Right Side (Connect Front and Back on Right)
        _triangles[6] = 3; _triangles[7] = 1; _triangles[8] = 5; // Triangle 1
        _triangles[9] = 5; _triangles[10] = 7; _triangles[11] = 3; // Triangle 2

        // Top Side (Connect Front and Back on Top)
        _triangles[12] = 0; _triangles[13] = 4; _triangles[14] = 5; // Triangle 1
        _triangles[15] = 5; _triangles[16] = 1; _triangles[17] = 0; // Triangle 2

        // Bottom Side (Connect Front and Back on Bottom)
        _triangles[18] = 6; _triangles[19] = 2; _triangles[20] = 3; // Triangle 1
        _triangles[21] = 3; _triangles[22] = 7; _triangles[23] = 6; // Triangle 2

        // Front Face (Closer to Camera)
        _triangles[24] = 2; _triangles[25] = 0; _triangles[26] = 1; // Triangle 1
        _triangles[27] = 1; _triangles[28] = 3; _triangles[29] = 2; // Triangle 2

        // Back Face (Further from Camera)
        //_triangles[30] = 7; _triangles[31] = 5; _triangles[32] = 4; // Triangle 1
        //_triangles[33] = 4; _triangles[34] = 6; _triangles[35] = 7; // Triangle 2

        _material = new Material(_variant);
        _material.SetFloat("_Surface", 1);
        // Set the render mode to Transparent
        _material.SetFloat("_Mode", 3); // 3 is for Transparent mode in Unity's standard shader
        _material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt("_ZWrite", 0);  // Disable ZWrite for transparency
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.EnableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _material.renderQueue = 3000; // Set render queue to be in the transparent range

        // Adjust the transparency (alpha value) of the material
        Color materialColor = _material.color;  // Get the material's color
        materialColor.a = 0.7f;  // Set alpha to 50% transparency
        _material.color = materialColor;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(_upL.transform.position, _previusUpL) <= _dist ||
            Vector3.Distance(_upR.transform.position, _previusUpR) <= _dist)
        {
            return;
        }

        // Define vertices for the new mesh segment
        _vertices = new Vector3[8];

        _vertices[0] = _previusUpL;
        _vertices[1] = _previusUpR;
        _vertices[2] = _previusDownL;
        _vertices[3] = _previusDownR;
        _vertices[4] = _upL.transform.position;
        _vertices[5] = _upR.transform.position;
        _vertices[6] = _downL.transform.position;
        _vertices[7] = _downR.transform.position;


        // Create a new child object under _trailMesh
        GameObject segmentObject = new GameObject("TrailSegment");
        segmentObject.transform.localPosition = Vector3.zero;
        
        // Set the _cycle GameObject as the parent while retaining world transformations
        segmentObject.transform.SetParent(_trail.transform); // `true` ensures world positions are maintained

        // Add a MeshFilter and MeshRenderer
        MeshFilter meshFilter = segmentObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = segmentObject.AddComponent<MeshRenderer>();
        // Add the Crash script
        Crash crashScript = segmentObject.AddComponent<Crash>();

        // Create and assign the mesh
        Mesh segmentMesh = new Mesh();
        segmentMesh.vertices = _vertices;
        segmentMesh.triangles = _triangles;
        //segmentMesh.RecalculateNormals(); // Recalculate normals for proper shading
        meshFilter.mesh = segmentMesh;

        // Optional: Assign a material to the MeshRenderer
        meshRenderer.material = _material; // Replace with your desired material
        //meshRenderer.material.
        // Add a MeshCollider and configure it
        MeshCollider meshCollider = segmentObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = segmentMesh; // Assign the mesh to the MeshCollider
        meshCollider.convex = true; 
        meshCollider.isTrigger = true;

        // Update the previous positions for the next segment
        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;
        _previusDownL = _downL.transform.position;
        _previusDownR = _downR.transform.position;

        if (_first)
        {
            //gets rid of the front face we need at start
            List<int> triangleList = new List<int>(_triangles);

            // Remove the last (6 triangles)
            triangleList.RemoveRange(triangleList.Count - 6, 6);

            // Convert back to an array if needed
            _triangles = triangleList.ToArray();

            _first = false;
        }
    }

}
