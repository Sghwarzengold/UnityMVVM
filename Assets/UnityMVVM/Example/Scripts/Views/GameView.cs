using System;
using MVVMExample.ViewModels;

using UnityEngine;
using UnityMvvm;
using System.Collections.Generic;
using System.Linq;

public class GameView : View<GameViewModel>
{
    public ManView BlackManPrefab;
    public ManView WhiteManPrefab;

    List<ManView> men = new List<ManView>();

    ManView currentMan = null;

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

    private void Go_Clicked(ManView sender)
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

        var toDestroy = new List<ManView>();
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

    private void OnMouseDown()
    {
        if (currentMan == null)
            return;

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var tX = Math.Round((pos.x - ViewConstants.ZERO_X) / ViewConstants.STEP);
        var tY = Math.Round((pos.y - ViewConstants.ZERO_Y) / ViewConstants.STEP) * -1;

        GetViewModel().Turn(currentMan.GetViewModel(), (int)tX, (int)tY);
        currentMan.SetHighlight(false);
        currentMan = null;
    }
}
