﻿using System;

using UnityEngine;
using UnityMvvm;

using MVVMExample.ViewModels;

public class DraughtView : View<DraughtViewModel>
{
    float zeroX;
    float zeroY;
    float zeroZ;

    float step;

    public event Action<DraughtView> Clicked;

    protected override void InitState()
    {
        zeroX = ViewConstants.ZERO_X;
        zeroY = ViewConstants.ZERO_Y;
        zeroZ = ViewConstants.ZERO_Z;
        step = ViewConstants.STEP;

        var vm = GetViewModel();

        transform.position = new Vector3(zeroX + step * vm.X, zeroY - step * vm.Y, zeroZ);
    }

    protected override void UpdateState()
    {
        var vm = GetViewModel();

        transform.position = new Vector3(zeroX + step * vm.X, zeroY - step * vm.Y, zeroZ);
    }

    private void OnMouseDown()
    {
        if (Clicked != null)
            Clicked.Invoke(this);
    }
}
