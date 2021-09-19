## Motion Graphics Shader by Sakura Shader

The Motion Graphics shader is a shader that moves, transforms, and combines a few basic shapes to create motion graphics on Unity's quad.
You can use up to six shapes be freely combined, and each shape can be transformed.

## Main Color

In the Main Color section, you can set the texture, color, transparency, and outline settings to be applied to the entire shape.
Note that these settings will be shared and used by all shapes.
This means that if you want to assign a different texture to each shape, for example, you will need to create another Quad.

### Texture

Set the texture to be used.
By adjusting the Tiling and Offset settings well, it is possible to display the texture nicely on the shape.

### Color

Set the basic color.
If you have set a texture, it will be multiplied by the texture color.

### Alpha Transparency

Sets the transparency.
If the texture and color contain transparency, the multiplied value will be used.

### Render Outline

Sets whether the outline will be render or not.
Note that if an outline is render, it will be render outside the original shape.

<img src="https://user-images.githubusercontent.com/10832834/133925669-f8b644b7-713c-4450-bb9d-98d93d12b820.PNG" height="200px" />

### Outline Color

Specifies the outline color.

## 1st Shape

### Shape

Specify the shape to be render. See below for images of each shape.

| Shape Function                                       | Result                                                                                                                             |
| ---------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| Circle                                               | <img src="https://user-images.githubusercontent.com/10832834/133926188-b7dd959c-14e7-4942-b3a7-6a98006a8908.PNG" height="100px" /> |
| Box (HW=1,1)                                         | <img src="https://user-images.githubusercontent.com/10832834/133926218-9787d900-299f-46bf-a7fe-c5f192378680.PNG" height="100px" /> |
| Equilateral Triangle                                 | <img src="https://user-images.githubusercontent.com/10832834/133926255-0d9f27eb-6eed-4814-b208-19bdd50787eb.PNG" height="100px" /> |
| Isosceles Triangle (HW=1,2)                          | <img src="https://user-images.githubusercontent.com/10832834/133926287-754ca2a7-eaf0-4775-89b3-1b7d0202374a.PNG" height="100px" /> |
| Pentagon                                             | <img src="https://user-images.githubusercontent.com/10832834/133926335-a20b76db-77b3-4743-8279-1625b60e21be.PNG" height="100px" /> |
| Hexagon                                              | <img src="https://user-images.githubusercontent.com/10832834/133926356-688b40f3-b27b-4f4a-a4d3-f53ff18748e7.PNG" height="100px" /> |
| Star                                                 | <img src="https://user-images.githubusercontent.com/10832834/133926378-888b82f0-801c-4f9c-992a-7553c7df7436.PNG" height="100px" /> |
| Heart                                                | <img src="https://user-images.githubusercontent.com/10832834/133926390-21c351a7-ef65-4c8a-bee7-954772a9e5f7.PNG" height="100px" /> |
| Segment (Unstable - A=0,05, B=1,0.5, Thickness=0.01) | <img src="https://user-images.githubusercontent.com/10832834/133926416-1c5ec9fb-5f89-4160-926f-d35a71dc099d.PNG" height="100px" /> |
| Pie (Angle=100)                                      | <img src="https://user-images.githubusercontent.com/10832834/133926460-373d6f4c-7ac7-4532-adcc-98a8d8091067.PNG" height="100px" /> |

### Position Transform

Set the position to which the shape will be render.
The center of the Quad (0.5, 0.5) is set as (0, 0), and the direction in which it will be shifted is set.

### Rotation

Set the rotation angle of the shape (eg. 120°).
Most shapes are rotated around the center of the shape.

### Scale

Specifies the magnification ratio of the figure.
Most shapes are set to fill as much of the image as possible when Scale=1.

### Repeat Mode

Specify how the shape will be repeated.

