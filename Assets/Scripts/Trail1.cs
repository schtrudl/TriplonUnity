/*  
 *  Trail1 deluje na nacin, da se GameObjectu doda mesh, ki se spreminja glede na nove lokacije igralca
 *  Ob ubeležitvi novih tock, se mesh prepiše z novim, ki vsebuje nove dodatne koordinate, ter doda collider tem temu odseku
 */

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trail1 : MonoBehaviour
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
    private List<Vector3> _vertices;
    private List<int> _triangles;
    private int _vertexCount;
    private int _trianglesCount;    
    private Vector3 _previusUpL;
    private Vector3 _previusUpR;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _vertices = new List<Vector3>();
        _triangles = new List<int>();

        _vertices.Add(_upL.transform.position);
        _vertices.Add(_downL.transform.position);
        _vertices.Add(_upR.transform.position);
        _vertices.Add(_downR.transform.position);
        _triangles.Add(0);
        _triangles.Add(3);
        _triangles.Add(1);
        _triangles.Add(0);
        _triangles.Add(2);
        _triangles.Add(3);
        _triangles.Add(3);
        _triangles.Add(2);
        _triangles.Add(1);
        _triangles.Add(2);
        _triangles.Add(0);
        _triangles.Add(1);
        _trianglesCount = 12;
        _vertexCount = 4;

        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;

        _mesh = new Mesh();
        _trailMesh.GetComponent<MeshFilter>().mesh = _mesh;

        UpdateMesh();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(_upL.transform.position, _previusUpL) <= 2 || 
            Vector3.Distance(_upR.transform.position, _previusUpR) <= 2)
        {
            return;
        }

        _vertices.Add(_upL.transform.position);
        _vertices.Add(_downL.transform.position);
        _vertices.Add(_upR.transform.position);
        _vertices.Add(_downR.transform.position);

        _vertexCount += 4;
        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;

        // Define triangles with counter-clockwise (CCW) winding order
        _triangles.Add(_vertexCount - 8); // First triangle
        _triangles.Add(_vertexCount - 7);
        _triangles.Add(_vertexCount - 3);

        _triangles.Add(_vertexCount - 8); // Second triangle
        _triangles.Add(_vertexCount - 3);
        _triangles.Add(_vertexCount - 4);

        _triangles.Add(_vertexCount - 6); // Third triangle
        _triangles.Add(_vertexCount - 8);
        _triangles.Add(_vertexCount - 4);

        _triangles.Add(_vertexCount - 6); // Fourth triangle
        _triangles.Add(_vertexCount - 4);
        _triangles.Add(_vertexCount - 2);

        _triangles.Add(_vertexCount - 5); // Fifth triangle
        _triangles.Add(_vertexCount - 6);
        _triangles.Add(_vertexCount - 2);

        _triangles.Add(_vertexCount - 5); // Sixth triangle
        _triangles.Add(_vertexCount - 2);
        _triangles.Add(_vertexCount - 1);

        _triangles.Add(_vertexCount - 7); // Seventh triangle
        _triangles.Add(_vertexCount - 5);
        _triangles.Add(_vertexCount - 1);

        _triangles.Add(_vertexCount - 7); // Eighth triangle
        _triangles.Add(_vertexCount - 1);
        _triangles.Add(_vertexCount - 3);

        //move the last face to new location
        _triangles[6] += 4;
        _triangles[7] += 4;
        _triangles[8] += 4;
        _triangles[9] += 4;
        _triangles[10] += 4;
        _triangles[11] += 4;

        _trianglesCount += 24;

        UpdateMesh();
        CreateBoxCollider(_vertexCount - 8);
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
    }

    void CreateBoxCollider(int startIndex)
    {
        if (startIndex < 0 || startIndex + 8 > _vertexCount) return;

        // Calculate the bounds for the BoxCollider
        Vector3 min = _vertices[startIndex];
        Vector3 max = _vertices[startIndex];

        for (int i = startIndex; i < startIndex + 8; i++)
        {
            min = Vector3.Min(min, _vertices[i]);
            max = Vector3.Max(max, _vertices[i]);
        }

        // Create an empty GameObject as a child of _trailMesh
        GameObject colliderObject = new GameObject("BoxCollider_" + startIndex / 4);
        colliderObject.transform.SetParent(_trailMesh.transform);
        colliderObject.transform.localPosition = Vector3.zero; // Keep local position at origin

        // Configure the BoxCollider for this new child
        BoxCollider boxCollider = colliderObject.AddComponent<BoxCollider>();
        boxCollider.center = (min + max) / 2f;
        boxCollider.size = max - min;
        boxCollider.isTrigger = true;
        boxCollider.AddComponent<Crash>();
    }

}
