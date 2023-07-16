using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public NetworkPacket(Opcodes pOpcode = Opcodes.MSG_NONE)
        {
            SetOpcode(pOpcode);

            mByteArray = new byte[byte.MaxValue];
            mStream = new MemoryStream(mByteArray);
            mWriter = new BinaryWriter(mStream);
        }

        public NetworkPacket(byte[] pPacket)
        {
            mStream = new MemoryStream(pPacket);
            //MemoryStreamEndianChanger.ChangeEndianness(mStream); // Inverse l'endianness du MemoryStream

            mReader = new BinaryReader(mStream);
            
            UInt32 lOpcode = ReadUint32();
            UInt32 lSize = ReadUint32();

            int test = 0;
  
        }

        public Opcodes GetOpcode()
        {
            return mOpcode;
        }

        public void SetOpcode(Opcodes pOpcode)
        {
            mOpcode = pOpcode;
        }

        /* ---------------------------
			  Reader part
	       ---------------------------
	    */


        public UInt32 ReadUint32()
        {
            return BitConverter.ToUInt32(mReader.ReadBytes(4).Reverse().ToArray());
        }

        /* ---------------------------
			  Writter part
	       ---------------------------
	    */

        public void WriteUInt32(UInt32 pValue)
        {
            mWriter.Write(pValue);
        }

        public void WriteString(String pString)
        {
            WriteBytes(Encoding.UTF8.GetBytes(pString));
        }

        public void WriteBytes(byte[] pBytes)
        {
            WriteUInt32(Convert.ToUInt32(pBytes.Length));
            mWriter.Write(pBytes);
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
                    lWriter.Write(Convert.ToUInt32(mStream.Length + 8));

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