| Repeat Mode              | Result                                                                                                                             |
| ------------------------ | ---------------------------------------------------------------------------------------------------------------------------------- |
| None                     | <img src="https://user-images.githubusercontent.com/10832834/133926188-b7dd959c-14e7-4942-b3a7-6a98006a8908.PNG" height="100px" /> |
| Infinity                 | <img src="https://user-images.githubusercontent.com/10832834/133926582-0951c5b9-252f-4c63-97bd-3e6c8b96b76d.PNG" height="100px" /> |
| Limited (A=-2,-2, B=2,2) | <img src="https://user-images.githubusercontent.com/10832834/133926607-5bc4c520-5cf7-4540-b016-aa58cc49b0a6.PNG" height="100px" /> |

### Repeat Period

Specifies how often to repeat. The closer it is to 0, the narrower it is, and the closer it is to 1, the farther it is.

### Repeat Limit Box Range A, B

Specifies the number of repetitions.
For example, if you specify A=(-2, -2) and B=(2, 2), a total of five shapes will be displayed.

### Onion Mode

Renders only the outline of the shape.

<img src="https://user-images.githubusercontent.com/10832834/133926698-94acc850-6697-4316-b294-2dc056468dd8.PNG" height="200px" />

### Onion Thickness

Sets the thickness of the line in Onion Mode.
The larger the value, the larger the shape itself will be.

### Corner Round (Unstable)

Add rounding to the corners of the basic shapes.

<img src="https://user-images.githubusercontent.com/10832834/133926773-cb61d0f7-2df9-4b14-b138-ba4ce0057bf6.PNG" height="200px" />

### Box Size (WH)

Specifies the Width (X) and Height (Y) of a Box as a Shape.

### Triangle Size (WH)

Specifies the Width (X) and Height (Y) of a Isosceles Triangle as a Shape.

### Segment A, B

Specifies the start (A) and end (B) points when Segment is specified for Shape.

### Segment Thickness

Specifies the thickness of the line segment when Segment is specified for Shape.

### Pie Angle

Specifies the rendering range when Pie is specified for Shape.

## 2nd Shape ... 6th Shape

From the 2nd Shape onwards, the following two properties can be used in addition on the 1st Shape.
As an example, let's assume that the 1st shape outputs the left image and the 2nd shape outputs the right image.

| 1st Shape                                                                                                                          | 2nd Shape                                                                                                                          |
| ---------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| <img src="https://user-images.githubusercontent.com/10832834/133925669-f8b644b7-713c-4450-bb9d-98d93d12b820.PNG" height="100px" /> | <img src="https://user-images.githubusercontent.com/10832834/133925818-a7e9dd77-21b2-4271-9bfe-0adcb6880f28.PNG" height="100px" /> |

### Combination Function

Specifies how the shapes will be combined. The shapes will be merged as follows.

| Combination Function     | Result                                                                                                                             |
| ------------------------ | ---------------------------------------------------------------------------------------------------------------------------------- |
| Union                    | <img src="https://user-images.githubusercontent.com/10832834/133925891-9bc51547-fe9f-4337-9326-ee76a284b142.PNG" height="100px" /> |
| SmoothUnion              | <img src="https://user-images.githubusercontent.com/10832834/133925916-e5e0a46d-b664-437e-8f53-195b392fbb0c.PNG" height="100px" /> |
| Subtraction              | <img src="https://user-images.githubusercontent.com/10832834/133925934-3a408275-af54-4fa9-a95a-189c5153ca75.PNG" height="100px" /> |
| Intersection             | <img src="https://user-images.githubusercontent.com/10832834/133925949-e9720101-9335-4405-bc69-78bf7c50baec.PNG" height="100px" /> |
| Difference               | <img src="https://user-images.githubusercontent.com/10832834/133925965-60b373df-e758-4421-9a8a-63759145304f.PNG" height="100px" /> |
| Interpolation (Rate=0.5) | <img src="https://user-images.githubusercontent.com/10832834/133925996-3ba7a0c6-1fd1-4af3-bff0-9e5cb3abf375.PNG" height="100px" /> |
| None                     | <img src="https://user-images.githubusercontent.com/10832834/133925860-089bc378-b794-4577-bf1c-3242fb074d5e.PNG" height="100px" /> |

### Combination Rate

If Interpolation is used for the composition function, specify which one should be given priority.
If the value is close to 0, it will be applied to the already render shape, and if it is close to 1, it will be applied to the newly drawn shape.
