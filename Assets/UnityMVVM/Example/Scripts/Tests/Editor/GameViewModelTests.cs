using System.Collections.Generic;

using NUnit.Framework;

using MVVMExample.ViewModels;
using MVVMExample.Models;

public class GameViewModelTests {

    //Constructor initialization
    [Test]
    public void GenerateViewModelBasedOnModel()
    {
        var model = new GameModel();
        model.Blacks.Add(new Man { X = 0, Y = 1 });
        model.Whites.Add(new Man { X = 2, Y = 3 });

        var controller = new GameViewModel(model);

        Assert.AreEqual(GameState.Game, controller.State);

        Assert.AreEqual(controller.Blacks.Count, model.Blacks.Count);
        Assert.AreEqual(controller.Whites.Count, model.Whites.Count);
        Assert.AreEqual(controller.Blacks[0].X, 0);
        Assert.AreEqual(controller.Blacks[0].Y, 1);
        Assert.AreEqual(controller.Whites[0].X, 2);
        Assert.AreEqual(controller.Whites[0].Y, 3);
    }


    [Test]
    public void CheckDraughtCountAfterStart()
    {
        var model = new GameModel();
        var controller = new GameViewModel(model);

        controller.InitiStartState();

        Assert.AreEqual(controller.Blacks.Count, 12);
        Assert.AreEqual(controller.Whites.Count, 12);
    }

    [Test]
    public void CheckDraughtPositionsAfterStart()
    {
        var model = new GameModel();
        var controller = new GameViewModel(model);

        controller.InitiStartState();

        var blacks = GetOriginalBlacksPostions();
        var whites = GetOriginalWhitesPostions();

        foreach (var item in blacks)
            Assert.IsTrue(controller.Blacks.Find(c => c.X == item.X && c.Y == item.Y) != null);
        foreach (var item in whites)
            Assert.IsTrue(controller.Whites.Find(c => c.X == item.X && c.Y == item.Y) != null);
    }

    //Player turn
    [Test]
    public void BlacksTurnForwardLeftFromStart()
    {
        var model = new GameModel();
        var controller = new GameViewModel(model);

        controller.InitiStartState();
        var black = controller.Blacks.Find(d => d.X == 1 && d.Y == 2);

        controller.Turn(black, 2, 3);

        Assert.AreEqual(black.X, 2);
        Assert.AreEqual(black.Y, 3);
    }

    [Test]
    public void BlackTriesTurnWrong()
    {
        var model = new GameModel();
        var controller = new GameViewModel(model);

        controller.InitiStartState();
        var black = controller.Blacks.Find(d => d.X == 1 && d.Y == 2);

        controller.Turn(black, 5, 6);

        Assert.AreEqual(black.X, 1);
        Assert.AreEqual(black.Y, 2);
    }

    [Test]
    public void WhiteTurnForwardRightFromStart()
    {
        var model = new GameModel();
        var controller = new GameViewModel(model);

        controller.InitiStartState();
        var white = controller.Whites.Find(d => d.X == 0 && d.Y == 5);

        controller.Turn(white, 1, 4);

        Assert.AreEqual(white.X, 1);
        Assert.AreEqual(white.Y, 4);
    }

    [TestCase(0, 1, -1, 2)]
    [TestCase(1, 0, -1, 1)]
    [TestCase(3, 7, 2, 8)]
    [TestCase(7, 2, 8, 3)]
    public void DraughtTriesToMoveOutOfDeck(int oX, int oY, int tX, int tY)
    {
        var model = new GameModel();
        model.Blacks.Add(new Man { X = oX, Y = oY });

        var controller = new GameViewModel(model);
        var draught = controller.Blacks[0];
        controller.Turn(draught, tX, tY);

        Assert.AreEqual(draught.X, oX);
        Assert.AreEqual(draught.Y, oY);
    }

    [Test]
    //Eat condition
    public void BlackEatWhiteOnTurn()
    {
        var model = new GameModel();
        model.Blacks.Add(new Man { X = 1, Y = 0 });
        model.Whites.Add(new Man { X = 2, Y = 1 });

        var controller = new GameViewModel(model);
        var draught = controller.Blacks[0];

        controller.Turn(draught, 3, 2);

        Assert.AreEqual(draught.X, 3);
        Assert.AreEqual(draught.Y, 2);
        Assert.IsEmpty(controller.Whites);
    }

    [Test]
    public void WhiteEatBlackOnTurn()
    {
        var model = new GameModel();
        model.Blacks.Add(new Man { X = 2, Y = 1 });
        model.Whites.Add(new Man { X = 3, Y = 2 });

        var controller = new GameViewModel(model);
        var draught = controller.Whites[0];

        controller.Turn(draught, 1, 0);

        Assert.AreEqual(draught.X, 1);
        Assert.AreEqual(draught.Y, 0);
        Assert.IsEmpty(controller.Blacks);
    }

    //GameOver
    [Test]
    public void BlackWins()
    {
        var model = new GameModel();
        model.Blacks.Add(new Man { X = 1, Y = 0 });
        model.Whites.Add(new Man { X = 2, Y = 1 });

        var controller = new GameViewModel(model);
        var draught = controller.Blacks[0];

        controller.Turn(draught, 3, 2);

        Assert.AreEqual(GameState.BlackWins, controller.State);
    }

    [Test]
    public void WhiteWins()
    {
        var model = new GameModel();
        model.Blacks.Add(new Man { X = 2, Y = 1 });
        model.Whites.Add(new Man { X = 3, Y = 2 });

        var controller = new GameViewModel(model);
        var draught = controller.Whites[0];

        controller.Turn(draught, 1, 0);

        Assert.AreEqual(GameState.WhiteWins, controller.State);
    }

    private List<Man> GetOriginalWhitesPostions()
    {
        var res = new List<Man>();

        res.Add(new Man { X = 0, Y = 5 });
        res.Add(new Man { X = 2, Y = 5 });
        res.Add(new Man { X = 4, Y = 5 });
        res.Add(new Man { X = 6, Y = 5 });
        res.Add(new Man { X = 1, Y = 6 });
        res.Add(new Man { X = 3, Y = 6 });
        res.Add(new Man { X = 5, Y = 6 });
        res.Add(new Man { X = 7, Y = 6 });
        res.Add(new Man { X = 0, Y = 7 });
        res.Add(new Man { X = 2, Y = 7 });
        res.Add(new Man { X = 4, Y = 7 });
        res.Add(new Man { X = 6, Y = 7 });

        return res;
    }

    private List<Man> GetOriginalBlacksPostions()
    {
        var res = new List<Man>();

        res.Add(new Man { X = 1, Y = 0 });
        res.Add(new Man { X = 3, Y = 0 });
        res.Add(new Man { X = 5, Y = 0 });
        res.Add(new Man { X = 7, Y = 0 });
        res.Add(new Man { X = 0, Y = 1 });
        res.Add(new Man { X = 2, Y = 1 });
        res.Add(new Man { X = 4, Y = 1 });
        res.Add(new Man { X = 6, Y = 1 });
        res.Add(new Man { X = 1, Y = 2 });
        res.Add(new Man { X = 3, Y = 2 });
        res.Add(new Man { X = 5, Y = 2 });
        res.Add(new Man { X = 7, Y = 2 });

        return res;
    }
}

