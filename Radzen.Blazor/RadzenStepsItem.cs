﻿using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Radzen.Blazor
{
    public class RadzenStepsItem : RadzenComponent
    {
        private string _text;
        [Parameter]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    if (Steps != null)
                    {
                        Steps.Refresh();
                    }
                }
            }
        }

        [Parameter]
        public bool Selected { get; set; }

        bool _visible = true;
        [Parameter]
        public override bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    if (Steps != null)
                    {
                        Steps.Refresh();
                    }
                }
            }
        }

        bool _disabled;
        [Parameter]
        public bool Disabled
        {
            get
            {
                return _disabled;
            }
            set
            {
                if (_disabled != value)
                {
                    _disabled = value;
                    if (Steps != null)
                    {
                        Steps.Refresh();
                    }
                }
            }
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        RadzenSteps _steps;

        [CascadingParameter]
        public RadzenSteps Steps
        {
            get
            {
                return _steps;
            }
            set
            {
                if (_steps != value)
                {
                    _steps = value;
                    _steps.AddStep(this);
                }
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.DidParameterChange(nameof(Selected), Selected))
            {
                var selected = parameters.GetValueOrDefault<bool>(nameof(Selected));
                if (!selected)
                {
                    Steps?.SelectFirst();
                }
                else
                {
                    Steps?.SelectStep(this);
                }
            }

            await base.SetParametersAsync(parameters);
        }

        public override void Dispose()
        {
            base.Dispose();
            Steps?.RemoveStep(this);
        }
    }
}