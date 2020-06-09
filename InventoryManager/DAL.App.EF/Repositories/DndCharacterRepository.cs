using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DndCharacterRepository : 
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.App.DndCharacter, DAL.App.DTO.DndCharacter>, 
        IDndCharacterRepository
    {
        public DndCharacterRepository(AppDbContext repoDbContext) 
            : base(repoDbContext, new com.enola.inventorymanager.DAL.Base.Mappers.BaseMapper<Domain.App.DndCharacter, DAL.App.DTO.DndCharacter>())
        {
        }


        public async Task<IEnumerable<DndCharacterSummary>> CustomGetAllAsync(Guid? userId = default, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            
            var dndCharacters = query
                .Include(e => e.Armor)
                .Include(e => e.Weapons)
                .Include(e => e.MagicalItems)
                .Include(e => e.OtherEquipment)
                .Select(e => new DndCharacterSummary
            {
                Id = e.Id,
                Comment = e.Comment,
                Name = e.Name,
                ArmorCount = e.Armor!.Count,
                MagicalItemCount = e.MagicalItems!.Count,
                OtherEquipmentCount = e.OtherEquipment!.Count,
                WeaponCount = e.Weapons!.Count,
                TreasureInGp = (float) e.PlatinumPieces * 10 + 
                               (float) e.GoldPieces + 
                               (float) e.ElectrumPieces / 2 + 
                               (float) e.SilverPieces / 10 + 
                               (float) e.CopperPieces / 100
            });

            var result = await dndCharacters.ToListAsync();

            return result;
        }

        public async Task<DndCharacter> CustomFirstOrDefaultAsync(Guid? id, Guid? userId = default, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntityQuery = await query
                .Include(e => e.Armor)
                .Include(e => e.Weapons)
                .Include(e => e.MagicalItems)
                .Include(e => e.OtherEquipment)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        
            var result = MapCharacterWithEquipment(domainEntityQuery);
        
            return result;
        }

        private DndCharacter MapCharacterWithEquipment(Domain.App.DndCharacter entity)
        {
            var itemValues = CalculateAllItemsWeightAndValue(entity.Armor, entity.Weapons, entity.MagicalItems, entity.OtherEquipment);
            
            var dalEntity = new DndCharacter()
            {
                AppUserId = entity.AppUserId,
                Id = entity.Id,
                Name = entity.Name,
                Comment = entity.Comment,
                PlatinumPieces = entity.PlatinumPieces,
                GoldPieces = entity.GoldPieces,
                ElectrumPieces = entity.ElectrumPieces,
                SilverPieces = entity.SilverPieces,
                CopperPieces = entity.CopperPieces,
                Armor = MapArmor(entity.Armor),
                Weapons = MapWeapons(entity.Weapons),
                MagicalItems = MapMagicalItems(entity.MagicalItems),
                OtherEquipment = MapOtherEquipments(entity.OtherEquipment),
                AllItemsWeight = itemValues.totalWeight,
                AllItemsValueInGp = itemValues.totalValue,
                // todo: future fix calculate in bll
                TreasureInGp = (float) entity.PlatinumPieces * 10 + 
                               (float) entity.GoldPieces + 
                               (float) entity.ElectrumPieces / 2 + 
                               (float) entity.SilverPieces / 10 + 
                               (float) entity.CopperPieces / 100
            };


            return dalEntity;
        }
        
        // TODO future fix: eraldi faili
        private ICollection<Armor> MapArmor(ICollection<Domain.App.Armor>? armors)
        {
            if (armors == null || armors.Count == 0)
                return new List<Armor>();

            ICollection<Armor> result = armors.Select(a => new Armor()
            {
                AppUserId = a.AppUserId,
                DndCharacterId = a.DndCharacterId,
                Id = a.Id,
                Name = a.Name,
                Comment = a.Comment,
                ArmorType = a.ArmorType,
                Ac = a.Ac,
                StealthDisadvantage = a.StealthDisadvantage,
                StrengthRequirement = a.StrengthRequirement,
                ValueInGp = a.ValueInGp,
                Weight = a.Weight,
                Quantity = a.Quantity
            }).ToList();

            return result;
        }
        
        private ICollection<Weapon> MapWeapons(ICollection<Domain.App.Weapon>? weapons)
        {
            if (weapons == null || weapons.Count == 0)
                return new List<Weapon>();

            ICollection<Weapon> result = weapons.Select(w => new Weapon()
            {
                AppUserId = w.AppUserId,
                DndCharacterId = w.DndCharacterId,
                Id = w.Id,
                Name = w.Name,
                Comment = w.Comment,
                DamageDice = w.DamageDice,
                DamageType = w.DamageType,
                WeaponType = w.WeaponType,
                WeaponRange = w.WeaponRange,
                Properties = w.Properties,
                ValueInGp = w.ValueInGp,
                Weight = w.Weight,
                Quantity = w.Quantity
            }).ToList();

            return result;
        }
        
        private ICollection<MagicalItem> MapMagicalItems(ICollection<Domain.App.MagicalItem>? magicalItems)
        {
            if (magicalItems == null || magicalItems.Count == 0)
                return new List<MagicalItem>();

            ICollection<MagicalItem> result = magicalItems.Select(mi => new MagicalItem()
            {
                AppUserId = mi.AppUserId,
                DndCharacterId = mi.DndCharacterId,
                Id = mi.Id,
                Name = mi.Name,
                Comment = mi.Comment,
                Spell = mi.Spell,
                MaxCharges = mi.MaxCharges,
                CurrentCharges = mi.CurrentCharges,
                ValueInGp = mi.ValueInGp,
                Weight = mi.Weight,
                Quantity = mi.Quantity
            }).ToList();

            return result;
        }

        private ICollection<OtherEquipment> MapOtherEquipments(ICollection<Domain.App.OtherEquipment>? equipments)
        {
            if (equipments == null || equipments.Count == 0)
                return new List<OtherEquipment>();

            ICollection<OtherEquipment> result = equipments.Select(oe => new OtherEquipment()
            {
                AppUserId = oe.AppUserId,
                DndCharacterId = oe.DndCharacterId,
                Id = oe.Id,
                Name = oe.Name,
                Comment = oe.Comment,
                ValueInGp = oe.ValueInGp,
                Weight = oe.Weight,
                Quantity = oe.Quantity
            }).ToList();

            return result;
        }
        
        private (float totalValue, float totalWeight) CalculateAllItemsWeightAndValue(ICollection<Domain.App.Armor>? armors,
            ICollection<Domain.App.Weapon>? weapons, ICollection<Domain.App.MagicalItem>? magicalItems,
            ICollection<Domain.App.OtherEquipment>? equipments)
        {
            float totalValue = 0;
            float totalWeight = 0;
            
            if (armors != null && armors.Count > 0)
            {
                foreach (var armor in armors)
                {
                    totalValue = totalValue + (armor.ValueInGp * armor.Quantity);
                    totalWeight = totalWeight + (armor.Weight * armor.Quantity);
                }
            }
            
            if (weapons != null && weapons.Count > 0)
            {
                foreach (var weapon in weapons)
                {
                    totalValue = totalValue + (weapon.ValueInGp * weapon.Quantity);
                    totalWeight = totalWeight + (weapon.Weight * weapon.Quantity);
                }
            }
            
            if (magicalItems != null && magicalItems.Count > 0)
            {
                foreach (var magicalItem in magicalItems)
                {
                    totalValue = totalValue + (magicalItem.ValueInGp * magicalItem.Quantity);
                    totalWeight = totalWeight + (magicalItem.Weight * magicalItem.Quantity);
                }
            }

            if (equipments != null && equipments.Count > 0)
            {
                foreach (var equipment in equipments)
                {
                    totalValue = totalValue + (equipment.ValueInGp * equipment.Quantity);
                    totalWeight = totalWeight + (equipment.Weight * equipment.Quantity);
                }
            }

            return (totalValue, totalWeight);
        }
    }
}