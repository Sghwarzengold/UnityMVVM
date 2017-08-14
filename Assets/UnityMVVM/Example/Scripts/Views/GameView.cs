using System;
using MVVMExample.ViewModels;

using UnityEngine;
using UnityMvvm;
using System.Collections.Generic;
using System.Linq;

public class GameView : View<GameViewModel>
{
    public DraughtView BlackDraught;
    public DraughtView WhiteDraught;

    List<DraughtView> draughts = new List<DraughtView>();

    DraughtView currentDraught = null;

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

    private void Go_Clicked(DraughtView sender)
    {
        currentDraught = sender;
    }

    protected override void UpdateState()
    {
        var vm = GetViewModel();
        var allLiveDraughts = vm.Blacks.Concat(vm.Whites).ToList();

        var toDestroy = new List<DraughtView>();
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

    private void OnMouseDown()
    {
        if (currentDraught == null)
            return;

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var tX = Math.Round((pos.x - ViewConstants.ZERO_X) / ViewConstants.STEP);
        var tY = Math.Round((pos.y - ViewConstants.ZERO_Y) / ViewConstants.STEP) * -1;

        GetViewModel().Turn(currentDraught.GetViewModel(), (int)tX, (int)tY);

        currentDraught = null;
    }
}
