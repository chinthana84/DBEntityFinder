using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System;
using Task = System.Threading.Tasks.Task;

namespace VSIXProject1
{
    [Command("<insert guid from .vsct file>", 0x0100)]
    internal sealed class Command1 : BaseCommand<Command1>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            VS.Notifications.ShowMessage("Command1", "Button clicked");

            return Task.CompletedTask;
        }
    }
}
