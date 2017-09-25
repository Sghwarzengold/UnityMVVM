using System;

using UnityEngine;
using UnityMvvm;

using MVVMExample.ViewModels;

public class ManView : View<ManViewModel>
{
    float zeroX;
    float zeroY;
    float zeroZ;

    float step;

    public event Action<ManView> Clicked;

    [SerializeField]
    GameObject highlightObject;

    public void SetHighlight(bool on)
    {
        highlightObject.SetActive(on);
    }

    protected override void InitState()
    {
        SetHighlight(false);
        zeroX = ViewConstants.ZERO_X;
        zeroY = ViewConstants.ZERO_Y;
        zeroZ = ViewConstants.ZERO_Z;
        step = ViewConstants.STEP;
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
