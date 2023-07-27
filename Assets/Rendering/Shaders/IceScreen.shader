// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:1,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:1,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32740,y:33254,varname:node_2865,prsc:2|emission-6141-OUT,voffset-2248-OUT;n:type:ShaderForge.SFN_TexCoord,id:1811,x:31887,y:33713,varname:node_1811,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:6419,x:32076,y:33713,varname:node_6419,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-1811-UVOUT;n:type:ShaderForge.SFN_ProjectionParameters,id:9219,x:31880,y:33982,varname:node_9219,prsc:2;n:type:ShaderForge.SFN_Vector1,id:7820,x:31880,y:33915,varname:node_7820,prsc:2,v1:1;n:type:ShaderForge.SFN_Append,id:3844,x:32076,y:33962,varname:node_3844,prsc:2|A-7820-OUT,B-9219-SGN;n:type:ShaderForge.SFN_Multiply,id:2248,x:32313,y:33844,varname:node_2248,prsc:2|A-6419-OUT,B-3844-OUT;n:type:ShaderForge.SFN_Tex2d,id:6048,x:31752,y:32859,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6048,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4860-OUT;n:type:ShaderForge.SFN_Tex2d,id:7598,x:31399,y:33229,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_7598,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5fe830ed26c0cef4ea2ff6d039c68201,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:5612,x:29411,y:32982,varname:node_5612,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Power,id:6936,x:29627,y:32982,varname:node_6936,prsc:2|VAL-5612-U,EXP-2352-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2352,x:29411,y:33158,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_2352,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Power,id:373,x:29627,y:33111,varname:node_373,prsc:2|VAL-5612-V,EXP-2352-OUT;n:type:ShaderForge.SFN_OneMinus,id:9749,x:29627,y:33245,varname:node_9749,prsc:2|IN-5612-U;n:type:ShaderForge.SFN_OneMinus,id:1563,x:29627,y:33387,varname:node_1563,prsc:2|IN-5612-V;n:type:ShaderForge.SFN_Power,id:6767,x:29843,y:33245,varname:node_6767,prsc:2|VAL-9749-OUT,EXP-2352-OUT;n:type:ShaderForge.SFN_Power,id:9644,x:29843,y:33387,varname:node_9644,prsc:2|VAL-1563-OUT,EXP-2352-OUT;n:type:ShaderForge.SFN_Add,id:4405,x:29908,y:33038,varname:node_4405,prsc:2|A-6936-OUT,B-373-OUT;n:type:ShaderForge.SFN_Add,id:7290,x:30053,y:33320,varname:node_7290,prsc:2|A-6767-OUT,B-9644-OUT;n:type:ShaderForge.SFN_Add,id:6171,x:30259,y:33161,varname:node_6171,prsc:2|A-4405-OUT,B-7290-OUT;n:type:ShaderForge.SFN_Lerp,id:6141,x:32002,y:33138,varname:node_6141,prsc:2|A-6048-RGB,B-3264-OUT,T-9235-OUT;n:type:ShaderForge.SFN_Power,id:4100,x:30484,y:33161,varname:node_4100,prsc:2|VAL-6171-OUT,EXP-862-OUT;n:type:ShaderForge.SFN_Vector1,id:862,x:30259,y:33303,varname:node_862,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:9235,x:30645,y:33161,varname:node_9235,prsc:2|IN-4100-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5818,x:31399,y:33419,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_5818,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:3264,x:31589,y:33229,varname:node_3264,prsc:2|A-7598-RGB,B-5818-OUT;n:type:ShaderForge.SFN_Multiply,id:2075,x:31047,y:32925,varname:node_2075,prsc:2|A-9235-OUT,B-1348-OUT;n:type:ShaderForge.SFN_Vector1,id:1348,x:30877,y:33042,varname:node_1348,prsc:2,v1:0.2;n:type:ShaderForge.SFN_TexCoord,id:2115,x:31047,y:32775,varname:node_2115,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:95,x:31241,y:32775,varname:node_95,prsc:2|A-2115-UVOUT,B-2075-OUT;n:type:ShaderForge.SFN_Subtract,id:3930,x:31241,y:32925,varname:node_3930,prsc:2|A-2115-UVOUT,B-2075-OUT;n:type:ShaderForge.SFN_Lerp,id:4860,x:31486,y:32796,varname:node_4860,prsc:2|A-95-OUT,B-3930-OUT,T-7059-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7059,x:31445,y:33002,varname:node_7059,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7598-RGB;proporder:6048-7598-2352-5818;pass:END;sub:END;*/

Shader "Shader Forge/Test3" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Size ("Size", Float ) = 3
        _Emission ("Emission", Float ) = 1
    }
    SubShader {
        Tags {
            "Queue"="Geometry+1"
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Size)
                UNITY_DEFINE_INSTANCED_PROP( float, _Emission)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                v.vertex.xyz = float3(((o.uv0*2.0+-1.0)*float2(1.0,_ProjectionParams.r)),0.0);
                o.pos = v.vertex;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float _Size_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Size );
                float node_9235 = saturate(pow(((pow(i.uv0.r,_Size_var)+pow(i.uv0.g,_Size_var))+(pow((1.0 - i.uv0.r),_Size_var)+pow((1.0 - i.uv0.g),_Size_var))),3.0));
                float node_2075 = (node_9235*0.2);
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float2 node_4860 = lerp((i.uv0+node_2075),(i.uv0-node_2075),_Mask_var.rgb.rg);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4860, _MainTex));
                float _Emission_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Emission );
                float3 emissive = lerp(_MainTex_var.rgb,(_Mask_var.rgb*_Emission_var),node_9235);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
