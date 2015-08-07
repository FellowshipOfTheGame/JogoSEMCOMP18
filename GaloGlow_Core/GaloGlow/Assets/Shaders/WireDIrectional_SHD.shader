// Shader created with Shader Forge v1.17 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.17;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:33462,y:32717,varname:node_3138,prsc:2|emission-463-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32818,y:32465,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Time,id:9621,x:31375,y:32945,varname:node_9621,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:418,x:31338,y:32773,varname:node_418,prsc:2,uv:0;n:type:ShaderForge.SFN_Cos,id:386,x:32151,y:32924,varname:node_386,prsc:2|IN-8644-OUT;n:type:ShaderForge.SFN_Subtract,id:8644,x:31842,y:32841,varname:node_8644,prsc:2|A-1420-OUT,B-1267-OUT;n:type:ShaderForge.SFN_Multiply,id:3836,x:32958,y:32783,varname:node_3836,prsc:2|A-5564-OUT,B-8692-OUT;n:type:ShaderForge.SFN_Slider,id:1154,x:31228,y:33124,ptovrint:False,ptlb:NoiseSpeed,ptin:_NoiseSpeed,varname:node_1154,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:150;n:type:ShaderForge.SFN_Multiply,id:1267,x:31582,y:33043,varname:node_1267,prsc:2|A-9621-T,B-1154-OUT;n:type:ShaderForge.SFN_Multiply,id:1420,x:31615,y:32731,varname:node_1420,prsc:2|A-3501-OUT,B-418-U;n:type:ShaderForge.SFN_Slider,id:3501,x:31259,y:32615,ptovrint:False,ptlb:NoiseFrequency,ptin:_NoiseFrequency,varname:node_3501,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:30,max:300;n:type:ShaderForge.SFN_Add,id:5794,x:32321,y:32924,varname:node_5794,prsc:2|A-386-OUT,B-206-OUT;n:type:ShaderForge.SFN_Vector1,id:206,x:32151,y:33048,varname:node_206,prsc:2,v1:1;n:type:ShaderForge.SFN_Divide,id:9485,x:32491,y:32924,varname:node_9485,prsc:2|A-5794-OUT,B-9920-OUT;n:type:ShaderForge.SFN_Vector1,id:9920,x:32321,y:33048,varname:node_9920,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:8692,x:32687,y:32924,varname:node_8692,prsc:2|A-9485-OUT,B-4744-OUT;n:type:ShaderForge.SFN_Add,id:463,x:33137,y:32761,varname:node_463,prsc:2|A-7241-RGB,B-3836-OUT;n:type:ShaderForge.SFN_Slider,id:4744,x:32304,y:33165,ptovrint:False,ptlb:Contrast,ptin:_Contrast,varname:node_4744,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_Cos,id:8961,x:32124,y:32590,varname:node_8961,prsc:2|IN-2126-OUT;n:type:ShaderForge.SFN_Multiply,id:2126,x:31945,y:32438,varname:node_2126,prsc:2|A-5381-OUT,B-418-U;n:type:ShaderForge.SFN_Tau,id:5381,x:31724,y:32438,varname:node_5381,prsc:2;n:type:ShaderForge.SFN_Power,id:7255,x:32502,y:32613,varname:node_7255,prsc:2|VAL-944-OUT,EXP-2240-OUT;n:type:ShaderForge.SFN_Slider,id:2240,x:32072,y:32763,ptovrint:False,ptlb:Falloff,ptin:_Falloff,varname:node_2240,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5,max:5;n:type:ShaderForge.SFN_Clamp01,id:944,x:32292,y:32613,varname:node_944,prsc:2|IN-8961-OUT;n:type:ShaderForge.SFN_Vector1,id:8377,x:32445,y:32428,varname:node_8377,prsc:2,v1:1;n:type:ShaderForge.SFN_Subtract,id:5564,x:32661,y:32602,varname:node_5564,prsc:2|A-8377-OUT,B-7255-OUT;proporder:7241-1154-3501-4744-2240;pass:END;sub:END;*/

Shader "Lhama/WireDIrectional_SHD" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _NoiseSpeed ("NoiseSpeed", Range(0, 150)) = 1
        _NoiseFrequency ("NoiseFrequency", Range(0, 300)) = 30
        _Contrast ("Contrast", Range(0, 1)) = 0.2
        _Falloff ("Falloff", Range(0, 5)) = 5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _NoiseSpeed;
            uniform float _NoiseFrequency;
            uniform float _Contrast;
            uniform float _Falloff;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float node_8377 = 1.0;
                float node_944 = saturate(cos((6.28318530718*i.uv0.r)));
                float4 node_9621 = _Time + _TimeEditor;
                float3 emissive = (_Color.rgb+((node_8377-pow(node_944,_Falloff))*(((cos(((_NoiseFrequency*i.uv0.r)-(node_9621.g*_NoiseSpeed)))+1.0)/2.0)*_Contrast)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
