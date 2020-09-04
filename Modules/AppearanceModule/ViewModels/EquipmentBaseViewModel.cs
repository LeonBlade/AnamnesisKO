﻿// Concept Matrix 3.
// Licensed under the MIT license.

namespace Anamnesis.AppearanceModule.ViewModels
{
	using System;
	using System.ComponentModel;
	using Anamnesis;
	using Anamnesis.AppearanceModule.Items;
	using Anamnesis.GameData;
	using Anamnesis.Memory;
	using Anamnesis.Services;
	using PropertyChanged;

	public abstract class EquipmentBaseViewModel : INotifyPropertyChanged
	{
		public static readonly DummyNoneItem NoneItem = new DummyNoneItem();
		public static readonly DummyNoneDye NoneDye = new DummyNoneDye();
		public static readonly NpcBodyItem NpcbodyItem = new NpcBodyItem();

		public readonly ActorViewModel Actor;

		protected Vector scale;
		protected ushort modelBase;
		protected ushort modelSet;
		protected ushort modelVariant;
		protected byte dyeId;

		private IItem item;
		private IDye dye;

		public EquipmentBaseViewModel(ItemSlots slot, ActorViewModel actor)
		{
			this.Actor = actor;
			this.Slot = slot;
		}

		public delegate void ChangedHandler();

		public event ChangedHandler Changed;
		public event PropertyChangedEventHandler PropertyChanged;

		public ItemSlots Slot
		{
			get;
			private set;
		}

		[AlsoNotifyFor(nameof(ModelSet), nameof(ModelBase), nameof(ModelVariant))]
		public IItem Item
		{
			get
			{
				return this.item;
			}

			set
			{
				IItem oldItem = this.item;
				this.item = value;

				if (value != null)
				{
					bool useSubModel = this.Slot == ItemSlots.OffHand && value.HasSubModel;

					this.modelSet = useSubModel ? value.SubModelSet : value.ModelSet;
					this.modelBase = useSubModel ? value.SubModelBase : value.ModelBase;
					this.modelVariant = useSubModel ? value.SubModelVariant : value.ModelVariant;
				}
				else
				{
					this.modelSet = 0;
					this.modelBase = 0;
					this.modelVariant = 0;
				}

				if (this.modelBase == 0)
					this.Dye = NoneDye;

				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ModelSet)));
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ModelBase)));
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ModelVariant)));

				if (oldItem != null && oldItem != this.item)
				{
					this.Apply();
				}
			}
		}

		public IDye Dye
		{
			get
			{
				return this.dye;
			}

			set
			{
				IDye oldDye = this.dye;
				this.dye = value;
				this.dyeId = this.dye != null ? value.Id : (byte)0;

				if (oldDye != null && oldDye != this.dye)
				{
					this.Apply();
				}
			}
		}

		public byte DyeId
		{
			get
			{
				return this.dyeId;
			}
			set
			{
				this.dyeId = value;
				this.Dye = this.GetDye();
			}
		}

		public Vector Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				if (this.scale == value)
					return;

				this.scale = value;
				this.Apply();
			}
		}

		[DependsOn(nameof(EquipmentBaseViewModel.Item))]
		public int Key
		{
			get
			{
				if (this.item == null)
					return 0;

				return this.Item.Key;
			}
			set
			{
				IItem item = GameDataService.Items.Get(value);

				if (item != null && item.FitsInSlot(this.Slot))
				{
					this.Item = item;
				}
			}
		}

		[DependsOn(nameof(EquipmentBaseViewModel.Item))]
		public ushort ModelBase
		{
			get
			{
				return this.modelBase;
			}
			set
			{
				this.modelBase = value;
				this.Item = this.GetItem();
			}
		}

		[DependsOn(nameof(EquipmentBaseViewModel.Item))]
		public ushort ModelVariant
		{
			get
			{
				return this.modelVariant;
			}

			set
			{
				this.modelVariant = value;
				this.Item = this.GetItem();
			}
		}

		[DependsOn(nameof(EquipmentBaseViewModel.Item))]
		public ushort ModelSet
		{
			get
			{
				return this.modelSet;
			}
			set
			{
				this.modelSet = value;
				this.Item = this.GetItem();
			}
		}

		public abstract void Dispose();

		public abstract void Apply();

		protected IItem GetItem()
		{
			if (this.ModelBase == 0 && this.modelVariant == 0)
				return NoneItem;

			if (this.ModelBase == NpcbodyItem.ModelBase)
				return NpcbodyItem;

			foreach (IItem tItem in GameDataService.Items.All)
			{
				if (this.Slot == ItemSlots.MainHand || this.Slot == ItemSlots.OffHand)
				{
					if (!tItem.IsWeapon)
						continue;
				}
				else
				{
					if (!tItem.FitsInSlot(this.Slot))
						continue;
				}

				// Big old hack, but we prefer the emperors bracelets to the promise bracelets (even though they are the same model)
				if (this.Slot == ItemSlots.Wrists && tItem.Name.StartsWith("Promise of"))
					continue;

				if (this.Slot == ItemSlots.MainHand || this.Slot == ItemSlots.OffHand)
				{
					if (tItem.ModelSet == this.modelSet && tItem.ModelBase == this.ModelBase && tItem.ModelVariant == this.ModelVariant)
					{
						return tItem;
					}

					if (tItem.HasSubModel && tItem.SubModelSet == this.modelSet && tItem.SubModelBase == this.ModelBase && tItem.SubModelVariant == this.ModelVariant)
					{
						return tItem;
					}
				}
				else
				{
					if (tItem.ModelBase == this.ModelBase && tItem.ModelVariant == this.ModelVariant)
					{
						return tItem;
					}
				}
			}

			foreach (IItem tItem in Module.Props)
			{
				if (tItem.ModelSet == this.ModelSet && tItem.ModelBase == this.ModelBase && tItem.ModelVariant == this.ModelVariant)
				{
					return tItem;
				}
			}

			return new DummyItem(this.ModelSet, this.ModelBase, this.ModelVariant);
		}

		protected IDye GetDye()
		{
			// None
			if (this.DyeId == 0)
				return NoneDye;

			foreach (IDye dye in GameDataService.Dyes.All)
			{
				if (dye.Id == this.DyeId)
				{
					return dye;
				}
			}

			return NoneDye;
		}
	}
}
