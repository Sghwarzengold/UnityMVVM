using MVVMExample.Models;
using MVVMExample.ViewModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameView gameView;

    GameViewModel gameViewModel;

    GameModel model;

	// Use this for initialization
	void Start ()
    {
        model = new GameModel();
        gameViewModel = new GameViewModel(model);

        gameViewModel.InitiStartState();

        gameView.BindWith(gameViewModel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
