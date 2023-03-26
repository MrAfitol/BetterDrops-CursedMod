namespace BetterDrops_CursedMod.Commands
{
    using System;
    using Features.Extensions;
    using CommandSystem;
    using PlayerRoles;

    public class MtfCommand : ICommand
    {
        public static MtfCommand Instance { get; } = new MtfCommand();

        public string Command { get; } = "mtf";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                response = "You don't have perms to do that!";
                return false;
            }

            Team.FoundationForces.SpawnDrops(Plugin.Instance.Config.MtfDropWave, Plugin.Instance.Config.MtfDropWave.NumberOfDrops);

            response = "Spawned!";
            return true;
        }
    }
}
