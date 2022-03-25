// -----------------------------------------------------------------------
// <copyright file="GenerateCachePatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Mistaken.Events.Patches
{
    internal static class GenerateCachePatch
    {
        public static void Postfix()
        {
            Handlers.CustomEvents.InvokeGeneratedCache();
        }
    }
}
