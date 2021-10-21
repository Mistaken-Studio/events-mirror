// -----------------------------------------------------------------------
// <copyright file="ChangingSpectatedPlayerPatch.cs" company="Mistaken">
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

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(SpectatorManager), nameof(SpectatorManager.CurrentSpectatedPlayer), MethodType.Setter)]
    internal static class ChangingSpectatedPlayerPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            Label continueLabel = generator.DefineLabel();

            // Label continueAndPopLabel = generator.DefineLabel();
            LocalBuilder player = generator.DeclareLocal(typeof(Player));

            int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
            newInstructions.InsertRange(
                index,
                new CodeInstruction[]
                {
                    /*
                     *  var spectator = Player.Get(__instance._hub);
                     *  if (spectator != null)
                     *  {
                     *      var ev = new ChangingSpectatedPlayerEventArgs(spectator, Player.Get(__instance.CurrentSpectatedPlayer), Player.Get(value));
                     *
                     *      Handlers.CustomEvents.InvokeChangingSpectatedPlayer(ev);
                     *  }
                     */

                    new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]), // [this]
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SpectatorManager), nameof(SpectatorManager._hub))), // [ReferenceHub]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), nameof(Player.Get), new System.Type[] { typeof(ReferenceHub) })), // [Player]
                    new CodeInstruction(OpCodes.Dup), // [Player, Player]

                    new CodeInstruction(OpCodes.Stloc, player),
                    new CodeInstruction(OpCodes.Brfalse_S, continueLabel), // [Player]
                    new CodeInstruction(OpCodes.Ldloc, player),

                    // new CodeInstruction(OpCodes.Brfalse_S, continueAndPopLabel), // [Player]
                    new CodeInstruction(OpCodes.Ldarg_0), // [this, Player]
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(SpectatorManager), nameof(SpectatorManager._currentSpectatedPlayer))), // [ReferenceHub(OldSpectated), Player(Spectator)]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), nameof(Player.Get), new System.Type[] { typeof(ReferenceHub) })), // [Player(OldSpectated), Player(Spectator)]
                    new CodeInstruction(OpCodes.Ldarg_1), // [ReferenceHub, Player(OldSpectated), Player(Spectator)]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), nameof(Player.Get), new System.Type[] { typeof(ReferenceHub) })), // [Player(NewSpectated), Player(OldSpectated), Player(Spectator)]
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(ChangingSpectatedPlayerEventArgs))[0]),  // [EventArgs]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeChangingSpectatedPlayer))),  // []

                    // new CodeInstruction(OpCodes.Pop).WithLabels(continueAndPopLabel),
                    new CodeInstruction(OpCodes.Nop).WithLabels(continueLabel),
                });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
