using Data_Server.Communication;

namespace Data_Server.Network.ClientPacket {
    public sealed class CpCharacterLeftGame : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            var characterId = msg.ReadInt32();

            var character = Global.FindCharacterById(characterId);

            // O personagem é salvo quando deixa o jogo, portanto, não é mais necessário um update.
            // Esse caso somente é usado quando o programa é fechado e a lista de personagens é salva. 
            if (character != null) {
                character.NeedSave = false;
                character.Connected = false;
            }
        }
    }
}