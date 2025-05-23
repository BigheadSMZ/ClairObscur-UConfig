﻿using System.Security.Policy;
using System;

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
        // Credit: itsbttq
        public static string Collection_Engine33()
        {
            return ("r.FilmGrain = 0\r\n" +
                    "r.Tonemapper.GrainQuantization = 0\r\n" +
                    "r.NT.Lens.ChromaticAberration.Intensity = 0\r\n" +
                    "r.SceneColorFringe.Max = 0\r\n" +
                    "r.SceneColorFringeQuality = 0\r\n" +
                    "r.MaxAnisotropy = 16\r\n" +
                    "r.Tonemapper.Sharpen = 0.2\r\n" +
                    "AudioThread.BatchAsyncBatchSize = 256\r\n" +
                    "AudioThread.EnableBatchProcessing = 1\r\n" +
                    "bAllowMultiThreadedAnimationUpdate = 1\r\n" +
                    "bCanBlueprintsTickByDefault = 0\r\n" +
                    "bOptimizeAnimBlueprintMemberVariableAccess = 1\r\n" +
                    "bSupportsGPUScene = 1\r\n" +
                    "bSupportsWaveOperations = 1\r\n" +
                    "bUseAsyncComputeContext = 1\r\n" +
                    "csv.trackWaitsGT = 0\r\n" +
                    "csv.trackWaitsRT = 0\r\n" +
                    "EnableMathOptimisations = 1\r\n" +
                    "FX.AllowAsyncTick = 1\r\n" +
                    "FX.BatchAsync = 1\r\n" +
                    "FX.BatchAsyncBatchSize = 128\r\n" +
                    "FX.EarlyScheduleAsync = 1\r\n" +
                    "fx.EnableCircularAnimTrailDump = 0\r\n" +
                    "fx.Niagara.AsyncCompute = 1\r\n" +
                    "fx.Niagara.DebugDraw.Enabled = 0\r\n" +
                    "fx.ParticlePerfStats.Enabled = 0\r\n" +
                    "GeometryCache.OffloadUpdate = 1\r\n" +
                    "landscape.RenderNanite = 1\r\n" +
                    "memory.logGenericPlatformMemoryStats = 0\r\n" +
                    "niagara.CreateShadersOnLoad = 1\r\n" +
                    "Slate.bAllowThrottling = 0\r\n" +
                    "t.MaxFPS = 0\r\n" +
                    "TaskGraph.Enable = 1\r\n" +
                    "UseAllCores = 1\r\n" +
                    "r.TemporalAACurrentFrameWeight = 0.2\r\n" +
                    "r.TemporalAAFilterSize = 1.0\r\n" +
                    "r.TemporalAASamples = 4\r\n" +
                    "r.TemporalAASharpness = 0\r\n" +
                    "D3D11.AFRUseFramePacing = 1\r\n" +
                    "D3D11.Aftermath = 0\r\n" +
                    "D3D11.AsyncDeferredDeletion = 1\r\n" +
                    "D3D11.ForceThirtyHz = 0\r\n" +
                    "D3D11.InsertOuterOcclusionQuery = 1\r\n" +
                    "D3D11.MaximumFrameLatency = 3\r\n" +
                    "D3D11.ResidencyManagement = 1\r\n" +
                    "D3D12.AFRUseFramePacing = 1\r\n" +
                    "D3D12.Aftermath = 0\r\n" +
                    "D3D12.AsyncDeferredDeletion = 1\r\n" +
                    "D3D12.ForceThirtyHz = 0\r\n" +
                    "D3D12.InsertOuterOcclusionQuery = 1\r\n" +
                    "D3D12.MaximumFrameLatency = 3\r\n" +
                    "D3D12.ResidencyManagement = 1\r\n" +
                    "r.AsyncCompute = 1\r\n" +
                    "r.AsyncCompute.AdaptiveBuffer = 1\r\n" +
                    "r.AOAsyncBuildQueue = 1\r\n" +
                    "r.AsyncCompute.ParallelDispatch = 1\r\n" +
                    "r.AsyncCreateLightPrimitiveInteractions = 1\r\n" +
                    "r.AsyncPipelineCompile = 1\r\n" +
                    "r.CompileShadersForDevelopment = 0\r\n" +
                    "r.CookOutUnusedDetailModeComponents = 1\r\n" +
                    "r.D3D.RemoveUnusedInterpolators = 1\r\n" +
                    "r.EnableAsyncComputeTranslucencyLightingVolumeClear = 1\r\n" +
                    "r.EnableAsyncComputeVolumetricFog = 1\r\n" +
                    "r.UseAsyncShaderPrecompilation = 1\r\n" +
                    "r.D3D11.GPUCrashDebuggingMode = 0\r\n" +
                    "r.D3D11.GPUTimeout = 0\r\n" +
                    "r.D3D12.GPUCrashDebuggingMode = 0\r\n" +
                    "r.D3D12.GPUTimeout = 0\r\n" +
                    "r.GPUCrash.Collectionenable = 0\r\n" +
                    "r.GPUCrash.DataDepth = 0\r\n" +
                    "r.GPUCrashDebugging.Aftermath.Callstack = 0\r\n" +
                    "r.GPUCrashDebugging.Aftermath.Markers = 0\r\n" +
                    "r.GPUCrashDebugging = 0\r\n" +
                    "r.GPUCrashDump = 0\r\n" +
                    "r.GPUDefrag.MaxRelocations = 0\r\n" +
                    "r.GraphicsThread.EnableBackgroundThreads = 1\r\n" +
                    "r.GraphicsThread.UseThreadedDestruction = 1\r\n" +
                    "r.GTSyncType = 2\r\n" +
                    "r.RHIThread = 1\r\n" +
                    "r.RHIThread.Priority = 2\r\n" +
                    "r.RHICmdUseParallelAlgorithms = 1\r\n" +
                    "r.RHICmdUseThread = 1\r\n" +
                    "RHI.ResourceTableCaching = 1\r\n" +
                    "RHI.SyncAllowEarlyKick = 1\r\n" +
                    "RHI.SyncThreshold = 999\r\n" +
                    "r.RHI.UseParallelDispatch = 1\r\n" +
                    "r.RHICmdBuffer.EnableThreadedCompletion = 1\r\n" +
                    "r.RHICmdBypass = 0\r\n" +
                    "r.IO.UseDirectStorage = 1\r\n" +
                    "r.IO.VirtualTextures = 1\r\n" +
                    "r.Lumen.DiffuseIndirect.Allow = 1\r\n" +
                    "r.Lumen.DiffuseIndirect.AsyncCompute = 1\r\n" +
                    "r.Lumen.Scene.Lighting.AsyncCompute = 1\r\n" +
                    "r.Lumen.ScreenProbeGather.AsyncCompute = 1\r\n" +
                    "r.ParallelAnimationCacheConversion = 1\r\n" +
                    "r.ParallelAnimationCacheConversionAsync = 1\r\n" +
                    "r.ParallelAnimationCacheStreaming = 1\r\n" +
                    "r.ParallelAnimationCompression = 1\r\n" +
                    "r.ParallelAnimationCompressionAsync = 1\r\n" +
                    "r.ParallelAnimationEvaluation = 1\r\n" +
                    "r.ParallelAnimationRetargeting = 1\r\n" +
                    "r.ParallelAnimationRetargetingAsync = 1\r\n" +
                    "r.ParallelAnimationStreaming = 1\r\n" +
                    "r.ParallelAnimationStreamingAsync = 1\r\n" +
                    "r.ParallelAnimationUpdate = 1\r\n" +
                    "r.ParallelAsyncComputeSkinCache = 1\r\n" +
                    "r.ParallelAsyncComputeTranslucency = 1\r\n" +
                    "r.ParallelBasePass = 1\r\n" +
                    "r.ParallelBatchDispatch = 1\r\n" +
                    "r.ParallelCulling = 1\r\n" +
                    "r.ParallelDestruction = 1\r\n" +
                    "r.ParallelDistanceField = 1\r\n" +
                    "r.ParallelDistributedScene = 1\r\n" +
                    "r.ParallelGraphics = 1\r\n" +
                    "r.ParallelInitViews = 1\r\n" +
                    "r.ParallelLandscapeLayerUpdate = 1\r\n" +
                    "r.ParallelLandscapeSplatAtlas = 1\r\n" +
                    "r.ParallelLandscapeSplineSegmentCalc = 1\r\n" +
                    "r.ParallelLandscapeSplineUpdate = 1\r\n" +
                    "r.ParallelLightingBuild = 1\r\n" +
                    "r.ParallelLightingComposition = 1\r\n" +
                    "r.ParallelLightingInject = 1\r\n" +
                    "r.ParallelLightingPropagation = 1\r\n" +
                    "r.ParallelLightingSetup = 1\r\n" +
                    "r.ParallelMeshBuildUseJobCulling = 1\r\n" +
                    "r.ParallelMeshBuildUseJobMerging = 1\r\n" +
                    "r.ParallelMeshDrawCommands = 1\r\n" +
                    "r.ParallelMeshMerge = 1\r\n" +
                    "r.ParallelMeshProcessing = 1\r\n" +
                    "r.ParallelNavBoundsCalc = 1\r\n" +
                    "r.ParallelNavBoundsInit = 1\r\n" +
                    "r.ParallelNavBoundsUpdate = 1\r\n" +
                    "r.ParallelNavOctreeUpdate = 1\r\n" +
                    "r.ParallelParticleUpdate = 1\r\n" +
                    "r.ParallelPhysicsScene = 1\r\n" +
                    "r.ParallelPhysicsStepAsync = 1\r\n" +
                    "r.ParallelPostProcessing = 1\r\n" +
                    "r.ParallelPrePass = 1\r\n" +
                    "r.ParallelReflectionCaptures = 1\r\n" +
                    "r.ParallelReflectionEnvironment = 1\r\n" +
                    "r.ParallelRendering = 1\r\n" +
                    "r.ParallelRenderUploads = 1\r\n" +
                    "r.ParallelSceneCapture = 1\r\n" +
                    "r.ParallelSceneColorGather = 1\r\n" +
                    "r.ParallelShaderCompile = 1\r\n" +
                    "r.ParallelSkeletalClothBoundsCalc = 1\r\n" +
                    "r.ParallelSkeletalClothGather = 1\r\n" +
                    "r.ParallelSkeletalClothPrepareSim = 1\r\n" +
                    "r.ParallelSkeletalClothSimulate = 1\r\n" +
                    "r.ParallelSkeletalClothSkinning = 1\r\n" +
                    "r.ParallelSkeletalClothUpdate = 1\r\n" +
                    "r.ParallelSkeletalClothUpdateBounds = 1\r\n" +
                    "r.ParallelSkeletalClothUpdateVerts = 1\r\n" +
                    "r.ParallelTaskShaderCompilation = 1\r\n" +
                    "r.ParallelTonemapping = 1\r\n" +
                    "r.ParallelTranslucency = 1\r\n" +
                    "r.ParallelVelocity = 1\r\n" +
                    "r.ParallelZPrepass = 1\r\n" +
                    "r.PipelineStateCache.AsyncCompileAfterTypes = 1\r\n" +
                    "r.PreTileTextures = 1\r\n" +
                    "r.RDG.AsyncCompute = 1\r\n" +
                    "r.Renderer.UseGPUInstancing = 1\r\n" +
                    "r.RenderThread.EnableTaskGraphThread = 1\r\n" +
                    "r.RenderThread.Priority = 2\r\n" +
                    "r.ShaderLibrary.PrintExtendedStats = 0\r\n" +
                    "r.Shadow.Virtual.Enable = 1\r\n" +
                    "r.SkyAtmosphere.AerialPerspectiveLUT.FastApplyOnOpaque = 1\r\n" +
                    "r.SSS.Burley.EnableProfileIdCache = 1\r\n" +
                    "r.VRS.ContrastAdaptiveShading = 0\r\n" +
                    "r.VRS.Enable = 1\r\n" +
                    "r.VRS.EnableImage = 1\r\n" +
                    "r.VRS.Tier = 2\r\n" +
                    "r.VT.ParallelFeedbackTasks = 1\r\n" +
                    "r.TextureStreaming = 1\r\n" +
                    "r.TextureStreaming.DiscardUnusedMips = 1\r\n" +
                    "r.TextureStreaming.UseBackgroundThreadPool = 1\r\n" +
                    "r.TextureStreaming.UseDeferredLock = 1\r\n" +
                    "r.ThreadPool.BackgroundThreadPriority = 0\r\n" +
                    "r.ThreadPool.EnableBackgroundThreads = 1\r\n" +
                    "r.ThreadPool.EnableHighPriorityThreads = 1\r\n" +
                    "r.Streaming.DefragDynamicBounds = 1\r\n" +
                    "r.Streaming.LimitPoolSizeToVRAM = 1\r\n" +
                    "r.Streaming.MaxMipLevelReduction = 0\r\n" +
                    "r.Streaming.StressTest.ExtraAsyncLatency = 0\r\n" +
                    "r.Streaming.UseAsyncRequestsForDDC = 1\r\n" +
                    "r.Streaming.UseBackgroundThreadPool = 1\r\n" +
                    "r.Streaming.UseNewMetrics = 1\r\n" +
                    "r.Streaming.UsePerTextureBias = 1\r\n" +
                    "r.AllowMultiThreadedShaderCreation = 1\r\n" +
                    "r.DontLimitOnBattery = 1\r\n" +
                    "r.DumpGPU = 0\r\n" +
                    "r.Emitter.FastPoolEnable = 1\r\n" +
                    "r.EnableDebugSpam_GetObjectPositionAndScale = 0\r\n" +
                    "r.EnableMultiThreadedRendering = 1\r\n" +
                    "r.FinishCurrentFrame = 0\r\n" +
                    "r.ForceAllCoresForShaderCompiling = 1\r\n" +
                    "r.ForceOcclusionQueryBatching = 1\r\n" +
                    "r.GeometryCollection.Nanite.AsyncCompute = 1\r\n" +
                    "r.GeometryCollection.Nanite = 1\r\n" +
                    "r.NGX.LogLevel = 0\r\n" +
                    "r.OneFrameThreadLag = 1\r\n" +
                    "r.Shaders.Optimize = 1\r\n" +
                    "r.SupportAllShaderPermutations = 0\r\n" +
                    "r.ThreadedShaderCompilation = 1\r\n" +
                    "r.ShaderDrawDebug = 0\r\n" +
                    "r.UniformBufferPooling = 1\r\n" +
                    "PoolSizeVRAMPercentage = 70\r\n" +
                    "s.AsyncLoadingThreadEnabled = 1\r\n" +
                    "s.AsyncLoadingThreadPriority = 2\r\n" +
                    "s.MinBulkDataSizeForAsyncLoading = 262144\r\n" +
                    "s.ProcessPrestreamingRequests = 1\r\n" +
                    "gc.MultithreadedDestructionEnabled = 1\r\n" +
                    "gc.AllowParallelGC = 1\r\n" +
                    "bAllowAsynchronousShaderCompiling = 1\r\n" +
                    "bAllowCompilingThroughWorkerThreads = 1\r\n" +
                    "bAsyncShaderCompileWorkerThreads = 1\r\n" +
                    "bEnableOptimizedShaderCompilation = 1\r\n" +
                    "MaxShaderJobBatchSize = 150\r\n" +
                    "MaxShaderJobs = 1000\r\n" +
                    "NumUnusedShaderCompilingThreads = 2\r\n" +
                    "r.ShaderPipelineCache.AsyncCompileRate = 32\r\n" +
                    "r.ShaderPipelineCache.BackgroundBatchSize = 32\r\n" +
                    "r.ShaderPipelineCache.BatchTime = 3\r\n" +
                    "r.ShaderPipelineCache.Enabled = 1\r\n" +
                    "r.ShaderPipelineCache.PrecompileBatchTime = 5\r\n" +
                    "r.ShaderPipelineCache.PrecompileFrameTime = 20\r\n" +
                    "r.ShaderPipelineCache.StartupCache = 1\r\n" +
                    "bAllowMultiThreadedShaderCompile = 1\r\n" +
                    "bAllowShaderCompilingWorker = 1\r\n" +
                    "bOptimizeForLocalShaderBuilds = 1\r\n" +
                    "bUseBackgroundCompiling = 1\r\n" +
                    "WorkerThreadPriority = 0\r\n" +
                    "bEnableMouseSmoothing = 0\r\n" +
                    "bViewAccelerationEnabled = 0\r\n" +
                    "RawMouseInputEnabled = 1\r\n" +
                    "bAllowAsyncRenderThreadUpdates = 1\r\n" +
                    "bAllowThreadedRendering = 1\r\n" +
                    "p.AsyncSceneEnabled = 1\r\n" +
                    "p.Chaos.PerParticleCollision.ISPC = 1\r\n" +
                    "p.Chaos.Spherical.ISPC = 1\r\n" +
                    "p.Chaos.Spring.ISPC = 1\r\n" +
                    "p.Chaos.TriangleMesh.ISPC = 1\r\n" +
                    "p.Chaos.VelocityField.ISPC = 1\r\n" +
                    "p.Chaos.VisualDebuggerEnable = 0\r\n" +
                    "bEnableMultiCoreRendering = 1\r\n" +
                    "bAgreeToCrashUpload = 0\r\n" +
                    "bImplicitSend = 0\r\n" +
                    "r.NGX.DLSS.AutoExposure = 1\r\n" +
                    "r.NGX.DLSS.EnableAutoExposure = 1\r\n" +
                    "r.NIS.Enable = 0\r\n" +
                    "r.NGX.DLSS.PreferNISSharpen = 0\r\n" +
                    "r.NGX.DLSS.Sharpness = 0\r\n" +
                    "r.FidelityFX.FSR2.AutoExposure = 1\r\n" +
                    "r.FidelityFX.FSR3.AutoExposure = 1\r\n" +
                    "r.FidelityFX.FSR4.AutoExposure = 1\r\n" +
                    "r.FidelityFX.FSR.RCAS = 0\r\n" +
                    "r.FidelityFX.FSR2.Sharpness = 0.0\r\n" +
                    "r.FidelityFX.FSR3.Sharpness = 0.0\r\n" +
                    "r.FidelityFX.FSR4.Sharpness = 0.0\r\n" +
                    "r.BasePassForceOutputsVelocity = 1\r\n" +
                    "r.DefaultFeature.AntiAliasing = 2\r\n" +
                    "r.Reflections.Denoiser = 2\r\n" +
                    "r.Reflections.Denoiser.TemporalAccumulation = 1\r\n" +
                    "r.PostProcessAAQuality = 6\r\n" +
                    "r.TemporalAA.Algorithm = 1\r\n" +
                    "r.TemporalAA.Upsampling = 1\r\n" +
                    "bPromptForRemoteDebugging = 0\r\n" +
                    "bPromptForRemoteDebugOnEnsure = 0\r\n" +
                    "bEnableTelemetry = 0\r\n" +
                    "net.AllowAsyncLoading = 1\r\n");
        }
    }
}
