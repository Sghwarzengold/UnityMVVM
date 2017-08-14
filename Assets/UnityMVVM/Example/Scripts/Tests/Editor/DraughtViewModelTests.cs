using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using MVVMExample.Models;
using MVVMExample.ViewModels;

public class DraughtViewModelTests {

	[Test]
	public void InitValuesByModel()
    {
        var model = new Draught { X = 1, Y = 2 };
        var viewModel = new DraughtViewModel(model);

        Assert.AreEqual(viewModel.X, model.X);
        Assert.AreEqual(viewModel.Y, model.Y);
	}

    [Test]
    public void ChangeValuesInModelByChangingInViewModel()
    {
        var model = new Draught { X = 1, Y = 2 };
        var viewModel = new DraughtViewModel(model);
        viewModel.X = 3;
        viewModel.Y = 4;

        Assert.AreEqual(model.X, 3);
        Assert.AreEqual(model.Y, 4);
    }

}
