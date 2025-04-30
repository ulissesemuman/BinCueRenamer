using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinRenamer
{
    internal class Command
    {
        CommandType type;
        string nameBefore;
        string nameAfter;
        string[] lines;
        bool done = false;

        public CommandType Type { get => type; set => type = value; }
        public string NameBefore { get => nameBefore; set => nameBefore = value; }
        public string NameAfter { get => nameAfter; set => nameAfter = value; }
        //public bool Done { get => done; set => done = value; }

        public string[] Lines
        {
            get => lines;
            set 
            {
                lines = new string[value.Length];

                for (int i = 0; i < value.Length; i++)
                {
                    lines[i] = value[i];
                }
            }
        }

        public enum CommandType
        {
            FileWriteAllLines,
            FileMove,
            DirectoryMove

        }

        public Command()
        { 
        }

        public Command(CommandType type, string nameBefore, string nameAfter, string[] lines)
        {
            this.type = type;
            this.nameBefore = nameBefore;
            this.nameAfter = nameAfter;
            this.lines = lines;
        }

        //public void Run()
        //{
        //    switch (type)
        //    {
        //        case CommandType.FileWriteAllLines:
        //            File.WriteAllLines(nameBefore, lines);
        //            break;
        //        case CommandType.FileMove:
        //            File.Move(NameBefore, NameAfter);
        //            break;
        //        case CommandType.DirectoryMove:
        //            Directory.Move(NameBefore, NameAfter);
        //            break;
        //        default: break;
        //    }
        //}

        public void Undo()
        {
            switch (type) 
            { 
                case CommandType.FileWriteAllLines:
                    File.WriteAllLines(nameBefore, lines);
                    break; 
                case CommandType.FileMove:
                    File.Move(NameAfter, NameBefore);
                    break; 
                case CommandType.DirectoryMove:
                    Directory.Move(NameAfter, NameBefore);
                    break; 
                default: break;
            }
        }
    }
}
