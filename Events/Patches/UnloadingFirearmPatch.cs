// -----------------------------------------------------------------------
// <copyright file="UnloadingFirearmPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Exiled.API.Features;
using HarmonyLib;
using InventorySystem;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.BasicMessages;
using Mirror;
using Mistaken.Events.EventArgs;
using UnityEngine;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(FirearmBasicMessagesHandler), nameof(FirearmBasicMessagesHandler.ServerRequestReceived))]
    internal static class UnloadingFirearmPatch
    {
        public static bool Prefix(NetworkConnection conn, RequestMessage msg)
        {
            if (msg.Request != RequestType.Unload)
                return true;

            global::ReferenceHub referenceHub;
            if (!global::ReferenceHub.TryGetHub(conn.identity.gameObject, out referenceHub))
                return false;

            if (msg.Serial != referenceHub.inventory.CurItem.SerialNumber)
                return false;
            Firearm firearm = referenceHub.inventory.CurInstance as Firearm;
            if (firearm == null)
                return false;

            var player = Player.Get(referenceHub);
            var item = (Exiled.API.Features.Items.Firearm)Exiled.API.Features.Items.Item.Get(firearm);
            var ev = new UnloadingFirearmEventArgs(player, item);

            if (!ev.IsAllowed)
                return false;

            if (firearm.AmmoManagerModule.ServerTryUnload())
                conn.Send<RequestMessage>(new RequestMessage(msg.Serial, RequestType.Unload), 0);

            return false;
        }
    }
}
