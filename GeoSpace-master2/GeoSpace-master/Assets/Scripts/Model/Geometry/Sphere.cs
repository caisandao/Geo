using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Triangular Pyramid
public class Sphere : Geometry
{
    // Positive +    Negative -

    /*
    public const int PNP_A = 0;
    public const int NNP_B = 1;
    public const int NNN_C = 2;
    public const int PNN_D = 3;
    public const int PPP_E = 4;
    public const int NPP_F = 5;
    public const int NPN_G = 6;
    public const int PPN_H = 7;
    */

    private VertexSphere[] vertexSpheres;

    public override void Init()
    {
        //base.Init();

        Name = "Sphere";
        Type = GeometryType.Sph;

        VertexSphere a0 = new VertexSphere(0, 1, 0);
        VertexSphere b0 = new VertexSphere(0, 0.75, 0.6614378278);
        VertexSphere b1 = new VertexSphere(0.6614378278 / 2, 0.75, 0.6614378278 / 2);
        VertexSphere b2 = new VertexSphere(0.6614378278, 0.75, 0);
        VertexSphere b3 = new VertexSphere(0.6614378278 / 2, 0.75, -0.6614378278 / 2);
        VertexSphere b4 = new VertexSphere(0, 0.75, -0.6614378278);
        VertexSphere b5 = new VertexSphere(-0.6614378278 / 2, 0.75, -0.6614378278 / 2);
        VertexSphere b6 = new VertexSphere(-0.6614378278, 0.75, 0);
        VertexSphere b7 = new VertexSphere(-0.6614378278 / 2, 0.75, 0.6614378278 / 2);
        VertexSphere c0 = new VertexSphere(0, 0.5, 0.8660254038);
        VertexSphere c1 = new VertexSphere(0.8660254038 / 2, 0.5, 0.8660254038 / 2);
        VertexSphere c2 = new VertexSphere(0.8660254038, 0.5, 0);
        VertexSphere c3 = new VertexSphere(0.8660254038 / 2, 0.5, -0.8660254038 / 2);
        VertexSphere c4 = new VertexSphere(0, 0.5, -0.8660254038);
        VertexSphere c5 = new VertexSphere(-0.8660254038 / 2, 0.5, -0.8660254038 / 2);
        VertexSphere c6 = new VertexSphere(-0.8660254038, 0.5, 0);
        VertexSphere c7 = new VertexSphere(-0.8660254038 / 2, 0.5, 0.8660254038 / 2);
        VertexSphere d0 = new VertexSphere(0, 0.25, 0.9682458366);
        VertexSphere d1 = new VertexSphere(0.9682458366 / 2, 0.25, 0.9682458366 / 2);
        VertexSphere d2 = new VertexSphere(0.9682458366, 0.25, 0);
        VertexSphere d3 = new VertexSphere(0.9682458366 / 2, 0.25, -0.9682458366 / 2);
        VertexSphere d4 = new VertexSphere(0, 0.25, -0.9682458366);
        VertexSphere d5 = new VertexSphere(-0.9682458366 / 2, 0.25, -0.9682458366 / 2);
        VertexSphere d6 = new VertexSphere(-0.9682458366, 0.25, 0);
        VertexSphere d7 = new VertexSphere(-0.9682458366 / 2, 0.25, 0.9682458366 / 2);
        VertexSphere e0 = new VertexSphere(0, 0, 1);
        VertexSphere e1 = new VertexSphere(0.5, 0, 0.5);
        VertexSphere e2 = new VertexSphere(1, 0, 0);
        VertexSphere e3 = new VertexSphere(0.5, 0, -0.5);
        VertexSphere e4 = new VertexSphere(0, 0, -1);
        VertexSphere e5 = new VertexSphere(-0.5, 0, -0.5);
        VertexSphere e6 = new VertexSphere(-1, 0, 0);
        VertexSphere e7 = new VertexSphere(-0.5, 0, 0.5);
        VertexSphere f0 = new VertexSphere(0, -0.25, 0.9682458366);
        VertexSphere f1 = new VertexSphere(0.9682458366 / 2, -0.25, 0.9682458366 / 2);
        VertexSphere f2 = new VertexSphere(0.9682458366, -0.25, 0);
        VertexSphere f3 = new VertexSphere(0.9682458366 / 2, -0.25, -0.9682458366 / 2);
        VertexSphere f4 = new VertexSphere(0, -0.25, -0.9682458366);
        VertexSphere f5 = new VertexSphere(-0.9682458366 / 2, -0.25, -0.9682458366 / 2);
        VertexSphere f6 = new VertexSphere(-0.9682458366, -0.25, 0);
        VertexSphere f7 = new VertexSphere(-0.9682458366 / 2, -0.25, 0.9682458366 / 2);
        VertexSphere g0 = new VertexSphere(0, -0.5, 0.8660254038);
        VertexSphere g1 = new VertexSphere(0.8660254038 / 2, -0.5, 0.8660254038 / 2);
        VertexSphere g2 = new VertexSphere(0.8660254038, -0.5, 0);
        VertexSphere g3 = new VertexSphere(0.8660254038 / 2, -0.5, -0.8660254038 / 2);
        VertexSphere g4 = new VertexSphere(0, -0.5, -0.8660254038);
        VertexSphere g5 = new VertexSphere(-0.8660254038 / 2, -0.5, -0.8660254038 / 2);
        VertexSphere g6 = new VertexSphere(-0.8660254038, -0.5, 0);
        VertexSphere g7 = new VertexSphere(-0.8660254038 / 2, -0.5, 0.8660254038 / 2);
        VertexSphere h0 = new VertexSphere(0, -0.75, 0.6614378278);
        VertexSphere h1 = new VertexSphere(0.6614378278 / 2, -0.75, 0.6614378278 / 2);
        VertexSphere h2 = new VertexSphere(0.6614378278, -0.75, 0);
        VertexSphere h3 = new VertexSphere(0.6614378278 / 2, -0.75, -0.6614378278 / 2);
        VertexSphere h4 = new VertexSphere(0, -0.75, -0.6614378278);
        VertexSphere h5 = new VertexSphere(-0.6614378278 / 2, -0.75, -0.6614378278 / 2);
        VertexSphere h6 = new VertexSphere(-0.6614378278, -0.75, 0);
        VertexSphere h7 = new VertexSphere(-0.6614378278 / 2, -0.75, 0.6614378278 / 2);
        VertexSphere i0 = new VertexSphere(0, -1, 0);

        AddBaseVertex(a0);
        AddBaseVertex(b0);
        AddBaseVertex(b1);
        AddBaseVertex(b2);
        AddBaseVertex(b3);
        AddBaseVertex(b4);
        AddBaseVertex(b5);
        AddBaseVertex(b6);
        AddBaseVertex(b7);
        AddBaseVertex(c0);
        AddBaseVertex(c1);
        AddBaseVertex(c2);
        AddBaseVertex(c3);
        AddBaseVertex(c4);
        AddBaseVertex(c5);
        AddBaseVertex(c6);
        AddBaseVertex(c7);
        AddBaseVertex(d0);
        AddBaseVertex(d1);
        AddBaseVertex(d2);
        AddBaseVertex(d3);
        AddBaseVertex(d4);
        AddBaseVertex(d5);
        AddBaseVertex(d6);
        AddBaseVertex(d7);
        AddBaseVertex(e0);
        AddBaseVertex(e1);
        AddBaseVertex(e2);
        AddBaseVertex(e3);
        AddBaseVertex(e4);
        AddBaseVertex(e5);
        AddBaseVertex(e6);
        AddBaseVertex(e7);
        AddBaseVertex(f0);
        AddBaseVertex(f1);
        AddBaseVertex(f2);
        AddBaseVertex(f3);
        AddBaseVertex(f4);
        AddBaseVertex(f5);
        AddBaseVertex(f6);
        AddBaseVertex(f7);
        AddBaseVertex(g0);
        AddBaseVertex(g1);
        AddBaseVertex(g2);
        AddBaseVertex(g3);
        AddBaseVertex(g4);
        AddBaseVertex(g5);
        AddBaseVertex(g6);
        AddBaseVertex(g7);
        AddBaseVertex(h0);
        AddBaseVertex(h1);
        AddBaseVertex(h2);
        AddBaseVertex(h3);
        AddBaseVertex(h4);
        AddBaseVertex(h5);
        AddBaseVertex(h6);
        AddBaseVertex(h7);
        AddBaseVertex(i0);

        vertexSpheres = new VertexSphere[] { a0, b0, b1, b2, b3, b4, b5, b6, b7, c0, c1, c2, c3, c4, c5, c6, c7, d0, d1, d2, d3, d4, d5, d6, d7, e0, e1, e2, e3, e4, e5, e6, e7, f0, f1, f2, f3, f4, f5, f6, f7, g0, g1, g2, g3, g4, g5, g6, g7, h0, h1, h2, h3, h4, h5, h6, h7, i0 };

        GeoVertex aa0 = new GeoVertex(a0, true);
        GeoVertex bb0 = new GeoVertex(b0, true);
        GeoVertex bb1 = new GeoVertex(b1, true);
        GeoVertex bb2 = new GeoVertex(b2, true);
        GeoVertex bb3 = new GeoVertex(b3, true);
        GeoVertex bb4 = new GeoVertex(b4, true);
        GeoVertex bb5 = new GeoVertex(b5, true);
        GeoVertex bb6 = new GeoVertex(b6, true);
        GeoVertex bb7 = new GeoVertex(b7, true);
        GeoVertex cc0 = new GeoVertex(c0, true);
        GeoVertex cc1 = new GeoVertex(c1, true);
        GeoVertex cc2 = new GeoVertex(c2, true);
        GeoVertex cc3 = new GeoVertex(c3, true);
        GeoVertex cc4 = new GeoVertex(c4, true);
        GeoVertex cc5 = new GeoVertex(c5, true);
        GeoVertex cc6 = new GeoVertex(c6, true);
        GeoVertex cc7 = new GeoVertex(c7, true);
        GeoVertex dd0 = new GeoVertex(d0, true);
        GeoVertex dd1 = new GeoVertex(d1, true);
        GeoVertex dd2 = new GeoVertex(d2, true);
        GeoVertex dd3 = new GeoVertex(d3, true);
        GeoVertex dd4 = new GeoVertex(d4, true);
        GeoVertex dd5 = new GeoVertex(d5, true);
        GeoVertex dd6 = new GeoVertex(d6, true);
        GeoVertex dd7 = new GeoVertex(d7, true);
        GeoVertex ee0 = new GeoVertex(e0, true);
        GeoVertex ee1 = new GeoVertex(e1, true);
        GeoVertex ee2 = new GeoVertex(e2, true);
        GeoVertex ee3 = new GeoVertex(e3, true);
        GeoVertex ee4 = new GeoVertex(e4, true);
        GeoVertex ee5 = new GeoVertex(e5, true);
        GeoVertex ee6 = new GeoVertex(e6, true);
        GeoVertex ee7 = new GeoVertex(e7, true);
        GeoVertex ff0 = new GeoVertex(f0, true);
        GeoVertex ff1 = new GeoVertex(f1, true);
        GeoVertex ff2 = new GeoVertex(f2, true);
        GeoVertex ff3 = new GeoVertex(f3, true);
        GeoVertex ff4 = new GeoVertex(f4, true);
        GeoVertex ff5 = new GeoVertex(f5, true);
        GeoVertex ff6 = new GeoVertex(f6, true);
        GeoVertex ff7 = new GeoVertex(f7, true);
        GeoVertex gg0 = new GeoVertex(g0, true);
        GeoVertex gg1 = new GeoVertex(g1, true);
        GeoVertex gg2 = new GeoVertex(g2, true);
        GeoVertex gg3 = new GeoVertex(g3, true);
        GeoVertex gg4 = new GeoVertex(g4, true);
        GeoVertex gg5 = new GeoVertex(g5, true);
        GeoVertex gg6 = new GeoVertex(g6, true);
        GeoVertex gg7 = new GeoVertex(g7, true);
        GeoVertex hh0 = new GeoVertex(h0, true);
        GeoVertex hh1 = new GeoVertex(h1, true);
        GeoVertex hh2 = new GeoVertex(h2, true);
        GeoVertex hh3 = new GeoVertex(h3, true);
        GeoVertex hh4 = new GeoVertex(h4, true);
        GeoVertex hh5 = new GeoVertex(h5, true);
        GeoVertex hh6 = new GeoVertex(h6, true);
        GeoVertex hh7 = new GeoVertex(h7, true);
        GeoVertex ii0 = new GeoVertex(i0, true);
    
        AddGeoVertex(aa0);
        AddGeoVertex(bb0);
        AddGeoVertex(bb1);
        AddGeoVertex(bb2);
        AddGeoVertex(bb3);
        AddGeoVertex(bb4);
        AddGeoVertex(bb5);
        AddGeoVertex(bb6);
        AddGeoVertex(bb7);
        AddGeoVertex(cc0);
        AddGeoVertex(cc1);
        AddGeoVertex(cc2);
        AddGeoVertex(cc3);
        AddGeoVertex(cc4);
        AddGeoVertex(cc5);
        AddGeoVertex(cc6);
        AddGeoVertex(cc7);
        AddGeoVertex(dd0);
        AddGeoVertex(dd1);
        AddGeoVertex(dd2);
        AddGeoVertex(dd3);
        AddGeoVertex(dd4);
        AddGeoVertex(dd5);
        AddGeoVertex(dd6);
        AddGeoVertex(dd7);
        AddGeoVertex(ee0);
        AddGeoVertex(ee1);
        AddGeoVertex(ee2);
        AddGeoVertex(ee3);
        AddGeoVertex(ee4);
        AddGeoVertex(ee5);
        AddGeoVertex(ee6);
        AddGeoVertex(ee7);
        AddGeoVertex(ff0);
        AddGeoVertex(ff1);
        AddGeoVertex(ff2);
        AddGeoVertex(ff3);
        AddGeoVertex(ff4);
        AddGeoVertex(ff5);
        AddGeoVertex(ff6);
        AddGeoVertex(ff7);
        AddGeoVertex(gg0);
        AddGeoVertex(gg1);
        AddGeoVertex(gg2);
        AddGeoVertex(gg3);
        AddGeoVertex(gg4);
        AddGeoVertex(gg5);
        AddGeoVertex(gg6);
        AddGeoVertex(gf7);
        AddGeoVertex(hh0);
        AddGeoVertex(hh1);
        AddGeoVertex(hh2);
        AddGeoVertex(hh3);
        AddGeoVertex(hh4);
        AddGeoVertex(hh5);
        AddGeoVertex(hh6);
        AddGeoVertex(hh7);
        AddGeoVertex(ii0);
        /*
        GeoEdge e0 = new GeoEdge(u0, u1, true);
        GeoEdge e1 = new GeoEdge(u1, u2, true);
        GeoEdge e2 = new GeoEdge(u2, u3, true);
        GeoEdge e3 = new GeoEdge(u3, u0, true);
        GeoEdge e4 = new GeoEdge(u4, u5, true);
        GeoEdge e5 = new GeoEdge(u5, u6, true);
        GeoEdge e6 = new GeoEdge(u6, u7, true);
        GeoEdge e7 = new GeoEdge(u7, u4, true);
        GeoEdge e8 = new GeoEdge(u0, u4, true);
        GeoEdge e9 = new GeoEdge(u1, u5, true);
        GeoEdge e10 = new GeoEdge(u2, u6, true);
        GeoEdge e11 = new GeoEdge(u3, u7, true);
        AddGeoEdge(e0);
        AddGeoEdge(e1);
        AddGeoEdge(e2);
        AddGeoEdge(e3);
        AddGeoEdge(e4);
        AddGeoEdge(e5);
        AddGeoEdge(e6);
        AddGeoEdge(e7);
        AddGeoEdge(e8);
        AddGeoEdge(e9);
        AddGeoEdge(e10);
        AddGeoEdge(e11);

        GeoFace f0 = new GeoFace(new VertexUnit[] { u0, u1, u2, u3 }, true);
        GeoFace f1 = new GeoFace(new VertexUnit[] { u0, u4, u5, u1 }, true);
        GeoFace f2 = new GeoFace(new VertexUnit[] { u1, u5, u6, u2 }, true);
        GeoFace f3 = new GeoFace(new VertexUnit[] { u2, u6, u7, u3 }, true);
        GeoFace f4 = new GeoFace(new VertexUnit[] { u3, u7, u4, u0 }, true);
        GeoFace f5 = new GeoFace(new VertexUnit[] { u7, u6, u5, u4 }, true);
        AddGeoFace(f0);
        AddGeoFace(f1);
        AddGeoFace(f2);
        AddGeoFace(f3);
        AddGeoFace(f4);
        AddGeoFace(f5);
        */

        InitDatas();
    }

