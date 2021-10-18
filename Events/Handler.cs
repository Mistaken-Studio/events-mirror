// -----------------------------------------------------------------------
// <copyright file="Handler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Mistaken.API.Diagnostics;

namespace Mistaken.Events
{
    internal class Handler : Module
    {
        public Handler(PluginHandler plugin)
            : base(plugin)
        {
        }

        public override string Name => "CustomEvents";

        public override void OnEnable()
        {
            Exiled.Events.Handlers.Player.Verified += this.Handle<Exiled.Events.EventArgs.VerifiedEventArgs>((ev) => this.Player_Verified(ev));
        }

        public override void OnDisable()
        {
            Exiled.Events.Handlers.Player.Verified -= this.Handle<Exiled.Events.EventArgs.VerifiedEventArgs>((ev) => this.Player_Verified(ev));
        }

        private static readonly HashSet<string> JoinedButNotLeft = new HashSet<string>();

        private void Player_Verified(Exiled.Events.EventArgs.VerifiedEventArgs ev)
        {
            if (!JoinedButNotLeft.Contains(ev.Player.UserId))
            {
                JoinedButNotLeft.Add(ev.Player.UserId);
                Handlers.CustomEvents.InvokeFirstTimeJoined(new EventArgs.FirstTimeJoinedEventArgs(ev.Player));
            }
        }
    }
}
