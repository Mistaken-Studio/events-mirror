// -----------------------------------------------------------------------
// <copyright file="PickingItemPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using HarmonyLib;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;
using Searching;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(ItemSearchCompletor), "ValidateAny")]
    internal static class PickingItemPatch
    {
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        public static bool Prefix(ItemSearchCompletor __instance)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            if (__instance?.TargetPickup == null)
                return true;
            if (__instance?.TargetItem == null)
                return true;
            if (__instance?.Hub == null)
                return true;
            if (__instance?.TargetPickup.ItemId == null)
                return true;
            PickItemRequestEventArgs data = new PickItemRequestEventArgs(Exiled.API.Features.Player.Get(__instance.Hub), __instance.TargetPickup);
            CustomEvents.InvokeOnRequestPickItem(ref data);
            if (!data.IsAllowed)
            {
                data.Pickup.InUse = false;
                return false;
            }

            return true;
        }
    }
}
