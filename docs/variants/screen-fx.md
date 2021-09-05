# ScreenFX Shader by SakuraShader

Screen-Space Shader for VFX, Particle Live, and VRMV in VRChat.

## Distortion

The Distortion effect makes some changes to the UVs of the entire screen (by GrabScreen).
For example, mosaicing and screen shifting are included in this effect.

### Screen Movement

Shifts the UV of the entire screen by the specified value.
If Z is changed, scaling will take place.

https://user-images.githubusercontent.com/10832834/132112612-c962de7c-bc78-47ff-a845-359a7617d101.mp4

#### Properties

| Property   | Type             | Description             |
| ---------- | ---------------- | ----------------------- |
| Movement X | `Range(-1, 1)`   | Shift the UV Horizontal |
| Movement Y | `Range(-1, 1)`   | Shift the UV Vertical   |
| Movement Z | `Range(-32, 32)` | Shift the UV Scaling    |

### Screen Rotation

WIP

### Pixelation

Changes the entire screen into a so-called mosaic shape.
The pixels used will be the smallest UV values in each block.

https://user-images.githubusercontent.com/10832834/132112720-e50930f7-dc33-4311-b9d1-8d5e089ca35e.mp4

#### Properties

| Property | Type            | Description       |
| -------- | --------------- | ----------------- |
| Height   | `Range(1, 128)` | Pixelation Height |
| Width    | `Range(1, 128)` | Pixelation Width  |

### Checkerboard

Checkerboard divides the entire screen into blocks of a specific size, and shifts the UV coordinates of each block by a specified value.

https://user-images.githubusercontent.com/10832834/132112757-f5dedbe1-08a5-4aa5-95a0-2d0b1ab1bb7b.mp4

#### Properties

| Property | Type            | Description                      |
| -------- | --------------- | -------------------------------- |
| Angle    | `Range(0, 360)` | Rotation Angle of Block          |
| Height   | `Range(0, 1)`   | Block Height                     |
| Width    | `Range(0, 1)`   | Block Width                      |
| Offset   | `Int`           | The block number of shifting UVs |

## Colors

### Chromatic Aberration

### Color Inverse

### Grayscale

### HUE Shift

### Sepia

### Color Layer

## Effects

### Cinemascope

### Glitch

### GirlsCam

### Checkerboard (Colored)
