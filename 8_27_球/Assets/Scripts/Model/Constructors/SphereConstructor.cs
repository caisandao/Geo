using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCondition : ResolvedBodyCondition
{
    //两个直角边长
    public float height;//z轴竖直方向的坐标

    public float radius;//半径

    public SphereCondition(float height, float radius)
    {
        this.height = height;
        this.radius = radius;
    }
}


public class SphereConditionTool : ResolvedBodyConditionTool
{
    public override FormInput FormInput()
    {
        // FormInput formInput = new FormInput(7);

        // formInput.inputs[0] = new FormText("垂直边长");
        // formInput.inputs[1] = new FormText("=");
        // formInput.inputs[2] = new FormNum();
        // formInput.inputs[3] = new FormText("，");
        // formInput.inputs[4] = new FormText("水平边长");
        // formInput.inputs[5] = new FormText("=");
        // formInput.inputs[6] = new FormNum();
        return null;
    }

    public override bool ValidateInput(Geometry geometry, FormInput formInput)
    {
        if (!(geometry is ResolvedBody))
            return false;
        ResolvedBody resolvedBody = (ResolvedBody)geometry;

        // FormNum height = (FormNum)formInput.inputs[2];
        // FormNum width = (FormNum)formInput.inputs[6];
        // if (!IsValidLength(height) || !IsValidLength(width))
        //     return false;

        return true;
    }

    public override Condition GenerateCondition(Geometry geometry, FormInput formInput)
    {
        bool valid = ValidateInput(geometry, formInput);
        if (!valid)
            return null;

        // FormNum height = (FormNum)formInput.inputs[2];
        // FormNum width = (FormNum)formInput.inputs[6];
        SphereCondition condition = new SphereCondition(0, 1);

        return condition;
    }
}

public class SphereConditionState : ConditionState
{
    new SphereCondition condition;
    ResolvedBody geometry;

    public SphereConditionState(Tool tool, Condition condition, Geometry geometry) : base(tool, condition)
    {
        if (condition is SphereCondition)
            this.condition = (SphereCondition)condition;

        if (geometry is ResolvedBody)
            this.geometry = (ResolvedBody)geometry;
    }

    public override int[] DependVertices()
    {
        return new int[] { };
    }

    public override FormInput Title()
    {
        FormInput formInput = new FormInput(1);

        formInput.inputs[0] = new FormText("旋转图形: 球形");

        return formInput;
    }

}