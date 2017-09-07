using MVVMExample.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMvvm;
using System;
using System.Linq;

public class GameView3D : View<GameViewModel> {

    public DraughtView3D BlackDraught;
    public DraughtView3D WhiteDraught;

    List<DraughtView3D> draughts = new List<DraughtView3D>();

    internal void PivotClicked(int x, int y)
    {
        if (currentDraught == null)
            return;

        GetViewModel().Turn(currentDraught.GetViewModel(), x, y);
        currentDraught.SetHighlight(false);
        currentDraught = null;
    }

    DraughtView3D currentDraught = null;

    protected override void InitState()
    {
        var vm = GetViewModel();

        foreach (var item in vm.Blacks)
        {
            var go = Instantiate(BlackDraught);
            go.BindWith(item);
            go.Clicked += Go_Clicked;
            draughts.Add(go);
        }

        foreach (var item in vm.Whites)
        {
            var go = Instantiate(WhiteDraught);
            go.BindWith(item);
            go.Clicked += Go_Clicked;
            draughts.Add(go);
        }
    }

    private void Go_Clicked(DraughtView3D sender)
    {
        currentDraught = sender;

        foreach (var dr in draughts)
        {
            dr.SetHighlight(dr == sender);
        }
    }

    protected override void UpdateState()
    {
        var vm = GetViewModel();
        var allLiveDraughts = vm.Blacks.Concat(vm.Whites).ToList();

        var toDestroy = new List<DraughtView3D>();
        foreach (var item in draughts)
        {
            if (allLiveDraughts.Contains(item.GetViewModel()))
                continue;

            toDestroy.Add(item);
        }

        foreach (var item in toDestroy)
        {
            draughts.Remove(item);
            item.Clicked -= Go_Clicked;
            Destroy(item.gameObject);
        }
    }

}
