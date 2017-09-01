using UnityEngine;

[ExecuteInEditMode]
public class Pivot3D : MonoBehaviour {

    public int X;
    public int Y;

    GameView3D gameView;

    private void Start()
    {
        gameView = FindObjectOfType<GameView3D>();
    }

    private void Update()
    {
        name = Y + " "+ X;
    }

    private void OnMouseUpAsButton()
    {
        gameView.PivotClicked(X, Y);
    }
}
