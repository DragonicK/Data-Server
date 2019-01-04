using System;
using System.Collections.Generic;

namespace Data_Server.Data {
    [Serializable()]
    public sealed class Character {
        public int AccountId { get; set; }
        public int CharacterId { get; set; }
        public byte CharacterIndex { get; set; }
        public string Name { get; set; }
        public byte Classe { get; set; }
        public byte Sex { get; set; }
        public int Sprite { get; set; }
        public int OriginalSprite { get; set; }
        public byte Access { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Strength { get; set; }
        public int Endurance { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int Vitality { get; set; }
        public int Dexterity { get; set; }
        public int Points { get; set; }
        public int TitleID { get; set; }
        public byte TitleVisibility { get; set; }
        public byte PK { get; set; }
        public int Map { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public byte Dir { get; set; }
        public byte Muted { get; set; }
        public byte TutorialState { get; set; }
        public int CostumeId { get; set; }
        public int BindMap { get; set; }
        public int BindX { get; set; }
        public int BindY { get; set; }
        
        // Está atualmente conectado no servidor.
        public bool Connected { get; set; }
        public bool NeedSave { get; set; }

        public List<Spell> Spell { get; set; }
        public List<Variable> Variable { get; set; }
        public List<Inventory> Inventory { get; set; }
        public List<Hotbar> Hotbar { get; set; }
        public List<Title> Title { get; set; }
        public List<SpellBuff> SpellBuff { get; set; }
        public List<Currency> Currency { get; set; }
        public List<Craft> Craft { get; set; }
        public List<Achievement> Achievement { get; set; }
        public List<Inventory> Warehouse { get; set; }
        public List<Inventory> Equipment { get; set; }

        public Character() {
            Spell = new List<Spell>();
            Variable = new List<Variable>();
            Inventory = new List<Inventory>();
            Hotbar = new List<Hotbar>();
            Title = new List<Title>();
            SpellBuff = new List<SpellBuff>();
            Currency = new List<Currency>();
            Craft = new List<Craft>();
            Achievement = new List<Achievement>();
            Warehouse = new List<Inventory>();
            Equipment = new List<Inventory>();
        }
    }
}