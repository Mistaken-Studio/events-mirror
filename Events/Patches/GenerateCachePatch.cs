// -----------------------------------------------------------------------
// <copyright file="GenerateCachePatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Mistaken.Events.Patches
{
    internal static class GenerateCachePatch
    {
        public static void Postfix()
        {
            MEC.Timing.RunCoroutine(InvokeDelay());
        }

        private static IEnumerator<float> InvokeDelay()
        {
            yield return MEC.Timing.WaitForOneFrame;
            Handlers.CustomEvents.InvokeGeneratedCache();
        }
    }
}
