using System;
namespace JameNegar.Interfaces
{
    internal interface ITransitionCommand
    {
        Action TransitionMovePreviousCommand { get; set; }
        Action TransitionMoveNextCommand { get; set; }

    }
}
