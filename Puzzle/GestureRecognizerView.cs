namespace Puzzle
{
    using System;
    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Util;
    using Android.Views;
    using System.Collections.Generic;

    /// <summary>
    ///   This class will show how to respond to touch events using a custom subclass
    ///   of View.
    /// </summary>
    public class GestureRecognizerView : View
    {
        private static readonly int ZERO = 0;
        private float _lastTouchX;
        private float _lastTouchY;
        private float _posX;
        private float _posY;
        List<PuzzlePart> listPuzzle = new List<PuzzlePart>();

        public GestureRecognizerView(Context context)
            : base(context, null, 0)
        {
            PuzzlePart part1 = new PuzzlePart();
            part1.Picture = context.Resources.GetDrawable(Resource.Drawable.part1a);
            part1.Picture.SetBounds(0, 0, part1.Picture.IntrinsicWidth, part1.Picture.IntrinsicHeight);
            listPuzzle.Add(part1);
        }

        private IOnTouchListener OnTouchEvent(View v, MotionEvent ev)
        {
            throw new NotImplementedException();
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            int pointerIndex;
            MotionEventActions action = ev.Action & MotionEventActions.Mask;
            System.Diagnostics.Debug.Print(action.ToString());
            switch (action)
            {
                case MotionEventActions.Down:
                    _lastTouchX = ev.GetX();
                    _lastTouchY = ev.GetY();
                    PuzzlePart containPart = listPuzzle.Find(p => p.Picture.Bounds.Contains((int)ev.GetX(), (int)ev.GetY()));
                    if(containPart != null)
                    {
                        containPart.IsActived = true;
                    }
                    break;

                case MotionEventActions.Move:
                    try
                    {
                        pointerIndex = ev.FindPointerIndex(ZERO);
                        int x = (int)ev.GetX(pointerIndex);
                        int y = (int)ev.GetY(pointerIndex);
                        PuzzlePart activedPart = listPuzzle.Find(p => p.IsActived == true);
                        if(activedPart != null)
                        {
                            activedPart.Picture.SetBounds(  x,
                                                            y,
                                                            x + activedPart.Picture.IntrinsicWidth,
                                                            y + activedPart.Picture.IntrinsicHeight);
                            Invalidate();
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                        
                    
                    break;

                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    //release all part
                    foreach(PuzzlePart part in listPuzzle)
                    {
                        part.IsActived = false;
                    }
                    break;

                case MotionEventActions.PointerUp:
                    pointerIndex = (int)(ev.Action & MotionEventActions.PointerIndexMask) >> (int)MotionEventActions.PointerIndexShift;
                    if(pointerIndex == ZERO)
                    {
                        //release all part
                        foreach (PuzzlePart part in listPuzzle)
                        {
                            part.IsActived = false;
                        }
                    }

                    break;
            }
            return true;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.Save();
            //canvas.Translate(_posX, _posY);
            //_icon.Draw(canvas);
            foreach(PuzzlePart part in listPuzzle)
            {
                part.Picture.Draw(canvas);
            }
            canvas.Restore();
        }
    }
}
