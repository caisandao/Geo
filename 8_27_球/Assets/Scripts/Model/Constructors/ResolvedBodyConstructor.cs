using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResolvedBodyCondition : Condition 
{

}

public abstract class ResolvedBodyConditionTool : ConditionTool
{

}

public class ResolvedBodyConstructor : Constructor
{
    private RectangleCondition rectangleCondition;
	private TriangleCondition triangleCondition;
    private SphereCondition sphereCondition;
    new ResolvedBody geometry;
    private bool geometrySetted;


    public ResolvedBodyConstructor(Geometry geometry) : base(geometry)
    {
        if (geometry is ResolvedBody)
            this.geometry = (ResolvedBody)geometry;

        geometrySetted = false;
    }

    public override bool AddCondition(Condition condition) {
        if (!(condition is ResolvedBodyCondition) || geometrySetted)
            return false;

        if (CheckAddCondition((ResolvedBodyCondition)condition))
        {
            Resolve();
            return true;
        }

        return false;
    }

    public override bool RemoveCondition(Condition condition) {
        if (!(condition is ResolvedBodyCondition))
            return false;
        if (condition is RectangleCondition)
        {
            if (rectangleCondition != (RectangleCondition)condition)
                return false;
            rectangleCondition = null;
            return true;
        }
		if (condition is TriangleCondition) {
            if (triangleCondition != (TriangleCondition)condition) {
                return false;
            }
            triangleCondition = null;
            return true;
        }
        
        if (condition is SphereCondition)
        {
            if (sphereCondition != (SphereCondition)condition)
            {
                return false;
            }
            sphereCondition = null;
            return true;
        }
        return false;
    }

    public override void ClearConditions() {
        rectangleCondition = null;
		triangleCondition = null;
        sphereCondition = null;
    }

    private bool CheckAddCondition(ResolvedBodyCondition condition)
    {

        if (condition is ResolvedBodyCondition)
        {
            if (condition is RectangleCondition)
            {
                if (rectangleCondition != null)
                    return false;
                rectangleCondition = (RectangleCondition)condition;
                return true;
            }
            if (condition is TriangleCondition)
            {
                if (triangleCondition != null)
                    return false;
                triangleCondition = (TriangleCondition)condition;
                return true;
            }
            if (condition is SphereCondition)
            {
                if (sphereCondition != null)
                    return false;
                sphereCondition = (SphereCondition)condition;
                return true;
            }

        }

        return false;
    }

    private void Resolve()
    {
        GeometryBehaviour geometryBehaviour = GameObject.Find("/3D/Geometry").GetComponent<GeometryBehaviour>();
        if (rectangleCondition != null){
            Vector2 position = new Vector2(rectangleCondition.height, rectangleCondition.width);
            Vector3[] points = new Vector3[4];
            points[0] = new Vector3(0, position.x / 2, 0);
            points[1] = new Vector3(0, -position.x / 2, 0);
            points[2] = new Vector3(0, -position.x / 2, position.y);
            points[3] = new Vector3(0, position.x / 2, position.y);
            geometry.SetRectangle(points);
            geometryBehaviour.InitGeometry(geometry);
            geometrySetted = true;
        }
		if (triangleCondition != null) {
            Vector2 position = new Vector2(triangleCondition.height, triangleCondition.width);
            Vector3[] points = new Vector3[3];
            points[0] = new Vector3(0, position.x / 2, 0);
            points[1] = new Vector3(0, -position.x / 2, 0);
            points[2] = new Vector3(0, -position.x / 2, position.y);
            geometry.SetTriangle(points);
            geometryBehaviour.InitGeometry(geometry);
            geometrySetted = true;
        }
        if (sphereCondition != null)
        {
            //八分之π
            float pi8 = 22.5f;
            float sinpi8 = Mathf.Sin(Mathf.Deg2Rad * pi8);
            //四分之pi
            float sinpi4= Mathf.Sin(Mathf.Deg2Rad * pi8*2);
            //八分之三pi
            float sinpi38= Mathf.Sin(Mathf.Deg2Rad * pi8 * 3);
            float cospi8 = sinpi38;
            float cospi4 = sinpi4;
            float cospi38 = sinpi8;

            Vector2 position = new Vector2(sphereCondition.height, sphereCondition.radius);
            Vector3[] points = new Vector3[9];
            points[0] = new Vector3(0,position.x+ position.y / 2, 0);
            points[1] = new Vector3(0, position.x + sinpi38 *position.y / 2, cospi38*position.y / 2);
            points[2] = new Vector3(0, position.x + sinpi4 *position.y / 2,cospi4* position.y/2);
            points[3] = new Vector3(0, position.x + sinpi8 * position.y / 2, cospi8 * position.y / 2);
            points[4] = new Vector3(0, position.x ,  position.y / 2);
            points[5] = new Vector3(0, position.x - sinpi8 * position.y / 2, cospi8 * position.y / 2);
            points[6] = new Vector3(0, position.x - sinpi4 * position.y / 2, cospi4 * position.y / 2);
            points[7] = new Vector3(0, position.x - sinpi38 * position.y / 2, cospi38 * position.y / 2);
            points[8] = new Vector3(0, position.x -  position.y / 2,  0);
            geometry.SetSphere(points);
            geometryBehaviour.InitGeometry(geometry);
            geometrySetted = true;
        }
    }

}