    public override void MoveVertex(VertexUnit vertex, Ray ray, Transform camera, bool snap)
    {
        base.MoveVertex(vertex, ray, camera, snap);
        if (!vertex.isBase)
            return;
        Vector3 position = vertex.Position();
        SetVerticesAbsPosition(position);
    }


    public override VertexUnit[] VerticesOfMoveVertex(VertexUnit vertex)
    {
        if (!vertex.isBase)
            return base.VerticesOfMoveVertex(vertex);
        else
            return (VertexUnit[])vertexCuboids.Clone();
    }

    public void SetVerticesAbsPosition(Vector3 position)
    {
        foreach (VertexCuboid unit in vertexCuboids)
            unit.SetAbsPosition(position);
    }

}

public class CuboidGeometryTool : GeometryTool
{
    public override Geometry GenerateGeometry()
    {
        Cuboid cuboid = new Cuboid();
        cuboid.Constructor = new CuboidConstructor(cuboid);
        cuboid.Assistor = new Assistor(cuboid);
        cuboid.Implement = new Implement(cuboid);
        cuboid.Init();

        return cuboid;
    }
}

public class CuboidGeometryState : GeometryState
{
    new Cuboid geometry;

    public CuboidGeometryState(Tool tool, Geometry geometry) : base(tool, geometry)
    {
        if (geometry is Cuboid)
            this.geometry = (Cuboid)geometry;
    }

    public override FormInput Title()
    {
        FormElement formElement = new FormElement(8);
        
        formElement.fields[4] = geometry.VertexSign(Cuboid.PNP_A);
        formElement.fields[5] = geometry.VertexSign(Cuboid.NNP_B);
        formElement.fields[6] = geometry.VertexSign(Cuboid.NNN_C);
        formElement.fields[7] = geometry.VertexSign(Cuboid.PNN_D);
        formElement.fields[0] = geometry.VertexSign(Cuboid.PPP_E);
        formElement.fields[1] = geometry.VertexSign(Cuboid.NPP_F);
        formElement.fields[2] = geometry.VertexSign(Cuboid.NPN_G);
        formElement.fields[3] = geometry.VertexSign(Cuboid.PPN_H);

        FormInput formInput = new FormInput(1);

        formInput.inputs[0] = formElement;

        return formInput;
    }
}