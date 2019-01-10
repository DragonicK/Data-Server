using System;
using System.Collections.Generic;
using System.Text;
using Data_Server.Data;

namespace Data_Server.Database {
    public sealed partial class DBGameDatabase {

        public bool ExistName(string name) {
            var query = "SELECT CharacterID FROM CharacterData WHERE Name=@Name";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.AddParameter("@Name", name);
            sqlCommand.SetCommand(query);

            var sqlReader = sqlCommand.ExecuteReader();
            var result = sqlReader.Read();

            sqlReader.Close();

            return result;
        }

        public void DeleteCharacter(int accountID, int characterIndex) {
            var query = "DeleteCharacter";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            sqlCommand.AddParameter("@UserId", accountID);
            sqlCommand.AddParameter("@SlotIndex", characterIndex);
            sqlCommand.Execute();
         }
        
        public void UpdateLasteLoginDate(long accountID) {
            var query = "UPDATE Account SET LastLoginDate=@LastLoginDate WHERE AccountID=@AccountID";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@LastLoginDate", DateTime.Now);
            sqlCommand.Execute();
        }

        #region Character Achievement
        public List<Achievement> GetCharacterAchievements(int characterId) {
            var query = "SELECT AchievementIndex, AchievementValue FROM CharacterAchievement WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterId);

            var sqlReader = sqlCommand.ExecuteReader();
            var list = new List<Achievement>();

            while (sqlReader.Read()) {
                var achievement = new Achievement() {
                    Index = (int)sqlReader.GetData("AchievementIndex"),
                    Value = (int)sqlReader.GetData("AchievementValue")
                };

                list.Add(achievement);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateAchievement(int characterId, List<Achievement> achievement) {
            var query = "UpdateAchievement";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < achievement.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", achievement[n].Index);
                sqlCommand.AddParameter("SlotValue", achievement[n].Value);
                sqlCommand.Execute();
            }
        }

        #endregion

        #region Character Craft
        public List<Craft> GetCharacterCraft(int characterID) {
            var query = "SELECT * FROM CharacterCraft WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterID);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<Craft>();

            while (sqlReader.Read()) {
                var craft = new Craft {
                    Recipes = new List<int>(),

                    Type = Convert.ToByte(sqlReader.GetData("CraftType")),
                    Index = Convert.ToByte(sqlReader.GetData("CraftIndex")),
                    Level = (int)sqlReader.GetData("CraftLevel"),
                    Experience = (int)sqlReader.GetData("CraftExperience")
                };

                var recipes = ((string)sqlReader.GetData("RecipeList")).Split(',');

                for (var i = 0; i < recipes.Length; i++) {
                    craft.Recipes.Add(Convert.ToInt32(recipes[i]));
                }

                list.Add(craft);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateCraft(int characterId, List<Craft> craft) {
            var query = "UpdateCraft";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < craft.Count; n++) {
                sqlCommand.ClearParameter();

                var recipes = string.Empty;
                for (var i = 0; i < craft[n].Recipes.Count; i++) {
                    recipes += craft[n].Recipes[i] + ",";
                }

                // Remove a última vírgula.
                recipes = recipes.Remove(recipes.Length - 1, 1);

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("CraftType", craft[n].Type);
                sqlCommand.AddParameter("SlotIndex", craft[n].Index);
                sqlCommand.AddParameter("CraftLevel", craft[n].Level);
                sqlCommand.AddParameter("Experience", craft[n].Experience);
                sqlCommand.AddParameter("Recipes", recipes);
                sqlCommand.Execute();
            }
        }
        #endregion

        #region Character Currency

        public List<Currency> GetCharacterCurrency(int characterID) {
            var query = "SELECT * FROM CharacterCurrency WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterID);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<Currency>();

            if (sqlReader.Read()) {
                var currencyDefault = new Currency() {
                    Index = 0,
                    Value = (int)sqlReader.GetData("DefaultCurrency")
                };

                var currency1 = new Currency() {
                    Index = 1,
                    Value = (int)sqlReader.GetData("Currency1")
                };

                var currency2 = new Currency() {
                    Index = 2,
                    Value = (int)sqlReader.GetData("Currency2")
                };

                list.Add(currencyDefault);
                list.Add(currency1);
                list.Add(currency2);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateCurrency(int characterId, List<Currency> currency) {
            var query = "UpdateCurrency";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            sqlCommand.AddParameter("CharId", characterId);
            sqlCommand.AddParameter("CurDefault", currency[0].Value);
            sqlCommand.AddParameter("Cur1", currency[1].Value);
            sqlCommand.AddParameter("Cur2", currency[2].Value);

            sqlCommand.Execute();  
        }

        #endregion

        #region Character Data
        public List<TempCharacter> GetCharacterList(int accountID) {
            var query = "SELECT CharacterID, CharacterIndex, Name, Classe, Sprite, Access FROM CharacterData WHERE AccountID=@AccountID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.AddParameter("@AccountID", accountID);
            sqlCommand.SetCommand(query);

            var sqlReader = sqlCommand.ExecuteReader();
            var list = new List<TempCharacter>();

            while (sqlReader.Read()) {
                var tChar = new TempCharacter() {
                    CharacterID = Convert.ToInt32(sqlReader.GetData("CharacterID")),
                    Index = Convert.ToInt32(sqlReader.GetData("CharacterIndex")),
                    Name = Convert.ToString(sqlReader.GetData("Name")),
                    Classe = Convert.ToByte(sqlReader.GetData("Classe")),
                    Sprite = Convert.ToInt32(sqlReader.GetData("Sprite")),
                    Access = Convert.ToByte(sqlReader.GetData("Access"))
                };

                list.Add(tChar);
            }

            sqlReader.Close();

            return list;
        }

        public int GetCharacterId(int accountId, int characterIndex) {
            var query = "SELECT CharacterID FROM CharacterData WHERE AccountID = @AccountID AND CharacterIndex = @CharacterIndex";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@AccountID", accountId);
            sqlCommand.AddParameter("@CharacterIndex", characterIndex);

            var sqlReader = sqlCommand.ExecuteReader();
            var characterId = 0;
            
            if (sqlReader.Read()) {
                characterId = (int)sqlReader.GetData("CharacterID");
            }

            sqlReader.Close();

            return characterId;
        }

        public Character GetCharacter(int accountId, int characterIndex) {
            var query = "SELECT * FROM CharacterData WHERE AccountID=@AccountID AND CharacterIndex=@CharacterIndex";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@AccountID", accountId);
            sqlCommand.AddParameter("@CharacterIndex", characterIndex);

            var sqlReader = sqlCommand.ExecuteReader();
            var character = new Character();

            if (sqlReader.Read()) {
                character.CharacterId = (int)sqlReader.GetData("CharacterID");
                character.CharacterIndex = (byte)characterIndex;
                character.Name = (string)sqlReader.GetData("Name");
                character.Classe = Convert.ToByte(sqlReader.GetData("Classe"));
                character.Sprite = (int)sqlReader.GetData("Sprite");
                character.OriginalSprite = (int)sqlReader.GetData("OriginalSprite");
                character.Access = Convert.ToByte(sqlReader.GetData("Access"));
                character.Level = (int)sqlReader.GetData("Level");
                character.Experience = (int)sqlReader.GetData("Experience");
                character.HP = (int)sqlReader.GetData("HP");
                character.MP = (int)sqlReader.GetData("MP");
                character.Strength = (int)sqlReader.GetData("Strength");
                character.Endurance = (int)sqlReader.GetData("Endurance");
                character.Intelligence = (int)sqlReader.GetData("Intelligence");
                character.Agility = (int)sqlReader.GetData("Agility");
                character.Vitality = (int)sqlReader.GetData("Vitality");
                character.Dexterity = (int)sqlReader.GetData("Dexterity");
                character.Points = (int)sqlReader.GetData("Points");
                character.PK = Convert.ToByte(sqlReader.GetData("PK"));
                character.TitleID = (int)sqlReader.GetData("TitleID");
                character.TitleVisibility = Convert.ToByte(sqlReader.GetData("TitleVisibility"));
                character.Map = (int)sqlReader.GetData("Map");
                character.X = (int)sqlReader.GetData("X");
                character.Y = (int)sqlReader.GetData("Y");
                character.Dir = Convert.ToByte(sqlReader.GetData("Dir"));
                character.Muted = Convert.ToByte(sqlReader.GetData("IsMuted"));
                character.TutorialState = Convert.ToByte(sqlReader.GetData("TutorialState"));
                character.BindMap = (int)sqlReader.GetData("BindMap");
                character.BindX = (int)sqlReader.GetData("BindX");
                character.BindY = (int)sqlReader.GetData("BindY");
            }

            sqlReader.Close();

            return character;
        }

        public int InsertCharacter(Character character) {
            var query = new StringBuilder();
            query.Append("INSERT INTO CharacterData (AccountID, CharacterIndex, Name, Classe, Sprite, OriginalSprite, Access, Level, ");
            query.Append("Experience, HP, MP, Strength, Endurance, Intelligence, Agility, Vitality, Dexterity, Points, ");
            query.Append("PK, TitleID, TitleVisibility, ");
            query.Append("Map, X, Y, Dir, ");
            query.Append("IsMuted, TutorialState, CostumeID) ");
            query.Append("VALUES (@AccountID, @CharacterIndex, @Name, @Classe, @Sprite, @OriginalSprite, @Access, @Level, ");
            query.Append("@Experience, @HP, @MP, @Strength, @Endurance, @Intelligence, @Agility, @Vitality, @Dexterity, @Points, ");
            query.Append("@PK, @TitleID, @TitleVisibility, ");
            query.Append("@Map, @X, @Y, @Dir, ");
            query.Append("@IsMuted, @TutorialState, @CostumeID)");

            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query.ToString());
            sqlCommand.AddParameter("@AccountID", character.AccountId);
            sqlCommand.AddParameter("@CharacterIndex", character.CharacterIndex);
            sqlCommand.AddParameter("@Name", character.Name);
            sqlCommand.AddParameter("@Classe", character.Classe);
            sqlCommand.AddParameter("@Sprite", character.Sprite);
            sqlCommand.AddParameter("@OriginalSprite", character.OriginalSprite);
            sqlCommand.AddParameter("@Access", character.Access);
            sqlCommand.AddParameter("@Level", character.Level);
            sqlCommand.AddParameter("@Experience", character.Experience);
            sqlCommand.AddParameter("@HP", character.HP);
            sqlCommand.AddParameter("@MP", character.MP);
            sqlCommand.AddParameter("@Strength", character.Strength);
            sqlCommand.AddParameter("@Endurance", character.Endurance);
            sqlCommand.AddParameter("@Intelligence", character.Intelligence);
            sqlCommand.AddParameter("@Agility", character.Agility);
            sqlCommand.AddParameter("@Vitality", character.Vitality);
            sqlCommand.AddParameter("@Dexterity", character.Dexterity);
            sqlCommand.AddParameter("@Points", character.Points);
            sqlCommand.AddParameter("@PK", character.PK);
            sqlCommand.AddParameter("@TitleID", character.TitleID);
            sqlCommand.AddParameter("@TitleVisibility", character.TitleVisibility);
            sqlCommand.AddParameter("@Map", character.Map);
            sqlCommand.AddParameter("@X", character.X);
            sqlCommand.AddParameter("@Y", character.Y);
            sqlCommand.AddParameter("@Dir", character.Dir);
            sqlCommand.AddParameter("@IsMuted", character.Muted);
            sqlCommand.AddParameter("@TutorialState", character.TutorialState);
            sqlCommand.AddParameter("@CostumeID", character.CostumeId);
            return sqlCommand.Execute();
        }

        public int UpdateCharacter(Character character) {
            var query = new StringBuilder();
            query.Append("UPDATE CharacterData SET Name = @Name, Classe = @Classe, Sprite = @Sprite, OriginalSprite = @OriginalSprite, Access = @Access, Level = @Level, ");
            query.Append("Experience = @Experience, HP = @HP, MP = @MP, ");
            query.Append("Strength = @Strength, Endurance = @Endurance, Intelligence = @Intelligence, Agility = @Agility, Vitality = @Vitality, Dexterity = @Dexterity, ");
            query.Append("Points = @Points, PK = @PK, ");
            query.Append("TitleID = @TitleID, TitleVisibility = @TitleVisibility, ");
            query.Append("Map = @Map, X = @X, Y = @Y, Dir = @Dir, ");
            query.Append("IsMuted = @IsMuted, TutorialState = @TutorialState, CostumeID = @CostumeID, ");
            query.Append("BindMap = @BindMap, BindX = @BindX, BindY = @BindY ");
            query.Append("WHERE CharacterID = @CharacterID");

            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query.ToString());
            sqlCommand.AddParameter("@CharacterID", character.CharacterId);
            sqlCommand.AddParameter("@Name", character.Name);
            sqlCommand.AddParameter("@Classe", character.Classe);
            sqlCommand.AddParameter("@Sprite", character.Sprite);
            sqlCommand.AddParameter("@OriginalSprite", character.OriginalSprite);
            sqlCommand.AddParameter("@Access", character.Access);
            sqlCommand.AddParameter("@Level", character.Level);
            sqlCommand.AddParameter("@Experience", character.Experience);
            sqlCommand.AddParameter("@HP", character.HP);
            sqlCommand.AddParameter("@MP", character.MP);
            sqlCommand.AddParameter("@Strength", character.Strength);
            sqlCommand.AddParameter("@Endurance", character.Endurance);
            sqlCommand.AddParameter("@Intelligence", character.Intelligence);
            sqlCommand.AddParameter("@Agility", character.Agility);
            sqlCommand.AddParameter("@Vitality", character.Vitality);
            sqlCommand.AddParameter("@Dexterity", character.Dexterity);
            sqlCommand.AddParameter("@Points", character.Points);
            sqlCommand.AddParameter("@PK", character.PK);
            sqlCommand.AddParameter("@TitleID", character.TitleID);
            sqlCommand.AddParameter("@TitleVisibility", character.TitleVisibility);
            sqlCommand.AddParameter("@Map", character.Map);
            sqlCommand.AddParameter("@X", character.X);
            sqlCommand.AddParameter("@Y", character.Y);
            sqlCommand.AddParameter("@Dir", character.Dir);
            sqlCommand.AddParameter("@IsMuted", character.Muted);
            sqlCommand.AddParameter("@TutorialState", character.TutorialState);
            sqlCommand.AddParameter("@CostumeID", character.CostumeId);
            sqlCommand.AddParameter("@BindMap", character.BindMap);
            sqlCommand.AddParameter("@BindX", character.BindX);
            sqlCommand.AddParameter("@BindY", character.BindY);
            return sqlCommand.Execute(); 
        }
        #endregion

        #region Character Hotbar
        public List<Hotbar> GetCharacterHotbar(int characterID) {
            var query = "SELECT * FROM CharacterHotbar WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterID);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<Hotbar>();

            while (sqlReader.Read()) {
                var hotbar = new Hotbar() {
                    Index = (int)sqlReader.GetData("SlotIndex"),
                    Value = (int)sqlReader.GetData("Value"),
                    Type = Convert.ToByte(sqlReader.GetData("Type"))
                };

                list.Add(hotbar);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateHotbar(int characterId, List<Hotbar> hotbar) {
            var query = "UpdateHotbar";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < hotbar.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("HotbarIndex", hotbar[n].Index);
                sqlCommand.AddParameter("HotbarValue", hotbar[n].Value);
                sqlCommand.AddParameter("HotbarType", hotbar[n].Type);
                sqlCommand.Execute();
            }
        }
        #endregion

        #region Character Spell
        public List<Spell> GetCharacterSpells(int characterId) {
            var query = "SELECT * FROM CharacterSpell WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterId);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<Spell>();

            while (sqlReader.Read()) {
                var spell = new Spell {

                    Index = (int)sqlReader.GetData("SpellIndex"),
                    ID = (int)sqlReader.GetData("SpellID"),
                    Uses = (int)sqlReader.GetData("SpellUses"),
                    Level = (int)sqlReader.GetData("SpellLevel")
                };

                list.Add(spell);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateSpell(int characterId, List<Spell> spell) {
            var query = "UpdateSpell";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < spell.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", spell[n].Index);
                sqlCommand.AddParameter("SpellId", spell[n].ID);
                sqlCommand.AddParameter("SpellUses", spell[n].Uses);
                sqlCommand.AddParameter("SpellLevel", spell[n].Level);
                sqlCommand.Execute();
            }
        }
        #endregion

        #region Character SpellBuff
        public List<SpellBuff> GetCharacterSpellBuff(int characterID) {
            var query = "SELECT * FROM CharacterSpellBuff WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterID);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<SpellBuff>();

            while (sqlReader.Read()) {
                var buff = new SpellBuff() {
                    Index = (int)sqlReader.GetData("BuffIndex"),
                    ID = (int)sqlReader.GetData("BuffID"),
                    Duration = (int)sqlReader.GetData("BuffDuration"),
                    Level = (int)sqlReader.GetData("BuffLevel")
                };

                list.Add(buff);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateSpellBuff(int characterId, List<SpellBuff> spellBuff) {
            var query = "UpdateSpellBuff";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < spellBuff.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", spellBuff[n].Index);
                sqlCommand.AddParameter("BuffId", spellBuff[n].ID);
                sqlCommand.AddParameter("BuffDuration", spellBuff[n].Duration);
                sqlCommand.AddParameter("BuffLevel", spellBuff[n].Level);
                sqlCommand.Execute();
            }
        }
        #endregion

        #region Character Title
        public List<Title> GetCharacterTitle(int characterID) {
            var query = "SELECT * FROM CharacterTitle WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterID);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<Title>();

            while (sqlReader.Read()) {
                var Title = new Title() {
                    Index = (int)sqlReader.GetData("TitleIndex"),
                    ID = (int)sqlReader.GetData("TitleID"),
                };

                list.Add(Title);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateTitle(int characterId, List<Title> title) {
            var query = "UpdateTitle";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < title.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", title[n].Index);
                sqlCommand.AddParameter("TitleId", title[n].ID);
                sqlCommand.Execute();
            }
        }  
        #endregion

        #region Character Variable
        public List<Variable> GetCharacterVariable(int characterID) {
            var query = "SELECT * FROM CharacterVariable WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterID);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<Variable>();

            while (sqlReader.Read()) {
                var variable = new Variable() {
                    ID = (int)sqlReader.GetData("VariableNum"),
                    Value = (int)sqlReader.GetData("VariableValue")
                };

                list.Add(variable);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateVariable(int characterId, List<Variable> variable) {
            var query = "UpdateVariable";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < variable.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", variable[n].ID);
                sqlCommand.AddParameter("Variable", variable[n].Value);
                sqlCommand.Execute();
            }
        }
        #endregion

        #region Character Equipment, Inventory, Warehouse
        public List<Inventory> GetCharacterItems(int characterID, InventoryType type) {
            string inventory = string.Empty;
            string inventoryIndex = string.Empty;

            if (type == InventoryType.Inventory) {
                inventory = "CharacterInventory";
                inventoryIndex = "InventoryIndex";
            }
            else if (type == InventoryType.Warehouse) {
                inventory = "CharacterWarehouse";
                inventoryIndex = "WarehouseIndex";
            }
            else if (type == InventoryType.Equipment) {
                inventory = "CharacterEquipment";
                inventoryIndex = "EquipmentIndex";
            }

            var query = $"SELECT * FROM {inventory} WHERE CharacterID = @CharacterID";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterID", characterID);

            var sqlReader = sqlCommand.ExecuteReader();

            var list = new List<Inventory>();

            while (sqlReader.Read()) {
                var warehouse = new Inventory() {
                    Index = (int)sqlReader.GetData(inventoryIndex),
                    ItemID = (int)sqlReader.GetData("ItemID"),
                    ItemValue = (int)sqlReader.GetData("ItemValue"),
                    ItemLevel = (int)sqlReader.GetData("ItemLevel"),
                    ItemBound = Convert.ToByte(sqlReader.GetData("ItemBound"))
                };

                list.Add(warehouse);
            }

            sqlReader.Close();
            return list;
        }

        public void UpdateEquipment(int characterId, List<Inventory> equipment) {
            var query = "UpdateEquipment";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < equipment.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", equipment[n].Index);
                sqlCommand.AddParameter("ItemId", equipment[n].ItemID);
                sqlCommand.AddParameter("ItemValue", equipment[n].ItemValue);
                sqlCommand.AddParameter("ItemLevel", equipment[n].ItemLevel);
                sqlCommand.AddParameter("ItemBound", equipment[n].ItemBound);
                sqlCommand.Execute();
            }
        }

        public void UpdateInventory(int characterId, List<Inventory> inventory) {
            var query = "UpdateInventory";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < inventory.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", inventory[n].Index);
                sqlCommand.AddParameter("ItemId", inventory[n].ItemID);
                sqlCommand.AddParameter("ItemValue", inventory[n].ItemValue);
                sqlCommand.AddParameter("ItemLevel", inventory[n].ItemLevel);
                sqlCommand.AddParameter("ItemBound", inventory[n].ItemBound);
                sqlCommand.Execute();
            }
        }

        public void UpdateWarehouse(int characterId, List<Inventory> warehouse) {
            var query = "UpdateWarehouse";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            for (var n = 0; n < warehouse.Count; n++) {
                sqlCommand.ClearParameter();

                sqlCommand.AddParameter("CharId", characterId);
                sqlCommand.AddParameter("SlotIndex", warehouse[n].Index);
                sqlCommand.AddParameter("ItemId", warehouse[n].ItemID);
                sqlCommand.AddParameter("ItemValue", warehouse[n].ItemValue);
                sqlCommand.AddParameter("ItemLevel", warehouse[n].ItemLevel);
                sqlCommand.AddParameter("ItemBound", warehouse[n].ItemBound);
                sqlCommand.Execute();
            }
        }
        #endregion
    }
}