using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRadiusCondition : SphereCondition
{
    public float radius;

    public SphereRadiusCondition(float radius)
    {
        this.radius = radius;

        GizmoLength gizmoLength = new GizmoLength(Sphere.PNP_A, Sphere.PPP_E);
        gizmos = new Gizmo[] { gizmoLength };
    }
}


public class SphereRadiusConditionTool : SphereConditionTool
{
    public override FormInput FormInput()
    {
        FormInput formInput = new FormInput(3);

        formInput.inputs[0] = new FormText("半径");
        formInput.inputs[1] = new FormText("=");
        formInput.inputs[2] = new FormNum();

        return formInput;
    }

    public override bool ValidateInput(Geometry geometry, FormInput formInput)
    {
        if (!(geometry is Sphere))
            return false;
        Sphere sphere = (Sphere)geometry;

        FormNum formNum = (FormNum)formInput.inputs[2];
        if (!IsValidLength(formNum))
            return false;

        return true;
    }

    public override Condition GenerateCondition(Geometry geometry, FormInput formInput)
    {
        bool valid = ValidateInput(geometry, formInput);
        if (!valid)
            return null;

        FormNum formNum = (FormNum)formInput.inputs[2];
        SphereRadiusCondition condition = new SphereRadiusCondition(formNum.num);

        return condition;
    }
}

public class SphereRadiusConditionState : ConditionState
{
    new SphereRadiusCondition condition;
    Sphere geometry;

    public SphereRadiusConditionState(Tool tool, Condition condition, Geometry geometry) : base(tool, condition)
    {
        if (condition is SphereRadiusCondition)
            this.condition = (SphereRadiusCondition)condition;

        if (geometry is Sphere)
            this.geometry = (Sphere)geometry;
    }

    public override int[] DependVertices()
    {
        return new int[] { };
    }

    public override FormInput Title()
    {
        FormInput formInput = new FormInput(3);

        formInput.inputs[0] = new FormText("半径");
        formInput.inputs[1] = new FormText("=");
        formInput.inputs[2] = new FormNum(condition.radius);

        return formInput;
    }

}