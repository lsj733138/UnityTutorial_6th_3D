using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool isRun, isFly, isSwim;

    private IMove move;
    private MoveRun run;
    private MoveFly fly;
    private MoveSwim swim;

    private void Start()
    {
        run = new MoveRun();
        fly = new MoveFly();
        swim = new MoveSwim();
    }

    private void Update()
    {
        if (isRun)
            move = run;

        if (isFly)
            move = fly;

        if (isSwim)
            move = swim;
    }
}
