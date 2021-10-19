// -----------------------------------------------------------------------
// <copyright file="WeaponPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1118 // Parameter should not span multiple lines

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Exiled.API.Features;
using HarmonyLib;
using InventorySystem.Items.Firearms.BasicMessages;
using InventorySystem.Items.Firearms.Modules;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;
using NorthwoodLib.Pools;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(FirearmBasicMessagesHandler), nameof(FirearmBasicMessagesHandler.ServerRequestReceived))]
    internal static class WeaponPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            Label endLabel = generator.DefineLabel();
            newInstructions[newInstructions.Count - 1].WithLabels(endLabel);

            // Unloading
            int unloadingIndex = newInstructions.FindIndex(x =>
            {
                if (x.opcode != OpCodes.Callvirt)
                    return false;
                if (!(x.operand is MethodInfo))
                    return false;
                var operand = (MethodInfo)x.operand;
                return operand.DeclaringType == typeof(IAmmoManagerModule) && operand.Name == nameof(IAmmoManagerModule.ServerTryUnload);
            }) - 2;
            newInstructions.InsertRange(
                unloadingIndex,
                new CodeInstruction[]
                {
                    /*
                     * var ev = new UnloadingWeaponEventArgs(Player.Get(referenceHub), true);
                     *
                     * Handlers.CustomEvents.InvokeUnloadingWeapon(ev);
                     *
                     * if (!ev.IsAllowed)
                     *     return;
                     */

                    new CodeInstruction(OpCodes.Ldloc_0, null).MoveLabelsFrom(newInstructions[unloadingIndex]),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), "Get", new Type[] { typeof(ReferenceHub) })),
                    new CodeInstruction(OpCodes.Ldc_I4_1),
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(UnloadingWeaponEventArgs))[0]),
                    new CodeInstruction(OpCodes.Dup),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeUnloadingWeapon))),
                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(UnloadingWeaponEventArgs), nameof(UnloadingWeaponEventArgs.IsAllowed))),
                    new CodeInstruction(OpCodes.Brfalse, endLabel),
                });

            // Aiming In
            int aimingInIndex = newInstructions.FindIndex(x =>
            {
                if (x.opcode != OpCodes.Callvirt)
                    return false;
                if (!(x.operand is MethodInfo))
                    return false;
                var operand = (MethodInfo)x.operand;
                return operand.DeclaringType == typeof(IAdsModule) && operand.ReturnType == typeof(void);
            }) - 3;
            newInstructions.InsertRange(
                aimingInIndex,
                new CodeInstruction[]
                {
                    /*
                     *  var ev = new AimingEventArgs(Player.Get(referenceHub), true);
                     *
                     *  Handlers.CustomEvents.InvokeAiming(ev);
                     */

                    new CodeInstruction(OpCodes.Ldloc_0, null).MoveLabelsFrom(newInstructions[aimingInIndex]),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), "Get", new Type[] { typeof(ReferenceHub) })),
                    new CodeInstruction(OpCodes.Ldc_I4_1),
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(AimingEventArgs))[0]),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeAiming))),
                });

            // Aiming Out
            int aimingOutIndex = aimingInIndex + 5 /*Number of instructions in AimIn*/ + 6 /*Offset between AimIn and AimOut*/;
            newInstructions.InsertRange(
                aimingOutIndex,
                new CodeInstruction[]
                {
                    /*
                     *  var ev = new AimingEventArgs(Player.Get(referenceHub), false);
                     *
                     *  Handlers.CustomEvents.InvokeAiming(ev);
                     */

                    new CodeInstruction(OpCodes.Ldloc_0, null).MoveLabelsFrom(newInstructions[aimingOutIndex]),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), "Get", new Type[] { typeof(ReferenceHub) })),
                    new CodeInstruction(OpCodes.Ldc_I4_0),
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(AimingEventArgs))[0]),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeAiming))),
                });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
