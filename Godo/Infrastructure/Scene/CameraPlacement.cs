namespace Godo.Infrastructure.Scene
{
    public class CameraPlacement
    {
        public static byte[] SceneCamera(byte[] data, int o, bool[] formationOptions, int k, byte[]camera)
        {
            int r = 0;
            while (r < 4)
            {
                if ((data[o] != 255 && data[o + 1] != 255) && (formationOptions[0] || formationOptions[1]))
                {
                    // Using the byte array to retain camera data
                    // Primary Battle Idle Camera Position
                    data[o] = camera[k]; o++; k++; // Camera X Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Camera Y Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Camera Z Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Focus X Direction
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Focus Y Direction
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++;// Focus Z Direction
                    data[o] = camera[k]; o++; k++;


                    // Secondary Battle Idle Camera Position
                    data[o] = camera[k]; o++; k++; // Camera X Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Camera Y Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Camera Z Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Focus X Direction
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Focus Y Direction
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Focus Z Direction
                    data[o] = camera[k]; o++; k++;


                    // Tertiary Battle Idle Camera Position
                    data[o] = camera[k]; o++; k++;// Camera X Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++;// Camera Y Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++; // Camera Z Position
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++;// Focus X Direction
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++;// Focus Y Direction
                    data[o] = camera[k]; o++; k++;

                    data[o] = camera[k]; o++; k++;// Focus Z Direction
                    data[o] = camera[k]; o++; k++;

                    // Unused Battle Camera Position - FF Padding
                    o += 12;
                }
                else
                {
                    // Skip and retain data
                    o += 48;
                }
                r++;
                k = 0;
            }
            return data;
        }
    }
}
