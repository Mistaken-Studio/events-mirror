// -----------------------------------------------------------------------
// <copyright file="SendingCommandPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using HarmonyLib;
using Mistaken.API.Extensions;
using Mistaken.Events.EventArgs;
using RemoteAdmin;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.ProcessQuery))]
    internal static class SendingCommandPatch
    {
        public static bool Prefix(string q, CommandSender sender)
        {
            var player = sender.GetPlayer();
            if (player == null)
                return true;

            var ev = new SendingCommandEventArgs(player, q);
            Handlers.CustomEvents.InvokeSendingCommand(ev);

            if (!ev.IsAllowed)
                return false;

            return true;
        }
    }
}
