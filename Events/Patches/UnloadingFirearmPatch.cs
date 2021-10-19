﻿// -----------------------------------------------------------------------
// <copyright file="UnloadingFirearmPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;
using HarmonyLib;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.BasicMessages;
using Mirror;
using Mistaken.Events.EventArgs;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(FirearmBasicMessagesHandler), nameof(FirearmBasicMessagesHandler.ServerRequestReceived))]
    internal static class UnloadingFirearmPatch
    {
        public static bool Prefix(NetworkConnection conn, RequestMessage msg)
        {
            if (msg.Request != RequestType.Unload)
                return true;

            ReferenceHub referenceHub;
            if (!ReferenceHub.TryGetHub(conn.identity.gameObject, out referenceHub))
                return false;

            if (msg.Serial != referenceHub.inventory.CurItem.SerialNumber)
                return false;
            Firearm firearm = referenceHub.inventory.CurInstance as Firearm;
            if (firearm == null)
                return false;

            var player = Player.Get(referenceHub);
            var item = (Exiled.API.Features.Items.Firearm)Exiled.API.Features.Items.Item.Get(firearm);
            var ev = new UnloadingFirearmEventArgs(player, item);

            Handlers.CustomEvents.InvokeUnloadingFirearm(ev);

            if (!ev.IsAllowed)
                return false;

            if (firearm.AmmoManagerModule.ServerTryUnload())
                conn.Send(new RequestMessage(msg.Serial, RequestType.Unload), 0);

            return false;
        }
    }
}
