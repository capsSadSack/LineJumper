using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Assets.Scripts
{
    public class MessageHelper
    {
        public static async Task CountDown(Text text)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
            text.text = "Ready!";
            await Task.Delay(500);
            text.text = "Steady!";
            await Task.Delay(500);
            text.text = "Go!";
            await Task.Delay(500);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        public static void GameOver(Text text)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
            text.text = "Game over";
        }
    }
}
