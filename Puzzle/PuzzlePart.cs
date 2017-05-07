using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;

namespace Puzzle
{
    public class PuzzlePart
    {
        public float LastX { get; set; }
        public float LastY { get; set; }
        public Drawable Picture { get; set; }
        public bool IsActived { get; set; }

        public PuzzlePart()
        {
            IsActived = false;
        }
    }
}