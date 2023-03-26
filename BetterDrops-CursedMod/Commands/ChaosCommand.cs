﻿namespace BetterDrops_CursedMod.Commands
{
    using System;
    using Features.Extensions;
    using CommandSystem;
    using PlayerRoles;

    public class ChaosCommand : ICommand
    {
        public static ChaosCommand Instance { get; } = new ChaosCommand();

        public string Command { get; } = "chaos";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                response = "You don't have perms to do that!";
                return false;
            }

            Team.ChaosInsurgency.SpawnDrops(Plugin.Instance.Config.ChaosDropWave, Plugin.Instance.Config.ChaosDropWave.NumberOfDrops);

            response = "Spawned!";
            return true;
        }
    }
}
