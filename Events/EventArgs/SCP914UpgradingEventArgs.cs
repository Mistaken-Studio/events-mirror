// -----------------------------------------------------------------------
// <copyright file="SCP914UpgradingEventArgs.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using Exiled.API.Features;
using Scp914;
using UnityEngine;

namespace Mistaken.Events.EventArgs
{
    /// <inheritdoc/>
    public class SCP914UpgradingEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SCP914UpgradingEventArgs"/> class.
        /// Constructor.
        /// </summary>
        public SCP914UpgradingEventArgs(Scp914KnobSetting knobSetting, bool isAllowed = true)
        {
            this.Scp914 = Exiled.API.Features.Scp914.Scp914Controller;
            this.OutputPosition = Exiled.API.Features.Scp914.OutputBooth.position;
            this.KnobSetting = knobSetting;
            this.IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets SCP 914 Controller.
        /// </summary>
        public Scp914Controller Scp914 { get; }

        /// <summary>
        /// Gets SCP 914 Output Position.
        /// </summary>
        public Vector3 OutputPosition { get; }

        /// <summary>
        /// Gets or sets SCP 914 knob setting.
        /// </summary>
        public Scp914KnobSetting KnobSetting { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether upgrading is allowed.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}