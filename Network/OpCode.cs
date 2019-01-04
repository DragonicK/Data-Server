using System;
using System.Collections.Generic;
using Data_Server.Network.ClientPacket;
using Data_Server.Network.ServerPacket;

namespace Data_Server.Network {
    public sealed class OpCode {
        public static Dictionary<int, Type> RecvPacket = new Dictionary<int, Type>();
        public static Dictionary<Type, int> SendPacket = new Dictionary<Type, int>();

        public static void InitOpCode() {
            RecvPacket.Add((int)Packet.AccountData, typeof(CpRequestAccountData));
            RecvPacket.Add((int)Packet.DeleteChar, typeof(CpRequestDeleteChar));
            RecvPacket.Add((int)Packet.RequestCharacterData, typeof(CpRequestCharacterData));

            RecvPacket.Add((int)Packet.CharacterData, typeof(CpSaveCharacterData));
            RecvPacket.Add((int)Packet.CharacterSpell, typeof(CpSaveCharacterSpell));
            RecvPacket.Add((int)Packet.CharacterInventory, typeof(CpSaveCharacterInventory));
            RecvPacket.Add((int)Packet.CharacterEquipment, typeof(CpSaveCharacterEquipment)); 
            RecvPacket.Add((int)Packet.CharacterWarehouse, typeof(CpSaveCharacterWarehouse));
            RecvPacket.Add((int)Packet.CharacterHotbar, typeof(CpSaveCharacterHotbar));
            RecvPacket.Add((int)Packet.CharacterTitle, typeof(CpSaveCharacterTitle));
            RecvPacket.Add((int)Packet.CharacterVariable, typeof(CpSaveCharacterVariable));
            RecvPacket.Add((int)Packet.CharacterSpellBuff, typeof(CpSaveCharacterSpellBuff));
            RecvPacket.Add((int)Packet.CharacterCurrency, typeof(CpSaveCharacterCurrency));
            RecvPacket.Add((int)Packet.CharacterCraft, typeof(CpSaveCharacterCraft));
            RecvPacket.Add((int)Packet.CharacterAchievement, typeof(CpSaveCharacterAchievement));
            RecvPacket.Add((int)Packet.NameExist, typeof(CpRequestExistName));
            RecvPacket.Add((int)Packet.RequestSaveCharacter, typeof(CpRequestSaveCharacter));
            RecvPacket.Add((int)Packet.CharacterLeftGame, typeof(CpCharacterLeftGame));

            SendPacket.Add(typeof(SpAccountData), (int)Packet.AccountData);
            SendPacket.Add(typeof(SpCharacterList), (int)Packet.DeleteChar);
            SendPacket.Add(typeof(SpCharacterData), (int)Packet.RequestCharacterData);
            SendPacket.Add(typeof(SpCharacterSpell), (int)Packet.CharacterSpell);
            SendPacket.Add(typeof(SpCharacterInventory), (int)Packet.CharacterInventory);
            SendPacket.Add(typeof(SpCharacterEquipment), (int)Packet.CharacterEquipment);
            SendPacket.Add(typeof(SpCharacterWarehouse), (int)Packet.CharacterWarehouse);
            SendPacket.Add(typeof(SpCharacterHotbar), (int)Packet.CharacterHotbar);
            SendPacket.Add(typeof(SpCharacterTitle), (int)Packet.CharacterTitle);
            SendPacket.Add(typeof(SpCharacterVariable), (int)Packet.CharacterVariable);
            SendPacket.Add(typeof(SpCharacterSpellBuff), (int)Packet.CharacterSpellBuff);
            SendPacket.Add(typeof(SpCharacterCurrency), (int)Packet.CharacterCurrency);
            SendPacket.Add(typeof(SpCharacterCraft), (int)Packet.CharacterCraft);
            SendPacket.Add(typeof(SpCharacterAchievement), (int)Packet.CharacterAchievement);
            SendPacket.Add(typeof(SpExistName), (int)Packet.NameExist);
            SendPacket.Add(typeof(SpCharacterId), (int)Packet.ContinueSavePlayer);
            SendPacket.Add(typeof(SpPing), (int)Packet.Ping);
        }
    }
}