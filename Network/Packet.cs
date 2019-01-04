namespace Data_Server.Network {
    public enum Packet {
        AccountData = 1,
        DeleteChar,
        RequestCharacterData,
        CharacterData,
        CharacterSpell,
        CharacterInventory,
        CharacterEquipment,
        CharacterWarehouse,
        CharacterHotbar,
        CharacterTitle,
        CharacterVariable,
        CharacterSpellBuff,
        CharacterCurrency,
        CharacterCraft,
        CharacterCraftSupplement,
        CharacterEnchant,
        CharacterAchievement,
        NameExist,
        ContinueSavePlayer,
        RequestSaveCharacter,
        CharacterLeftGame,
        Ping
    }
}