// -----------------------------------------------------------------------
// <copyright file="SendingCommandPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable SA1118 // Parameter should not span multiple lines

using System.Collections.Generic;
using System.Reflection.Emit;
using Exiled.API.Features;
using HarmonyLib;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;
using NorthwoodLib.Pools;
using RemoteAdmin;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.ProcessQuery))]
    internal static class SendingCommandPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            Label continueLabel = generator.DefineLabel();
            newInstructions.InsertRange(
                0,
                new CodeInstruction[]
                {
                    /*
                     *  var ev = new SendingCommandEventArgs(Extensions.GetPlayer(sender), q);
                     *  Handlers.CustomEvents.InvokeSendingCommand(ev);
                     *
                     *  if (!ev.IsAllowed)
                     *      return;
                     */
                    new CodeInstruction(OpCodes.Ldarg_1),
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), nameof(Player.Get), new System.Type[] { typeof(CommandSender) })),
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldc_I4_1),
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(SendingCommandEventArgs))[0]),
                    new CodeInstruction(OpCodes.Dup),

                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeSendingCommand))),

                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(SendingCommandEventArgs), nameof(SendingCommandEventArgs.IsAllowed))),
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
