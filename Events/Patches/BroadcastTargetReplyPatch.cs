// -----------------------------------------------------------------------
// <copyright file="BroadcastTargetReplyPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs;
using HarmonyLib;
using Mirror;
using Mistaken.Events.Handlers;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(RemoteAdmin.QueryProcessor), "TargetReply")]
    internal static class BroadcastTargetReplyPatch
    {
        public static readonly Dictionary<string, List<string>> Targets = new Dictionary<string, List<string>>();

        public static bool Prefix(NetworkConnection conn, string content)
        {
            if (!content.StartsWith("@"))
                return true;
            content = content.Substring(1);
            if (!Targets.ContainsKey(content))
            {
                Targets.Add(content, NorthwoodLib.Pools.ListPool<string>.Shared.Rent());
                Mistaken.API.Diagnostics.Module.CallSafeDelayed(
                    2,
                    () =>
                    {
                        var tmp = content.Split('~');
                        var admin = "SYSTEM";
                        if (tmp.Length > 1 && !content.Contains("'~'"))
                            admin = tmp.Last();
                        var contentWithoutAdmin = tmp.Length == 1 ? content : content.Substring(0, content.Length - (admin.Length + 1));
                        var ev = new BroadcastEventArgs(Broadcast.BroadcastFlags.AdminChat, contentWithoutAdmin, admin, Targets[content].ToArray());
                        CustomEvents.InvokeOnBroadcast(ref ev);

                        NorthwoodLib.Pools.ListPool<string>.Shared.Return(Targets[content]);
                        Targets.Remove(content);
                    },
                    "BroadcastTargetReplayPatch");
            }

            var userId = ReferenceHub.GetHub(conn?.identity?.gameObject)?.characterClassManager?.UserId;
            if (!string.IsNullOrWhiteSpace(userId))
                Targets[content].Add(userId);
            return true;
        }
    }
}
