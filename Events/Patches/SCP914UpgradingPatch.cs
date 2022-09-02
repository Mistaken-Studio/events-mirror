// -----------------------------------------------------------------------
// <copyright file="SCP914UpgradingPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1118 // Parameter should not span multiple lines

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
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

            var continueLabel = generator.DefineLabel();

            int startIndex = 0;
            newInstructions[0].WithLabels(continueLabel);
            newInstructions.InsertRange(
                startIndex,
                new CodeInstruction[]
                {
                    /*
                     *  var ev = new SCP914UpgradingEventArgs(Scp914KnobSetting, bool);
                     *  Handlers.CustomEvents.InvokeSCP914Upgrading(ev);
                     */
                    // ev = new SCP914UpgradingEventArgs(Scp914KnobSetting, bool)
                    new CodeInstruction(OpCodes.Ldarg_3),  // [Scp914KnobSetting]
                    new CodeInstruction(OpCodes.Ldc_I4_1),  // [bool, Scp914KnobSetting]
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(SCP914UpgradingEventArgs))[0]),  // [EventArgs]
                    new CodeInstruction(OpCodes.Dup),  // [EventArgs, EventArgs]
                    new CodeInstruction(OpCodes.Dup),  // [EventArgs, EventArgs, EventArgs]

                    // CustomEvents.InvokeSCP914Upgrading(ev)
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeSCP914Upgrading))),  // [EventArgs, EventArgs]

                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(SCP914UpgradingEventArgs), nameof(SCP914UpgradingEventArgs.KnobSetting))),  // [bool, EventArgs]
                    new CodeInstruction(OpCodes.Starg_S, 3),  // [EventArgs]

                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(SCP914UpgradingEventArgs), nameof(SCP914UpgradingEventArgs.IsAllowed))),  // [bool]
                    new CodeInstruction(OpCodes.Brtrue_S, continueLabel),  // []

                    new CodeInstruction(OpCodes.Ret),  // []
                });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
