// -----------------------------------------------------------------------
// <copyright file="LoadedPluginsPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Mistaken.Events.Handlers;
using NorthwoodLib.Pools;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(ServerConsole), nameof(ServerConsole.CheckRoot))]
    internal static class LoadedPluginsPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            newInstructions.InsertRange(
                0,
                new CodeInstruction[]
                {
                    /*
                     *  Handlers.CustomEvents.LoadedPlugins();
                     */
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeLoadedPlugins))),
                });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
