﻿using System.Runtime.Serialization;

using UnityEditor;

using UnityEngine;

namespace NatsunekoLaboratory.SakuraShader
{
    public class AvatarsEffectsInspector : SakuraShaderInspector
    {
        public override void OnGUI(MaterialEditor me, MaterialProperty[] properties)
        {
            var material = (Material)me.target;

            InjectMaterialProperties(properties);

            OnHeaderGui("Avatars Effects Shader");
            OnInitialize(material);
            OnInitializeFoldout(FoldoutStatus1, FoldoutStatus2);

            OnToonColor(me);
            OnVoxelization(me);
            OnStencilGui(me);
            OnOthersGui(me, Culling, ZWrite);
            OnStoreFoldout(FoldoutStatus1, FoldoutStatus2);
        }

        private void OnToonColor(MaterialEditor me)
        {
            OnFoldOutGui(Category.ToonColor, () =>
            {
                me.TexturePropertySingleLine(new GUIContent("Main Texture"), MainTexture);
                me.TextureScaleOffsetProperty(MainTexture);
                me.ShaderProperty(MainColor, "Color");
            });
        }

        private void OnVoxelization(MaterialEditor me)
        {
            OnFoldoutAndToggleGui(Category.Voxelization, IsEnableVoxelization, () =>
            {
                me.ShaderProperty(VoxelSource, "Voxel Source");
                using (new EditorGUI.DisabledGroupScope(IsEqualsTo(VoxelSource, 0)))
                {
                    me.ShaderProperty(VoxelSize, "Voxel Base Size");
                    me.ShaderProperty(VoxelSizeRatio, "Voxel Size Ratio");
                }

                me.ShaderProperty(VoxelUvSamplingSource, "UV Sampling Source");
                using (new EditorGUI.DisabledGroupScope(!IsEqualsTo(VoxelUvSamplingSource, 0)))
                    me.ShaderProperty(VoxelColor, "Voxel Color");
                
                me.ShaderProperty(IsEnableVoxelBoundary, "Voxel Boundary");
                using (new EditorGUI.DisabledGroupScope(IsEqualsTo(IsEnableVoxelBoundary, 0)))
                {
                    me.ShaderProperty(VoxelBoundaryX, "Boundary X");
                    me.ShaderProperty(VoxelBoundaryY, "Boundary Y");
                    me.ShaderProperty(VoxelBoundaryZ, "Boundary Z");
                    me.ShaderProperty(VoxelBoundaryRange, "Boundary Range");
                }

                me.ShaderProperty(VoxelOffset, "Voxel Offset (X, Y, Z, N)");
                me.ShaderProperty(IsEnableVoxelAnimation, "Voxel Animation");

            });
        }


        private void OnStencilGui(MaterialEditor me)
        {
            OnFoldOutGui(Category.Stencil, () =>
            {
                me.ShaderProperty(StencilRef, "Reference");
                me.ShaderProperty(StencilComp, "Compare Function");
                me.ShaderProperty(StencilPass, "Pass");
                me.ShaderProperty(StencilFail, "Fail");
                me.ShaderProperty(StencilZFail, "ZFail");
                me.ShaderProperty(StencilReadMask, "Read Mask");
                me.ShaderProperty(StencilWriteMask, "Write Mask");
            });
        }

        private enum Category
        {
            ToonColor,

            [EnumMember(Value = "Effects - Voxelization")]
            Voxelization = 10,

            Stencil
        }
        // ReSharper disable InconsistentNaming

        private MaterialProperty MainTexture;
        private MaterialProperty MainColor;
        private MaterialProperty IsEnableVoxelization;
        private MaterialProperty VoxelSource;
        private MaterialProperty VoxelSize;
        private MaterialProperty VoxelSizeRatio;
        private MaterialProperty VoxelUvSamplingSource;
        private MaterialProperty VoxelColor;
        private MaterialProperty IsEnableVoxelBoundary;
        private MaterialProperty VoxelBoundaryX;
        private MaterialProperty VoxelBoundaryY;
        private MaterialProperty VoxelBoundaryZ;
        private MaterialProperty VoxelBoundaryRange;
        private MaterialProperty VoxelOffset;
        private MaterialProperty IsEnableVoxelAnimation;
        private MaterialProperty StencilRef;
        private MaterialProperty StencilComp;
        private MaterialProperty StencilPass;
        private MaterialProperty StencilFail;
        private MaterialProperty StencilZFail;
        private MaterialProperty StencilReadMask;
        private MaterialProperty StencilWriteMask;
        private MaterialProperty Culling;
        private MaterialProperty ZWrite;
        private MaterialProperty FoldoutStatus1;
        private MaterialProperty FoldoutStatus2;

        // ReSharper restore InconsistentNaming
    }
}