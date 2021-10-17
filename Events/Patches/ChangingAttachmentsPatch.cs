// -----------------------------------------------------------------------
// <copyright file="ChangingAttachmentsPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;
using HarmonyLib;
using InventorySystem;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using Mirror;
using Mistaken.Events.EventArgs;
using UnityEngine;

namespace Mistaken.Events.Patches
{
    [HarmonyPatch(typeof(AttachmentsServerHandler), nameof(AttachmentsServerHandler.ServerReceiveChangeRequest))]
    internal static class ChangingAttachmentsPatch
    {
        public static bool Prefix(NetworkConnection conn, AttachmentsChangeRequest msg)
        {
            global::ReferenceHub referenceHub;
            if (!NetworkServer.active || !global::ReferenceHub.TryGetHub(conn.identity.gameObject, out referenceHub))
                return false;
            Firearm firearm = referenceHub.inventory.CurInstance as Firearm;
            if (firearm == null)
                return false;
            if (referenceHub.inventory.CurItem.SerialNumber != msg.WeaponSerial)
                return false;
            bool flag = referenceHub.characterClassManager.CurClass == global::RoleType.Spectator;
            if (!flag)
            {
                foreach (WorkstationController workstationController in WorkstationController.AllWorkstations)
                {
                    if (!(workstationController == null) && workstationController.Status == 3 && workstationController.IsInRange(referenceHub))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            if (flag)
            {
                var player = Player.Get(referenceHub);
                var item = (Exiled.API.Features.Items.Firearm)Exiled.API.Features.Items.Item.Get(firearm);
                var ev = new ChangingAttachmentsEventArgs(player, item, msg.AttachmentsCode);

                if (!ev.IsAllowed)
                    return false;

                var attachmentCode = firearm.ValidateAttachmentsCode(ev.NewAttachmentsCode);

                firearm.ApplyAttachmentsCode(attachmentCode, true);
                if (firearm.Status.Ammo > firearm.AmmoManagerModule.MaxAmmo)
                    referenceHub.inventory.ServerAddAmmo(firearm.AmmoType, (int)(firearm.Status.Ammo - firearm.AmmoManagerModule.MaxAmmo));
                firearm.Status = new FirearmStatus((byte)Mathf.Min((int)firearm.Status.Ammo, (int)firearm.AmmoManagerModule.MaxAmmo), firearm.Status.Flags, attachmentCode);
            }

            return false;
        }
    }
}
