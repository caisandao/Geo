using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpin : MonoBehaviour
{

    float RotateSpeed = 120f;
    float PreAngle = 0;
    float Radius1;
    float Radius2;
    float Radius3;
    float Radius4;
    float Radius5;
    float Radius6;
    float Radius7;
    float Radius8;
   
    float Radius0;
    Geometry geometry;
    VertexUnit[] vertices;
    GeometryBehaviour geometryBehaviour;
    List<GeoEdge> geoEdges;
    bool Spinning = true;

    void Update()
    {
        if (!Spinning)
            return;
        foreach (GeoEdge edge in geoEdges)
        {
            if (geometryBehaviour.ContainsEdge(edge))
            {
                geometryBehaviour.RemoveElement(edge);
            }
        }
        geoEdges.Clear();
        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        AddCurFace(transform.localEulerAngles.y);
        if (transform.localEulerAngles.y >= 360 - RotateSpeed / 10)
        {
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
            AddCurFace(360);
            geoEdges.Clear();
            Spinning = false;
            DestroyGameObject();
        }
    }

    private void AddCurFace(float Angle)
    {
        float sin = Mathf.Sin(Mathf.Deg2Rad * Angle);
        float pre_sin = Mathf.Sin(Mathf.Deg2Rad * PreAngle);
        float cos = Mathf.Cos(Mathf.Deg2Rad * Angle);
        float pre_cos = Mathf.Cos(Mathf.Deg2Rad * PreAngle);
        float X0 = Radius0 * sin;
        float Z0 = Radius0 * cos;
        float preX0 = Radius0 * pre_sin;
        float preZ0 = Radius0 * pre_cos;

        float X1 = Radius1 * sin;
        float Z1 = Radius1 * cos;
        float preX1 = Radius1 * pre_sin;
        float preZ1 = Radius1 * pre_cos;

        float X2 = Radius2 * sin;
        float Z2 = Radius2 * cos;
        float preX2 = Radius2 * pre_sin;
        float preZ2 = Radius2 * pre_cos;
        if (vertices.Length == 3)
        {
            VertexSpace v0 = new VertexSpace(X0, vertices[0].Position().y, Z0);
            VertexSpace v1 = new VertexSpace(preX0, vertices[0].Position().y, preZ0);
            VertexSpace v2 = new VertexSpace(X1, vertices[1].Position().y, Z1);
            VertexSpace v3 = new VertexSpace(preX1, vertices[1].Position().y, preZ1);
            VertexSpace v4 = new VertexSpace(X2, vertices[2].Position().y, Z2);
            VertexSpace v5 = new VertexSpace(preX2, vertices[2].Position().y, preZ2);
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v0, v2, v3, v1 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v0, v1, v5, v4 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v2, v3, v5, v4 }));
            geometryBehaviour.AddElement(new GeoEdge(v0, v1));
            geometryBehaviour.AddElement(new GeoEdge(v2, v3));
            geometryBehaviour.AddElement(new GeoEdge(v4, v5));
            addBorderLine(v0.Position(), v2.Position());
            addBorderLine(v2.Position(), v4.Position());
            addBorderLine(v4.Position(), v0.Position());
        }
        else if (vertices.Length == 4)
        {
            float X3 = Radius3 * sin;
            float Z3 = Radius3 * cos;
            float preX3 = Radius3 * pre_sin;
            float preZ3 = Radius3 * pre_cos;

            VertexSpace v0 = new VertexSpace(X0, vertices[0].Position().y, Z0);
            VertexSpace v1 = new VertexSpace(preX0, vertices[0].Position().y, preZ0);
            VertexSpace v2 = new VertexSpace(X1, vertices[1].Position().y, Z1);
            VertexSpace v3 = new VertexSpace(preX1, vertices[1].Position().y, preZ1);
            VertexSpace v4 = new VertexSpace(X2, vertices[2].Position().y, Z2);
            VertexSpace v5 = new VertexSpace(preX2, vertices[2].Position().y, preZ2);
            VertexSpace v6 = new VertexSpace(X3, vertices[3].Position().y, Z3);
            VertexSpace v7 = new VertexSpace(preX3, vertices[3].Position().y, preZ3);
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v0, v2, v3, v1 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v0, v1, v7, v6 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v2, v3, v5, v4 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v4, v5, v7, v6 }));
            geometryBehaviour.AddElement(new GeoEdge(v0, v1));
            geometryBehaviour.AddElement(new GeoEdge(v2, v3));
            geometryBehaviour.AddElement(new GeoEdge(v4, v5));
            geometryBehaviour.AddElement(new GeoEdge(v6, v7));
            addBorderLine(v0.Position(), v2.Position());
            addBorderLine(v2.Position(), v4.Position());
            addBorderLine(v4.Position(), v6.Position());
            addBorderLine(v6.Position(), v0.Position());
        }
        else if (vertices.Length == 9)
        {
            float X3 = Radius3 * sin;
            float Z3 = Radius3 * cos;
            float preX3 = Radius3 * pre_sin;
            float preZ3 = Radius3 * pre_cos;

            float X4 = Radius4 * sin;
            float Z4 = Radius4 * cos;
            float preX4 = Radius4 * pre_sin;
            float preZ4 = Radius4 * pre_cos;

            float X5 = Radius5 * sin;
            float Z5 = Radius5 * cos;
            float preX5 = Radius5 * pre_sin;
            float preZ5 = Radius5 * pre_cos;

            float X6 = Radius6 * sin;
            float Z6 = Radius6 * cos;
            float preX6 = Radius6 * pre_sin;
            float preZ6 = Radius6 * pre_cos;

            float X7 = Radius7 * sin;
            float Z7 = Radius7 * cos;
            float preX7 = Radius7 * pre_sin;
            float preZ7 = Radius7 * pre_cos;

            float X8 = Radius8 * sin;
            float Z8 = Radius8 * cos;
            float preX8 = Radius8 * pre_sin;
            float preZ8 = Radius8 * pre_cos;
            


            VertexSpace v0 = new VertexSpace(X0, vertices[0].Position().y, Z0);
            VertexSpace vv0 = new VertexSpace(preX0, vertices[0].Position().y, preZ0);
            VertexSpace v1 = new VertexSpace(X1, vertices[1].Position().y, Z1);
            VertexSpace vv1 = new VertexSpace(preX1, vertices[1].Position().y, preZ1);
            VertexSpace v2 = new VertexSpace(X2, vertices[2].Position().y, Z2);
            VertexSpace vv2 = new VertexSpace(preX2, vertices[2].Position().y, preZ2);
            VertexSpace v3 = new VertexSpace(X3, vertices[3].Position().y, Z3);
            VertexSpace vv3 = new VertexSpace(preX3, vertices[3].Position().y, preZ3);

            VertexSpace v4 = new VertexSpace(X4, vertices[4].Position().y, Z4);
            VertexSpace vv4 = new VertexSpace(preX4, vertices[4].Position().y, preZ4);
            VertexSpace v5 = new VertexSpace(X5, vertices[5].Position().y, Z5);
            VertexSpace vv5 = new VertexSpace(preX5, vertices[5].Position().y, preZ5);
            VertexSpace v6 = new VertexSpace(X6, vertices[6].Position().y, Z6);
            VertexSpace vv6 = new VertexSpace(preX6, vertices[6].Position().y, preZ6);
            VertexSpace v7 = new VertexSpace(X7, vertices[7].Position().y, Z7);
            VertexSpace vv7 = new VertexSpace(preX7, vertices[7].Position().y, preZ7);

            VertexSpace v8 = new VertexSpace(X8, vertices[8].Position().y, Z8);
            VertexSpace vv8 = new VertexSpace(preX8, vertices[8].Position().y, preZ8);
          






            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v0,vv0,v1,vv1 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v2, vv2, v1, vv1 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v2, vv2, v3, vv3 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v3, vv3, v4, vv4 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v4, vv4, v5, vv5 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v5, vv5, v6, vv6 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v6, vv6, v7, vv7 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v7, vv7, v8, vv8 }));
            geometryBehaviour.AddElement(new GeoFace(new VertexUnit[] { v8, vv8, v0, vv0 }));
          



            geometryBehaviour.AddElement(new GeoEdge(v0, vv0));
            geometryBehaviour.AddElement(new GeoEdge(v1, vv1));
            geometryBehaviour.AddElement(new GeoEdge(v2, vv2));
            geometryBehaviour.AddElement(new GeoEdge(v3, vv3));
            geometryBehaviour.AddElement(new GeoEdge(v4, vv4));
            geometryBehaviour.AddElement(new GeoEdge(v5, vv5));
            geometryBehaviour.AddElement(new GeoEdge(v6, vv6));
            geometryBehaviour.AddElement(new GeoEdge(v7, vv7));
            geometryBehaviour.AddElement(new GeoEdge(v8, vv8));
            addBorderLine(v0.Position(), v1.Position());
            addBorderLine(v1.Position(), v2.Position());
            addBorderLine(v2.Position(), v3.Position());
            addBorderLine(v3.Position(), v4.Position());
            addBorderLine(v4.Position(), v5.Position());
            addBorderLine(v5.Position(), v6.Position());
            addBorderLine(v6.Position(), v7.Position());
            addBorderLine(v7.Position(), v8.Position());
            addBorderLine(v8.Position(), v0.Position());
           

        }
        PreAngle = Angle;
    }

    private void addBorderLine(Vector3 vertice1, Vector3 vertice2)
    {
        GeoEdge geoEdge = new GeoEdge(new VertexSpace(vertice1), new VertexSpace(vertice2));
        geoEdges.Add(geoEdge);
        geometryBehaviour.AddElement(geoEdge);
    }

    public void GetData(Geometry geometry)
    {
        this.geometry = geometry;

        GeoVertex[] geoVertices = geometry.GeoVertices();
        vertices = new VertexUnit[geoVertices.Length];
        for (int i = 0; i < geoVertices.Length; i++)
        {
            vertices[i] = geoVertices[i].VertexUnit();
        }
        Radius0 = vertices[0].Position().z;
        Radius1 = vertices[1].Position().z;
        Radius2 = vertices[2].Position().z;
        if (vertices.Length == 4)
        {
            Radius3 = vertices[3].Position().z;
        }
        else if(vertices.Length==3)
        {
            Radius3 = Radius0;
        }
        else if(vertices.Length==9)
        {
            Radius3 = vertices[3].Position().z;
            Radius4 = vertices[4].Position().z;
            Radius5 = vertices[5].Position().z;
            Radius6 = vertices[6].Position().z;
            Radius7 = vertices[7].Position().z;
            Radius8 = vertices[8].Position().z;
            
        }
        geometryBehaviour = GameObject.Find("/3D/Geometry").GetComponent<GeometryBehaviour>();

        geoEdges = new List<GeoEdge>();
    }

    void DestroyGameObject()
    {
        Type type = Type.GetType("SpinAuxiliaryTool");
        SpinAuxiliaryTool auxiliaryTool;
        if (type != null)
        {
            auxiliaryTool = (SpinAuxiliaryTool)Activator.CreateInstance(type);
            auxiliaryTool.GenerateResolvedBody(geometry);
        }
        Destroy(gameObject);
    }
}