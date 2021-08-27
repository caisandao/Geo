using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResolvedBody : Geometry
{
    public bool shapeSetted = false;
    public bool isSpinned = false;
    public bool isSpread = false;
    private VertexResolvedBody[] vertexResolvedBodies;

    public override void Init()
    {
        base.Init();

        Name = "ResolvedBody";
        Type = GeometryType.ResolvedBody;
        vertexResolvedBodies = new VertexResolvedBody[] { };

    }

    public override void MoveVertex(VertexUnit vertex, Ray ray, Transform camera, bool snap)
    {
        if (isSpinned)
        {
            return;
        }
        base.MoveVertex(vertex, ray, camera, snap);
        int i = 0;
        foreach (VertexUnit item in (VertexUnit[])vertexResolvedBodies)
        {
            if (vertex == item)
            {
                break;
            }
            i++;
        }
        if (!vertex.isBase)
            return;
        Vector3 position = vertex.Position();
        vertexResolvedBodies[i].SetAbsPosition(position);
        // SetVerticesAbsPosition(i, position);
    }

    public override VertexUnit[] VerticesOfMoveVertex(VertexUnit vertex)
    {
        if (!vertex.isBase)
            return base.VerticesOfMoveVertex(vertex);
        else
            return (VertexUnit[])vertexResolvedBodies.Clone();
    }

    public void SetVerticesAbsPosition(int index, Vector3 position)
    {
        Vector3[] vectors = new Vector3[4];

        for (int i = 0; i < vertexResolvedBodies.Length; i++)
        {
            vectors[i] = vertexResolvedBodies[i].Position();
        }

        if (index == 0 || index == 1)
        {
            float y = Mathf.Abs(position.y);
            vectors[0].y = y;
            vectors[1].y = -y;
        }
        else if (index == 2)
        {
            float y = position.y;
            float z = position.z;
            vectors[2].y = y;
            vectors[2].z = z;
        }
        else if (index == 3)
        {
            float y = position.y;
            float z = position.z;
            vectors[3].y = y;
            vectors[3].z = z;
        }

        for (int i = 0; i < vertexResolvedBodies.Length; i++)
        {
            VertexResolvedBody unit = vertexResolvedBodies[i];
            unit.SetAbsPosition(vectors[i]);
        }
    }

    public void SetRectangle(Vector3[] positions)
    {
        if (positions.Length != 4)
            return;
        Vector3 faceNormal = Vector3.right;

        VertexResolvedBody u0 = new VertexResolvedBody(positions[0].x, positions[0].y, positions[0].z, faceNormal);
        // u0.isFixed = true;
        VertexResolvedBody u1 = new VertexResolvedBody(positions[1].x, positions[1].y, positions[1].z, faceNormal);
        // u1.isFixed = true;
        VertexResolvedBody u2 = new VertexResolvedBody(positions[2].x, positions[2].y, positions[2].z, faceNormal);
        // u2.isFixed = true;
        VertexResolvedBody u3 = new VertexResolvedBody(positions[3].x, positions[3].y, positions[3].z, faceNormal);
        // u3.isFixed = true;
        AddBaseVertex(u0);
        AddBaseVertex(u1);
        AddBaseVertex(u2);
        AddBaseVertex(u3);
        vertexResolvedBodies = new VertexResolvedBody[] { u0, u1, u2, u3 };

        GeoVertex v0 = new GeoVertex(u0, true);
        GeoVertex v1 = new GeoVertex(u1, true);
        GeoVertex v2 = new GeoVertex(u2, true);
        GeoVertex v3 = new GeoVertex(u3, true);
        AddGeoVertex(v0);
        AddGeoVertex(v1);
        AddGeoVertex(v2);
        AddGeoVertex(v3);

        GeoEdge e0 = new GeoEdge(u0, u1, true);
        GeoEdge e1 = new GeoEdge(u1, u2, true);
        GeoEdge e2 = new GeoEdge(u2, u3, true);
        GeoEdge e3 = new GeoEdge(u0, u3, true);
        AddGeoEdge(e0);
        AddGeoEdge(e1);
        AddGeoEdge(e2);
        AddGeoEdge(e3);

        GeoFace f0 = new GeoFace(new VertexUnit[] { u0, u1, u2, u3 }, true);
        AddGeoFace(f0);

        InitDatas();

        NavAxisBehaviour axis = GameObject.Find("X").GetComponent<NavAxisBehaviour>();
        PointerEventData data = new PointerEventData(EventSystem.current);
        axis.OnPointerClick(data);

        shapeSetted = true;
    }

    public void SetTriangle(Vector3[] positions)
    {
        if (positions.Length != 3)
            return;
        Vector3 faceNormal = Vector3.right;

        VertexResolvedBody u0 = new VertexResolvedBody(positions[0].x, positions[0].y, positions[0].z, faceNormal);
        // u0.isFixed = true;
        VertexResolvedBody u1 = new VertexResolvedBody(positions[1].x, positions[1].y, positions[1].z, faceNormal);
        // u1.isFixed = true;
        VertexResolvedBody u2 = new VertexResolvedBody(positions[2].x, positions[2].y, positions[2].z, faceNormal);
        // u2.isFixed = true;
        AddBaseVertex(u0);
        AddBaseVertex(u1);
        AddBaseVertex(u2);
        vertexResolvedBodies = new VertexResolvedBody[] { u0, u1, u2 };

        GeoVertex v0 = new GeoVertex(u0, true);
        GeoVertex v1 = new GeoVertex(u1, true);
        GeoVertex v2 = new GeoVertex(u2, true);
        AddGeoVertex(v0);
        AddGeoVertex(v1);
        AddGeoVertex(v2);

        GeoEdge e0 = new GeoEdge(u0, u1, true);
        GeoEdge e1 = new GeoEdge(u1, u2, true);
        GeoEdge e2 = new GeoEdge(u2, u0, true);
        AddGeoEdge(e0);
        AddGeoEdge(e1);
        AddGeoEdge(e2);

        GeoFace f0 = new GeoFace(new VertexUnit[] { u0, u1, u2 }, true);
        AddGeoFace(f0);

        InitDatas();

        NavAxisBehaviour axis = GameObject.Find("X").GetComponent<NavAxisBehaviour>();
        PointerEventData data = new PointerEventData(EventSystem.current);
        axis.OnPointerClick(data);
        
        shapeSetted = true;
    }
    public void SetSphere(Vector3[] positions)
    {
        if (positions.Length != 9)
            return;
        Vector3 faceNormal = Vector3.right;

        VertexResolvedBody u0 = new VertexResolvedBody(positions[0].x, positions[0].y, positions[0].z, faceNormal);
        // u0.isFixed = true;
        VertexResolvedBody u1 = new VertexResolvedBody(positions[1].x, positions[1].y, positions[1].z, faceNormal);
        // u1.isFixed = true;
        VertexResolvedBody u2 = new VertexResolvedBody(positions[2].x, positions[2].y, positions[2].z, faceNormal);
        // u2.isFixed = true;
        AddBaseVertex(u0);
        AddBaseVertex(u1);
        AddBaseVertex(u2);
        VertexResolvedBody u3 = new VertexResolvedBody(positions[3].x, positions[3].y, positions[3].z, faceNormal);
        AddBaseVertex(u3);
        VertexResolvedBody u4 = new VertexResolvedBody(positions[4].x, positions[4].y, positions[4].z, faceNormal);
        AddBaseVertex(u4);
        VertexResolvedBody u5 = new VertexResolvedBody(positions[5].x, positions[5].y, positions[5].z, faceNormal);
        AddBaseVertex(u5);
        VertexResolvedBody u6 = new VertexResolvedBody(positions[6].x, positions[6].y, positions[6].z, faceNormal);
        AddBaseVertex(u6);
        VertexResolvedBody u7 = new VertexResolvedBody(positions[7].x, positions[7].y, positions[7].z, faceNormal);
        AddBaseVertex(u7);
        VertexResolvedBody u8 = new VertexResolvedBody(positions[8].x, positions[8].y, positions[8].z, faceNormal);
        AddBaseVertex(u8);
        
        vertexResolvedBodies = new VertexResolvedBody[] { u0, u1, u2,u3,u4,u5,u6,u7,u8 };

        GeoVertex v0 = new GeoVertex(u0, true);
        GeoVertex v1 = new GeoVertex(u1, true);
        GeoVertex v2 = new GeoVertex(u2, true);
        AddGeoVertex(v0);
        AddGeoVertex(v1);
        AddGeoVertex(v2);
        GeoVertex v3 = new GeoVertex(u3, true);
        AddGeoVertex(v3);
        GeoVertex v4 = new GeoVertex(u4, true);
        AddGeoVertex(v4);
        GeoVertex v5 = new GeoVertex(u5, true);
        AddGeoVertex(v5);
        GeoVertex v6 = new GeoVertex(u6, true);
        AddGeoVertex(v6);
        GeoVertex v7 = new GeoVertex(u7, true);
        AddGeoVertex(v7);
        GeoVertex v8 = new GeoVertex(u8, true);
        AddGeoVertex(v8);
        
        
        GeoEdge e0 = new GeoEdge(u0, u1, true);
        GeoEdge e1 = new GeoEdge(u1, u2, true);
        GeoEdge e2 = new GeoEdge(u2, u3, true);
        AddGeoEdge(e0);
        AddGeoEdge(e1);
        AddGeoEdge(e2);

        GeoEdge e3 = new GeoEdge(u3, u4, true);
        AddGeoEdge(e3);
        GeoEdge e4 = new GeoEdge(u4, u5, true);
        AddGeoEdge(e4);
        GeoEdge e5 = new GeoEdge(u5, u6, true);
        AddGeoEdge(e5);
        GeoEdge e6 = new GeoEdge(u6, u7, true);
        AddGeoEdge(e6);
        GeoEdge e7 = new GeoEdge(u7, u8, true);
        AddGeoEdge(e7);
        GeoEdge e8 = new GeoEdge(u8, u0, true);
        AddGeoEdge(e8);
        
        GeoFace f0 = new GeoFace(new VertexUnit[] { u0, u1, u2,u3,u4,u5,u6,u7,u8 }, true);
        AddGeoFace(f0);

        InitDatas();

        NavAxisBehaviour axis = GameObject.Find("X").GetComponent<NavAxisBehaviour>();
        PointerEventData data = new PointerEventData(EventSystem.current);
        axis.OnPointerClick(data);

        shapeSetted = true;
    }
}

public class ResolvedBodyGeometryTool : GeometryTool
{
    private StatusButton lockButton;

    public override Geometry GenerateGeometry()
    {
        ResolvedBody geo = new ResolvedBody();
        geo.Constructor = new ResolvedBodyConstructor(geo);
        geo.Assistor = new Assistor(geo);
        geo.Implement = new Implement(geo);
        geo.Init();
        // // transform camera
        // NavAxisBehaviour axis = GameObject.Find("X").GetComponent<NavAxisBehaviour>();
        // PointerEventData data = new PointerEventData(EventSystem.current);
        // axis.OnPointerClick(data);

        lockButton = GameObject.Find("LockButton").GetComponent<StatusButton>();
        lockButton.SetStatus(1);

        return geo;
    }
}

public class ResolvedBodyGeometryState : GeometryState
{
    new ResolvedBody geometry;

    public ResolvedBodyGeometryState(Tool tool, Geometry geometry) : base(tool, geometry)
    {
        if (geometry is ResolvedBody)
            this.geometry = (ResolvedBody)geometry;
    }

    public override FormInput Title()
    {
        // add state
        FormInput formInput = new FormInput(1);
        formInput.inputs[0] = new FormText("旋转体");

        return formInput;
    }
}