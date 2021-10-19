// -----------------------------------------------------------------------
// <copyright file="BroadcastRpcAddElementPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using HarmonyLib;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(Broadcast), "RpcAddElement")]
    internal static class BroadcastRpcAddElementPatch
    {
        public static bool Prefix(string data, ushort time, Broadcast.BroadcastFlags flags)
        {
            var tmp = data.Split('~');
            var admin = "SYSTEM";
            if (tmp.Length > 1 && !data.Contains("'~'"))
                admin = tmp.Last();
            var contentWithoutAdmin = tmp.Length == 1 ? data : data.Substring(0, data.Length - (admin.Length + 1));
            var ev = new BroadcastEventArgs(flags, contentWithoutAdmin, admin, Exiled.API.Features.Player.UserIdsCache.Keys.ToArray());
            CustomEvents.InvokeBroadcast(ref ev);
            return true;
        }
    }
}
