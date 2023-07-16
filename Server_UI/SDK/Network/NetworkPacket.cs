using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK.Singleton;

namespace SDK.Network
{

    public class MemoryStreamEndianChanger
    {
        public static byte[] ChangeEndianness(byte[] pPacket)
        {
            return pPacket.Reverse().ToArray();
        }
    }

    public class NetworkPacket
    {
        byte[] mByteArray;
        MemoryStream mStream;
        BinaryWriter mWriter;
        BinaryReader mReader;

        Opcodes mOpcode = Opcodes.MSG_NONE;
        UInt32 mSize = 0;
        UInt64 mTime_t = 0;

        public NetworkPacket(Opcodes pOpcode = Opcodes.MSG_NONE)
        {
            DateTimeOffset now = DateTimeOffset.Now;

            SetOpcode(pOpcode);
            SetSize(0);
            SetTimestamp((UInt64) now.ToUnixTimeSeconds());

            mByteArray = new byte[byte.MaxValue];
            mStream = new MemoryStream(mByteArray);
            mWriter = new BinaryWriter(mStream);
            mReader = new BinaryReader(mStream);
        }

        public NetworkPacket(byte[] pPacket)
        {
            mByteArray = pPacket;
            mStream = new MemoryStream(mByteArray);
            mReader = new BinaryReader(mStream);
            mWriter = new BinaryWriter(mStream);
            
            UInt32 lOpcode = ReadUint32();
            UInt32 lSize = ReadUint32();
            UInt64 lTime_t = ReadUint64();

            SetOpcode((Opcodes)lOpcode);
            SetSize(lSize);
            SetTimestamp(lTime_t);
  
        }

        public Opcodes GetOpcode()
        {
            return mOpcode;
        }

        public UInt32 GetSize()
        {
            return mSize;
        }

        public UInt64 GetTimestamp()
        {
            return mTime_t;
        }

        public void SetOpcode(Opcodes pOpcode)
        {
            mOpcode = pOpcode;
        }

        public void SetSize(UInt32 pSize)
        {
            mSize = pSize;
        }

        public void SetTimestamp(UInt64 pTimestamp)
        {
            mTime_t = pTimestamp;
        }

        /* ---------------------------
			  Reader part
	       ---------------------------
	    */

        public byte[] ReadBytes()
        {
            UInt32 lSize = ReadUint32();
            byte[] lBytes = mReader.ReadBytes((int)lSize);

            return lBytes;
        }

        public String ReadString()
        {
            byte[] lBytes = ReadBytes();

            if(lBytes.Length == 0)
                return "";

            return Encoding.UTF8.GetString(lBytes);
        }

        public UInt64 ReadUint64()
        {
            return BitConverter.ToUInt64(mReader.ReadBytes(8).Reverse().ToArray());
        }

        public UInt32 ReadUint32()
        {
            return BitConverter.ToUInt32(mReader.ReadBytes(4).Reverse().ToArray());
        }

        public byte ReadByte()
        {
            return mReader.ReadByte();
        }

        /* ---------------------------
			  Writter part
	       ---------------------------
	    */

        public void WriteUInt64(UInt64 pValue)
        {
            mWriter.Write(pValue);
            SetSize(GetSize() + sizeof(UInt64));
        }

        public void WriteUInt32(UInt32 pValue)
        {
            mWriter.Write(pValue);
            SetSize(GetSize() + sizeof(UInt32));
        }

        public void WriteString(String pString)
        {
            WriteBytes(Encoding.UTF8.GetBytes(pString));

        }

        public void WriteBytes(byte[] pBytes)
        {
            WriteUInt32(Convert.ToUInt32(pBytes.Length));
            mWriter.Write(pBytes);

            SetSize(GetSize() + sizeof(UInt32) + (uint)pBytes.Length);
        }


        /* ---------------------------
			    Build our Packet
	       ---------------------------
	    */

        public byte[] Build()
        {
            using (MemoryStream lTemp = new MemoryStream())
            {
                using (BinaryWriter lWriter = new BinaryWriter(lTemp))
                {
                    // We write our Opcode (uint32)
                    lWriter.Write(Convert.ToUInt32(GetOpcode()));

                    // We write our packet size (uint32)
                    lWriter.Write(Convert.ToUInt32(mStream.Length + 16));

                    // We write our packet timestamp (uint64)
                    lWriter.Write(GetTimestamp());

                    // We write our packet content
                    lWriter.Write(mStream.ToArray());

                    mStream = new MemoryStream();
                    mStream.SetLength(0);

                    mByteArray = new byte[byte.MaxValue];

                    byte[] lArray = lTemp.ToArray();

                    return lArray;
                }

                
            }
        }
    }
}
