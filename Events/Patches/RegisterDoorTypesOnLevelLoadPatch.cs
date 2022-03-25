// -----------------------------------------------------------------------
// <copyright file="RegisterDoorTypesOnLevelLoadPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Mistaken.Events.Patches
{
    internal static class RegisterDoorTypesOnLevelLoadPatch
    {
        public static void Postfix()
        {
            Handlers.CustomEvents.InvokeGeneratedCache();
        }
    }
}
