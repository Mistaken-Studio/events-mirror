// -----------------------------------------------------------------------
// <copyright file="ChangingAttachmentsPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
/*
#pragma warning disable SA1118 // Parameter should not span multiple lines

using System.Collections.Generic;
using System.Reflection.Emit;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using HarmonyLib;
using InventorySystem.Items.Firearms.Attachments;
using Mistaken.Events.EventArgs;
using Mistaken.Events.Handlers;
using NorthwoodLib.Pools;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(AttachmentsServerHandler), nameof(AttachmentsServerHandler.ServerReceiveChangeRequest))]
    internal static class ChangingAttachmentsPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            Label continueLabel = generator.DefineLabel();

            LocalBuilder evField = generator.DeclareLocal(typeof(ChangingAttachmentsEventArgs));

            int startIndex = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Brfalse) + 1;
            newInstructions.RemoveAt(startIndex); // ldloc.1
            newInstructions.RemoveAt(startIndex); // ldarg.1
            newInstructions.RemoveAt(startIndex); // ldfld     uint32 InventorySystem.Items.Firearms.Attachments.AttachmentsChangeRequest::AttachmentsCode

            newInstructions.InsertRange(
                startIndex,
                new CodeInstruction[]
                {
                    // tu były te gwiazdki obok tego kodu ale zostały usunięte na potrzeby wykomentowania tego kodu.
                    // var ev = new ChangingAttachmentsEventArgs(Player.Get(ReferenceHub), (Firearm)Item.Get(firearm), msg.AttachmentsCode, true);
                    // Handlers.CustomEvents.InvokeChangingAttachments(ev);
                    //
                    // if (!ev.IsAllowed)
                    //    return false;
                    // firearm.ApplyAttachmentsCode(ev.NewAttachmentsCode, true);
                    // ...
                    // firearm.Status = new FirearmStatus((byte)Mathf.Min((int)firearm.Status.Ammo, (int)firearm.AmmoManagerModule.MaxAmmo), firearm.Status.Flags, ev.NewAttachmentsCode);

                    // Player.Get(ReferenceHub)
                    new CodeInstruction(OpCodes.Ldloc_0), // [ReferenceHub]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Player), nameof(Player.Get), new System.Type[] { typeof(ReferenceHub) })), // [Player]

                    // (Exiled.API.Features.Items.Firearm)Exiled.API.Features.Items.Item.Get(firearm)
                    new CodeInstruction(OpCodes.Ldloc_1), // [BaseFirearm, Player]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Item), nameof(Item.Get))), // [Item, Player]
                    new CodeInstruction(OpCodes.Castclass, typeof(Exiled.API.Features.Items.Firearm)), // [Firearm, Player]

                    // ev = new ChangingAttachmentsEventArgs(player, item, AttachmentsChangeRequest.AttachmentsCode, true)
                    new CodeInstruction(OpCodes.Ldarg_1),  // [AttachmentRequestChangeRequest, Firearm, Player]
                    new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(AttachmentsChangeRequest), nameof(AttachmentsChangeRequest.AttachmentsCode))),  // [int, Firearm, Player]
                    new CodeInstruction(OpCodes.Ldc_I4_1), // [bool, int, Firearm, Player]
                    new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(typeof(ChangingAttachmentsEventArgs))[0]),  // [EventArgs]
                    new CodeInstruction(OpCodes.Stloc, evField), // []

                    // CustomEvents.InvokeChangingAttachments(ev)
                    new CodeInstruction(OpCodes.Ldloc, evField), // [EventArgs]
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CustomEvents), nameof(CustomEvents.InvokeChangingAttachments))),  // []

                    // if(!ev.IsAllowed) return
                    new CodeInstruction(OpCodes.Ldloc, evField), // [EventArgs]
                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ChangingAttachmentsEventArgs), nameof(ChangingAttachmentsEventArgs.IsAllowed))),  // [bool]
                    new CodeInstruction(OpCodes.Brtrue_S, continueLabel),  // []
                    new CodeInstruction(OpCodes.Ret), // END

                    // ev.NewAttachmentsCode
                    new CodeInstruction(OpCodes.Ldloc_1).WithLabels(continueLabel), // Input: [] | Result: [FirearmBase]
                    new CodeInstruction(OpCodes.Ldloc, evField),  // [EventArgs, FirearmBase]
                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ChangingAttachmentsEventArgs), nameof(ChangingAttachmentsEventArgs.NewAttachmentsCode))),  // [int, FirearmBase]
                });

            int lastIndex = newInstructions.FindLastIndex(x => x.opcode == OpCodes.Ldarg_1);

            newInstructions.RemoveAt(lastIndex);  // ldarg.1
            newInstructions.RemoveAt(lastIndex);  // ldfld     uint32 InventorySystem.Items.Firearms.Attachments.AttachmentsChangeRequest::AttachmentsCode

            newInstructions.InsertRange(
                lastIndex,
                new CodeInstruction[]
                {
                    // ev.NewAttachmentsCode
                    new CodeInstruction(OpCodes.Ldloc, evField), // [EventArgs]
                    new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeof(ChangingAttachmentsEventArgs), nameof(ChangingAttachmentsEventArgs.NewAttachmentsCode))), // [int]
                });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
            yield break;
        }
    }
}
*/