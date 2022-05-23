using System;
using UnityEngine.UI;

namespace DELTation.UI.Animations.Tweeners.Properties.Elements
{
    public sealed class GraphicTransparentElement : ITransparentElement
    {
        private readonly Graphic _graphic;

        public GraphicTransparentElement(Graphic graphic) =>
            _graphic = graphic ? graphic : throw new ArgumentNullException(nameof(graphic));

        public float Alpha
        {
            get => _graphic.color.a;
            set
            {
                var color = _graphic.color;
                color.a = value;
                _graphic.color = color;
            }
        }
    }
}