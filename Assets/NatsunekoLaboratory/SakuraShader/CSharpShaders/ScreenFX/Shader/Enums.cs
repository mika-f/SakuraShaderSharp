namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Shader
{
    public enum NoisePattern
    {
        Random,

        RandomColor,

        Block
    }

    public enum NoiseRandomFactor
    {
        Constant,

        Time,

        SinTime,

        CosTime,
    }

    public enum LayerBlendMode
    {
        None,

        // 比較 (暗)
        Darken,

        // 比較 (明)
        Lighten,

        // カラー比較 (暗)
        ColorDarken,

        // カラー比較 (明)
        ColorLighten,

        // 焼き込みカラー
        ColorBurn,

        // 焼き込み (リニア)
        LinearBurn,

        // 乗算
        Multiply,

        // スクリーン
        Screen,

        // 覆い焼き
        ColorDodge,

        // 覆い焼き (リニア)
        LinearDodge,

        Overlay,
    }

    public enum GlitchMode
    {
        Block,

        KinoAnalog,
    }
}