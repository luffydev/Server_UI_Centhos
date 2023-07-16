using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SDK.Singleton;

namespace SDK.Network
{
    public enum Opcodes
    {
        MSG_NONE,
        CMSG_CONNECT_CHALLENGE_REQUEST             = 0x0001,
        SMSG_CONNECT_CHALLENGE_RESPONSE            = 0x0002,
        CMSG_CONNECT_CLIENT_LIST_REQ               = 0X0003,
        SMSG_CONNECT_CLIENT_LIST_RESULT            = 0x0004,
    }
    public struct OpcodeStruct 
    {
        public uint id { get; set; }
        public String name { get; set;  }
        public Action<NetworkPacket, NetworkClient> handler { get; set; }
    }

    public class OpcodeStore : Singleton<OpcodeStore>
    {
        private Dictionary<uint, OpcodeStruct> mList = new Dictionary<uint, OpcodeStruct>();

        public void BuildOpcodeList()
        {
            /* 0x0000 */ StoreOpcode(Opcodes.MSG_NONE,                        "MSG_NONE",                             NetworkHandler.Handle_NULL);
            /* 0x0001 */ StoreOpcode(Opcodes.CMSG_CONNECT_CHALLENGE_REQUEST,  "CMSG_CONNECT_CHALLENGE_REQUEST",       NetworkHandler.Handle_NULL);
            /* 0x0002 */ StoreOpcode(Opcodes.SMSG_CONNECT_CHALLENGE_RESPONSE, "CMSG_CONNECT_CHALLENGE_REQUEST",       NetworkHandler.Handle_Connect_Challenge_Response);
        }

        private void StoreOpcode(Opcodes pOpcodeID, String pName, Action<NetworkPacket, NetworkClient> pHandler)
        {
            OpcodeStruct lStruct = new OpcodeStruct() 
            { 
                id = (UInt32)pOpcodeID, 
                name = pName,
                handler = pHandler
            };

            mList.Add(lStruct.id, lStruct);
        }

        public bool OpcodeExist(uint pOpcodeID)
        {
            return mList.ContainsKey(pOpcodeID);
        }

        public OpcodeStruct GetOpcodeStruct(uint pOpcodeID) 
        {
            OpcodeStruct lStruct = new OpcodeStruct();

            if (!OpcodeExist(pOpcodeID))
                return lStruct;

            return mList[pOpcodeID];
        }
    }
}
