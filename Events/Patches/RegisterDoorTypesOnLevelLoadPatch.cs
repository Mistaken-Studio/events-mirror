// -----------------------------------------------------------------------
// <copyright file="RegisterDoorTypesOnLevelLoadPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Mistaken.Events.Patches
{
    internal static class RegisterDoorTypesOnLevelLoadPatch
    {
        public static void Postfix()
        {
            MEC.Timing.RunCoroutine(InvokeDelay());
        }

        private static IEnumerator<float> InvokeDelay()
        {
            yield return MEC.Timing.WaitForSeconds(0.3f);
            Handlers.CustomEvents.InvokeGeneratedCache();
        }
    }
}
