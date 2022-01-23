// -----------------------------------------------------------------------
// <copyright file="UncuffingPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1118 // Parameter should not span multiple lines

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Exiled.API.Features;
using HarmonyLib;
using InventorySystem.Disarming;
using Mistaken.API.Extensions;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;
using NorthwoodLib.Pools;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(DisarmingHandlers), nameof(DisarmingHandlers.ServerProcessDisarmMessage))]
    internal static class UncuffingPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Call && (System.Reflection.MethodInfo)x.operand == AccessTools.Method(typeof(DisarmedPlayers), nameof(DisarmedPlayers.IsDisarmed))) + 2;

            Label continueLabel = generator.DefineLabel();
            newInstructions.InsertRange(
                index,
                new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldarg_1),
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(DisarmMessage), nameof(DisarmMessage.PlayerToDisarm))),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), nameof(Player.Get), new Type[] { typeof(ReferenceHub) })),
                    new CodeInstruction(OpCodes.Ldloc_0),
                    new CodeInstruction(OpCodes.Ldc_I4_1),
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(UncuffingEventArgs))[0]),
                    new CodeInstruction(OpCodes.Dup),

                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeUncuffing))),

                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(UncuffingEventArgs), nameof(UncuffingEventArgs.IsAllowed))),
                    new CodeInstruction(OpCodes.Brtrue_S, continueLabel),
                    new CodeInstruction(OpCodes.Ret),

                    new CodeInstruction(OpCodes.Nop).WithLabels(continueLabel),
                });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
