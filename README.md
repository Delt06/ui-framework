# UI Framework

A UI screen system for Unity (main purpose is to animate opening and closing screens).

# Usage

1) On a canvas, create a `GameObject` and attach `GameScreen` component to it.
2) Attach open/close animations to child object if requred.
3) Call `Open()`/`Close()` on the screen object.

## Animations

In the plugins, there is a number of animation components available. Each of them allows to define the keyframes for screen to be open and closed. Also, to configure how fast the animation should be played.

For now, the following animations are implemented:
- `ScreenAnchoredPositionAnimation` for anchored position
- `ScreenCanvasGroupAlphaAnimation` for alpha of `CanvasGroup`
- `ScreenGrapicAlphaAnimation` for alpha of `Graphic`-based components (e.g. `Image`)
- `ScreenLocalRotationAnimation` for local rotation
- `ScreenScaleAnimation` for local scale

### Parameters
For each of two states (open and closed) the following parameters are available:
- `Delay` for transition delay
- `Duration` for actual duration of transitons
- `Ease` for easing function
- `Overshoot` for overshoot value of easing function

### Easing
Easing function is a function that defines the interpolation between keyframes (i.e. an animation curve). The implementation is taken from [here](https://github.com/Delt06/easing).
