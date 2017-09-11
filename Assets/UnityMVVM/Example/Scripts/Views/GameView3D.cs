using MVVMExample.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMvvm;
using System;
using System.Linq;

public class GameView3D : View<GameViewModel> {

    public ManView3D BlackManPrefab;
    public ManView3D WhiteManPrefab;

    List<ManView3D> men = new List<ManView3D>();

    internal void PivotClicked(int x, int y)
    {
        if (currentMan == null)
            return;

        GetViewModel().Turn(currentMan.GetViewModel(), x, y);
        currentMan.SetHighlight(false);
        currentMan = null;
    }

    ManView3D currentMan = null;

    protected override void InitState()
    {
        var vm = GetViewModel();

        foreach (var item in vm.Blacks)
        {
            var go = Instantiate(BlackManPrefab);
            go.BindWith(item);
            go.Clicked += Go_Clicked;
            go.transform.SetParent(this.transform);
            men.Add(go);
        }

        foreach (var item in vm.Whites)
        {
            var go = Instantiate(WhiteManPrefab);
            go.BindWith(item);
            go.Clicked += Go_Clicked;
            go.transform.SetParent(this.transform);
            men.Add(go);
        }
    }

    private void Go_Clicked(ManView3D sender)
    {
        currentMan = sender;

        foreach (var dr in men)
        {
            dr.SetHighlight(dr == sender);
        }
    }

    protected override void UpdateState()
    {
        var vm = GetViewModel();
        var allLiveDraughts = vm.Blacks.Concat(vm.Whites).ToList();

        var toDestroy = new List<ManView3D>();
        foreach (var item in men)
        {
            if (allLiveDraughts.Contains(item.GetViewModel()))
                continue;

            toDestroy.Add(item);
        }

        foreach (var item in toDestroy)
        {
            men.Remove(item);
            item.Clicked -= Go_Clicked;
            Destroy(item.gameObject);
        }
    }

}
