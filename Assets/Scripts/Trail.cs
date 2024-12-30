using UnityEngine;

public class Trail : MonoBehaviour
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
    [SerializeField]
    private GameObject _colliders;

    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private int _vertexCount;
    private int _trianglesCount;    
    private Vector3 _previusUpL;
    private Vector3 _previusUpR;

    private const int NUM_VERTICES = 100000;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _vertices = new Vector3[NUM_VERTICES];
        _triangles = new int[((_vertices.Length - 4) * 2 + 2) * 3]; // 4 tocke = 2, 8 tock = 10, 12 tock = 18, 16 tock = 26, x tock = (x-4)*2 + 2

        _vertices[0] = _upL.transform.position;
        _vertices[1] = _downL.transform.position;
        _vertices[2] = _upR.transform.position;
        _vertices[3] = _downR.transform.position;
        _vertexCount = 4;
        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;

        _triangles[0] = 0;
        _triangles[1] = 1;
        _triangles[2] = 2;
        _triangles[3] = 1;
        _triangles[4] = 2;
        _triangles[5] = 3;
        _trianglesCount = 6;

        _mesh = new Mesh();
        _trailMesh.GetComponent<MeshFilter>().mesh = _mesh;

        UpdateMesh();
        CreateBoxCollider(0, 4);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(_upL.transform.position, _previusUpL) <= 2 || 
            Vector3.Distance(_upR.transform.position, _previusUpR) <= 2)
        {
            return;
        }
        

        _vertices[_vertexCount] = _upL.transform.position;
        _vertices[_vertexCount + 1] = _downL.transform.position;
        _vertices[_vertexCount + 2] = _upR.transform.position;
        _vertices[_vertexCount + 3] = _downR.transform.position;
        _vertexCount += 4;
        _previusUpL = _upL.transform.position;
        _previusUpR = _upR.transform.position;

        _triangles[_trianglesCount] = _vertexCount - 8;
        _triangles[_trianglesCount + 1] = _vertexCount - 7;
        _triangles[_trianglesCount + 2] = _vertexCount - 4;
        _triangles[_trianglesCount + 3] = _vertexCount - 7;
        _triangles[_trianglesCount + 4] = _vertexCount - 4;
        _triangles[_trianglesCount + 5] = _vertexCount - 3;
        _triangles[_trianglesCount + 6] = _vertexCount - 8;
        _triangles[_trianglesCount + 7] = _vertexCount - 6;
        _triangles[_trianglesCount + 8] = _vertexCount - 4;
        _triangles[_trianglesCount + 9] = _vertexCount - 6;
        _triangles[_trianglesCount + 10] = _vertexCount - 4;
        _triangles[_trianglesCount + 11] = _vertexCount - 2;
        _triangles[_trianglesCount + 12] = _vertexCount - 6;
        _triangles[_trianglesCount + 13] = _vertexCount - 5;
        _triangles[_trianglesCount + 14] = _vertexCount - 2;
        _triangles[_trianglesCount + 15] = _vertexCount - 5;
        _triangles[_trianglesCount + 16] = _vertexCount - 2;
        _triangles[_trianglesCount + 17] = _vertexCount - 1;
        _triangles[_trianglesCount + 18] = _vertexCount - 7;
        _triangles[_trianglesCount + 19] = _vertexCount - 5;
        _triangles[_trianglesCount + 20] = _vertexCount - 3;
        _triangles[_trianglesCount + 21] = _vertexCount - 5;
        _triangles[_trianglesCount + 22] = _vertexCount - 3;
        _triangles[_trianglesCount + 23] = _vertexCount - 1;
        _trianglesCount += 24;

        UpdateMesh();
        CreateBoxCollider(_vertexCount - 8, 8);
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
    }

    void CreateBoxCollider(int startIndex, int numVertices)
    {
        if (startIndex < 0 || startIndex + numVertices > _vertexCount) return;

        // Calculate the bounds for the BoxCollider
        Vector3 min = _vertices[startIndex];
        Vector3 max = _vertices[startIndex];

        for (int i = startIndex; i < startIndex + numVertices; i++)
        {
            min = Vector3.Min(min, _vertices[i]);
            max = Vector3.Max(max, _vertices[i]);
        }

        // Configure the BoxCollider
        BoxCollider boxCollider = _colliders.AddComponent<BoxCollider>();
        boxCollider.center = (min + max) / 2f;
        boxCollider.size = max - min;
        boxCollider.isTrigger = true;
    }
}
