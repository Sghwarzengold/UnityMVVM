using System;
using MVVMExample.ViewModels;
using UnityMvvm;
using System.Collections.Generic;
using UnityEngine;

public class ManView3D : View<ManViewModel>
{
    public Action<ManView3D> Clicked { get; internal set; }

    List<Pivot3D> pivots = new List<Pivot3D>();

    [SerializeField]
    GameObject highlightObject;

    public void SetHighlight(bool on)
    {
        highlightObject.SetActive(on);
    }

    protected override void InitState()
    {
        SetHighlight(false);
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
