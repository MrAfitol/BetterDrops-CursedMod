namespace BetterDrops_CursedMod
{
    using CursedMod.Loader.Modules.Enums;
    using CursedMod.Loader;
    using CursedMod.Loader.Modules;
    using BetterDrops_CursedMod.Features;
    using CursedMod.Events.Handlers.Round;
    using CursedMod.Events.Handlers.Respawning;

    public class Plugin : CursedModule
    {
        public override string ModuleName => "BetterDrops";
        public override string ModuleAuthor => "Jesus-QC, update by MrAfitol";
        public override string ModuleVersion => "1.0.0";
        public override byte LoadPriority => (byte)ModulePriority.Medium;
        public override string CursedModVersion => CursedModInformation.Version;

        public static Plugin Instance { get; private set; }

        public Config Config;

        public EventHandlers EventHandlers;

        public override void OnLoaded()
        {
            Instance = this;
            Config = GetConfig<Config>("config");
            EventHandlers = new EventHandlers();

            RoundEventsHandler.RestartingRound += EventHandlers.OnRestartingRound;
            RoundEventsHandler.RoundStarted += EventHandlers.OnStartingRound;
            RespawningEventsHandler.RespawningTeam += EventHandlers.OnRespawningTeam;

            base.OnLoaded();
        }

        public override void OnUnloaded()
        {
            RoundEventsHandler.RestartingRound -= EventHandlers.OnRestartingRound;
            RoundEventsHandler.RoundStarted -= EventHandlers.OnStartingRound;
            RespawningEventsHandler.RespawningTeam -= EventHandlers.OnRespawningTeam;

            EventHandlers = null;
            Instance = null;

            base.OnUnloaded();
        }
    }
}