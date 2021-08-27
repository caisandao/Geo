using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircularBehaviour : ElementBehaviour
{

    const float PLANE_COLLIDER_SIZE = 0.005f;
    const float PLANE_COLLIDER_OFFSET = 0.05f;
    private GeoController geoController;
    private GeoCamera geoCamera;
    private Mesh mesh; // Because of vertex color use for normal
    private Mesh colliderMesh;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private GeoCircular geoCircular;
    private Circular circular;
    public int Segments = 64;   // Segment count
    public Vector3[] vertexs;
    private int[] triangles;
    private GeometryBehaviour geometryBehaviour;
    private List<GeoEdge> geoEdges;

    public void Init(GeoCircular geoCircular, GeoCamera camera)
    {
        geoController = GameObject.Find("/GeoController").GetComponent<GeoController>();
        geometryBehaviour = GameObject.Find("/3D/Geometry").GetComponent<GeometryBehaviour>();
        geoCamera = camera;
        geoCamera.OnRotate += OnCameraRotate;

        this.geoCircular = geoCircular;

        mesh = new Mesh();
        colliderMesh = new Mesh();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = colliderMesh;

        meshFilter.sharedMesh = mesh;

        SetColorIndex(0);
        SetStyleIndex(0);

        StyleManager.OnStyleChange += () =>
        {
            SetColorIndex(0);
            SetStyleIndex(0);
        };

        visiable = true;
    }

    public void SetData(Circular c)
    {
        circular = c;
        mesh.vertices = new Vector3[] { };
        mesh.triangles = new int[] { };
        mesh.uv = new Vector2[] { };

        if (circular.type == CircularType.Cylinder)
        {
            CylinderMesh(mesh, circular);
            transformCamera();
        }
        else if (circular.type == CircularType.Spheree)
        {
            ConeMesh(mesh, circular);
            transformCamera();
        }
        OnCameraRotate();
        meshCollider.enabled = true;
        meshCollider.convex = true;
    }

    public override void SetColorIndex(int index)
    {
        base.SetColorIndex(index);
        StyleManager.SetPlaneProperty(meshRenderer, colorIndex);
    }

    public override void SetStyleIndex(int index)
    {
        base.SetStyleIndex(index);
        meshRenderer.sharedMaterial = ConfigManager.FaceStyle[styleIndex].Material;
    }

    private void CylinderMesh(Mesh mesh, Circular circular)
    {
        int segments = Segments;
        Vector3 vertice1 = circular.Vertices[0];
        Vector3 vertice2 = circular.Vertices[1];
        Vector3 vertice3 = circular.Vertices[2];
        Vector3 vertice4 = circular.Vertices[3];

        int vertices_count = Segments * 5;
        Vector3[] vertices = new Vector3[vertices_count];

        int start = 0;
        start = SetCircularVertices(vertices, vertice1, start);
        start = SetCircularVertices(vertices, vertice2, start);
        start = SetCircularVertices(vertices, vertice3, start);
        start = SetCircularVertices(vertices, vertice4, start);
        start = SetCircularVertices(vertices, vertice1, start);

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        //triangles
        int circle_count = 0;
        int circle_triangle_count = segments * 3;
        int rectangle_count = segments * 4;
        int rectangle_triangle_count = 2 * 3;
        triangles = new int[circle_triangle_count * circle_count + rectangle_count * rectangle_triangle_count];

        int index = 0;

        for (int j = 0; j < circular.Vertices.Length; j++)
        {
            int base_index = j * Segments;
            for (int i = 0; i < Segments; i++)
            {
                int[] vertex_indexs = new int[4];
                if (i != Segments - 1)
                {
                    vertex_indexs[0] = base_index + i;
                    vertex_indexs[1] = base_index + Segments + i;
                    vertex_indexs[2] = base_index + Segments + i + 1;
                    vertex_indexs[3] = base_index + i + 1;
                }
                else
                {
                    vertex_indexs[0] = base_index + 1;
                    vertex_indexs[1] = base_index + Segments + 1;
                    vertex_indexs[2] = base_index + Segments + i;
                    vertex_indexs[3] = base_index + i;
                }
                GetRectangleTriangles(vertex_indexs, index);
                index += rectangle_triangle_count;
            }
        }

        mesh.triangles = triangles;

        //uv:
        Vector2[] uvs = new Vector2[vertices_count];
        for (int i = 0; i < vertices_count; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / Mathf.Abs(vertices[i].z) / 2 + 0.5f, vertices[i].z / Mathf.Abs(vertices[i].z) / 2 + 0.5f);
        }
        mesh.uv = uvs;
    }

    private void ConeMesh(Mesh mesh, Circular circular)
    {
        int segments = Segments;
        Vector3 vertice1 = circular.Vertices[0];
        Vector3 vertice2 = circular.Vertices[1];
        Vector3 vertice3 = circular.Vertices[2];

        int vertices_count = Segments * 4;
        Vector3[] vertices = new Vector3[vertices_count];

        int start = 0;
        start = SetCircularVertices(vertices, vertice1, start);
        start = SetCircularVertices(vertices, vertice2, start);
        start = SetCircularVertices(vertices, vertice3, start);
        start = SetCircularVertices(vertices, vertice1, start);

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        //triangles
        int circle_count = 0;
        int circle_triangle_count = segments * 3;
        int rectangle_count = segments * 3;
        int rectangle_triangle_count = 2 * 3;
        triangles = new int[circle_triangle_count * circle_count + rectangle_count * rectangle_triangle_count];

        int index = 0;

        for (int j = 0; j < circular.Vertices.Length; j++)
        {
            int base_index = j * Segments;
            for (int i = 0; i < Segments; i++)
            {
                int[] vertex_indexs = new int[4];
                if (i != Segments - 1)
                {
                    vertex_indexs[0] = base_index + i;
                    vertex_indexs[1] = base_index + Segments + i;
                    vertex_indexs[2] = base_index + Segments + i + 1;
                    vertex_indexs[3] = base_index + i + 1;
                }
                else
                {
                    vertex_indexs[0] = base_index + 1;
                    vertex_indexs[1] = base_index + Segments + 1;
                    vertex_indexs[2] = base_index + Segments + i;
                    vertex_indexs[3] = base_index + i;
                }
                GetRectangleTriangles(vertex_indexs, index);
                index += rectangle_triangle_count;
            }
        }

        mesh.triangles = triangles;

        //uv:
        Vector2[] uvs = new Vector2[vertices_count];
        for (int i = 0; i < vertices_count; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / Mathf.Abs(vertices[i].z) / 2 + 0.5f, vertices[i].z / Mathf.Abs(vertices[i].z) / 2 + 0.5f);
        }
        mesh.uv = uvs;
    }
    private void SphereeMesh(Mesh mesh, Circular circular)
    {
        int segments = Segments;
        Vector3 vertice1 = circular.Vertices[0];
        Vector3 vertice2 = circular.Vertices[1];
        Vector3 vertice3 = circular.Vertices[2];
        Vector3 vertice4 = circular.Vertices[3];
        Vector3 vertice5 = circular.Vertices[4];
        Vector3 vertice6 = circular.Vertices[5];
        Vector3 vertice7 = circular.Vertices[6];
        Vector3 vertice8 = circular.Vertices[7];
        Vector3 vertice9 = circular.Vertices[8];

        int vertices_count = Segments * 10;
        Vector3[] vertices = new Vector3[vertices_count];

        int start = 0;
        start = SetCircularVertices(vertices, vertice1, start);
        start = SetCircularVertices(vertices, vertice2, start);
        start = SetCircularVertices(vertices, vertice3, start);
        start = SetCircularVertices(vertices, vertice4, start);
        start = SetCircularVertices(vertices, vertice5, start);
        start = SetCircularVertices(vertices, vertice6, start);
        start = SetCircularVertices(vertices, vertice7, start);
        start = SetCircularVertices(vertices, vertice8, start);
        start = SetCircularVertices(vertices, vertice9, start);
        start = SetCircularVertices(vertices, vertice1, start);

        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        //triangles
        int circle_count = 0;
        int circle_triangle_count = segments * 3;
        int rectangle_count = segments * 9;
        int rectangle_triangle_count = 2 * 3;
        triangles = new int[circle_triangle_count * circle_count + rectangle_count * rectangle_triangle_count];

        int index = 0;

        for (int j = 0; j < circular.Vertices.Length; j++)
        {
            int base_index = j * Segments;
            for (int i = 0; i < Segments; i++)
            {
                int[] vertex_indexs = new int[4];
                if (i != Segments - 1)
                {
                    vertex_indexs[0] = base_index + i;
                    vertex_indexs[1] = base_index + Segments + i;
                    vertex_indexs[2] = base_index + Segments + i + 1;
                    vertex_indexs[3] = base_index + i + 1;
                }
                else
                {
                    vertex_indexs[0] = base_index + 1;
                    vertex_indexs[1] = base_index + Segments + 1;
                    vertex_indexs[2] = base_index + Segments + i;
                    vertex_indexs[3] = base_index + i;
                }
                GetRectangleTriangles(vertex_indexs, index);
                index += rectangle_triangle_count;
            }
        }

        mesh.triangles = triangles;

        //uv:
        Vector2[] uvs = new Vector2[vertices_count];
        for (int i = 0; i < vertices_count; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / Mathf.Abs(vertices[i].z) / 2 + 0.5f, vertices[i].z / Mathf.Abs(vertices[i].z) / 2 + 0.5f);
        }
        mesh.uv = uvs;
    }
    private void GetCircleTriangles(int triangle_count, int vertices_count, int vertices_start, int triangle_start)
    {
        for (int i = 0, vi = 1; i <= triangle_count - 1; i += 3, vi++)
        {
            triangles[triangle_start + i] = vertices_start;
            triangles[triangle_start + i + 1] = vertices_start + vi;
            triangles[triangle_start + i + 2] = vertices_start + vi + 1;
        }
        // last triangle
        triangles[triangle_start + triangle_count - 3] = vertices_start;
        triangles[triangle_start + triangle_count - 2] = vertices_start + vertices_count - 1;
        triangles[triangle_start + triangle_count - 1] = vertices_start + 1;
    }

    private void GetRectangleTriangles(int[] vertex_indexs, int triangle_start)
    {
        for (int i = 1; i < vertex_indexs.Length - 1; i++)
        {
            triangles[triangle_start + i * 3 - 3] = vertex_indexs[0];
            triangles[triangle_start + i * 3 - 2] = vertex_indexs[i];
            triangles[triangle_start + i * 3 - 1] = vertex_indexs[i + 1];
        }
    }

    private void CircleColliderMesh(Mesh mesh, Mesh colliderMesh, Circle circle)
    {
        Vector3[] faceVertices = mesh.vertices;
        int count = faceVertices.Length;

        Vector3[] faceNormal = mesh.normals;
        Vector3[] vertices = new Vector3[count * 2];

        Vector3 total = Vector3.zero;
        for (int i = 0; i < count; i++)
        {
            total += faceVertices[i];
        }

        Vector3 center = total / count;

        for (int i = 0; i < count; i++)
        {
            Vector3 normal = faceNormal[i].normalized;
            Vector3 centerNormal = center - faceVertices[i];
            vertices[i * 2] = faceVertices[i] + normal * PLANE_COLLIDER_SIZE + centerNormal * PLANE_COLLIDER_OFFSET;
            vertices[i * 2 + 1] = faceVertices[i] - normal * PLANE_COLLIDER_SIZE + centerNormal * PLANE_COLLIDER_OFFSET;
        }

        List<int> meshTriangles = new List<int>();

        for (int i = 1; i < count - 1; i++)
        {
            meshTriangles.Add(0);
            meshTriangles.Add(i * 2);
            meshTriangles.Add(i * 2 + 2);

            meshTriangles.Add(i * 2 + 3);
            meshTriangles.Add(i * 2 + 1);
            meshTriangles.Add(1);
        }

        colliderMesh.vertices = vertices;
        colliderMesh.triangles = meshTriangles.ToArray();

        colliderMesh.RecalculateNormals();
    }

    private int SetCircularVertices(Vector3[] vertices, Vector3 vertice, int index)
    {
        float angledegree = 360.0f;
        float angleRad = Mathf.Deg2Rad * angledegree;
        float angledelta = angleRad / Segments;
        float radius = Mathf.Abs(vertice.z) - 0.02f;
        // if (vertice.z == 0)
        // {
        //     vertices[index++] = vertice;
        // }
        // else
        // {
        float angleCur = angleRad;
        float y = vertice.y;
        for (int i = 0; i < Segments; i++)
        {
            float cosA = Mathf.Cos(angleCur);
            float sinA = Mathf.Sin(angleCur);
            vertices[index + i] = new Vector3(radius * cosA, y, radius * sinA);
            angleCur -= angledelta;
        }
        index += Segments;
        // }
        return index;
    }

    private void OnCameraRotate()
    {
        if (geometryBehaviour.GetGeometryType() != GeometryType.ResolvedBody)
            return;
        if (geoEdges == null)
        {
            geoEdges = new List<GeoEdge>();
        }
        else
        {
            foreach (GeoEdge edge in geoEdges)
            {
                if (geometryBehaviour.ContainsEdge(edge))
                {
                    geometryBehaviour.RemoveElement(edge);
                }
            }
            geoEdges.Clear();
        }
        Vector3 cameraView = geoCamera.transform.position.normalized;
        // calculate vertical vector
        float radius0 = Mathf.Abs(circular.Vertices[0].z);
        float x0 = Mathf.Sqrt(Mathf.Pow(radius0, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
        float z0 = -cameraView.x * x0 / cameraView.z;

        float radius1 = Mathf.Abs(circular.Vertices[1].z);
        float x1 = Mathf.Sqrt(Mathf.Pow(radius1, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
        float z1 = -cameraView.x * x1 / cameraView.z;

        float radius2 = Mathf.Abs(circular.Vertices[2].z);
        float x2 = Mathf.Sqrt(Mathf.Pow(radius2, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
        float z2 = -cameraView.x * x2 / cameraView.z;
        Vector3 vertice11 = new Vector3(x0, circular.Vertices[0].y, z0);
        Vector3 vertice12 = new Vector3(x1, circular.Vertices[1].y, z1);
        Vector3 vertice13 = new Vector3(x2, circular.Vertices[2].y, z2);

        Vector3 vertice21 = new Vector3(-x0, circular.Vertices[0].y, -z0);
        Vector3 vertice22 = new Vector3(-x1, circular.Vertices[1].y, -z1);
        Vector3 vertice23 = new Vector3(-x2, circular.Vertices[2].y, -z2);
        if (circular.type == CircularType.Cylinder)
        {
            float radius3 = Mathf.Abs(circular.Vertices[3].z);
            float x3 = Mathf.Sqrt(Mathf.Pow(radius3, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
            float z3 = -cameraView.x * x3 / cameraView.z;

            Vector3 vertice14 = new Vector3(x3, circular.Vertices[3].y, z3);
            addBorderLine(vertice11, vertice12);
            addBorderLine(vertice12, vertice13);
            addBorderLine(vertice13, vertice14);
            addBorderLine(vertice14, vertice11);

            Vector3 vertice24 = new Vector3(-x3, circular.Vertices[3].y, -z3);
            addBorderLine(vertice21, vertice22);
            addBorderLine(vertice22, vertice23);
            addBorderLine(vertice23, vertice24);
            addBorderLine(vertice24, vertice21);
        }
        else if (circular.type == CircularType.Cone)
        {
            addBorderLine(vertice11, vertice12);
            addBorderLine(vertice12, vertice13);
            addBorderLine(vertice13, vertice11);

            addBorderLine(vertice21, vertice22);
            addBorderLine(vertice22, vertice23);
            addBorderLine(vertice23, vertice21);
        }
        else if(circular.type == CircularType.Spheree)
        {
            float radius3 = Mathf.Abs(circular.Vertices[3].z);
            float x3 = Mathf.Sqrt(Mathf.Pow(radius3, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
            float z3 = -cameraView.x * x3 / cameraView.z;

            float radius4 = Mathf.Abs(circular.Vertices[4].z);
            float x4 = Mathf.Sqrt(Mathf.Pow(radius4, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
            float z4 = -cameraView.x * x4 / cameraView.z;

            float radius5 = Mathf.Abs(circular.Vertices[5].z);
            float x5 = Mathf.Sqrt(Mathf.Pow(radius5, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
            float z5 = -cameraView.x * x5 / cameraView.z;

            float radius6 = Mathf.Abs(circular.Vertices[6].z);
            float x6 = Mathf.Sqrt(Mathf.Pow(radius6, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
            float z6 = -cameraView.x * x6 / cameraView.z;

            float radius7 = Mathf.Abs(circular.Vertices[7].z);
            float x7 = Mathf.Sqrt(Mathf.Pow(radius7, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
            float z7 = -cameraView.x * x7 / cameraView.z;

            float radius8 = Mathf.Abs(circular.Vertices[8].z);
            float x8 = Mathf.Sqrt(Mathf.Pow(radius8, 2) / (1 + Mathf.Pow(cameraView.x / cameraView.z, 2)));
            float z8 = -cameraView.x * x8 / cameraView.z;

            Vector3 vertice14 = new Vector3(x3, circular.Vertices[3].y, z3);
            Vector3 vertice24 = new Vector3(-x3, circular.Vertices[3].y, -z3);
            Vector3 vertice15 = new Vector3(x4, circular.Vertices[4].y, z4);
            Vector3 vertice25 = new Vector3(-x4, circular.Vertices[4].y, -z4);
            Vector3 vertice16 = new Vector3(x5, circular.Vertices[5].y, z5);
            Vector3 vertice26 = new Vector3(-x5, circular.Vertices[5].y, -z5);
            Vector3 vertice17 = new Vector3(x6, circular.Vertices[6].y, z6);
            Vector3 vertice27 = new Vector3(-x6, circular.Vertices[6].y, -z6);
            Vector3 vertice18 = new Vector3(x7, circular.Vertices[7].y, z7);
            Vector3 vertice28 = new Vector3(-x7, circular.Vertices[7].y, -z7);
            Vector3 vertice19 = new Vector3(x8, circular.Vertices[8].y, z8);
            Vector3 vertice29 = new Vector3(-x8, circular.Vertices[8].y, -z8);

            addBorderLine(vertice11, vertice12);
            addBorderLine(vertice12, vertice13);
            addBorderLine(vertice13, vertice14);
            addBorderLine(vertice14, vertice15);
            addBorderLine(vertice15, vertice16);
            addBorderLine(vertice16, vertice17);
            addBorderLine(vertice17, vertice18);
            addBorderLine(vertice18, vertice19);
            addBorderLine(vertice19, vertice11);

            addBorderLine(vertice21, vertice22);
            addBorderLine(vertice22, vertice23);
            addBorderLine(vertice23, vertice24);
            addBorderLine(vertice24, vertice25);
            addBorderLine(vertice25, vertice26);
            addBorderLine(vertice26, vertice27);
            addBorderLine(vertice27, vertice28);
            addBorderLine(vertice28, vertice29);
            addBorderLine(vertice29, vertice21);
        }
    }

    public void OnDestroy()
    {
        foreach (GeoEdge edge in geoEdges)
        {
            if (geometryBehaviour.ContainsEdge(edge))
            {
                geometryBehaviour.RemoveElement(edge);
            }
        }
        geoEdges.Clear();
        geoCamera.OnRotate -= OnCameraRotate;
    }

    private void addBorderLine(Vector3 vertice1, Vector3 vertice2)
    {
        GeoEdge geoEdge = new GeoEdge(new VertexSpace(vertice1), new VertexSpace(vertice2));
        geoEdges.Add(geoEdge);
        geometryBehaviour.AddElement(geoEdge);
    }

    private void transformCamera()
    {
        geoCamera.TriggerCenterRAnimation();
    }
}