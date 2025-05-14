namespace ClairObscurConfig
{
    internal partial class EngineINI
    {
        // Credit: PCGamingWiki
        public static string Collection_DisableLumen()
        {
            return ("r.DynamicGlobalIlluminationMethod = 0\r\n" +
                    "r.Reflections = 0\r\n" +
                    "r.ReflectionMethod = 0\r\n" +
                    "r.Lumen.TranslucencyReflections = 0\r\n" +
                    "r.Lumen.Reflections = 0\r\n" +
                    "r.Lumen = 0\r\n" +
                    "r.Lumen.DiffuseIndirect.Allow = 0\r\n" +
                    "r.Lumen.GlobalIllumination = 0\r\n" +
                    "r.Lumen.ScreenProbeGather = 0\r\n" +
                    "r.Lumen.DiffuseIndirect.Allow = 0\r\n");
        }
        // Credit: TenebrisVenit
        public static string Collection_EnhLumen()
        {
            return ("r.Lumen.TraceMeshSDFs = 1\r\n" +
                    "r.Lumen.TraceMeshSDFs.Allow = 1\r\n" +
                    "r.Lumen.TraceMeshSDFs.TraceDistance = 300\r\n" +
                    "r.LumenScene.DirectLighting.OffscreenShadowing.TraceMeshSDFs = 1\r\n");
        }
        // Credit: MisterAlerion
        public static string Collection_PerfectClarity()
        {
            return ("r.DFDistanceScale = 2\r\n" +
                    "r.ViewDistanceScale = 2\r\n" +
                    "foliage.LODDistanceScale = 2\r\n" +
                    "r.postprocessing.disablematerials = 1\r\n" +
                    "r.DepthOfFieldQuality = 0\r\n" +
                    "r.SceneColorFringeQuality = 0\r\n" +
                    "r.Tonemapper.Quality = 1\r\n" +
                    "r.Tonemapper.Sharpen = 0\r\n" +
                    "r.Tonemapper.GrainQuantization = 0\r\n" +
                    "r.SkeletalMeshLODBias = -15\r\n" +
                    "r.MotionBlur.TargetFPS = 0\r\n");
        }
        // Credit: Omniscye
        public static string Collection_UltimateHighEnd()
        {
            return ("r.Fog = 1\r\n" +
                    "r.FogDensity = 0.05\r\n" +
                    "r.FogStartDistance = 50.0\r\n" +
                    "r.FogHeightFalloff = 0.15\r\n" +
                    "r.VolumetricFog = 1\r\n" +
                    "r.VolumetricFog.GridPixelSize = 4\r\n" +
                    "r.VolumetricFog.GridSizeZ = 128\r\n" +
                    "r.VolumetricFog.HistoryMissSupersampleCount = 4\r\n" +
                    "r.VolumetricFog.Jitter = 0\r\n" +
                    "r.DynamicLights = 1\r\n" +
                    "r.HighQualityLightmaps = 1\r\n" +
                    "r.Streaming.MipBias = -1\r\n" +
                    "r.Streaming.LimitPoolSizeToVRAM = 0\r\n" +
                    "r.Streaming.UseAsyncRequestsForDDC = 1\r\n" +
                    "r.Streaming.FullyLoadUsedTextures = 1\r\n" +
                    "r.Streaming.DefragDynamicBounds = 1\r\n" +
                    "r.Streaming.HLODStrategy = 2\r\n" +
                    "r.TextureStreaming = 1\r\n" +
                    "r.MaxAnisotropy = 16\r\n" +
                    "PoolSizeVRAMPercentage = 95\r\n" +
                    "r.ShadowQuality = 5\r\n" +
                    "r.Shadow.MaxResolution = 4096\r\n" +
                    "r.Shadow.DistanceScale = 2.5\r\n" +
                    "r.Shadow.CSM.MaxCascades = 4\r\n" +
                    "r.Shadow.CSM.TransitionScale = 2.0\r\n" +
                    "r.Shadow.CSM.FadeTransitionStart = 0.1\r\n" +
                    "r.ViewDistanceScale = 3.0\r\n" +
                    "r.DFDistanceScale = 3.0\r\n" +
                    "foliage.LODDistanceScale = 3.0\r\n" +
                    "r.ReflectionEnvironment = 1\r\n" +
                    "r.Lumen.Reflections = 1\r\n" +
                    "r.Lumen.ScreenProbeGather.ScreenTraces = 1\r\n" +
                    "r.Lumen.Reflections.HighQuality = 1\r\n" +
                    "r.Lumen.Reflections.HierarchicalScreenTraces = 1\r\n" +
                    "r.Lumen.GlobalIllumination = 1\r\n" +
                    "r.Lumen.DiffuseIndirect = 1\r\n" +
                    "r.SSR.Quality = 4\r\n" +
                    "r.SSGI.Quality = 4\r\n" +
                    "r.Tonemapper.Sharpen = 2\r\n" +
                    "r.Tonemapper.Quality = 5\r\n" +
                    "r.Tonemapper.GrainQuantization = 0\r\n" +
                    "r.SceneColorFringeQuality = 0\r\n" +
                    "r.SkeletalMeshLODBias = -15\r\n" +
                    "r.MotionBlur.Max = 0\r\n" +
                    "r.MotionBlur.TargetFPS = 0\r\n" +
                    "r.MotionBlurQuality = 0\r\n" +
                    "r.DepthOfFieldQuality = 2\r\n" +
                    "r.AmbientOcclusionLevels = 4\r\n" +
                    "r.AmbientOcclusionRadiusScale = 2.0\r\n" +
                    "r.GTAO.FalloffEnd = 400\r\n" +
                    "r.GTAO.NumSteps = 8\r\n" +
                    "r.ParallelRendering = 1\r\n" +
                    "r.ParallelShadows = 1\r\n" +
                    "r.ParallelTranslucency = 1\r\n" +
                    "r.ShaderPipelineCache.Enabled = 1\r\n" +
                    "r.UseShaderCaching = 1\r\n" +
                    "r.Shaders.Optimize = 1\r\n" +
                    "r.GPUBusyWait = 0\r\n" +
                    "r.RHICmdBypass = 0\r\n");
        }
        // Credit: axbhub
        public static string Collection_6GBVRAM()
        {
            return ("r.Fog=1\r\n" +
                    "r.FogDensity = 0.1\r\n" +
                    "r.FogStartDistance = 100.0\r\n" +
                    "r.FogHeightFalloff = 0.2\r\n" +
                    "r.VolumetricFog = 1\r\n" +
                    "r.DynamicLights = 1\r\n" +
                    "r.HighQualityLightmaps = 1\r\n" +
                    "r.Streaming.MipBias = 0\r\n" +
                    "r.MaxAnisotropy = 8\r\n" +
                    "PoolSizeVRAMPercentage = 70\r\n" +
                    "r.Streaming.LimitPoolSizeToVRAM = 1\r\n" +
                    "r.Streaming.UseAsyncRequestsForDDC = 1\r\n" +
                    "r.ShadowQuality = 3\r\n" +
                    "r.ViewDistanceScale = 1.5\r\n" +
                    "r.DFDistanceScale = 1.5\r\n" +
                    "foliage.LODDistanceScale = 1.5\r\n" +
                    "r.ReflectionEnvironment = 1\r\n" +
                    "r.Lumen.Reflections = 1\r\n" +
                    "r.Lumen.GlobalIllumination = 0\r\n" +
                    "r.SSR.Quality = 3\r\n" +
                    "r.SSGI.Quality = 2\r\n" +
                    "r.Tonemapper.Sharpen = 1\r\n" +
                    "r.Tonemapper.Quality = 2\r\n" +
                    "r.Tonemapper.GrainQuantization = 0\r\n" +
                    "r.SceneColorFringeQuality = 0\r\n" +
                    "r.SkeletalMeshLODBias = -10\r\n" +
                    "r.MotionBlur.TargetFPS = 0\r\n" +
                    "r.DepthOfFieldQuality = 1\r\n" +
                    "r.AmbientOcclusionLevels = 2\r\n" +
                    "r.AmbientOcclusionRadiusScale = 1.5\r\n" +
                    "r.ParallelRendering = 0\r\n" +
                    "r.ParallelShadows = 0\r\n" +
                    "r.ParallelTranslucency = 0\r\n" +
                    "r.ShaderPipelineCache.Enabled = 1\r\n" +
                    "r.UseShaderCaching = 1\r\n" +
                    "r.Shaders.Optimize = 1\r\n");
        }
    }
}
