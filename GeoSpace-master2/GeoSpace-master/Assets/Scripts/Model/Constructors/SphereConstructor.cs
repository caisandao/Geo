using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SphereCondition : Condition
{

}

public abstract class SphereConditionTool : ConditionTool
{

}

public class SphereConstructor : Constructor
{
    // private List<CuboidCondition> conditions;
    private SphereRadiusCondition radiusCondition;

    new Sphere geometry;

    public SphereConstructor(Geometry geometry) : base(geometry)
    {
        if (geometry is Sphere)
            this.geometry = (Sphere)geometry;
    }

    public override bool AddCondition(Condition condition)
    {
        if (!(condition is SphereCondition))
            return false;

        if (CheckAddCondition((SphereCondition)condition))
        {
            Resolve();
            return true;
        }

        return false;

    }

    public override bool RemoveCondition(Condition condition)
    {
        if (!(condition is SphereCondition))
            return false;

        if (condition is SphereRadiusCondition)
        {
            if (radiusCondition != (SphereRadiusCondition)condition)
                return false;
            radiusCondition = null;
            return true;
        }

        return false;
    }

    public override void ClearConditions()
    {
        radiusCondition = null;
    }

    private bool CheckAddCondition(SphereCondition condition)
    {

        if (condition is SphereRadiusCondition)
        {
            if (radiusCondition != null)
                return false;
            radiusCondition = (SphereRadiusCondition)condition;
            return true;
        }

        return false;
    }

    private void Resolve()
    {
        float radius;
        CuboidLengthWidthHeight(out radius);

        if (radiusCondition != null)
            radius = radiusCondition.radius;

        SetVertices(radius);
    }

    private void SetVertices(float radius)
    {
        Vector3 position = new Vector3(0, radius, 0);
        geometry.SetVerticesAbsPosition(position);
    }

    private void SphereRadius(out float radius)
    {
        Vector3 position = geometry.UnitVector(Cuboid.PPP_E);

        radius = position.y;
    }

}
