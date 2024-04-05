using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Minesweeper.Client.Components
{
    public class VisibilityAwareComponent : ComponentBase, IDisposable
    {
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        private ElementReference thisElement;
        private DotNetObjectReference<VisibilityAwareComponent> _dotNetObjectReference;
        private bool isVisible;

        protected ElementReference ElementRef => thisElement;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetObjectReference = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("GridVirtualize.initIntersectionObserver", ElementRef, _dotNetObjectReference);
            }
        }

        [JSInvokable]
        public void OnIntersect()
        {
            isVisible = true;
            StateHasChanged();
        }

        protected override bool ShouldRender() => isVisible;

        public void Dispose()
        {
            _dotNetObjectReference?.Dispose();
        }
    }
}

