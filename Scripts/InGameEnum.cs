using System;

public static class InGameEnum
{
    //States of Player
    [Flags]
    public enum State
    {
        None = 0,
        WaitStart = 1,
        Run = 2,
        Hit = 3,

    }

}
