// -----------------------------------------------------------------------
// <copyright file="SCP914UpgradingPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1118 // Parameter should not span multiple lines

using System.Collections.Generic;
using System.Reflection.Emit;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using HarmonyLib;
using InventorySystem.Items.Firearms.Attachments;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;
using NorthwoodLib.Pools;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(Scp914.Scp914Upgrader), nameof(Scp914.Scp914Upgrader.Upgrade))]
    internal static class SCP914UpgradingPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int startIndex = 0;
            newInstructions.InsertRange(
                startIndex,
                new CodeInstruction[]
                {
                    /*
                     *  var ev = new SCP914UpgradingEventArgs();
                     *  Handlers.CustomEvents.InvokeSCP914Upgrading(ev);
                     */
                    // ev = new SCP914UpgradingEventArgs()
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(SCP914UpgradingEventArgs))[0]),  // [EventArgs]

                    // CustomEvents.InvokeSCP914Upgrading(ev)
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeSCP914Upgrading))),  // []
                });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
