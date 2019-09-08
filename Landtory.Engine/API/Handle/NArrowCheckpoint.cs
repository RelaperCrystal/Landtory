using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;
using NativeFunctionHook;
using Landtory.Engine.API;

namespace Landtory.Engine.API.Handle
{
    
    public class NArrowCheckpoint
    {
        Logger logger = new Logger();
        public Vector3 Position { get; private set; }
        Timer time;
        Timer render;

        bool EventSent;

        NativeFunctionHook.value.RGBColor RGB;

        public delegate void CheckpointTriggeredHandler(object sender, EventArgs e);
        public event CheckpointTriggeredHandler CheckpointTriggered;

        public NArrowCheckpoint(Vector3 positionArg, NativeFunctionHook.value.RGBColor color)
        {
            Position = positionArg;
            RGB = color;
            time = new Timer
            {
                Interval = 100
            };
            render = new Timer
            {
                Interval = 10
            };
            time.Tick += DetectPositionAct;
            render.Tick += RenderArrow;
        }

        private void RenderArrow(object sender, EventArgs e)
        {
            NWorld.DrawColouredArrow(Position, RGB);
        }

        private void DetectPositionAct(object sender, EventArgs e)
        {
            if(!EventSent && Game.LocalPlayer.Character.Position.DistanceTo(Position) <= 5.0f)
            {
                EventSent = true;   
                EventArgs eventArgs = new EventArgs();
                CheckpointTriggered(this, eventArgs);
            }
            if (EventSent && Game.LocalPlayer.Character.Position.DistanceTo(Position) >= 5.0f)
            {
                EventSent = false;
            }
        }

        public void DeleteNow()
        {
            logger.Log("Begins to delete the checkpoint", "Checkpoint", Logger.LogLevel.Info);
            time.Tick -= DetectPositionAct;
            render.Tick -= RenderArrow;

            time = null;
            render = null;

            logger.Log("Deleted an ArrowCheckpoint", "Checkpoint", Logger.LogLevel.Info);
        }
    }
}
