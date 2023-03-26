namespace BetterDrops_CursedMod.Features
{
    using MEC;
    using Respawning;
    using System.Collections.Generic;
    using Configs;
    using Features.Extensions;
    using Random = UnityEngine.Random;
    using PlayerRoles;
    using CursedMod.Events.Arguments.Respawning;
    using CursedMod.Features.Wrappers.AdminToys;
    using UnityEngine;

    public class EventHandlers
    {
        private readonly HashSet<CoroutineHandle> _coroutines = new HashSet<CoroutineHandle>();

        public void OnRestartingRound()
        {
            foreach (CoroutineHandle coroutine in _coroutines)
                Timing.KillCoroutines(coroutine);

            _coroutines.Clear();
        }

        public void OnStartingRound()
        {
            if (Plugin.Instance.Config.RandomDrops?.WaveSettings.IsEnabled == true && _coroutines.Count == 0)
                _coroutines.Add(Timing.RunCoroutine(RandomDropCoroutine(Plugin.Instance.Config.RandomDrops)));
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            Team team = (ev.TeamSpawning == SpawnableTeamType.NineTailedFox ? Team.FoundationForces : Team.ChaosInsurgency);

            if (team == Team.FoundationForces && !Plugin.Instance.Config.MtfDropWave.IsEnabled || team == Team.ChaosInsurgency && !Plugin.Instance.Config.ChaosDropWave.IsEnabled)
                return;

            DropConfig cfg = team == Team.FoundationForces ? Plugin.Instance.Config.MtfDropWave : Plugin.Instance.Config.ChaosDropWave;
            team.SpawnDrops(cfg, cfg.NumberOfDrops);
        }

        private static IEnumerator<float> RandomDropCoroutine(RandomDropConfigs configs)
        {
            yield return Timing.WaitForSeconds(configs.FirstRandomDropOffset);

            for (; ; )
            {
                Team team = Random.Range(0, 2) == 1 ? Team.FoundationForces : Team.ChaosInsurgency;

                team.SpawnDrops(configs.WaveSettings, configs.WaveSettings.NumberOfDrops);
                yield return Timing.WaitForSeconds(Random.Range(configs.MinRandomDropsInterval, configs.MaxRandomDropsInterval));
            }
        }
    }
}
