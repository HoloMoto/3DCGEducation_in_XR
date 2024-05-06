Shader "Unlit/Test"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size("Size",float)=1
        _Speed("Speed",float)=0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent""IgnoreProjector"="True"}
        LOD 100
	ZWrite Off
    Blend One One
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
           //ジオメトリシェーダーの宣言
            #pragma geometry geom
            #pragma fragment frag
             
            #include "TargetSite.hlsl"
            
            ENDHLSL
        }
    }
}