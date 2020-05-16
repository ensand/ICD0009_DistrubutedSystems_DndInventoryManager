using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IArmorService Armors { get; }
        public IDndCharacterService DndCharacters { get; }
        public IMagicalItemService MagicalItems { get; }
        public IOtherEquipmentService OtherEquipments { get; }
        public IWeaponService Weapons { get; }
    }
}