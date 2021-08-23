using SharpX.Library.ShaderLab.Attributes;
using SharpX.Library.ShaderLab.Enums;
using SharpX.Library.ShaderLab.Primitives;

namespace SakuraShader.Lyrics.Shader
{
    [Export("core.{extension}")]
    public static class Globals
    {
        #region Unity Global Fields

        [External]
        [GlobalMember]
        [Property("_Time")]
        public static SlFloat4 Time { get; }

        [External]
        [GlobalMember]
        [Property("_SinTime")]
        public static SlFloat4 SinTime { get; }

        [External]
        [GlobalMember]
        [Property("_CosTime")]
        public static SlFloat4 CosTime { get; }

        #endregion

        #region MainData

        [GlobalMember]
        [DisplayName("Texture")]
        [DefaultValue("\"white\" {}")]
        public static Sampler2D MainTexture { get; }

        [GlobalMember]
        [NotExportToInspector]
        [Property("MainTexture_ST")]
        public static SlFloat4 MainTextureST { get; }

        [GlobalMember]
        [DisplayName("Color")]
        [Color]
        public static SlFloat4 Color { get; }

        #endregion

        #region Shader Animations

        [GlobalMember]
        [DisplayName("Enable Shader Animation")]
        [DefaultValue(false)]
        public static SlBool IsAnimationEnabled { get; }

        [GlobalMember]
        [DisplayName("Shader Animation Sprite Texture")]
        [DefaultValue("\"white\" {}")]
        [NoScaleOffset]
        public static Sampler2D AnimationSpriteTexture { get; }

        [GlobalMember]
        [DisplayName("Shader Animation Sprite Split")]
        [DefaultValue(1)]
        public static SlInt AnimationSpriteSplit { get; }

        [GlobalMember]
        [DisplayName("Shader Animation Update Rate")]
        [DefaultValue(0.0)]
        public static SlFloat AnimationUpdateRate { get; }

        #endregion

        #region Outline

        [GlobalMember]
        [DisplayName("Enable Outline")]
        [DefaultValue(false)]
        public static SlBool IsOutlineEnabled { get; }

        [GlobalMember]
        [DisplayName("Outline Color")]
        [DefaultValue("(0, 0, 0, 1)")]
        public static SlFloat4 OutlineColor { get; }

        [GlobalMember]
        [DisplayName("Outline Width")]
        [Range(0, 20)]
        [DefaultValue(0)]
        public static SlFloat OutlineWidth { get; }

        [GlobalMember]
        [DisplayName("Outline Texture")]
        [DefaultValue("\"white\" {}")]
        public static Sampler2D OutlineTexture { get; }

        #endregion

        #region RotationZ

        [GlobalMember]
        [DisplayName("Enable Rotation by Z")]
        [DefaultValue(false)]
        public static SlBool IsRotationZEnabled { get; }

        [GlobalMember]
        [DisplayName("Rotation Angle")]
        [Range(-180, 180)]
        [DefaultValue(0)]
        public static SlFloat RotationZAngle { get; }

        #endregion

        #region SlideMode

        [GlobalMember]
        [DisplayName("Enable Slide Invisible")]
        [DefaultValue(false)]
        [CustomInspectorAttribute("MaterialToggle")]
        public static SlBool IsSlideInvisibleEnabled { get; }

        [GlobalMember]
        [DisplayName("Slide Visible From")]
        [Enum(typeof(ClippingMode))]
        [DefaultValue(ClippingMode.Left)]
        public static ClippingMode SlideVisibleFrom { get; }

        [GlobalMember]
        [DisplayName("Slide Visible Width")]
        [Range(0.0f, 1.0f)]
        [DefaultValue(0)]
        public static SlFloat SlideVisibleWidth { get; }

        #endregion

        #region Stencil

        [GlobalMember]
        [Property("_StencilRef")]
        [DisplayName("Stencil Ref")]
        [DefaultValue(1)]
        public static SlInt StencilRef { get; }

        [GlobalMember]
        [Property("_StencilCompare")]
        [DisplayName("Stencil Compare")]
        [Enum("UnityEngine.Rendering.CompareFunction")]
        [DefaultValue(CompareFunction.Always)]
        public static CompareFunction StencilCompare { get; }

        [GlobalMember]
        [Property("_StencilPass")]
        [DisplayName("Stencil Pass")]
        [Enum("UnityEngine.Rendering.StencilOp")]
        [DefaultValue(StencilOp.Keep)]
        public static StencilOp StencilPass { get; }

        #endregion

        #region Shader Properties

        [GlobalMember]
        [Property("_Culling")]
        [DisplayName("Culling")]
        [Enum("UnityEngine.Rendering.CullMode")]
        [DefaultValue(Culling.Off)]
        public static Culling Culling { get; }

        [GlobalMember]
        [Property("_ZWrite")]
        [DisplayName("ZWrite")]
        [Enum("Off", 0, "On", 1)]
        [DefaultValue(1)]
        public static Switch ZWrite { get; }

        #endregion

    }
}