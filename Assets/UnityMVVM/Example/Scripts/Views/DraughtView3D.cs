using System;
using MVVMExample.ViewModels;
using UnityMvvm;
using System.Collections.Generic;

public class DraughtView3D : View<DraughtViewModel>
{
    public Action<DraughtView3D> Clicked { get; internal set; }

    List<Pivot3D> pivots = new List<Pivot3D>();

    protected override void InitState()
    {
        var list = FindObjectsOfType(typeof(Pivot3D));
        foreach (var item in list)
            pivots.Add(item as Pivot3D);
    }

    protected override void UpdateState()
    {
        var x = GetViewModel().X;
        var y = GetViewModel().Y;

        var pivot = pivots.Find(p => p.X == x && p.Y == y);

        if (pivot != null)
        {
            transform.position = pivot.transform.position;
        }
        else
        {
            GetViewModel().UnsubscribeView(this);
            Destroy(this);
        }
    }

    private void OnMouseUpAsButton()
    {
        if (Clicked != null)
            Clicked.Invoke(this);
    }
}
