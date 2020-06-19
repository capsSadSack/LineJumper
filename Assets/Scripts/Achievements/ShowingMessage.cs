using System;
using UnityEngine;

public class ShowingMessage
{
    public GameObject Message { get; set; }
    public DateTime MessageAppearTime_Utc { get; set; }
    public bool IsMessageShowing { get; set; } = false;
}
