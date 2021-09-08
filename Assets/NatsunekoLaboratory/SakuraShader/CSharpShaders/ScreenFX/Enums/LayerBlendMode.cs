namespace NatsunekoLaboratory.SakuraShader.ScreenFX.Enums
{
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

        // 除算
        Divide,

        // 乗算
        Multiply,

        // 減算
        Subtract,

        // 差の絶対値
        Difference,

        // スクリーン
        Screen,

        // 覆い焼きカラー
        ColorDodge,

        // 覆い焼き (リニア)
        LinearDodge,

        // オーバーレイ
        Overlay
    }
}