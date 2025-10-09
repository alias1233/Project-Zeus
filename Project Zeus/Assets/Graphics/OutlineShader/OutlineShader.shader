Shader "OutlineShader.shader"
{
    // Properties that are exposed to editor
    Properties
    {

    }

    // The SubShader block containing the Shader code.
    SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        Tags
        {
            "RenderPipeline" = "UniversalRenderPipeline"
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }

        Pass
        {
            // The HLSL code block. Unity SRP uses the HLSL language.
            HLSLPROGRAM

            // This line defines the name of the vertex shader.
            #pragma vertex Vert

            // This line defines the name of the fragment shader.
            #pragma fragment Frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {

            };

            struct Varyings
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 texcoordStereo : TEXCOORD1;
				float3 viewSpaceDir : TEXCOORD2;
			};

			Varyings Vert(Attributes v)
			{
                Varyings OUT;
                
                return OUT;
            }

            float4 Frag(Attributes v)
			{
                float4 OUT;

                return OUT;
            }

            ENDHLSL
        }
    }
}