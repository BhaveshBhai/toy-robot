using System;
using System.Collections.Generic;
using System.Text;
using toy_robot.Repository;

namespace toy_robot.Model
{
    public class RobotDriver
    {
        public RobotDriver(Robot robot)
        {
            this.Robot = robot;
        }

        public Robot Robot { get; set; }

        public string Command(string command)
        {
            string response = "";
            InstructionArguments args = null;
            var instruction =(command.ToLower().Contains("output") == true)? Instruction.Output: GetInstruction(command, ref args);

            switch (instruction)
            {
                case Instruction.Place:
                    var placeArgs = (PlaceInstructionArguments)args;
                    if (!Robot.Place(placeArgs.X, placeArgs.Y, placeArgs.Facing))
                    {
                        command = Robot.LastError;
                    }
                   
                    break;
                case Instruction.Move:
                    if (!Robot.Move())
                    {
                        command = Robot.LastError;
                    }
                   
                    break;
                case Instruction.Left:
                    if (!Robot.Left())
                    {
                        command = Robot.LastError;
                    }
                    
                    break;
                case Instruction.Right:
                    if (!Robot.Right())
                    {
                        command = Robot.LastError;
                    }
                   
                    break;
                case Instruction.Report:
                    command = command + "\n Output : " + Robot.Report();
                    break;
                case Instruction.Output:
                    command = "";
                    break;
                default:
                    command = "Invalid command.";
                    break;
            }
            return command;
        }

        private Instruction GetInstruction(string command, ref InstructionArguments args)
        
        {
            Instruction result;
            string argString = "";

            int argsSeperatorPosition = command.IndexOf(" ");
            if (argsSeperatorPosition > 0)
            {
                argString = command.Substring(argsSeperatorPosition + 1);
                command = command.Substring(0, argsSeperatorPosition);
            }
            command = command.ToUpper();

            if (!Enum.TryParse<Instruction>(command, true, out result))
            {
                result = Instruction.Invalid;
            }
            else
            {
                if (result == Instruction.Place)
                {
                    if (!TryParsePlaceArgs(argString, ref args))
                    {
                        result = Instruction.Invalid;
                    }
                }
            }
            return result;
        }

        private bool TryParsePlaceArgs(string argString, ref InstructionArguments args)
        {
            var argParts = argString.Split(',');
            int x, y;
            Facing facing;

            if (argParts.Length == 3 &&
                TryGetCoordinate(argParts[0], out x) &&
                TryGetCoordinate(argParts[1], out y) &&
                TryGetFacingDirection(argParts[2], out facing))
            {
                args = new PlaceInstructionArguments
                {
                    X = x,
                    Y = y,
                    Facing = facing,
                };
                return true;
            }
            return false;
        }

        private bool TryGetCoordinate(string coordinate, out int coordinateValue)
        {
            return int.TryParse(coordinate, out coordinateValue);
        }

        private bool TryGetFacingDirection(string direction, out Facing facing)
        {
            return Enum.TryParse<Facing>(direction, true, out facing);
        }
    }
}
