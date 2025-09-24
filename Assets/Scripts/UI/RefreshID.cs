namespace YunSun
{
	public enum RefreshID
	{
		//!< GameScene
		NetworkFailure,
		ChangeLoginID,
		LoginServerList,
		UserCharacterList,
		AlreadyLoginServer,
		LoginServerConnect,
		LoginServerWaiting,
		CharacterCreate,
		CharacterRename,
		CharacterDelete,
		CharacterDeleteCancel,
		GameServerConnect,

		//!< Setting
		OutputSetting,
		PushSetting,
		Withdrawal,
		Joystick,

		//!< GameUI
		Inventory,
		ItemEquip,
		ItemSort,
		ItemDelete,
		ItemUse,
		ItemLock,

		Chatting,
		ChattingFilter,
		ChattingBlackList,

		MenuOpen,
		MenuClose, //!< 다른 UI열었을 때 MainMenuUI 닫는 용도
		MainMenu,  //!< MainMenuUI 버튼 업데이트 용도(레벨업 등)

		Quest,
		Quest_New,
		Quest_NewGroup,
		Tutorial_ResetUIs,
		Tutorial_OpenMiniMapUI,
		Tutorial_SummonEnd,

		Party,
		Radar,
		Create,
		CreateDone,
		CreateCount,
		Dissolve,
		Collection,
		ShopUI,
		ShopNpcUI,
		Exchange,
		ExchangeRedDot,
		ExchangeRecentPrice,

		//<! 재화
		Gold,
		Dia,
		Mileage,
		MileageShow,
		MileageHide,

		//<! 캐릭터
		CharacterInfo,
		CharacterStat,
		CharacterStatChange,
		CharacterLevel,
		CharacterLevelDown,
		CharacterEquipPreset,
		AutoHPChange,
		Lawful,
		HP,
		MP,

		//!< 스킬
		SkillUse,
		SkillLearn,
		SkillTimer,
		SkillEnchant,
		SkillEnchantResult,
		Buff,

		//!< 퀵슬롯
		QuickSlot,
		QuickSlotSet,

		//!< 미니맵
		MapInfo,
		MapPoint,
		MapScale,
		MapMark,
		MapQuickUIClose, //!< #122710

		MailBox,
		PVPInfo,
		Ranking,
		RankingEx,
		Enchant,
		EnchantResult,
		MagicStone,
		MagicStoneResult,

		//!< Pet
		PetLock,
		PetBase,
		PetSummon,
		PetSynthesis,
		PetFailReward,
		PetExchange,
		PetConfirm,
		PetExtract,

		//!< Trans
		TransLock,
		TransBase,
		Transform,
		TransShowOriginal,
		TransSynthesis,
		TransFailReward,
		TransExchange,
		TransConfirm,
		TransExtract,

		//!< Raid
		RaidTime,
		RaidReward,
		RaidActive,
		RaidMove,
		FieldBossGen,

		//!< Dungeon
		DungeonList,
		DungeonTime,
		DungeonEntry,

		//!< Guild
		GuildMode,
		GuildList,
		GuildInfo,
		GuildMember,
		GuildExchange,
		GuildDistribution,
		GuildDistributionHistory,
		GuildDistributionHistoryEx,
		GuildBuff,
		GuildUnion,
		GuildJoin,
		GuildJoinRequest,
		GuildDonation,
		GuildRank,
		GuildShopShow,
		GuildShopHide,

		//!< Guild Stronghold
		GuildStrongholdBuy,
		GuildStrongholdName,
		
		//!< Guild Task
		GuildTask,
		GuildTaskContentsInfo,
		GuildTaskContentsReward,
		GuildTaskDonateRoulette,
		GuildTaskWantedKillComplete,

		//!< Event
		CouponUse,
		Event,
		EventReward,
		EventList,
		EventTimeList,

		//!< Siege
		Siege,
		SiegeMove,
		SiegeStart,

		//!< ETC
		DailyTimer,
		Grace,
		GraceEnchant,
		Mission,
		MissionReward,
		Achievement,
		DeathPenalty,
		Restore,
		RestoreEx,
		GachaGet,
		GachaEnd,
		Interaction,
		Title,

		AutoMoveOn,
		AutoMoveOff,

		Stealth,
	}
}
