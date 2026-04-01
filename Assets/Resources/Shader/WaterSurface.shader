Shader "Custom/StylizedWaterShader"
{
    Properties
    {
        // Couleurs principales
        _ShallowColor ("Shallow Color", Color) = (0.1, 0.8, 0.9, 0.7)
        _DeepColor ("Deep Color", Color) = (0.0, 0.3, 0.5, 0.9)
        _DepthMaxDistance ("Depth Max Distance", Range(0, 50)) = 5
        
        // Paramètres des vagues
        _WaveNormal ("Wave Normal", 2D) = "bump" {}
        _WaveNormal2 ("Wave Normal 2", 2D) = "bump" {}
        _NormalStrength ("Normal Strength", Range(0, 1)) = 0.5
        _WaveSpeed ("Wave Speed", Range(0, 2)) = 0.5
        _WaveScale ("Wave Scale", Range(0, 10)) = 1.5
        
        // Paramètres de transparence et réflexion
        _Transparency ("Transparency", Range(0, 1)) = 0.7
        _Smoothness ("Smoothness", Range(0, 1)) = 0.8
        _Distortion ("Distortion", Range(0, 1)) = 0.1
        
        // Paramètres de surface
        _SurfaceDistortion ("Surface Distortion", Range(0, 1)) = 0.1
        _SurfaceFoam ("Surface Foam", 2D) = "white" {}
        _SurfaceFoamScale ("Surface Foam Scale", Range(0, 10)) = 5
        _SurfaceFoamCutoff ("Surface Foam Cutoff", Range(0, 1)) = 0.8
        
        // Paramètres de scintillement
        _SparkleTexture ("Sparkle Texture", 2D) = "black" {}
        _SparkleScale ("Sparkle Scale", Range(0, 50)) = 30
        _SparkleSpeed ("Sparkle Speed", Range(0, 5)) = 1
        _SparkleStrength ("Sparkle Strength", Range(0, 2)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" "RenderPipeline" = "UniversalPipeline" }
        LOD 300
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
        Cull Back
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            
            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float4 tangentOS    : TANGENT;
                float2 uv           : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS    : SV_POSITION;
                float3 positionWS    : TEXCOORD0;
                float3 normalWS      : TEXCOORD1;
                float3 tangentWS     : TEXCOORD2;
                float3 bitangentWS   : TEXCOORD3;
                float4 screenPosition : TEXCOORD4;
                float2 uv            : TEXCOORD5;
                float fogFactor      : TEXCOORD6;
            };
            
            // Propriétés du shader
            TEXTURE2D(_WaveNormal);
            SAMPLER(sampler_WaveNormal);
            TEXTURE2D(_WaveNormal2);
            SAMPLER(sampler_WaveNormal2);
            TEXTURE2D(_SurfaceFoam);
            SAMPLER(sampler_SurfaceFoam);
            TEXTURE2D(_SparkleTexture);
            SAMPLER(sampler_SparkleTexture);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _ShallowColor;
                float4 _DeepColor;
                float _DepthMaxDistance;
                
                float4 _WaveNormal_ST;
                float4 _WaveNormal2_ST;
                float _NormalStrength;
                float _WaveSpeed;
                float _WaveScale;
                
                float _Transparency;
                float _Smoothness;
                float _Distortion;
                
                float _SurfaceDistortion;
                float4 _SurfaceFoam_ST;
                float _SurfaceFoamScale;
                float _SurfaceFoamCutoff;
                
                float4 _SparkleTexture_ST;
                float _SparkleScale;
                float _SparkleSpeed;
                float _SparkleStrength;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                
                // Calculez la position et les normales
                output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.positionCS = TransformWorldToHClip(output.positionWS);
                
                // Espace tangent pour les normales
                output.tangentWS = normalize(TransformObjectToWorldDir(input.tangentOS.xyz));
                output.bitangentWS = normalize(cross(output.normalWS, output.tangentWS) * input.tangentOS.w);
                
                // UV d'écran pour la profondeur et les réflexions
                output.screenPosition = ComputeScreenPos(output.positionCS);
                
                // UV de texture
                output.uv = input.uv;
                
                // Facteur de brouillard
                output.fogFactor = ComputeFogFactor(output.positionCS.z);
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                // === CALCUL DE LA PROFONDEUR ===
                float2 screenUV = input.screenPosition.xy / input.screenPosition.w;
                float sceneDepth = LinearEyeDepth(SampleSceneDepth(screenUV), _ZBufferParams);
                float surfaceDepth = LinearEyeDepth(input.positionCS.z, _ZBufferParams);
                float depthDifference = sceneDepth - surfaceDepth;
                float waterDepthFactor = saturate(depthDifference / _DepthMaxDistance);
                
                // === NORMALES DES VAGUES ===
                // Échantillonnage des deux textures de normales avec animation
                float2 waveUV1 = input.uv * _WaveScale;
                float2 waveUV2 = input.uv * _WaveScale * 1.4;
                float2 waveOffset1 = float2(_Time.y * _WaveSpeed * 0.1, _Time.y * _WaveSpeed * 0.15);
                float2 waveOffset2 = float2(-_Time.y * _WaveSpeed * 0.12, _Time.y * _WaveSpeed * 0.1);
                
                float3 normalMap1 = UnpackNormal(SAMPLE_TEXTURE2D(_WaveNormal, sampler_WaveNormal, waveUV1 + waveOffset1));
                float3 normalMap2 = UnpackNormal(SAMPLE_TEXTURE2D(_WaveNormal2, sampler_WaveNormal2, waveUV2 + waveOffset2));
                
                // Fusion des normales
                float3 normalMap = normalize(float3(normalMap1.xy + normalMap2.xy, normalMap1.z * normalMap2.z));
                normalMap = lerp(float3(0, 0, 1), normalMap, _NormalStrength);
                
                // Transformation des normales en espace monde
                float3x3 tangentToWorld = float3x3(
                    input.tangentWS,
                    input.bitangentWS,
                    input.normalWS
                );
                float3 normalWS = mul(normalMap, tangentToWorld);
                
                // === DISTORSION DE SURFACE ===
                float2 distortion = normalMap.xy * _SurfaceDistortion * min(depthDifference, 1);
                float2 distortedScreenUV = screenUV + distortion;
                
                // === COULEUR DE L'EAU ===
                float4 waterColor = lerp(_ShallowColor, _DeepColor, waterDepthFactor);
                
                // === ÉCUME DE SURFACE ===
                float2 foamUV = input.uv * _SurfaceFoamScale;
                float2 foamOffset1 = float2(_Time.y * _WaveSpeed * 0.05, _Time.y * _WaveSpeed * 0.05);
                float2 foamOffset2 = float2(-_Time.y * _WaveSpeed * 0.05, -_Time.y * _WaveSpeed * 0.05);
                
                float edgeFoam = saturate((1 - waterDepthFactor * 5) * 2);
                float surfaceFoamMask = SAMPLE_TEXTURE2D(_SurfaceFoam, sampler_SurfaceFoam, foamUV + foamOffset1).r;
                float surfaceFoamMask2 = SAMPLE_TEXTURE2D(_SurfaceFoam, sampler_SurfaceFoam, foamUV * 0.7 + foamOffset2).r;
                float foamFinal = saturate((surfaceFoamMask + surfaceFoamMask2) * 0.5);
                foamFinal = saturate(foamFinal - (1 - edgeFoam) - _SurfaceFoamCutoff);
                
                // === SCINTILLEMENT ===
                float2 sparkleUV = input.uv * _SparkleScale;
                float2 sparkleOffset = float2(_Time.y * _SparkleSpeed * 0.1, _Time.y * _SparkleSpeed * 0.2);
                float sparkle = SAMPLE_TEXTURE2D(_SparkleTexture, sampler_SparkleTexture, sparkleUV + sparkleOffset).r;
                float sparkleIntensity = pow(sparkle, 8) * _SparkleStrength;
                
                // Atténuer le scintillement en profondeur
                sparkleIntensity *= (1 - waterDepthFactor * 0.5);
                
                // === ÉCLAIRAGE ===
                // Info sur la lumière principale
                Light mainLight = GetMainLight(TransformWorldToShadowCoord(input.positionWS));
                float3 lightDirection = mainLight.direction;
                float3 lightColor = mainLight.color;
                
                // Direction de vue
                float3 viewDirection = normalize(_WorldSpaceCameraPos - input.positionWS);
                
                // Réflexion
                float3 reflectionVector = reflect(-viewDirection, normalWS);
                float NdotL = saturate(dot(normalWS, lightDirection));
                float fresnel = pow(1.0 - saturate(dot(normalWS, viewDirection)), 4);
                
                // Couleur finale
                float3 finalColor = waterColor.rgb;
                
                // Ajouter l'écume
                finalColor = lerp(finalColor, float3(1, 1, 1), foamFinal);
                
                // Ajouter les reflets spéculaires
                float specular = pow(saturate(dot(reflectionVector, lightDirection)), _Smoothness * 100) * _Smoothness;
                finalColor += specular * lightColor * 0.5;
                
                // Ajouter le scintillement
                finalColor += sparkleIntensity;
                
                // Ajouter la réflexion de Fresnel
                finalColor = lerp(finalColor, float3(0.8, 0.9, 0.95), fresnel * 0.5 * _Smoothness);
                
                // Appliquer le brouillard
                finalColor = MixFog(finalColor, input.fogFactor);
                
                // Alpha basé sur la profondeur et la transparence
                float alpha = lerp(_ShallowColor.a, _DeepColor.a, waterDepthFactor);
                alpha = max(alpha, foamFinal); // Écume toujours visible
                alpha = max(alpha, fresnel * 0.5); // Fresnel améliore l'opacité
                alpha = lerp(alpha, 1.0, fresnel * _Smoothness); // Plus opaque aux angles rasants
                alpha *= _Transparency;
                
                return float4(finalColor, alpha);
            }
            ENDHLSL
        }
        
        // Passe d'ombre (utilisez une version simplifiée pour les surfaces transparentes)
        Pass
        {
            Name "ShadowCaster"
            Tags {"LightMode" = "ShadowCaster"}
            
            ZWrite On
            ZTest LEqual
            ColorMask 0
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            
            float _WaveScale;
            float _WaveSpeed;
            
            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float2 uv           : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
            };
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = GetShadowPositionHClip(input.positionOS, input.normalOS);
                return output;
            }
            
            half4 frag(Varyings input) : SV_TARGET
            {
                return 0;
            }
            ENDHLSL
        }
        
        // Passe DepthOnly
        Pass
        {
            Name "DepthOnly"
            Tags{"LightMode" = "DepthOnly"}
            
            ZWrite On
            ColorMask 0
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
            };
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                return output;
            }
            
            half4 frag(Varyings input) : SV_TARGET
            {
                return 0;
            }
            ENDHLSL
        }
    }
}