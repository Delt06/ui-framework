#  UI Framework

[![Version](https://img.shields.io/github/v/release/Delt06/ui-framework?sort=semver)](https://github.com/Delt06/ui-framework/releases)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A UI screen system for Unity. The main purpose is to animate screens opening and closing.

> Developed and tested with Unity 2020.3.16f1

## Table of contents

- [Installation](#installation)
- [Usage](#usage)

## Installation
### Option 1
- Open Package Manager through Window/Package Manager
- Click "+" and choose "Add package from git URL..."
- Insert the URL: https://github.com/Delt06/ui-framework.git?path=Packages/com.deltation.ui-framework

### Option 2  
Add the following line to `Packages/manifest.json`:
```
"com.deltation.ui-framework": "https://github.com/Delt06/ui-framework.git?path=Packages/com.deltation.ui-framework",
```

## Usage

If you need to use the plugin in an assembly definition, make sure to add a reference to `DELTation.UI`.

To create your first screen:

1) On a canvas, create a `GameObject` and attach `GameScreen` component to it.
2) Attach open/close animations to the child object if required.
3) Call `Open()`/`Close()` on the screen object.

### Animations

In the plugins, there is a number of animation components available. Each of them allows defining the keyframes for screen to be open and closed. Also, to configure how fast the animation should be played.

For now, the following animations are implemented:
- `ScreenAnchoredPositionAnimation` for anchored position
- `ScreenCanvasGroupAlphaAnimation` for alpha of `CanvasGroup`
- `ScreenGrapicAlphaAnimation` for alpha of `Graphic`-based components (e.g. `Image`)
- `ScreenLocalRotationAnimation` for local rotation
- `ScreenScaleAnimation` for local scale

### Parameters
For each of two states (open and closed) the following parameters are available:
- `Delay` for transition delay
- `Duration` for actual duration of transitions
- `Ease` for easing function
- `Overshoot` for overshoot value of easing function

### UniTask support

The plugin has an integration with [UniTask](https://github.com/Cysharp/UniTask).
It must be referenced as a separate assembly definition - `DELTation.UI.UniTaskSupport`.
