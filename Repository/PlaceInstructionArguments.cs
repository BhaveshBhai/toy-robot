using System;
using System.Collections.Generic;
using System.Text;

namespace toy_robot.Repository
{
    public class PlaceInstructionArguments : InstructionArguments
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Facing Facing { get; set; }
    }
}
