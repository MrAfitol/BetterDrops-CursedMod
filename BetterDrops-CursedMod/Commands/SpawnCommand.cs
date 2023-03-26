namespace BetterDrops_CursedMod.Commands
{
    using System;
    using Features.Extensions;
    using CommandSystem;
    using UnityEngine;
    using Random = UnityEngine.Random;
    using CursedMod.Features.Wrappers.Player;

    public class SpawnCommand : ICommand
    {
        public static SpawnCommand Instance { get; } = new SpawnCommand();

        public string Command { get; } = "spawn";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Spawn a drop";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                response = "You don't have perms to do that!";
                return false;
            }

            DropExtensions.SpawnDrop(CursedPlayer.Get(((CommandSender)sender).SenderId).Position + Vector3.up * 10f, Random.ColorHSV(), new[] { ItemType.Coin }, false, false);

            response = "Spawned!";
            return true;
        }
    }
}
