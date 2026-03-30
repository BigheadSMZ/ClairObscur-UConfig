using System.IO;
using System.Collections.Generic;

namespace ClairObscurConfig
{
    internal class CameraINI
    {
        // The path to the INI file.
        public static string IniPath;

        // Instance of the INI file class.
        public static IniFile Class;
        public static List<string> EntriesList;

        // Normal Camera Settings
        public static string FieldOfView;
        public static string TargetArmLength;
        public static string SocketOffsetX;
        public static string SocketOffsetY;
        public static string SocketOffsetZ;
        public static string EnableCameraLag;
        public static string CameraLagMaxDistance;
        public static string CameraLagSpeed;

        // World Map Camera Settings
        public static string WorldMapFocalLength;
        public static string PartyArmLength;
        public static string PartyOffsetX;
        public static string PartyOffsetY;
        public static string PartyOffsetZ;
        public static string EsquieArmLength;
        public static string EsquieOffsetX;
        public static string EsquieOffsetY;
        public static string EsquieOffsetZ;

        public static bool Initialize()
        {
            IniPath = Config.BasePath + "\\data\\Sandfall\\Binaries\\Win64\\CameraConfig.ini";

            if (!IniPath.TestPath())
                return false;

            CameraINI.Class = new IniFile(CameraINI.IniPath);
            CameraINI.EntriesList = new List<string> { };
            return true;
        }

		public static void LoadValues()
		{
            // Normal Camera Settings
            CameraINI.FieldOfView           = CameraINI.Class.Read("FOV", "CameraSettings");
            CameraINI.TargetArmLength       = CameraINI.Class.Read("TARGET_ARM_LENGTH", "CameraSettings");
            CameraINI.SocketOffsetX         = CameraINI.Class.Read("SOCKET_OFFSET_X", "CameraSettings");
            CameraINI.SocketOffsetY         = CameraINI.Class.Read("SOCKET_OFFSET_Y", "CameraSettings");
            CameraINI.SocketOffsetZ         = CameraINI.Class.Read("SOCKET_OFFSET_Z", "CameraSettings");
            CameraINI.EnableCameraLag       = CameraINI.Class.Read("ENABLE_CAMERA_LAG", "CameraSettings");
            CameraINI.CameraLagMaxDistance  = CameraINI.Class.Read("CAMERA_LAG_MAX_DISTANCE", "CameraSettings");
            CameraINI.CameraLagSpeed        = CameraINI.Class.Read("CAMERA_LAG_SPEED", "CameraSettings");

            // World Map Settings
            CameraINI.WorldMapFocalLength   = CameraINI.Class.Read("WORLDMAP_FOCAL_LENGTH", "WorldMapSettings");
            CameraINI.PartyArmLength        = CameraINI.Class.Read("PARTY_ARM_LENGTH", "WorldMapSettings");
            CameraINI.PartyOffsetX          = CameraINI.Class.Read("PARTY_OFFSET_X", "WorldMapSettings");
            CameraINI.PartyOffsetY          = CameraINI.Class.Read("PARTY_OFFSET_Y", "WorldMapSettings");
            CameraINI.PartyOffsetZ          = CameraINI.Class.Read("PARTY_OFFSET_Z", "WorldMapSettings");
            CameraINI.EsquieArmLength       = CameraINI.Class.Read("ESQUIE_ARM_LENGTH", "WorldMapSettings");
            CameraINI.EsquieOffsetX         = CameraINI.Class.Read("ESQUIE_OFFSET_X", "WorldMapSettings");
            CameraINI.EsquieOffsetY         = CameraINI.Class.Read("ESQUIE_OFFSET_Y", "WorldMapSettings");
            CameraINI.EsquieOffsetZ         = CameraINI.Class.Read("ESQUIE_OFFSET_Z", "WorldMapSettings");
		}

        public static void WriteValues()
        {
            // Normal Camera Settings
            CameraINI.Class.Write("FOV", CameraINI.FieldOfView, "CameraSettings");
            CameraINI.Class.Write("TARGET_ARM_LENGTH", CameraINI.TargetArmLength, "CameraSettings");
            CameraINI.Class.Write("SOCKET_OFFSET_X", CameraINI.SocketOffsetX, "CameraSettings");
            CameraINI.Class.Write("SOCKET_OFFSET_Y", CameraINI.SocketOffsetY, "CameraSettings");
            CameraINI.Class.Write("SOCKET_OFFSET_Z", CameraINI.SocketOffsetZ, "CameraSettings");
            CameraINI.Class.Write("ENABLE_CAMERA_LAG", CameraINI.EnableCameraLag, "CameraSettings");
            CameraINI.Class.Write("CAMERA_LAG_MAX_DISTANCE", CameraINI.CameraLagMaxDistance, "CameraSettings");
            CameraINI.Class.Write("CAMERA_LAG_SPEED", CameraINI.CameraLagSpeed, "CameraSettings");

            // World Map Settings
            CameraINI.Class.Write("WORLDMAP_FOCAL_LENGTH", CameraINI.WorldMapFocalLength, "WorldMapSettings");
            CameraINI.Class.Write("PARTY_ARM_LENGTH", CameraINI.PartyArmLength, "WorldMapSettings");
            CameraINI.Class.Write("PARTY_OFFSET_X", CameraINI.PartyOffsetX, "WorldMapSettings");
            CameraINI.Class.Write("PARTY_OFFSET_Y", CameraINI.PartyOffsetY, "WorldMapSettings");
            CameraINI.Class.Write("PARTY_OFFSET_Z", CameraINI.PartyOffsetZ, "WorldMapSettings");
            CameraINI.Class.Write("ESQUIE_ARM_LENGTH", CameraINI.EsquieArmLength, "WorldMapSettings");
            CameraINI.Class.Write("ESQUIE_OFFSET_X", CameraINI.EsquieOffsetX, "WorldMapSettings");
            CameraINI.Class.Write("ESQUIE_OFFSET_Y", CameraINI.EsquieOffsetY, "WorldMapSettings");
            CameraINI.Class.Write("ESQUIE_OFFSET_Z", CameraINI.EsquieOffsetZ, "WorldMapSettings");
        }
    }
}
