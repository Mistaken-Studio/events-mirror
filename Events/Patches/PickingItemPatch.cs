// -----------------------------------------------------------------------
// <copyright file="PickingItemPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1118 // Parameter should not span multiple lines

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem.Items.Pickups;
using InventorySystem.Searching;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;
using NorthwoodLib.Pools;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.ValidateStart))]
    internal static class PickingItemPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            Label returnTrueLabel = generator.DefineLabel();
            int index = newInstructions.Count - 2;
            newInstructions.InsertRange(
                index,
                new CodeInstruction[]
                {
                    /*
                     *  var ev = new PickItemRequestEventArgs(Exiled.API.Features.Player.Get(__instance.Hub), Exiled.API.Features.Items.Pickup.Get(__instance.TargetPickup));
                     *  CustomEvents.InvokeRequestPickItem(ev);
                     *
                     *  if (!ev.IsAllowed)
                     *      return false;
                     *
                     *  return true;
                     */

                    new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]), // [this]
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.Hub))), // [hub]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Exiled.API.Features.Player), nameof(Exiled.API.Features.Player.Get), new System.Type[] { typeof(ReferenceHub) })), // [player]
                    new CodeInstruction(OpCodes.Ldarg_0), // [this, player]
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.TargetPickup))), // [PickupBase, player]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Exiled.API.Features.Items.Pickup), nameof(Exiled.API.Features.Items.Pickup.Get))), // [pickup, player]
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(PickItemRequestEventArgs))[0]), // [EventArgs]
                    new CodeInstruction(OpCodes.Dup), // [EventArgs, EventArgs]

                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeRequestPickItem))), // [EventArgs]

                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(PickItemRequestEventArgs), nameof(PickItemRequestEventArgs.IsAllowed))), // [bool]
                    new CodeInstruction(OpCodes.Brtrue_S, returnTrueLabel), // []

                    new CodeInstruction(OpCodes.Ldc_I4_0), // [float]
                    new CodeInstruction(OpCodes.Ret), // []
                });

            newInstructions[newInstructions.Count - 2].WithLabels(returnTrueLabel);

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
