// -----------------------------------------------------------------------
// <copyright file="CustomEvents.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.Events.Extensions;
using Mistaken.Events.EventArgs;

namespace Mistaken.Events.Handlers
{
    /// <summary>
    /// Custom Events.
    /// </summary>
    public static class CustomEvents
    {
        /// <summary>
        /// Event called when requesting Item Pickup.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<PickItemRequestEventArgs> RequestPickItem;

        /// <summary>
        /// Event called when player joins first time in a session.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<FirstTimeJoinedEventArgs> FirstTimeJoined;

        /// <summary>
        /// Event called when player is changing attachments.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<ChangingAttachmentsEventArgs> ChangingAttachments;

        /// <summary>
        /// Event called when player is sending RA command.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<SendingCommandEventArgs> SendingCommand;

        /// <summary>
        /// Event called when SCP-914 is upgrading.
        /// </summary>
        public static event Exiled.Events.Events.CustomEventHandler<SCP914UpgradingEventArgs> SCP914Upgrading;

        /// <summary>
        /// Invokes <see cref="RequestPickItem"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeRequestPickItem(PickItemRequestEventArgs ev)
        {
            RequestPickItem.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="FirstTimeJoined"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeFirstTimeJoined(FirstTimeJoinedEventArgs ev)
        {
            FirstTimeJoined.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="ChangingAttachments"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeChangingAttachments(ChangingAttachmentsEventArgs ev)
        {
            ChangingAttachments.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="SendingCommand"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeSendingCommand(SendingCommandEventArgs ev)
        {
            SendingCommand.InvokeSafely(ev);
        }

        /// <summary>
        /// Invokes <see cref="SCP914Upgrading"/> with <paramref name="ev"/> as parameter.
        /// </summary>
        public static void InvokeSCP914Upgrading(SCP914UpgradingEventArgs ev)
        {
            SCP914Upgrading.InvokeSafely(ev);
        }
    }
}