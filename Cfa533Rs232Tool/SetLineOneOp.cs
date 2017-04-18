﻿using CommandLine;
using Petrsnd.Cfa533Rs232Driver;

namespace Cfa533Rs232Tool
{
    [Verb("setlineone", HelpText = "Set line one contents (deprecated).")]
    internal class SetLineOneOptions
    {
        [Option(HelpText = "Text to display on line one")]
        public string Text { get; set; }
    }
    internal static class SetLineOneOp
    {
        public static int Execute(LcdDevice device, SetLineOneOptions opts)
        {
            device.SetScreenLineOneContents(opts.Text);
            return 0;
        }
    }
}