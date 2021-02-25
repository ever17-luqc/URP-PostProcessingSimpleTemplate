Shader "Custom/TemplatePP"
{
    Properties
    {   _MainTex("Base (RGB)", 2D) = "white" {}
        _TemplateColor("Template Color",Color)=(0,0,0)
       




    }
        SubShader
            {
                Tags { "RenderType" = "Opaque"  "RenderPipeline" = "UniversalPipeline"}
                LOD 100


                Pass
                {   Name "Template PP"
                    Tags{"LightMode" = "UniversalForward"}
                   
                   HLSLPROGRAM
               
                #pragma vertex vert
                #pragma fragment frag
              
                 #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
                 

            
                struct appdata
                {   
                    float4 vertex : POSITION;
                    float2 uv     : TEXCOORD0;
                   
                    
                };

                struct v2f
                {
                    

                    float4 vertex : SV_POSITION;
                    float2 uv     : TEXCOORD0;



                };
                
                CBUFFER_START(UnityPerMaterial)
                half3  _TemplateColor ;
                float4 _MainTex_ST;
                CBUFFER_END

                TEXTURE2D(_MainTex) ;            SAMPLER(sampler_MainTex)  ;
                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = TransformObjectToHClip(v.vertex);
                    o.uv= v.uv;
                    
                  

                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {   
                    half4 camera_SourceCol=SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv);
                    half4 finColor = camera_SourceCol;
                    finColor.rgb=dot(finColor.rgb,half3(0.213,0.715,0.072))*_TemplateColor;
                  


                       return finColor;
                   }
                   ENDHLSL
               }




              
            }
               
}
