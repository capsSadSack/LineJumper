using System;

public class GameEndArgs : EventArgs
{
    public Difficulty Difficulty { get; set; }
    public int Score { get; set; }
    public string Player { get; set; }
}
