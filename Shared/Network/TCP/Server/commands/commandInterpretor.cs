using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Usefull_Tools.Network.TCP.server.commands
{
    public static class commandInterpretor
    {
        static List<clientCommand> commands = new List<clientCommand>();

        static clientCommand currentCommand;
        static List<string> args;

        /// <summary>
        /// This command try to create a command from the stream. Will return true if the command is created
        /// </summary>
        /// <param name="text"></param>
        /// <returns name="bool"></returns>
        public static bool interpreteCommand(string text)
        {
            bool stringIsCommand = isStartCommand(text);
            bool stringIsEndCommand = isEndCommand(text);

            System.Diagnostics.Debug.WriteLine("New commd : " + text);
            System.Diagnostics.Debug.WriteLine("Start command : " + stringIsCommand + Environment.NewLine + "End command : " + stringIsEndCommand);
            System.Diagnostics.Debug.WriteLine("");

            if (stringIsCommand)
            {
                currentCommand = new clientCommand();
                args = new List<string>();
                currentCommand.commandName = removeArro(text);
            }
            else if (stringIsEndCommand)
            {
                currentCommand.arguments = JsonConvert.SerializeObject(args);
                commands.Add(currentCommand);

                currentCommand = null;

                return true;
            }
            else if (currentCommand != null)
            {
                args.Add(text);
            }

            return false;
        }

        static bool isStartCommand(string command)
        {
            if (command != null && command != "")
            {
                if (command[0] == '@')
                {
                    if (!command.Contains(Environment.NewLine))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static bool isEndCommand(string endCommand)
        {
            if (endCommand.Contains("@"))
            {
                int aPos = endCommand.LastIndexOf("@");
                if (aPos == (endCommand.Length - 1))
                {
                    endCommand = endCommand.Remove(endCommand.Length - 1);
                    if (endCommand == currentCommand.commandName)
                    {
                        return true;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Error, commands not matching");
                    }
                }
            }
            return false;
        }

        static string removeArro(string text)
        {
            if (text[0] == '@')
            {
                text = text.Remove(0, 1);
            }
            return text;
        }

        public static clientCommand getLastCommand()
        {
            return getCommand(0);
        }
        /// <summary>
        /// This command return the last command (in reverse order)
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static clientCommand getCommand(int pos)
        {
            clientCommand[] aCommands = commands.ToArray();

            return aCommands[aCommands.Length - 1 - pos];
        }
    }
}
