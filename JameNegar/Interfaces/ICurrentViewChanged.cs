using System;
namespace JameNegar.Interfaces
{
    interface ICurrentViewChanged
    {
        Action CurrentViewChanged { get; set; }
    }
}
