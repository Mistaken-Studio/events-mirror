// -----------------------------------------------------------------------
// <copyright file="SCP079RecontainPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Exiled.API.Features;
using HarmonyLib;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Respawning;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(Recontainer079), "BeginContainment")]
    internal static class SCP079RecontainPatch
    {
        public static bool Recontained { get; private set; } = false;

        public static bool Recontaining { get; private set; } = false;

        public static int SecondsLeft { get; private set; }

        public static bool Waiting { get; set; }

        public static bool Prefix(bool forced)
        {
            Timing.RunCoroutine(SCP079RecontainPatch.Recontain(forced).CancelWith(Recontainer079.singleton.gameObject), Segment.FixedUpdate);
            return false;
        }

        public static void Postfix(bool forced)
            => Recontaining = true;

        public static void Restart()
        {
            Waiting = false;
            SecondsLeft = -1;
            Recontained = false;
            Recontaining = false;
        }

        public static IEnumerator<float> Recontain(bool forced)
        {
            SecondsLeft = 62;
            PlayerStats ps = Server.Host.ReferenceHub.playerStats;
            Waiting = true;
            while (Cassie.IsSpeaking || AlphaWarheadController.Host.inProgress)
                yield return float.NegativeInfinity;
            Waiting = false;
            if (!forced)
            {
                RespawnEffectsController.PlayCassieAnnouncement(
                    string.Concat(new object[]
                    {
                        "JAM_",
                        UnityEngine.Random.Range(0, 70).ToString("000"),
                        "_",
                        UnityEngine.Random.Range(2, 5),
                        " SCP079RECON5",
                    }),
                    false,
                    true);
            }

            for (int i = 0; i < 55; i++)
            {
                yield return Timing.WaitForSeconds(1f);
                SecondsLeft--;
            }

            Waiting = true;
            while (Cassie.IsSpeaking || AlphaWarheadController.Host.inProgress)
                yield return float.NegativeInfinity;

            Waiting = false;
            RespawnEffectsController.PlayCassieAnnouncement(
                string.Concat(new object[]
                {
                    "JAM_",
                    UnityEngine.Random.Range(0, 70).ToString("000"),
                    "_",
                    UnityEngine.Random.Range(1, 4),
                    " SCP079RECON6",
                }),
                true,
                true);
            RespawnEffectsController.PlayCassieAnnouncement((Scp079PlayerScript.instances.Count > 0) ? "SCP 0 7 9 SUCCESSFULLY TERMINATED USING GENERATOR RECONTAINMENT SEQUENCE" : "FACILITY IS BACK IN OPERATIONAL MODE", false, true);
            for (int i = 0; i < 7; i++)
            {
                yield return Timing.WaitForSeconds(1f);
                SecondsLeft--;
            }

            Generator079.Generators[0].ServerOvercharge(10f, true);
            HashSet<DoorVariant> lockedDoors = new HashSet<DoorVariant>();
            foreach (DoorVariant doorVariant in UnityEngine.Object.FindObjectsOfType<DoorVariant>())
            {
                Scp079Interactable scp079Interactable;
                if (doorVariant is BasicDoor && doorVariant.TryGetComponent<Scp079Interactable>(out scp079Interactable) && scp079Interactable.currentZonesAndRooms[0].currentZone == "HeavyRooms")
                {
                    lockedDoors.Add(doorVariant);
                    doorVariant.NetworkTargetState = false;
                    doorVariant.ServerChangeLock(DoorLockReason.NoPower, true);
                }
            }

            Recontainer079.isLocked = true;
            foreach (Scp079PlayerScript scp079PlayerScript in Scp079PlayerScript.instances)
            {
                ps.HurtPlayer(new PlayerStats.HitInfo(1000001f, "WORLD", DamageTypes.Recontainment, 0), scp079PlayerScript.gameObject, true, true);
                Respawning.RespawnTickets.Singleton.GrantTickets(SpawnableTeamType.NineTailedFox, 4);
            }

            Recontained = true;
            for (int i = 0; i < 10; i++)
                yield return Timing.WaitForSeconds(1f);

            foreach (DoorVariant doorVariant2 in lockedDoors)
                doorVariant2.ServerChangeLock(DoorLockReason.NoPower, false);

            Recontainer079.isLocked = false;

            SecondsLeft = -1;
            Waiting = false;
            yield break;
        }
    }
}
