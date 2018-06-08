﻿using System.Text;
using Cake.Electron.Net.Contracts;

namespace Cake.Electron.Net
{
    internal class CommandBuilder : ICommandBuilder
    {
        public string SwitchHelper(params string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach (string s in args)
            {
                sb.Append($" --{s}");
            }

            return sb.ToString();
        }
    }
}
