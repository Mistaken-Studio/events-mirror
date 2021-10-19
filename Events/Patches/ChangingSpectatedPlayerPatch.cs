// -----------------------------------------------------------------------
// <copyright file="ChangingSpectatedPlayerPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;
using HarmonyLib;
using Mistaken.Events.EventArgs;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(SpectatorManager), nameof(SpectatorManager.CurrentSpectatedPlayer), MethodType.Setter)]
    internal static class ChangingSpectatedPlayerPatch
    {
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        public static void Prefix(SpectatorManager __instance, ReferenceHub value)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            if (__instance.CurrentSpectatedPlayer == value)
                return;

            var spectator = Player.Get(__instance._hub);
            if (spectator == null)
                return;

            var oldPlayer = Player.Get(__instance.CurrentSpectatedPlayer);
            var newPlayer = Player.Get(value);
            var ev = new ChangingSpectatedPlayerEventArgs(spectator, oldPlayer, newPlayer);

            Handlers.CustomEvents.InvokeChangingSpectatedPlayer(ev);
        }
    }
}
