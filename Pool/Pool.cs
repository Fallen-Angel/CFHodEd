using Homeworld2.IFF;
using System;
using System.IO;

namespace Pool
{
    public class Pool
    {
        public byte[] TextureData { get; }
        public byte[] MeshData { get; }
        public byte[] FaceData { get; }

        public static BinaryReader TextureStream { get; private set; }
        public static BinaryReader MeshStream { get; private set; }
        public static BinaryReader FaceStream { get; private set; }

        private Pool(byte[] textureData, byte[] meshData, byte[] faceData)
        {
            TextureData = textureData;
            MeshData = meshData;
            FaceData = faceData;
            
            TextureStream = new BinaryReader(new MemoryStream(textureData));
            MeshStream = new BinaryReader(new MemoryStream(meshData));
            FaceStream = new BinaryReader(new MemoryStream(faceData));
        }

        public static Pool Read(IFFReader IFF, ChunkAttributes ChunkAttributes)
        {
            var type = IFF.ReadUInt32();

            var compressedTextureDataLength = IFF.ReadUInt32();
            var decompressedTextureDataLength = IFF.ReadUInt32();

            var compressedTextureData = IFF.ReadBytes((int)compressedTextureDataLength);
            var decompressedTextureData = Xpress_Decompress(compressedTextureData, (int)decompressedTextureDataLength);
            
            var compressedMeshDataLength = IFF.ReadUInt32();
            var decompressedMeshDataLength = IFF.ReadUInt32();
            
            var compressedMeshData = IFF.ReadBytes((int)compressedMeshDataLength);
            var decompressedMeshData = Xpress_Decompress(compressedMeshData, (int)decompressedMeshDataLength);

            var compressedFaceDataLength = IFF.ReadUInt32();
            var decompressedFaceDataLength = IFF.ReadUInt32();

            var compressedFaceData = IFF.ReadBytes((int)compressedFaceDataLength);
            var decompressedFaceData = Xpress_Decompress(compressedFaceData, (int)decompressedFaceDataLength);

            return new Pool(decompressedTextureData, decompressedMeshData, decompressedFaceData);
        }

        public static byte[] Xpress_Decompress(byte[] inputBuffer, int outputSize)
        {
            int outputIndex = 0;
            int inputIndex = 0;
            int indicator = 0;
            int indicatorBit = 30;
            int length = 0;
            int offset = 0;

            int inputSize = inputBuffer.Length;

            var outputBuffer = new byte[outputSize];
            while ((outputIndex < outputSize) && (inputIndex < inputSize))
            {
                indicatorBit++;
                if (indicatorBit == 31)
                {
                    if (inputIndex + 3 >= inputSize) goto Done;
                    indicator = GetInt(inputBuffer, inputIndex);
                    inputIndex += sizeof(int);
                    indicatorBit = 0;
                }

                //* check whether the bit specified by IndicatorBit is set or not
                //* set in Indicator. For example, if IndicatorBit has value 4
                //* check whether the 4th bit of the value in Indicator is set

                if (((indicator >> indicatorBit) & 1) == 0)
                {
                    if (outputIndex >= outputSize)
                        goto Done;
                    outputBuffer[outputIndex] = inputBuffer[inputIndex];
                    inputIndex += sizeof(byte);
                    outputIndex += sizeof(byte);
                }
                else
                {
                    if (inputIndex + 1 >= inputSize)
                        goto Done;

                    var byte1 = inputBuffer[inputIndex];
                    if ((byte1 & 0b11) == 0)
                    {
                        length = 3;
                        offset = byte1 >> 2;
                        inputIndex += 1;
                    }
                    else if ((byte1 & 0b11) == 0b10)
                    {
                        length = ((byte1 >> 2) & 0b1111) + 3;
                        offset = (inputBuffer[inputIndex + 1] << 2) | (byte1 >> 6);
                        inputIndex += 2;
                    }
                    else if ((byte1 & 0b11) == 0b01)
                    {
                        length = 3;
                        offset = (inputBuffer[inputIndex + 1] << 6) | (byte1 >> 2);
                        inputIndex += 2;
                    }
                    else if ((byte1 & 0b111) == 0b111)
                    {
                        length = (((inputBuffer[inputIndex + 1] & 0b111) << 5) | (byte1 >> 3)) + 3;
                        offset = (inputBuffer[inputIndex + 3] << 13) | (inputBuffer[inputIndex + 2] << 5) | (inputBuffer[inputIndex + 1] >> 3);
                        inputIndex += 4;
                    }
                    else if ((byte1 & 0b11) == 0b11)
                    {
                        length = (byte1 >> 3) + 3;
                        offset = (inputBuffer[inputIndex + 2] << 8) | inputBuffer[inputIndex + 1];
                        inputIndex += 3;
                    }
                    else
                    {

                    }

                    //if ((OutputIndex > 0xD0) && (OutputIndex < 0xF0)) printf("--7 Len: %02X (%d)\n", Length, Length);
                    //if (Length > 280) printf("DECOMP DEBUG: [0x%08X]->[0x%08X] Len: %d Offset: %08X\n",
                    //    OutputIndex, InputIndex, Length, Offset);
                    while (length != 0)
                    {
                        try
                        {
                            //if ((outputIndex >= outputSize) || ((offset + 1) >= outputIndex)) break;
                            outputBuffer[outputIndex] = outputBuffer[outputIndex - offset - 1 + 1];
                            outputIndex += sizeof(byte);
                            length -= sizeof(byte);
                        }
                        catch
                        {
                            goto Done;
                        }
                    }
                }

            }

            Done:

            if (outputIndex < outputBuffer.Length)
            {
                var buffer = new byte[outputIndex];
                Buffer.BlockCopy(outputBuffer, 0, buffer, 0, outputIndex);
                outputBuffer = buffer;
            }


            return outputBuffer;
        }

        public static int GetInt(byte[] buffer, int offset)
        {
            return
                ((int)buffer[offset]) |
                ((int)buffer[offset + 1] << 8) |
                ((int)buffer[offset + 2] << 16) |
                ((int)buffer[offset + 3] << 24);
        }
    }
}
