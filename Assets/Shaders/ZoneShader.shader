// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33076,y:32654,varname:node_3138,prsc:2|normal-5650-RGB,emission-7241-RGB,alpha-1225-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32510,y:32647,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:0.8;n:type:ShaderForge.SFN_DepthBlend,id:9484,x:32232,y:33214,varname:node_9484,prsc:2|DIST-7930-OUT;n:type:ShaderForge.SFN_Dot,id:3541,x:32216,y:33426,varname:node_3541,prsc:2,dt:0|A-6829-OUT,B-9245-OUT;n:type:ShaderForge.SFN_NormalVector,id:9245,x:31999,y:33511,prsc:2,pt:False;n:type:ShaderForge.SFN_ViewVector,id:6829,x:31999,y:33365,varname:node_6829,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:2977,x:32402,y:33214,varname:node_2977,prsc:2|IN-9484-OUT;n:type:ShaderForge.SFN_Tex2d,id:4823,x:32546,y:32927,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_4823,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False|UVIN-9712-UVOUT;n:type:ShaderForge.SFN_Vector1,id:4790,x:31975,y:32968,varname:node_4790,prsc:2,v1:1;n:type:ShaderForge.SFN_TexCoord,id:9338,x:31975,y:32782,varname:node_9338,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2394,x:32154,y:32863,varname:node_2394,prsc:2|A-9338-UVOUT,B-4790-OUT;n:type:ShaderForge.SFN_Time,id:9471,x:31987,y:33112,varname:node_9471,prsc:2;n:type:ShaderForge.SFN_Panner,id:9712,x:32349,y:32907,varname:node_9712,prsc:2,spu:0,spv:1|UVIN-2394-OUT,DIST-6649-OUT;n:type:ShaderForge.SFN_Slider,id:7930,x:31868,y:33260,ptovrint:False,ptlb:Intersection,ptin:_Intersection,varname:node_7930,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:10;n:type:ShaderForge.SFN_Add,id:1225,x:32941,y:33147,varname:node_1225,prsc:2|A-2977-OUT,B-7095-OUT,C-3250-OUT;n:type:ShaderForge.SFN_Multiply,id:7095,x:32583,y:33408,varname:node_7095,prsc:2|A-8210-OUT,B-2243-OUT;n:type:ShaderForge.SFN_Slider,id:2243,x:32222,y:33641,ptovrint:False,ptlb:Glancing,ptin:_Glancing,varname:node_2243,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.1,max:2;n:type:ShaderForge.SFN_OneMinus,id:8210,x:32360,y:33349,varname:node_8210,prsc:2|IN-3541-OUT;n:type:ShaderForge.SFN_Multiply,id:3250,x:32713,y:33063,varname:node_3250,prsc:2|A-4823-A,B-5257-OUT;n:type:ShaderForge.SFN_Slider,id:5257,x:32338,y:33127,ptovrint:False,ptlb:TextureOpacity,ptin:_TextureOpacity,varname:node_5257,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6649,x:32192,y:33042,varname:node_6649,prsc:2|A-7424-OUT,B-9471-T;n:type:ShaderForge.SFN_Slider,id:7424,x:31725,y:33056,ptovrint:False,ptlb:Anim Speed,ptin:_AnimSpeed,varname:node_7424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:5650,x:32718,y:32772,ptovrint:False,ptlb:node_5650,ptin:_node_5650,varname:node_5650,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:False|UVIN-9712-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:1029,x:32860,y:32950,varname:node_1029,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5650-RGB;proporder:7241-4823-7930-2243-5257-7424-5650;pass:END;sub:END;*/

Shader "Shader Forge/ZoneShader" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,0.8)
        _Texture ("Texture", 2D) = "black" {}
        _Intersection ("Intersection", Range(1, 10)) = 1
        _Glancing ("Glancing", Range(0.1, 2)) = 0.1
        _TextureOpacity ("TextureOpacity", Range(0, 1)) = 0
        _AnimSpeed ("Anim Speed", Range(0.1, 1)) = 1
        _node_5650 ("node_5650", 2D) = "bump" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _Color;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Intersection;
            uniform float _Glancing;
            uniform float _TextureOpacity;
            uniform float _AnimSpeed;
            uniform sampler2D _node_5650; uniform float4 _node_5650_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 projPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_9471 = _Time;
                float2 node_9712 = ((i.uv0*1.0)+(_AnimSpeed*node_9471.g)*float2(0,1));
                float4 _node_5650_var = tex2D(_node_5650,TRANSFORM_TEX(node_9712, _node_5650));
                float3 normalLocal = _node_5650_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_9712, _Texture));
                return fixed4(finalColor,((1.0 - saturate((sceneZ-partZ)/_Intersection))+((1.0 - dot(viewDirection,i.normalDir))*_Glancing)+(_Texture_var.a*_TextureOpacity)));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles n3ds wiiu 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
