﻿using Microsoft.AspNetCore.Components;
using System;

namespace AntBlazor
{
    /// <summary>
    /// Small numerical value or status descriptor for UI elements.
    /// </summary>
    public class AntBadgeBase : AntDomComponentBase
    {
        /// <summary>
        /// Customize Badge dot color
        /// </summary>
        [Parameter] public string Color { get; set; }
        /// <summary>
        /// Number to show in badge
        /// </summary>
        [Parameter] public int? Count { get; set; }
        /// <summary>
        /// Whether to display a red dot instead of count
        /// </summary>
        [Parameter] public bool Dot { get; set; } = false;
        /// <summary>
        /// Set offset of the badge dot, like[x, y]
        /// </summary>
        [Parameter] public Tuple<int, int> Offset { get; set; }
        /// <summary>
        /// Max count to show
        /// </summary>
        [Parameter] public int OverflowCount { get; set; } = 99;

        /// <summary>
        /// Whether to show badge when count is zero
        /// </summary>
        [Parameter] public bool ShowZero { get; set; } = false;
        /// <summary>
        /// Set Badge as a status dot
        /// </summary>
        [Parameter] public string Status { get; set; }
        /// <summary>
        /// If status is set, text sets the display text of the status dot
        /// </summary>
        [Parameter] public string Text { get; set; }
        /// <summary>
        /// Text to show when hovering over the badge
        /// </summary>
        [Parameter] public string Title { get; set; }
        /// <summary>
        /// Wrapping this item.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }
        /// <summary>
        /// Sets the default CSS classes.
        /// </summary>
        protected void SetClassMap()
        {
            string prefixName = "ant-badge";
            ClassMapper.Clear()
                .Add(prefixName)
                .If($"{prefixName}-status", () => !string.IsNullOrEmpty(Status) || !string.IsNullOrEmpty(Color))
                .If($"{prefixName}-not-a-wrapper", () => ChildContent == null)
                ;
        }
        /// <summary>
        /// Shows the Count and takes into account the OverFlowCount.
        /// </summary>
        protected string DisplayCount => Count > OverflowCount ? $"{OverflowCount}+"
                                        : Count == 0 && !ShowZero ? string.Empty
                                        : Count.ToString();
        /// <summary>
        /// Startup code
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!string.IsNullOrEmpty(Color) && !string.IsNullOrEmpty(Status))
                throw new ArgumentException($"You cannot provide a {nameof(Status)} and a {nameof(Color)}, choose one.");
            SetClassMap();
        }

        /// <summary>
        /// Runs everytime a parameter is set.
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            SetClassMap();
        }
    }
}