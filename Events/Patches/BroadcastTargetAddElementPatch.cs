// -----------------------------------------------------------------------
// <copyright file="BroadcastTargetAddElementPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Mirror;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(Broadcast), "TargetAddElement")]
    internal static class BroadcastTargetAddElementPatch
    {
        public static readonly Dictionary<string, List<string>> Targets = new Dictionary<string, List<string>>();

        public static bool Prefix(NetworkConnection conn, string data, ushort time, Broadcast.BroadcastFlags flags)
        {
            if (!Targets.ContainsKey(data))
            {
                Targets.Add(data, NorthwoodLib.Pools.ListPool<string>.Shared.Rent());
                Mistaken.API.Diagnostics.Module.CallSafeDelayed(
                    2,
                    () =>
                    {
                        var tmp = data.Split('~');
                        var admin = "SYSTEM";
                        if (tmp.Length > 1 && !data.Contains("'~'"))
                            admin = tmp.Last();
                        var contentWithoutAdmin = tmp.Length == 1 ? data : data.Substring(0, data.Length - (admin.Length + 1));
                        var ev = new BroadcastEventArgs(flags, contentWithoutAdmin, admin, Targets[data].ToArray());
                        CustomEvents.InvokeOnBroadcast(ref ev);

                        NorthwoodLib.Pools.ListPool<string>.Shared.Return(Targets[data]);
                        Targets.Remove(data);
                    },
                    "BroadcastTargetAddElementPatch");
            }

            if (conn?.identity?.gameObject == null)
                return true;
            var user = ReferenceHub.GetHub(conn.identity.gameObject);
            string uid = user?.characterClassManager?.UserId;
            if (uid == null)
                return true;
            Targets[data].Add(uid);
            return true;
        }
    }
}
