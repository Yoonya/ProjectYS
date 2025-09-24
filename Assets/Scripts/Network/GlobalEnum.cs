using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

namespace YunSun
{

public enum ServerVersion
{
	brNetProtocolVersion = 1008,
	brTStoreVersion = 263
};


public enum ServerTypeEnum
{
	eLoginServer		=	0,		// 로그인 서버
	eGameServer,	// 게임 서버
	eChattingServer	// 채팅 서버
};

public enum Error
{
	brNetProtocolOK = 0,
	eNewCharacterStatError,
	eNewCharacterNameError,
	eNewCharacterNoEmptySlot,
	eNewCharacterMakeIndexError,
	eAlreadyMakeCharacter,
	eCharacterFindError,
	eFindNotInventory,
	eMoveCancel
};

public enum ChunkSizeEnum
{
	ChunkSize_X = 16,
	ChunkSize_Y = 16,
};

public enum PlatformType
{
	kGoogle = 0,
	kTStore,
	kNStore,
	kApple,
	kGameCenter,
};

public enum RegionEnum
{
	kDev = 0,
	kSeoul,
	kHongkong,
	kTokyo,
	kSingapore,
	kOregon,
	kFankfurt,
};


public enum CharacterIDEnum
{
	eCharacterID_None = 0,
	eCharacterID_Knight = 1,
	eCharacterID_Elf= 2,
	eCharacterID_Sorcerer= 3,
	eCharacterID_Max,
};

public enum MaxEnum
{
	eMax_ServerIPURL = 255,
	eMax_ServerName = 20,

	eStartItemInvenSize = 30,
	eMaxItemInven = 300,
	eUserMaxLevel = 99,
	eMaxFriend = 30,
	eMaxBossPlayPoint = 10,

	kMaxEquipItem = 41,

	kMax_CharacterSlot = 4,
	kMax_CharacterSendLimit = 50,
	kMax_WarehouseInven = 500,
	kMax_WarehouseInOutMax = 18,

	kMax_ItemCreateCount = 30,
	kMax_ItemCreateMaxRate = 10000,
	kMax_ItemCreateMaterial = 8,
	kMax_ItemCreateRefundItemCount = 4,

	kMax_PartyMember = 4,

	kMax_PVPHistoryListSize = 50,
	kMax_ChatBlackListSize = 50,

	kMax_ShopAutoBuyListSize = 100,
	kMax_WarehouseAutoKeepListSize = 100,

	kMax_Integer = 2000000000,

	kMax_ShopSlot = 18,

	kMax_ItemCollectionSlot = 5,

	kMax_ItemBurn_Count = 20,

	kMax_Grace_LevelUp_MaterialCount = 3,

	kMax_SkillEnchantMaterial = 3,
	kMax_SkillEnchantRefundItem = 3,

	kMax_ItemRegistMax = 500,	// 거래소 단일 등록 품목 수량
	kMax_QuickSlotMax = 40,		// 퀵슬롯 최대 카운트
	kMax_CollectionRegist = 500,	// 한번에 보낼 수 있는 컬렉션 최대 갯수
	kMax_TempInvenSend = 300,			// 한번에 보낼 수 있는 확정인벤 최대 갯수
	kMax_PresetMax = 3,				//프리셋 최대 갯수
	kMax_PresetName_Length = 10,	//프리셋 네임 최대 길이

	eMax_ItemInfoSendSize = 200,	//인벤, 창고 최대 샌드 갯수
};

public enum CharacterInfoEnum
{
	eCharacterInfoEnum_BonusStatStartLevel = 2,
	eLawful_Max = 50000,
	eLawful_Min = -50000,
	eMoveSpeed = 420,
	eGoldItemTableID = 1001,
	eDia_TID = 1101,
	eShopDia_TID = 1102,
	eGuildPoint_TID = 2102,
	eGuildExp_TID = 2103,
	eGuildContribution_TID = 2104,
	eBlessingPowderItemTableID = 7001,

	eCantBattle_WeightRate = 85,
	eCantCharacterRecovery_WeightRate = 50,

	eMaxCharacterDeathDropItem = 3,

	eDeleteChracterLevel = 40,

	kPartyUIDStart = 1000000000,
};

public enum ItemInfoEnum
{
	kItemInfoEnum_CashItemGrade = 0,
	kItemInfoEnum_EventItemGrade = 9,
	kDropItemRandomValue = 1000000,
	kDropItem_MaxCount = 10,
	kMax_AdditionalOption = 5,
	kMax_ItmeEnchantMultipleCount = 18,
};

public enum NPCTypeEnum
{
	eNPCType_None = 0,
	eNPCType_Monster,
	eNPCType_NPC,
};

public enum NPCSubTypeEnum
{
	eNPCSubType_None = 0 ,
	eNPCSubType_NormalMonster = 1,
	eNPCSubType_FieldBossMonster = 2,
	eNPCSubType_WorldBossMonster = 3,
	eNPCSubType_EliteMonster,

	eNPCSubType_Warehouse = 1000,			// 창고
	eNPCSubType_Teleporter,			// 텔레포터
	eNPCSubType_NormalNPC,			// 일반 NPC (퀘스트 진행)
	eNPCSubType_Portal,				// 포탈
	eNPCSubType_SellShop,			// SellShop
	eNPCSubType_Inn,				// NPC 회복

	eNPCSubType_NPCShop_Start = 2000,		// NPC 상점 시작
	eNPCSubType_NPCShop,			// NPC 상점
	eNPCSubType_NPC_ChandleryShop,	// NPC 잡화상점
	eNPCSubType_NPC_SkillShop,		// NPC 잡화상점
	eNPCSubType_NPCShop_End,		// NPC 상점 끝

	eNPCSubType_Talker = 3000,	// 기능은 없고 맵에 구별되거나 표시되는 퀘스트 진행용
	eNPCSubType_Passerby,		// 기능 없는 NPC

	eNPCSubType_GuildShop = 4000,			// 길드 상점
	eNPCSubType_GuildGeneralShop,
	eNPCSubType_Tasks,

	eNPCSubType_Structure = 5000,
	eNPCSubType_CollisionDummy,		// 충돌 더미(보이지 않는 충돌 오브젝트)
};

public enum NPCTalkTypeEnum
{
	eNPCTalkType_None = 0,
	eNPCTalkType_Quest,
	eNPCTalkType_Message,
	eNPCTalkType_TeleportPopup,
	eNPCTalkType_SellShopOpen,
	eNPCTalkType_LongDistance,		// 거리가 너무 멀다
	eNPCTalkType_Tasks,
};

public enum ItemEquipSlotTypeEnum
{
	kEquipSlot_None = -1,
	kEquipSlot_Weapon = 0,			// 무기
	kEquipSlot_Helm = 1,			// 투구
	kEquipSlot_Armor = 2,			// 방어구
	kEquipSlot_Cloak = 3,			// 망토
	kEquipSlot_Glove = 4,			// 장갑
	kEquipSlot_Boots = 5,			// 부츠
	kEquipSlot_Tshirt = 6,			// 티셔츠
	kEquipSlot_SubEquipment = 7,	// 방패 / 보조무기
	kEquipSlot_Gaiter = 8,			// 각반
	kEquipSlot_Belt = 14,			// 벨트
	kEquipSlot_Necklace = 15,		// 목걸이
	kEquipSlot_Earring1 = 16,		// 귀걸이1
	kEquipSlot_Earring2 = 17,		// 귀걸이2
	kEquipSlot_Ring1 = 18,			// 반지1
	kEquipSlot_Ring2 = 19,			// 반지2
	kEquipSlot_Ring3 = 20,			// 반지3
	kEquipSlot_Ring4 = 21,			// 반지4
	kEquipSlot_Pendant = 22,		// 펜던트
	kEquipSlot_Runes = 23,			// 룬
	kEquipSlot_Bracelet1 = 24,		// 팔찌1
	kEquipSlot_Bracelet2 = 25,		// 팔찌2
	
	kEquipSlot_Sapphire = 30,		// 사파이어
	kEquipSlot_Topaz = 31,			// 토파즈
	kEquipSlot_Jade = 32,			// 비취
	kEquipSlot_Ruby = 33,			// 루비
	kEquipSlot_Amethyst = 34,		// 아멧
	kEquipSlot_Diamond = 35,		// 다이아몬드
	kEquipSlotMax = 41,
};

public enum Connect_LoginServer_Result
{
	eConnect_LoginServer_Result_Success = 0,	// 로그인 서버 접속 성공
	eConnect_LoginServer_Result_MakeIDDBError,	// DB에러로 생성 불가
	eConnect_LoginServer_Result_Unregist,		// 탈퇴한 계정
	eConnect_LoginServer_Result_Block,			// 블럭된 계정
	eConnect_LoginServer_Result_CS,				// CS 처리중인 계정
	eConnect_LoginServer_Result_ServerCheck,	// 점검 중
	eConnect_LoginServer_Result_InvalidDB,		// DB에러2
	eConnect_LoginServer_Result_InvalidToken,	// 유효하지 않은 토큰입니다.
	eConnect_LoginServer_Result_AlreadyLogin,	// 접속 중인 계정입니다. 접속할 수 없습니다.
	eConnect_LoginServer_Result_CanNotCreate_Server,	//캐릭생성이 제한된 서버입니다.
};

public enum AccountUnregistType
{
	AccountUnregistType_None = 0,	// 정상
	AccountUnregistType_Unregist,	// 1 탈퇴
	AccountUnregistType_Block,		// 2 제재 중
	AccountUnregistType_CS,			// 3 CS 처리 중
};

public enum ShopStoreType
{
	kShopStoreType_None = 0,
	kShopStoreType_Google,
	kShopStoreType_OneStore,
	kShopStoreType_Apple,
	kShopStoreType_Galaxy,
};

public enum LoginTypeEnum
{
	kLoginType_None = 0,
	kLoginType_Google,
	kLoginType_Facebook,
	kLoginType_Guest,
	kLoginType_GameCenter,
	kLoginType_AppleID,
};

public enum Connect_GameServer_Result
{
	kConnect_GameServer_Result_Success = 0,
	kConnect_GameServer_Result_IDError,			// 아이디를 찾을 수 없습니다.
	kConnect_GameServer_Result_AlreayLogin,		// 이미 접속중인 아이디 입니다.
	kConnect_GameServer_Result_ConnectDBError,	// 디비연결 오류
	kConnect_GameServer_Result_GameServerCheck,	// 서버 점검 중
	kConnect_GameServer_Result_InvalidToken,	// 유효하지 않은 토큰입니다.
};

public enum LoginCheckResult
{
	eLoginCheckSuccess = 0,
	eNotFoundId,			// 아이디를 찾을 수 없음
	eMissMatchPw,			// 비밀번호가 틀림
	eUnregistWait,
};

public enum UserMemberInfo
{
	eMinIdLength = 4,
	eMaxIdLength = 30,
	eMinPwLength = 6,
	eMaxPwLength = 12,

	eMax_MakeNickname = 8,
	eMaxNickNameLenth = 20,
	eMaxChrUIDLenth = 20,
};

public enum DayOfWeekEnum
{
	kDayNone = -1,
	kSun = 0,
	kMon,
	kTue,
	kWed,
	kThu,
	kFri,
	kSat
};

public enum DirectionEnum
{
		kDirectionLeftTop = 0 
	,	kDirectionTop		
	,	kDirectionRightTop
	,	kDirectionRight
	,	kDirectionRightDown
	,	kDirectionDown
	,	kDirectionLeftDown
	,	kDirectionLeft
	,	eDirectionMax
};

public enum ChattingTypeEnum
{
	kChattingType_Normal = 0,	// 일반 채팅
	kChattingType_World,		// 전체 채팅
	kChattingType_Guild,		// 길드 채팅
	kChattingType_Whisper,		// 귓속말 채팅
	kChattingType_Party,		// 파티
	kChattingType_System,		// 시스템 알림
	kChattingType_Alliance,		// 동맹 채팅
};

public enum AttributeTypeEnum
{
	kAttributeTypeEnum_None = 0,
	kAttributeTypeEnum_Water,
	kAttributeTypeEnum_Fire,
	kAttributeTypeEnum_Earth,
	kAttributeTypeEnum_Wind,
};

public enum GuildEnum
{
	kMaxGuildName = 8,
	kMaxGuildNotice = 100,
	kMaxGuildProfile = 60,
	kMaxGuildMember = 35,

	kMaxGuildSubMaster = 3,
	kMaxGuildMemberMassage = 12,

	kMaxGuildJoinWaitUserCount = 30,

	kMaxGuildRankList = 50,
	kMaxGuildSearchList = 10,
	kMaxGuildLevel = 20,

	kMaxEnemyGuild = 20,
	kMaxAllianceGuild = 5,
	kMaxGuildHistorySize = 100,

	kMaxStrongholdName = 8,
};

public enum GuildMemberType
{
	kGuildProbationMember = 0,
	kGuildNormalMember = 1,
	kGuildSubMaster = 2,
	kGuildMaster = 3,
};

public enum GuildPlayType
{
	kGuildPlayType_None = 0,
	kGuildPlayType_Attendence,			// 출석
	kGuildPlayType_WeeklyContribution,	// 주간 공헌도
};

public enum WorldMapType
{
	kWorldMapType_Invalid = -1,
	kWorldMapType_Normal = 0,			// 일반 맵
	kWorldMapType_GuildRaid,			// 길드 레이드 인스턴스
	kWorldMapType_GuildHouse,			// 길드 하우스 인스턴스
	kWorldMapType_TimeDungeon,			// 시간 던전
	kWorldMapType_BossRaid,				// 보스레이드 
	kWorldMapType_Siege,				// 공성존
	kWorldMapType_Stronghold,           // 길드 요새
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class UserCharacterInfo
{
	public Int64	_chrUID;	// 캐릭터 고유 번호 캐릭터UID
	public Int64	_userUID;	// 계정의 고유 번호 계정UID
	public Int32	_tableID;	// 테이블ID
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)(UserMemberInfo.eMaxNickNameLenth+1) )]
	public string	_nickName;	// 닉네임

	public Int64	_gold;	// 계정의 소지 골드

	public Int16	_itemInvenSize;	// 가방 최대 칸수
	public Int64	_createTime;	// 캐릭터 생성 시간

	public Int16	_chrLevel;	// 캐릭터 레벨
	public Int64	_chrExp;	// 경험치

	public Int32	_mapNum;	// 현재 위치한 맵 WorldMapTable ID
	public Int32	_x;	// 좌표
	public Int32	_y;
	public Int32	_z;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)ItemEquipSlotTypeEnum.kEquipSlotMax)]
	public Int64[]		_equipItem;	// 장착 아이템 UID 리스트

	public Int64	_dia; // 소지한 캐시 재화
	public Int64	_shopDia;	// 유료 다이아 잔여 수량

	public Int32	_lawful;	// 성향치
	public Int64	_guildUID;	// 길드UID
	public Int32	_autoTransformID;	// 변신 주문서 사용시 변신될 Transform TID
	public Int32	_summonTID;	// 펫 소환 시 소환할 펫 TID
	
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isTransformShowOriginal;	// 기본 외형으로 변신을 보여줄 것인지
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class LightUserCharacterInfo
{
	public Int64	_chrUID;			// 캐릭터 UID
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)(UserMemberInfo.eMaxNickNameLenth+1) )]
	public string	_nickName;			// 닉네임
	public Int16	_level;				// 레벨
	public Int64	_exp;				// 경험치
	public Int32	_tableID;			// CharacterTable ID
	public Int32	_deleteWaitSeconds;	// 삭제 대기 시간
	public byte		_nicknameChange;	// 닉네임 변경권 횟수
	public Int32	_worldMapTID;		// 맵 위치 WorldMapTable ID
	public Int64	_lastLoginTime;		// 마지막 접속 시간
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName + 1)]
	public string	_guildName;			// 길드명
	public Int32	_chrTotalRank;		// 전체 랭킹
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)(UserMemberInfo.eMaxChrUIDLenth + 1))]
	public string	_globalChrUID;
};

[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class SearchCharacterInfo
{
	public Int32	_serverNum;			// 서버 번호
	public Int64	_chrUID;			// 캐릭터 UID
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)(UserMemberInfo.eMaxNickNameLenth + 1))]
	public string	_nickName;			// 닉네임
	public Int16	_level;				// 레벨
	public Int32	_tableID;			// CharacterTable ID
	public Int64	_lastLoginTimeSec;	// 마지막 접속 시간(초)
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class ServerInfo
{
	public Int16	 _eType;	//ServerTypeEnum
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerIPURL+1)]
	public string	_ipAddr;	// 주소
	public Int32	_port;		// 포트
	public byte	_channel;		// 채널
};

[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class LoginServerInfo
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerIPURL+1)]
	public string		_ipAddr;	// 로그인 서버 url 주소
	public Int32		_port;			// 로그인 서버 포트
	public byte			_region;		// 리전 이넘
	public Int16		_serverNum;		// 서버 번호
	public Int32		_showIndox;		// 서버 순서
	public Int32		_connectUser;	// 동시 접속자 수
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isShow;		// true=리스트에 보여줌.
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isNew;			// true=신규 서버
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isUse;			// false=점검중
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isCreateCharacter;	// true=캐릭터생성 가능
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isRecommend;	// true=추천서버
	[MarshalAs(UnmanagedType.U1)]
	public bool		_defaultserver;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName+1)]
	public string		_name_kr;	// 한글 서버명
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName+1)]
	public string		_name_eng;	// 영문 서버명
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_jpn;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_scn;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_tcn;
};

[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class LoginServerInfoV2
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerIPURL + 1)]
	public string		_ipAddr;	// 로그인 서버 url 주소
	public Int32		_port;			// 로그인 서버 포트
	public byte			_region;		// 리전 이넘
	public Int16		_serverNum;		// 서버 번호
	public Int32		_showIndox;		// 서버 순서
	public Int32		_connectUser;	// 동시 접속자 수
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isShow;		// true=리스트에 보여줌.
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isNew;			// true=신규 서버
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isUse;			// false=점검 중
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isCreateCharacter;	// true=캐릭터생성 가능
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isRecommend;	// true=추천서버
	[MarshalAs(UnmanagedType.U1)]
	public bool		_defaultserver;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_kr;	// 한글 서버명
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_eng;	// 영문 서버명
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_jpn;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_scn;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)MaxEnum.eMax_ServerName + 1)]
	public string		_name_tcn;
};

public enum IntervalInfo
{
	eFriendSendInterval = 86400,
	eFriendDeleteInterval = 86400,
	eMailDeleteInterval_2Day = 172800,
};

public enum JoinResult
{
	eJoinOk = 0,
	eAlreadyMakeId,	// 이미 존재하는 아이디
	eShortId,		// 짧은 아이디
	eLongId,		// 긴 아이디
	eShortPw,		// 짧은 패스워드
	eLongPw,		// 긴 패스워드
	eWrongId,		// 잘못된 아이디
	eWrongPw,		// 잘못된 패스워드
	eMakeIDDBError,		// DB에 생성하지 못함.
	eMakeIDLimited,		// 디바이스 계정 생성 횟수 제한 초과
	eSameProposer,		// 같은 디바이스의 가입자에게 추천했다.
	eNotFoundProposer,	// 추천인을 찾을 수 없다.
};


public enum KickResult
{
	eAccessFromOtherDevice = 0, // 다른기기에서 접속했습니다.
};

public enum JoinChattingChannelResult
{
	kJoinChattingChannel_Success = 0,
	kJoinChattingChannel_OverChannel,	// 잘못된 채널
	kJoinChattingChannel_ChannelFull,	// 채널이 가득참.
};

public enum AccountMappingResult
{
	kAccountMapping_Success = 0,
	kAccountMapping_CurrentAccountAlreadyMapping,	// 현재 계쩡이 맵핑된 상태이다.
	kAccountMapping_SocialAccountOtherMapping,		// 소셜 게정이 다른 계정에 맵핑되어있다.
};

public enum ItemTypeEnum
{
	kItemTypeNone	= -1,
	kEtc			= 0,
	kUseItem		= 1,
	kEquipItem		= 2,
};

public enum ItemSubTypeEnum
{
	kItemSubTypeNone = -1,
	
	kWeaponTypeStart = 100,
	kWeapon_Dagger,			// 단검
	kWeapon_OneHandSword,	// 한손검
	kWeapon_TwoHandSword,	// 양손검
	kWeapon_Bow,			// 활
	kWeapon_Wand,			// 완드
	kWeapon_Scimitar,		// 시미터
	kWeaponTypeEnd,
	
	kArmorTypeStart = 200,
	kHelm,
	kArmor,
	kCloak,
	kGlove,
	kBoots,
	kTshirt,
	kSubEquipment,
	kGaiter,
	kArmorTypeEnd,
	
	kAccessoryTypeStart = 300,
	kBelt,
	kNecklace,
	kEarring,
	kRing,
	kPendant,
	kRunes,
	kBracelet,
	kGem_Sapphire,
	kGem_Topaz,
	kGem_Jade,
	kGem_Ruby,
	kGem_Amethyst,
	kGem_Diamond,
	kAccessoryTypeEnd,
	kEquipTypeMax,

	kItemSubType_Pet = 400,	// 마법인형
	kItemSubType_Transform =  500,	// 변신

	kUseItemStart = 3000,
	kItem_EnchantStone,		// 강화석
	kItem_ProtectEnchantStone,		// 보호 강화석
	kItem_AttributeStone,		// 속성 강화석
	kItem_AttributeChange,		// 속성 변경석
	kItemSubType_MagicBook,		// 공용 스킬북
	kItemSubType_KnightSkillBook,	// 기사 스킬북
	kItemSubType_ElfSkillBook,		// 궁사 스킬북
	kItem_Potion,			// 체력 회복 물약
	kItem_SpeedPotion,		// 체력 회복 물약
	kItemSubType_RandomBox,		// 랜덤 상자, 주머니
	kItemSubType_ItemBox,		// 고정 아이템 상자
	kItemSubType_Teleport,		// 이동관련 주문서
	kItem_OptionResetScroll,		// 리셋 스크롤
	kItem_GemEnchantScroll,		// 매직스톤 강화 주문서
	kItemSubType_TransformScroll,	// 변신 주문서
	kItemSubType_TransformScroll_Set,	// 확정 변신 주문서
	kItemSubType_SummonPet,		// 펫 소환
	kItemSubType_TransformRandomBox,	// 변신 뽑기
	kItemSubType_PetRandomBox,	// 펫 뽑기
	kItemSubType_PetBox,		// 펫 상자 (고정)
	kItemSubType_TransformBox,	// 변신카드 상자 (고정)
	kItemSubType_SummonNPC,		// NPC 소환 주문서	
	kItemSubType_Restore,		// 복구 티켓
	kItemSubType_ExpPlus,		// 경험치 주문서
	kItem_LawfulRecoveryBook,		// 라우풀 증가 책
	kItemSubType_CharacterBless,	// 캐릭터 축복 아이템
	kItemSubType_DungeonTimePlus,	// 시간 던전 충전 아이템
	kItemSubType_NickNameChange,	// 닉네임 변경권
	kItem_ExpRecoveryScroll,		// 경험치 복구 스크롤
	kItemSubType_WizardSkillBook,	// 마법사 스킬북
	kItemSubType_QuestScroll,		// 퀘스트 스크롤
	kUseItemEnd,
	
	kItem_ReturnTown,				// 귀환

	kItemSubType_Permit,			// NPC허가증서(인벤X 구매 시 즉시 사용 개념)
	
	kItem_Warehouse_Expand,			// NPC상점 구매용
	kItem_Inven_Expand,

	kItemSubType_EtcStart = 10000,
	kItemSubType_Goods,				// 재화
	kItemSubType_Material,			// 재료
	kItemSubType_SiegeSwitch,		// 공성 아이템
	kItemSubType_Gold,				// 골드
	kItemSubType_Dia,				// 다이아
	kItemSubType_GuildPoint,		// 길드포인트
	kItemSubType_EtcEnd,
	kItemSubType_QuestItem = 20000,			// 퀘스트아이템
};

public enum WeaponTypeEnum
{
	kWeaponType_None = 0,
	kWeaponType_Dagger,			// 단검
	kWeaponType_OneHandSword,	// 한손검
	kWeaponType_TwoHandSword,	// 양손검
	kWeaponType_Bow,			// 활
};

public enum BlessTypeEnum
{
	kBlessType_None = 0,		// 일반
	kBlessType_Bless,			// 축복
	kBlessType_Curse,			// 저주
};


public enum SkillTypeEnum
{
	kSkillType_None = 0,
	kSkillType_Attack,				// 공격
	kSkillType_ProjectileAttack,	// 발사체 공격
	kSkillType_Buff,				// 버프
	kSkillType_DeBuff,				// 디버프
	kSkillType_Active,				// 발동
	kSkillType_HpHeal,				// Hp 회복
	kSkillType_MpHeal,				// Mp 회복
	kSkillType_CounterAttack,		// 반격 스킬
	kSkillType_RemoveBuff,
	kSkillType_Hit,					// 피격시 발동 스킬

	kSkillType_Detoxification,		// 중독 해제
	kSkillType_RateActiveBuff,		// 확률로 발동하는 버프    eOption_ActiveRate의 확률로 자신에게 eOption_Buff의 버프를 적용함
	kSkillType_RateActiveDeBuff,	// 확률로 발동하는 디버프  eOption_ActiveRate의 확률로 대상에게 eOption_Buff의 버프를 적용함
	kSkillType_Blink,				// 블링크 (대상에게 이동 후 단일, 범위로 추가 공격을 진행할 수 있는 스킬 타입)
	kSkillType_RemoveCostume,		// 코스튬 해제 (코스튬을 해제하고 옵션에 있는 디버프를 적용한다.)
	kSkillType_RemovePet,			// 펫 해제 (펫 소환을 해제하고 옵션에 있는 디버프를 적용한다.)
};

public enum SkillClassTypeEnum			// 스킬 클래스 타입
{
	kSkillClassType_None = 0,	// 마법
	kSkillClassType_Knight,		// 기사 : 기술
	kSkillClassType_Archer,		// 요정 : 정령
	kSkillClassType_Magic,		// 마법 : 마법사
};

public enum SkillTargetEnum
{
	kSkillTarget_None = 0,
	kSkillTarget_Caster,		// 시전자
	kSkillTarget_Target,		// PC & NPC
	kSkillTarget_PC,			// PC
	kSkillTarget_NPC,			// PC
	kSkillTarget_PC_Caster,		// PC or Caster
	kSkillTarget_Guild_Caster,	// Guild or Caster
	kSkillTarget_Party,			// Caster
	kSkillTarget_GuildBoss,		// GuildBoss
	kSkillTarget_Caster_Party,	// Castar or Party
};

public enum BuffListFilterTypeEnum
{
	kBuffListFilterType_None = 0,			// 없음
	kBuffListFilterType_Buff = 1,			// 일반 버프
	kBuffListFilterType_Debuff = 2,			// 디버프
	kBuffListFilterType_SpecialBuff = 3,	// 특수 버프
};

public enum BuffTypeEnum
{
	kBuffType_None = 0,
	kBuffType_Shield,
	kBuffType_Acceleration1,
	kBuffType_Acceleration2,
	kBuffType_Acceleration3,
	kBuffType_StatUp,
	kBuffType_WeightUp,
	kBuffType_BlessedArmor,
	kBuffType_Stun,
	kBuffType_StoneSkin,
	kBuffType_Clairvoyance,
	kBuffType_EvadeAttack,
	kBuffType_CounterOffensive,
	kBuffType_Slow,
	kBuffType_VitalAttack,
	kBuffType_GuildBuff,
	kBuffType_Pet,					// 펫 소환 버프
	kBuffType_Transform,
	kBuffType_RankBuff,
	kBuffType_GemBonus,
	kBuffType_LilithBless,
	kBuffType_Acceleration,
	kBuffType_WordLock,
	kBuffType_DivineShield,
	kBuffType_Hunter,
	kBuffType_LawfulBuff,
	kBuffType_Defender,
	kBuffType_ProtectionAttack,
	kBuffType_SwordImage,
	kBuffType_Persistent,
	kBuffType_FalconInsight,
	kBuffType_InsightVision,
	kBuffType_QuickLoad,
	kBuffType_Camouflage,
	kBuffType_HighShield,
	kBuffType_Vigor,
	kBuffType_Awake,
	kBuffType_Poison,

	kBuffType_GuildBuffAll,		// 길드 버프샵 전체
	kBuffType_GuildBuffIndi,	// 개인버프 (버프샵과 관계없음)

	kBuffType_KillingAura,
	kBuffType_Predator,
	kBuffType_Grease,
	kBuffType_BanPosition,
	kBuffType_UltiamteProtection,
	kBuffType_MortalBlow,
	kBuffType_HeartTracker,
	kBuffType_PVPReinforced,
	kBuffType_PVEReinforced,
	kBuffType_CashExp,
	kBuffType_Exp,
	kBuffType_Memroize,

	kBuffType_MpRecoveryTickPotion,

	kBuffType_Hyperion,
	kBuffType_DefenceArmor,

	kBuffType_IntervalRecovery_MaxHpRate,	// 최대 체력의 일정 비율로 일정 횟수만큼 회복
	kBuffType_IntervalRecovery_MaxMpRate,	// 최대 체력의 일정 비율로 일정 횟수만큼 회복

	kBuffType_Siege,	// 성 버프

	kBuffType_Grace_1,
	kBuffType_Grace_2,
	kBuffType_Grace_3,
	kBuffType_Grace_4,
	kBuffType_Grace_5,
	kBuffType_Grace_6,

	kBuffType_Food_1,
	kBuffType_Food_2,
	kBuffType_MultipleSurport,
	kBuffType_StealthDebuff,		// 투명화 해제시 적용되는 디버프
	kBuffType_StealthCooldown,
	kBuffType_StealthPenaltie,

	// 사용불가
	kBuffType_CantUseCostume,
	kBuffType_CantSummonPet,

	kBuffType_PVEPotion_1,		// 성던전에서 드랍되는 포션의 버프타입
	kBuffType_PVEPotion_2,		// 성던전에서 드랍되는 포션의 버프타입
};

public enum GuildChannel_Result
{
	kGuildChannel_Succes,			// 채널 접속 성공
	kGuildChannel_IsNotJoin,		// 길드에 가입되어있지 않습니다.
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class ItemInfo
{
	public Int64   _uid;			// item 고유 번호UID
	public Int32	_tableID;		// ItemTable ID
	public Int64	_itemCount;		// 수량
	public Int16	_equipSlot;		// 장착 중인 경우 장착 슬롯Index
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isLock;		// 잠금 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isExpireItem;		// 삭제 예정 아이템인 경우 true
	public Int64	_expireTime;	// 삭제 예정 시간
	public byte	_attributeType;		// 속성 값
	public byte	_attributeEnchant;	// 속성 강화 값
	public Int16	_slot;			// 인벤토리 내 슬롯 인덱스
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;		// 가변 옵션 타입
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;	// 가변 옵션 값
	public Int32	_exchange_limitCount;	// 남은 거래 횟수 제한
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class PetInfo
{
	public Int32	_tableID;			// PetTable ID
	public Int64	_itemCount;			// 보유 수량, 표기는 보유 수량 -1
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isLock;			// 잠금 상태 true=잠금
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class PetTempInfo
{
	public Int64   _uid;				// 고유번호 UID
	public Int32	_tableID;			// PetTableID
	public byte		_retryCount;		// 다시뽑기 가능 횟수
	public Int64	_retryEndTime;		// 다시뽑기 진행 가능한 시간(unixtime, 서버만 사용)
	public Int64	_retryEndSeconds;	// 다시 뽑기 진행 가능한 남은 시간(초)
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class PetGachaInfo
{
	public Int64   _uid;
	public Int32	_tableID;
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isBlessSynthesis;
};

public enum Item_Equip_Result
{
	kItem_Equip_Result_Success = 0,
	kItem_Equip_Result_NotFoundItem,		// 소유하고 있는 장비가 아니다
	kItem_Equip_Result_NotEquipItem,		// 장비가 아니다
	kItem_Equip_Result_AlreadyEquip,		// 장착중인 장비다
	kItem_Equip_Result_LowLevel,			// 레벨이 낮다
	kItem_Equip_Result_NotUseableClass,		// 사용할 수 없는 클래스 입니다.
	kItem_Equip_Result_EquipCountOver,		// 더이상 착용할 수 없습니다.
	kItem_Equip_Result_OverLevel,			// 레벨이 높다
};

public enum Item_UnEquip_Result
{
	kItem_UnEquip_Result_Success = 0,
	kItem_UnEquip_Result_NotFoundItem, // 장착중인 장비가 아니다
};

public enum HuntingAreaJoinResult
{
	kHuntingAreaJoinResult_Success = 0,
	kHuntingAreaJoinResult_NotEntryableLevel,	// 진입 가능한 레벨이 아니다
	kHuntingAreaJoinResult_NotEntryableTime,	// 진입 가능한 시간이 아니다
	kHuntingAreaJoinResult_NotFondHuntingArea,	// 잘못된 사냥터 ID
};

public enum CreateCharacterResult
{
	kCreateCharacter_Success = 0,
	kCreateCharacter_AlreadyMakeNickName,	// 존재하는 닉네임 입니다.
	kCreateCharacter_BadNickName,			// 생성할 수 없는 닉네임
	kCreateCharacter_SlangNickName,			// 비속어가 포함되어 있는 닉네임
	kCreateCharacter_CharacterSlotFull,		// 캐릭터 슬롯이 꽉찬 상태
	kCreateCharacter_CanNotCreate,			// 캐릭터를 생성할 수 없는 계정입니다. (제재 중)
	kCreateCharacter_InvalidAccess,			// 잘못된 접근
	kCreateCharacter_NicknameLengthError,	// 닉네임 길이 오류
	kCreateCharacter_CanNotCreate_CS,		// CS 처리중인 계정 (고객센터 문의를 처리중인 계정)
	kCreateCharacter_CanNotCreate_Server,		// 캐릭터 생성이 제한된 서버입니다.
};

public enum HideStateEnum
{
	kHideState_None = 0,
	kHideState_Normal = 1,			// 일반 투명
	kHideState_Developer = 2		// 운영자 투명
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class MonsterMoveInfo
{
	public Int64	monsterUID;		// 몬스터 or NPC UID  (고유값)
	public Int32	monsterTableID;	// npcTableID
	public Int32	chunkIdx;		// 몬스터가 있는 청크의 Index
	public Int32	posX;			// 이하 좌표
	public Int32	posY;
	public Int32	posZ;
	public Int32	moveSpeed;		// 이동 속도
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ActiveTitleInfo
{
	public Int16 prefixTID;
	public Int16 suffixTID;
	public Int16 panelTID;
	public Int16 colorTID;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class CharacterMoveInfo
{
	public Int64	characterUID;		// 캐릭터 UID
	public Int32	characterTableID;	// 클래스 TID
	public Int32	chunkIdx;			// 현재 있는 맵 내 청크 Index
	public Int32	posX;				// 좌표
	public Int32	posY;
	public Int32	posZ;
	public Int32	_equipWeaponItemTID;	// 장착 무기 TID
	public Int32	petTID;				// 펫 TID
	public Int32	moveSpeed;			// 이동 속도
	[MarshalAs(UnmanagedType.U1)]
	public bool	isTeleport;				// true인 경우 텔레포트로 이동된 정보
	public Int32	stunTime;			// 스턴이 걸려있는 경우 남은 스턴 시간
	public Int32	lawful;				// 준법 성향치
	public Int64	guildUID;			// 길드 UID
	public Int32	guildMark;			// 길드 마크
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)(UserMemberInfo.eMaxNickNameLenth+1) )]
	public string	_nickName;			// 닉네임
	[MarshalAs(UnmanagedType.U1)]
	public bool		isDead;				// 죽은 상태
	public Int32	_transformID;		// 변신 TID
	public Int32	_skillAniSpeed;		// 애니메이션 속도
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isBattleState;			//  전투 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isWordLock;			// 워드락 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isUltiamteProtection;	// 울티메이트 프로텍션 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isKillingAura;			// 킬링 아우라 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isBanPosition;			// 포션락 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isPoison;				// 중독 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isHeartTracker;		// 하트트래커 상태
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isGrease;
	public byte	_guildMemberType;		// 길드 멤버 타입
	public byte	_hide;	// 0=일반, 1=일반투명화, 2=운영자투명화
	public ActiveTitleInfo _activeTitleInfo;	// 칭호 정보
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class OtherChracterEquipInfo
{
	public Int64	_chracterUID;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)ItemEquipSlotTypeEnum.kEquipSlotMax)]
	public Int32[]		_equipItemTableID;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class AttackInfo
{
	public Int64 attackerUID;		// 공격자의 UID
	public Int32 attackSpeed;		// 공격 속도
	public Int64 targetUID;			// 공격 대상 UID
	[MarshalAs(UnmanagedType.U1)]
	public bool isCritical;			// 크리티컬 발동 여부
	[MarshalAs(UnmanagedType.U1)]
	public bool isWeaponMagicActive;	// 무기 마법 발동 여부
	[MarshalAs(UnmanagedType.U1)]
	public bool isDollMagicActive;		// 펫 마법 발동 여부
	[MarshalAs(UnmanagedType.U1)]
	public bool isMiss;				// 회피된 경우
	[MarshalAs(UnmanagedType.U1)]
	public bool isDead;				// 대상의 사망 여부
	[MarshalAs(UnmanagedType.U1)]
	public bool isAttackArrow;		// 화살 공격 여부
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class CharacterStatsInfo
{
	public Int32	_hp;		// 현재 HP
	public Int32	_maxHp;		// 최대 HP
	public Int32	_mp;		// 현재 MP
	public Int32	_maxMp;		// 최대 HP
	public Int32	_str;		// str(힘)
	public Int32	_dex;		// dex(민첩)
	public Int32	_con;		// con(체력)
	public Int32	_int;		// int(지식)
	public Int32	_wiz;		// wiz(지혜)
	public Int32	_bonusStat; // 잔여 보너스 포인트
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class BuffInfo
{
	public Int32 _tableID;		// BuffTableID
	public Int32 _lifeTime;		// 남은 시간(초)
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class BuyItemInfo
{
	public Int32 npcShopTableID;
	public Int32 buyCount;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class SkillInfo
{
	public Int32 _tableID;
	public Int32 _coolTime;
	public Int16 _skillTimerSec;
	[MarshalAs(UnmanagedType.U1)]
	public bool  _isUseTimer;
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class ItemMailInfo
{
	public Int64	_mailUID;		// 우편함 아이템UID
	public byte	_mailType;			// MailTypeEnum
	public Int32	_itemTableID;	// ItemTable ID
	public Int64	_itemCount;		// 수량
	public byte	_attributeType;		// 속성 타입
	public byte	_attributeEnchant;	// 속성 강화 단계
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isExpire;			// 미수령 시 자동 삭제되는 우편물인 경우 true
	public Int64	_expireSeconds;	// 삭제까지 남은 시간(초)
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;		// 가변 옵션 타입
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;	// 가변 옵션 수치
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class CollectionRegistInfo
{
	public Int32 _collectionTID;		// 펫 or 코스튬 컬렉션테이블ID
	public byte  _slotIdx;				// 등록할 슬롯넘버
	public Int32 _materialTID;			// 펫 or 코스튬 테이블ID
};

[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class CollectionInfo
{
	public Int32 _collectionTID;		// 펫 or 코스튬 컬렉션테이블ID
	public byte  _collectionData;		// 컬렉션 등록 정보(비트플레그)
};

public enum OptionTypeEnum
{
	eOption_None = 0,
	eOption_ItemOptionStart = 1000,
	eOption_Str_Up = 1001,						//	힘 증가
	eOption_Dex_Up = 1002,						//	덱스 증가
	eOption_Con_Up = 1003,						//	콘 증가
	eOption_Int_Up = 1004,						//	인트 증가
	eOption_Wiz_Up = 1005,						//	위즈 증가

	eOption_Armor = 1013,						//	방어력
	eOption_Reduction = 1014,					//	대미지 감소
	eOption_EnchantArmor = 1015,				// 인첸트 방어력

	eOption_AttackDamage_Melee = 1021,			//	근접 대미지
	eOption_AddDamage_Melee = 1022,			//	근접 추가 대미지
	eOption_AttackRate_Melee = 1023,			//	근거리 명중
	eOption_CriticalRate_Melee = 1024,			//	근거리 치명타
	eOption_Weight = 1025,						// 소지 무게
	eOption_Dodge_Melee = 1026,				//	근거리 회피

	eOption_AttackDamage_Range = 1031,			//	원거리 대미지
	eOption_AddDamage_Range = 1032,			//	원거리 추가 대미지
	eOption_AttackRate_Range = 1033,			//	원거리 명중
	eOption_CriticalRate_Range = 1034,			//	원거리 치명타
	eOption_Dodge_Range = 1035,				//	원거리 회피

	eOption_MaxHp = 1041,						// 최대 체력 증가
	eOption_HpRecoveryTick = 1042,				//	체력 회복 틱
	eOption_HpPotionRecoveryAddRate = 1043,	//	물약 체력 회복율 증가

	eOption_AttackDamage_Magic = 1051,			//	마법 대미지
	eOption_DefaultAttackDamage_Magic = 1052,	//  마법사 기본대미지
	eOption_AddDamage_Magic = 1053,			//	마법 추가 대미지
	eOption_AttackRate_Magic = 1054,			//  마법 일반 공격 적중률
	eOption_AttackRate_MagicSkill = 1055,		//	마법 적중률
	eOption_CriticalRate_Magic = 1056,			//	마법 치명타
	eOption_MpCostReduce = 1057,				//	마나 소모 감소

	eOption_MaxMp = 1061,						// 최대 마나 증가
	eOption_MpRecoveryTick = 1062,				//	마나 회복 증가
	eOption_MpPotionRecoveryTick = 1063,		//	마나 물약 회복 틱
	eOption_MagicRegist = 1064,				//	마법 방어

	eOption_AttackSpeedRate = 1071,			//	공격 속도 증가
	eOption_MoveSpeedRate = 1072,				//	이동 속도 증가
	eOption_DamageRecoverySpeed = 1073,		//	피격 회복 속도
	eOption_SkillAniSpeedRate = 1074,  // 스킬 애니메이션 증가 속도

	eOption_Regist_Fire = 1081,				//	불 속성 저항
	eOption_Regist_Water = 1082,				//	물 속성 저항
	eOption_Regist_Wind = 1083,				//	바람 속성 저항
	eOption_Regist_Earth = 1084,				//	땅 속성 저항

	eOption_UndeadAddDamage = 1091,				//	언데드 추가 대미지

	eOption_PvpAddDamage = 1101,				//	PVP 추가 대미지
	eOption_PvpReduction = 1102,				//  PVP 대미지 감소

	eOption_ExpUp = 1111,						//	경험치 추가 획득
	eOption_HpPotionRecoveryAddPoint = 1112,	//	물약 체력 회복 증가
	eOption_ActiveDamageReductionRate = 1113,	//	확률 대미지 감소
	eOption_GoldUp = 1114,						//  골드 추가 획득
	eOption_ExpSeal = 1115,						//  경험치 획득 불가

	eOption_KnightSkillDefence = 1121,			// 기술 내성
	eOption_AttackRate_KnightSkill = 1122,		// 기술 적중
	eOption_ArcherSkillDefence = 1123,			// 정령 내성
	eOption_AttackRate_ArcherSkill = 1124,		// 정령 적중

	eOption_RandomBox = 1131,					// 랜럼 박스
	eOption_ItemBox = 1132,						// 아이템 상자

	eOption_Buff = 1141,						//  버프 발동
	eOption_HpRecovery = 1142,					//	HP 회복
	eOption_MpRecovery = 1143,					//	MP 회복
	eOption_Teleport = 1144,					//	텔레포트
	eOption_ReturnTown = 1145,					//	귀환
	eOption_MagicActive_1 = 1146,				//	마법 발동
	eOption_MagicActive_2 = 1147,				//	마법 발동
	eOption_CurePoison = 1148,					//	해독
	eOption_CharacterBlessTime = 1149,			// 캐릭터 축복 효과 시간(초)

	eOption_WeaponAttribute = 1151,			// 무기 속성 (0:없음, 1:물, 2:불, 3:땅, 4:바람)
	eOption_WeaponAttribute_Water = 1152,	// 클라 표기용 무기 물 속성
	eOption_WeaponAttribute_Fire = 1153,    // 클라 표기용 무기 불 속성
	eOption_WeaponAttribute_Earth = 1154,   // 클라 표기용 무기 땅 속성
	eOption_WeaponAttribute_Wind = 1155,    // 클라 표기용 무기 바람 속성

	eOption_LearnSkill = 1161,				// 스킬 습득
	eOption_PetRandomBoxCount = 1162,		// 펫 랜덤 박스 수량
	eOption_TransfromSet = 1163,			// 변신 세트ID
	eOption_ProtectEnchant = 1164,				// 강화 파괴 방지
	eOption_PveAddDamage = 1165,			// Pve 대미지
	eOption_Restore_Event = 1166,			// 복구 이벤트 티켓

	eOption_SummonNPC_Group = 1171,		// npc소환
	eOption_TimeLimited = 1172,			// 타임값
	eOption_ExpPlus = 1173,				// 경험치 획득
	eOption_Transform_Set = 1174,			// 확정 변신 스크롤

	eOption_CriticalReductionRate = 1181,	//	치명타 대미지 공식 변경 참고
	eOption_CriticalDamageAddRate = 1182,	//	치명타 대미지 공식 변경 참고
	eOption_PveReduction = 1183,			//	신규(PVE대미지 감소)
	eOption_PvpAddDamage_Melee = 1184,		//	PVP대미지 공격 방식별 세분화 추가
	eOption_PvpAddDamage_Range = 1185,		//	PVP대미지 공격 방식별 세분화 추가
	eOption_PvpAddDamage_Magic = 1186,		// PVP대미지 공격 방식별 세분화 추가
	eOption_SkillDefence = 1187,			//	기술, 궁술 내성 통합한 공통 내성 추가
	eOption_HealRatePlus = 1188,			//	힐 증가량 공식 변경 참고
	eOption_LawfulRecovery = 1189,			//	로우풀 증가 아이템용

	eOption_CharacterStealth = 1191,		//	추후 투명 망토 연결용
	eOption_OpenCharacterSlot = 1192,		// 캐릭터 빈스롯 오픈
	eOption_TransformRandomBoxCount = 1193,	// 변신 뽑기 카운트
	eOption_ReturnGuildHouse = 1194,		//	아지트 귀환
	eOption_DungeonTimeGroupID = 1195,		// 시간 던전 시간 그룹ID
	eOption_DungeonTimePlus = 1196,		// 충전할 시간
	eOption_PoisonDamageReduction = 1197,	// (독 대미지 감소 수치) 신규 옵션
	eOption_Detect					= 1198,	// 투명 해제
	eOption_DetectRange				= 1199,	// 투명 해제 범위
	eOption_PoisonTargetActive_1 = 1200,	// 중독 대상 공격 시 발동될 스킬1
	eOption_PoisonTargetActive_2 = 1201,	// 중독 대상 공격 시 발동될 스킬2
	eOption_DeBuff = 1202,	// 공격스킬에서 사용할 디버프 옵션
	eOption_AttackRate_AssassinSkill = 1203, // 암살자 스킬 적중
	eOption_AssassinSkillDefence = 1204, // 암살자 스킬 내성
	eOption_PoisonTargetDeBuff = 1205,	// 중독 상태를 대상으로 하는 디버프

	eOption_UnlockNPC = 1220,		// 길드 아지트 npc언락 옵션
	eOption_ItemAvailabilityZone = 1221,	// 

	eOption_DeveloperStealth = 1224,	// 운영자용 은신
	eOption_AddStealthDebuff_1 = 1225,	// 투명화 해제 시 적용하는 디버프
	eOption_AddStealthDebuff_2 = 1226,	// 투명화 해제 시 적용하는 디버프

	eOption_ExpandCount = 1227,	// NPC상점 슬롯확장 카운트
	eOption_pvpDamage_Rate = 1228,	// 카운터어택 pvp 데미지 배율 조정용
	eOption_pveDamage_Rate = 1229,	// 카운터어택 pve 데미지 배율 조정용
	eOption_AcceptingQuest = 1230,  // 긴급 퀘스트 TID
	eOption_ItemOptionEnd,


	eOption_GlobalOptionStart = 2000,
	eOption_AttackRange = 2001,				// 공격 거리
	eOption_ActiveRate = 2002,					// 발동 확률
	eOption_MultiShoot = 2003,					// 더블샷
	eOption_Transform = 2004,					// 변신테이블
	eOption_ReductionRate = 2005,				// 이뮨
	eOption_HpPotionLock = 2006,				// 포션락
	eOption_MaxHpRate = 2007,					// 최대 체력 비율 상승
	eOption_MaxMpRate = 2008,					// 최대 체력 비율 상승
	eOption_RemoveBuff = 2009,					// 버프 삭제
	eOption_DrainMana = 2000,					// 마나 드레인
	eOption_Heal = 2011,						// 힐
	eOption_AttackDamageRange = 2012,			// 공격 대미지 범위
	eOption_TeleportLock = 2013,				// 텔레포트 불가상태
	eOption_HpRecoveryRate = 2014,				// 자기 최대 체력 비율로 회복
	eOption_HealRateDecrease = 2015,			//	힐 회복 감소비율
	eOption_FormulaType = 2016,				// 마법 대미지 공식 연계(1:근접,2:원거리,3:마법)
	eOption_MagicDamage_Rate = 2017,			// 마법 대미지 적용 배수(1:1000)
	eOption_HpRecoverySecAbsolute = 2018,		// 초당 절대 HP 회복
	eOption_HpRecoverySecMaxHpRate = 2019,		// 초당 비례 HP 회복
	eOption_MpRecoverySecMaxMpRate = 2020,		// 초당 비례 MP 회복
	eOption_IntervalRecovery_MaxHpRate = 2021,	// (1회 당 최대 체력 비례 회복)(무게 상관없이) (천분율) 신규 옵션
	eOption_IntervalRecovery_MaxMpRate = 2022,	// (1회 당 최대 마나 비례 회복)(무게 상관없이) (천분율) 신규 옵션
	eOption_IntervalRecovery_Seconds = 2023,	// 1회 당 회복 발동 시간 간격(초)
	eOption_HpHeal = 2024,						// 힐 스킬 전용
	eOption_PoisonDamage = 2025,				// (1회 당 독 대미지) 신규 옵션
	eOption_FatalHP = 2026,						// 스킬 사용후 해당 옵션이 있는경우 해당 수치로 체력을 낮춘다. 현재 체력이 낮은 경우 적용되지 않음.
	eOption_PlusDamage_MP = 2027,				// 공격 스킬 사용 시 MP를 모두 소모해서 추가 대미지를 입힌다.
	eOption_SkillExtension_1 = 2028,				// Value=추가 사용할 스킬TID(버프타입만 가능), 사용한 스킬의 A(Step+1)를 Value 버프 스킬의 Step에 더해서 발동한다. Value버프 스킬이 습득된 상태가 아니면 미적용됨.
	eOption_SkillExtension_2 = 2029,				// Value=추가 사용할 스킬TID(버프타입만 가능), 사용한 스킬의 A(Step+1)를 Value 버프 스킬의 Step에 더해서 발동한다. Value버프 스킬이 습득된 상태가 아니면 미적용됨.
	eOption_NickNameChange = 2030,			//닉네임 변경권
	eOption_Pet = 2031,						// 펫테이블
	eOption_WeightDePenalty_Recovery = 2032,	// 회복 무게패널티 무시
	eOption_WeightDePenalty_Action = 2033,		// 공격 무게패널티 무시
	eOption_BlessEnchant = 2034,			// 축복강화 옵션
	eOption_GlobalOptionEnd,


	eOption_Max,
};

public enum Chat_Whisper_Result
{
	kChat_Whisper_Result_Success = 0,
	kChat_Whisper_Result_DontSelf,
	kChat_Whisper_Result_NotFound,
	kChat_Whisper_Result_Logout,
	kChat_Whisper_Result_IsBlock,
};

public enum Item_Enchant_Result
{
	kItem_Enchant_Result_Success = 0,	// 성공
	kItem_Enchant_Result_Fail,			// 실패
	kItem_Enchant_Result_AttributeEnchant_Success,	// 속성 강화 성공
	kItem_Enchant_Result_AttributeEnchant_Fail,		// 속성 강화 실패
	kItem_Enchant_Result_Lock,			// 잠겨있는 아이템이다
	kItem_Enchant_Result_CantCurse,		// 저주를 사용할 수 없다.
	kItem_Enchant_Result_DontEnchant,	// 더이상 강화할 수 없습니다.
	kItem_Enchant_Result_IsLock,		// 잠금 상태의 장비는 강화를 진행할 수 없습니다. 잠금 상태를 해제하고 진행해주세요.

	kItem_Enchant_Result_NotSameAttribute,  // 다른 속성으로 강화할 수 없습니다.
	kItem_Enchant_Result_AttributeMax,		// 속성 강화 최대치 입니다.
	kItem_Enchant_Result_CantEnchantNotFoundEquipItem,	// 강화를 진행할 수 없습니다. 존재하지 않는 아이템 입니다.

	kItem_Enchant_Result_ProtectEnchantCantProgress,	// 수호 강화를 진행할 수 없는 등급입니다.
	kItem_Enchant_Result_ProtectEnchantFail,		//

	kItem_Enchant_Result_DontChange_NotAttributeItem,	// 속성이 없는 아이템입니다.
	kItem_Enchant_Result_DontChange_SameAttribute,		// 같은 속성으로 변경할 수 없습니다.
	kItem_Enchant_Result_Success_AttributeChange,		// 속성이 변경되었습니다.

	kItem_Enchant_Result_NotEnoughCostItem,		// 강화 비용이 부족합니다.

	kItem_Enchant_Result_InvalidAccess,			// 알 수 없는 오류가 발생했습니다.
};

public enum StatPlus_Result
{
	kStatPlus_Result_Success = 0,
	kStatPlus_Result_InvalidAccess,		// DB오류 등으로 진행할 수 없을때
	kStatPlus_Result_OverStat,			// 오버 스탯.
};

public enum StatReset_Result
{
	kStatReset_Result_Success = 0,
	kStatReset_Result_NotEnoughCost,		// 재화가 부족합니다.
	kStatReset_Result_LowLevel,				// 부여된 보너스 스탯이 없습니다.
	kStatReset_Result_RemainingBonusStat,	// 보너스 스탯이 남아있습니다. 모두 사용 후 초기화를 진행할 수 있습니다.
};

public enum NPCShop_Buy_Result
{
	kNPCShop_Buy_Result_Success = 0,
	kNPCShop_Buy_Result_NotEnoughCostItem,	// 재화가 부족합니다.
	kNPCShop_Buy_Result_WeightOver,			// 무게가 오버됨.
	kNPCShop_Buy_Result_InventoryFull,		// 인벤토리 빈공간이 부족합니다.
	kNPCShop_Buy_Result_NotMatchNPC,		// 해당 NPC에게 구매할 수 있는 상품이 아닙니다.
	kNPCShop_Buy_Result_LongDistance,		// 해당 NPC에게 구매할 수 있는 상품이 아닙니다.
	kNPCShop_Buy_Result_CantBuyAnymore,     // 더이상 구매할 수 없습니다.
	kNPCShop_Buy_Result_LowGuildLevel,      // 길드 레벨이 낮아서 구입할 수 없습니다.
	kNPCShop_Buy_Result_NotGuildMember,     // 길드 멤버가 아닙니다.
	kNPCShop_Buy_Result_NotGuildMaster,     // 길드장이 아닙니다.
	kNPCShop_Buy_Result_NoStronghold,       // 요새가 없습니다.
};

public enum Item_Lock_Result
{
	kItem_Lock_Result_Success = 0,
	kItem_Lock_Result_NotFound,				// 존재하지 않는 아이템입니다.
};

public enum Pet_Synthesis_Enum
{
	kPet_Synthesis_Enum_SrcSetCount = 11,	// 합성을 진행할 수 있는 최대 Set
	
	kPet_Synthesis_Enum_StartGrade = 1,		// 합성을 진행할 수 있는 최소 등급
	kPet_Synthesis_Enum_EndGrade = 5,		// 합성을 진행할 수 있는 마지막 등급
	
	kPet_Synthesis_Enum_SrcItemCount = 4,		// SpecialGrade보다 낮은 등급의 합성 재료 수량	
	kPet_Synthesis_Enum_SpecialGrade = 5,
	kPet_Synthesis_Enum_SpecialGradeSrcItemCount = 2, // SpecialGrade보다 큰 등급의 합성 재료 수량
};

public enum Pet_BlessSynthesis_Enum
{
	kPet_BlessSynthesis_Enum_SrcSetCount = 11,

	kPet_BlessSynthesis_Enum_StartGrade = 4,
	kPet_BlessSynthesis_Enum_EndGrade = 6,

	kPet_BlessSynthesis_Enum_SrcItemCount = 3,
};

public enum Item_Use_Result
{
	kItem_Use_Result_Success = 0,
	kItem_Use_Result_CantUse_InventoryFull,
	kItem_Use_Result_CantUse_LowLevel,			 // 레벨 {0} 이상 부터 사용 할 수 있습니다.
	kItem_Use_Result_CantUse_NotUseableClass,	 // 사용할 수 없는 클래스 입니다.
	kItem_Use_Result_CantUse_IsLock,			 // 사용할 수 없는 클래스 입니다.
	kItem_Use_Result_CantUse_UseDelay,			 // 사용할 수 없는 클래스 입니다.
	kItem_Use_Result_CantUse_AlreadyLearnSkill,	 // 이미 습득한 스킬입니다.
	kItem_Use_Result_CantUse_AlreadyBUffPotion,	 // 사용할 수 없습니다. 아이템의 버프가 적용 중입니다.
	kItem_Use_Result_CantUse_PotionLock,		 // 포션락으로 체력회복제 사용할 수 없음
	kItem_Use_Result_CantUse_RandomTeleportZone, // 이곳에서는 무작위 텔레포트를 사용할 수 없습니다.
	kItem_Use_Result_CantUse_OverLevel,			 // 레벨 {0} 이하만 사용할 수 있습니다.
	kItem_Use_Result_CantUse_State,				 // 사용할 수 없는 상태입니다.
	kItem_Use_Result_CantUse_MaxLawful,			 // 라우풀 수치가 최대치여서 사용할 수 없다.
	kItem_Use_Result_CantUse_NotFoundGuildHouse, // 귀환할 수 있는 길드 아지트가 없습니다. (길드원이 아니거나, 길드에 가입되어있더라도 대여중인 아지트가 없는경우 아지트 귀환 주문서(eOption_ReturnGuildHouse) 사용불가)
	kItem_Use_Result_CantUse_DungeonTimeNotTimeOver,		// 시간던전 시간이 모두 소진되지 않아서 사용할 수 없음.
	kItem_Use_Result_CantUse_DungeonTimeMaxCharge,			// 해당 시간 던전의 충전 가능 횟수를 모두 사용함. (일일 제한maxDailyChargeCount)
	kItem_Use_Result_CantUse_DungeonTimeOverResetTime,		// 충전 시 리셋타임을 넘어서 충전되서 충전을 진행할 수 없습니다.(충전 불가한 시간)
	kItem_Use_Result_CantUse_GuildHouseExpireTime,	        // 길드 하우스 만료 시간이 임박해서 입장 불가함.
	kItem_Use_Result_CantUse_DungeonTimeAddMaxTimeOver,     // 충전할 수 없습니다. 충전 시 최대 충전 시간을 초과합니다.
	kItem_Use_Result_CantUse_NickNameChange,	            // 해당 캐릭터는 이미 "닉네임 변경권"이 적용되어 있으니 확인해 주세요.
	kItem_Use_Result_CantUse_NotFoundStronghold,            // 요새를 찾을 수 없습니다.
	kItem_Use_Result_CantUse_NotAvailableZone,              // 사용할 수 없는 맵입니다.
	kItem_Use_Result_CantUse_CantSummonAnymore,             // 더이상 몬스터를 소환할 수 없습니다.
	kItem_Use_Result_CantUse_UrgentMissionInProgress,         // 같은 긴급 퀘스트가 이미 진행중입니다.
	kItem_Use_Result_CantUse_UrgentMissionHuntingSameMonster, // 같은 몬스터를 토벌중입니다.
	kItem_Use_Result_CantUse_UrgentMissionLimitExceeded,      // 너무 많은 긴급 퀘스트를 진행중입니다.
};

public enum Item_Use_Multiple_Result
{
	kItem_Use_Multiple_Result_Success = 0,
	kItem_Use_Multiple_Result_CantUse_InventoryFull,
	kItem_Use_Multiple_Result_CantUse_LowLevel,			// 레벨 {0} 이상 부터 사용 할 수 있습니다.
	kItem_Use_Multiple_Result_CantUse_NotUseableClass,	// 사용할 수 없는 클래스 입니다.
	kItem_Use_Multiple_Result_CantUse_IsLock,			// 사용할 수 없는 클래스 입니다.
	kItem_Use_Multiple_Result_CantUse_UseDelay,			// 사용할 수 없는 클래스 입니다.
	kItem_Use_Multiple_Result_CantUse_PotionLock,		// 포션락으로 체력회복제 사용할 수 없음
	kItem_Use_Multiple_Result_CantUse_OverLevel,			// 레벨 {0} 이하만 사용할 수 있습니다.
	kItem_Use_Multiple_Result_CantUse_State,				// 사용할 수 없는 상태입니다.
	kItem_Use_Multiple_Result_CantUse_MaxLawful,			// 라우풀 수치가 최대치여서 사용할 수 없다.
	kItem_Use_Multiple_Result_CantUse_CountOver,			// 소지 수량보다 많은 양을 사용하려고 할 경우
	kItem_Use_Multiple_Result_CantUse_LimitCount,			// 1미만 100초과
	kItem_Use_Multiple_Result_PetTempInvenIsFull,			// 펫 확정인벤 300개 초과
	kItem_Use_Multiple_Result_TCTempInvenIsFull,			// 코스튬 확정인벤 300개 초과
	kItem_Use_Multiple_Result_CantUse_NotAvailableZone,     // 사용할 수 없는 맵입니다.
};

public enum Skill_Use_Result
{
	kSkill_Use_Result_Success = 0,
	kSkill_Use_Result_NotEnoughHp,		// 체력 부족
	kSkill_Use_Result_NotEnoughMp,		// 마나 부족
	kSkill_Use_Result_DontLearnSkill,	// 배우지 않은 스킬
	kSkill_Use_Result_TargetError,		// 잘못된 대상
	kSkill_Use_Result_WaitDelay,		// 쿨타임 중
	kSkill_Use_Result_DontUseState,		//스킬을 사용할 수 없는 상태입니다.
	kSkill_Use_Result_AttackRangeOver,	// 스킬 사용 거리 초과
	kSkill_Use_Result_NotEquipBow,		// 활을 착용하고 있지 않다
	kSkill_Use_Result_DontHaveCostItem,	// 스킬 사용에 소모되는 재료 부족
	kSkill_Use_Result_DontUseWeight,	// 소지 아이템의 무게가 무거워서 스킬을 사용할 수 없습니다.
	kSkill_Use_Result_Fail,				// 적중 실패
	kSkill_Use_Result_CanUse_RandomTeleportZone, // 이곳에서는 무작위 텔레포트를 사용할 수 없습니다.
	kSkill_Use_Result_BlinkTargetError,	// 대상 주변에 이동할 공간이 없거나 장애물이 있는 경우
	kSkill_Use_Result_SiegeAlliances,	// 공성전맵에서 연맹 및 길드를 공격하려는 경우
};

public enum MailBoxEnum
{
	kMailBoxEnum_DefaultExpireTime = 259200, // 3일 초

};

public enum MailTypeEnum
{
	kMailType_EventPresent = 0,				// 이벤트 선물
	kMailType_ServerCheckReward,			// 서버 점검 보상
	kMailType_ShopBuyItem,					// 상점 구매 아이템
	kMailType_DailyAttandenceReward,		// 출석 체크 보상
	kMailType_NewUserAttandenceReward,		// 신규 유저 출석 체크 보상
	kMailType_GuildAttandenceReward,		// 길드 출석 체크 보상
	kMailType_ItemExchangeBuyItem,			// 거래소에서 구매한 아이템
	kMailType_ItemSellCancel,				// 거래소 판매 취소 아이템
	kMailType_Coupon,						// 쿠폰 선물
	kMailType_ItemCreateFailReward,			// 아이템 제작 실패 보상
	kMailType_PetSynthesisFailReward,		// 펫 합성 실패 보상
	kMailType_BossRaidReward,				// 보스 레이드 보상
	kMailType_GuildItemExchangeBuyItem,		// 길드 거래소에서 구매한 아이템
	kMailType_GuildItemSellCancel,			// 길드 거래소 판매 취소 아이템
	kMailType_GuildDistributionDia,		// 길드 분배 다이아
	kMailType_TransformCardSynthesisFailReward,	// 변신카드 합성 실패 보상
	kMailType_GuildWeeklyContributionReward,	// 길드 주간 공헌도 보상
	kMailType_Guild_Donation_Reward,			// 길드 기부 보상
	kMailType_Guild_Package_BonusItem,		// 길드 패키지 길드원 추가 지급 상품
	kMailType_SkillEnchantFailReward,		// 스킬 강화실패 보상
	kMailType_CollectionAchieveReward,		// 업적 달성 보상
	kMailType_CSReward,						// cs보상
	kMailType_Postal,						// 우편 도착
	kMailType_Max,							// End Idx
};

public enum ItemMailBox_GetItem_Result
{
	kItemMailBox_GetItem_Result_Success = 0,
	kItemMailBox_GetItem_Result_Expiered,		// 이미 삭제된 우편물
	kItemMailBox_GetItem_Result_InvenFull,		// 인벤토리 공간 부족
	kItemMailBox_GetItem_Result_InvalidAccess,	// 다른 사람의 우편물이거나 예외 상황으로 수령 불가능
	kItemMailBox_GetItem_Result_Wait,			// 쿨타임 대기중
};

public enum AttendanceInfoEnum
{
	kDailyAttendance_RepeatRewardStartDay = 29,		// 29일부터 반복 보상을 지급
	kDailyAttendance_ResetDay = 1,					// 매월 1일
};

public enum DailyAttendanceResult
{
	kDailyAttendanceResult_Receive = 0,		// 아이템을 받았다.
	kDailyAttendanceResult_Received,		// 아이템을 이미 받았다.
	kDailyAttendanceResult_InvalidAccess,	// 잘못된 접근
};

public enum NewUserAttendanceResult
{
	kNewUserAttendanceResult_Receive = 0,	// 아이템을 받았다.
	kNewUserAttendanceResult_Received,		// 아이템을 이미 받았다.
	kNewUserAttendanceResult_AllReceived,	// 모든 보상을 수령했습니다.
	kNewUserAttendanceResult_InvalidAccess,	// 잘못된 접근
};

public enum ZoneType
{
	kZoneType_Safety = 0,
	kZoneType_PVP,
	kZoneType_NonPVP,
	kZoneType_Combat,
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class GuildInfo
{
	public Int64 uid;		// 길드UID
	public Int16 level;		// 길드레벨
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName+1)]
	public string guildName;	// 길드명
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth+1)]
	public string guildMasterNickname;	// 길드장 닉네임
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildNotice+1)]
	public string guildNotice;	// 길드 공지사항
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildProfile+1)]
	public string guildProfile;	// 길드 프로필
	public byte  joinType;	// 가입 타입
	public Int64 exp;						// 길드 경험치
	public Int64 guildPoint;				// 길드 전체 누적 공헌도
	public Int16 todayAttendanceCount;		// 오늘 출석 인원
	public Int16 yesterdayAttendanceCount;	// 어제 출석 인원
	public Int16 memberCount;				// 현재 전체 길드원 수
	public Int32  guildmark;				// 길드 마크 정보
	public Int64 guildDia;					// 길드 다이아
	public Int64 weeklyPoint;				// 길드의 주간 누적 공헌도
	public Int32 rank;						// 랭크
	public Int32 loginMemberCount;			// 현재 접속 인원
	public byte  changeNameTicket;			// 길드명 변경 티켓
	public byte  donationCount;				// 기부 가능 횟수
	public byte  tDonationCount;			// 오늘 길드 전체 기부 횟수
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxStrongholdName + 1)]
	public string strongholdName;	        // 요새 이름 (비어있으면 미보유)
}

public enum Guild_Create_Result
{
	kGuild_Create_Success = 0,
	kGuild_Create_SameName,				// 같은 이름이 존재한다.
	kGuild_Create_NotEnoughGold,		// 골드가 부족하다.
	kGuild_Create_NameEmpty,			// 길드명이 없다.
	kGuild_Create_NameLengthOver,		// 길드 명이 너무 길드
	kGuild_Create_ProfileEmpty,			// 길드 소개가 없다.
	kGuild_Create_ProfileLengthOver,	// 길드 소개가 길다.
	kGuild_Create_AlreadyGuildMember,	// 길드에 이미 소속된 사람이 길드 생성을 시도함.
	kGuild_Create_LowLevel,				// 레벨이 낮다.
	kGuild_Create_NoticeEmpty,			// 길드 공지를 입력해주세요.
	kGuild_Create_NoticeLengthOver,		// 길드 공지가 최대 글자수보다 많습니다.
	kGuild_Create_Join_GuildJoinInterval, // 길드 해산, 탈퇴 후 24시간 미만
	kGuild_Create_BadChar,				// 허용되지 않는 문자 포함
	kGuild_Create_DBError = 99,			// 길드 생성 에러
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class GuildMemberInfo
{
	public Int64 chrUID;		// 캐릭터UID
	public Int16 level;			// 캐릭터 레벨
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth+1)]
	public  string nickname;		// 캐릭터 닉네임
	public Int32 characterTypeID;	// 캐릭터테이블ID
	public byte  memberType;		// 멤버 타입
	public Int64 lastLoginTime;		// 마지막 로그인 시간
	public byte  isAttendance;		// 출석 여부
	[MarshalAs(UnmanagedType.U1)]
	public bool  isLogon;			// 로그온 여부
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildMemberMassage+1)]
	public  string message;			// 메시지
	public Int64 totalPoint;		// 전체 누적 공헌도
	public Int64 weeklyPoint;		// 주간 누적 공헌도
}


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class GuildJoinUserInfo
{
	public Int64 chrUID;		// 캐릭터UID
	public Int16 level;			// 캐릭터 레벨
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth+1)]
	public string nickname;		// 캐릭터 닉네임
	public Int32 characterTypeID;	// 캐릭터테이블ID
	public Int64 lastLoginTime;		// 마지막 접속 시간
	[MarshalAs(UnmanagedType.U1)]
	public bool  isLogon;			// 현재 접속중인지
	public Int64 actionTime;		// 요청일
}


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class GuildSearchInfo
{
	public Int64 guildUID;		// 길드 UID
	public Int16 level;			// 길드 레벨
	public byte  joinType;		// 가입 타입
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName+1)]
	public string guildName;	// 길드 명
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth+1)]
	public string guildMasterNickname;	// 길드장 닉네임
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildProfile+1)]
	public string guildProfile;		// 길드 프로필
	public Int32 mark;				// 길드 마크
	public byte  memberCount;		// 현재 멤버 인원
	public Int64 guildPoint;		// 길드 포인트
	public Int64 actionTime;		// 요청일
	public Int32 prevRank;			// 이전 랭킹
	public Int64 weeklyPoint;		// 주간 포인트
	public Int64 exp;				// 길드 경험치
}


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class GuildLightSearchInfo
{
	public Int64 guildUID;
	public Int16 level;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName+1)]
	public string guildName;
	public Int32 mark;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string guildMasterNickname;
}


public enum GuildJoinTypeEnum
{
	kGuildJoinTypeEnum_ImmediatelyJoin = 0,		// 즉시 가입
	kGuildJoinTypeEnum_ConfirmJoin,				// 가입 신청 후 승인, 거절
	kGuildJoinTypeEnum_RefuseJoin,				// 가입 거부
};

public enum Guild_SearchResult
{
	kGuild_Search_Success = 0,
	kGuild_Search_NotFoundGuild,	// 길드명으로 길드가 검색되지 않음
};

public enum Guild_Join_Result
{
	kGuild_Join_Success = 0,		// 길드에 가입되었다.
	kGuild_Join_NotFoundGuild,		// 존재하지 않는 길드에 가입 신청했다.
	kGuild_Join_FullMember,			// 길드 인원이 최대 인원이다.
	kGuild_Join_Wait,				// 길드 가입 신청이 완료됐다.
	kGuild_Join_MaxWaitUse,			// 더 이상 길드 가입을 신청할 수 없다.
	kGuild_Join_AlreadyJoinWait,	// 가입신청을 대기중인 길드 입니다.
	kGuild_Join_MaxRequest,			// 가입신청을 더이상 할 수 없다.
	kGuild_Join_RefuseJoin,			// 길드 가입을 거부 중인 길드 입니다.
	kGuild_Join_AlreadyJoinGuild,	// 길드에 가입할 수 없습니다. 길드에 이미 가입한 상태입니다.
	kGuild_Join_GuildJoinInterval,	// 길드에 가입할 수 없습니다. 탈퇴나 해산을 진행한 뒤 24시간뒤에 창설 또는 가입할 수 있습니다.
	kGuild_Join_ParticipateInaSiegeAssaultGuild, // 공성전이 진행중인 길드
	kGuild_Join_LowLevel			// 레벨이 낮아서 가입할 수 없습니다.
};

public enum Guild_Manager_JoinAwaitReject_Result
{
	kGuild_Manager_JoinAwaitReject_Result_Success = 0,
	kGuild_Manager_JoinAwaitReject_Result_NotGuildManager,		// 길드 관리자만 승인을 거부할 수 있음.
	kGuild_Manager_JoinAwaitReject_Result_NotFoundGuild,		// 길드가 조회되지 않음
	kGuild_Manager_JoinAwaitReject_Result_JoinedOtherGuild,		// 다른 길드에 가입되어있음.
	kGuild_Manager_JoinAwaitReject_Result_JoinedGuild,			// 같은 길드에 가입되어있음.
	kGuild_Manager_JoinAwaitReject_Result_NotFoundMember,		// 신청자를 찾을 수 없음
};

public enum Guild_Manager_JoinAwaitAccept_Result
{
	kGuild_Manager_JoinAwaitAccept_Result_Success = 0,
	kGuild_Manager_JoinAwaitAccept_Result_NotGuildManager,		// 길드 관리자마 승인을 허용할 수 있음
	kGuild_Manager_JoinAwaitAccept_Result_NotFoundGuild,		// 길드가 조회되지 않음
	kGuild_Manager_JoinAwaitAccept_Result_AlredayOtherGuildJoin, // 이미 길드에 가입된 사람임
	kGuild_Manager_JoinAwaitAccept_Result_MaxMember,			 // 맴버 인원이 가득참
	kGuild_Manager_JoinAwaitAccept_Result_GuildJoinInterval,	 // 해당 캐릭터가 탈퇴 또는 길드 해산 후 24시간이 지나지 않았습니다.
	kGuild_Manager_JoinAwaitAccept_Result_ParticipateInaSiegeAssaultGuild, //(처리됨) 공성전에 참여중인 길드 (가입불가 및 토스트메시지 처리를 위함)
	kGuild_Manager_JoinAwaitAccept_Result_NotFoundMember,		// 신청자를 찾을 수 없음
};

public enum Guild_LeaveResult
{
	kGuild_Leave_Success = 0,
	kGuild_Leave_GuildMaster,	// 길드 마스터는 탈퇴 할 수 없음.
	kGuild_Leave_NotFoundGuild, // 길드가 조회되지 않음
	kGuild_Leave_GuildBossTime, // 길드 보스가 진행중입니다.
	kGuild_Leave_GuildItemExchangeNotZero, // 길드거래소에 등록된 아이템이 있습니다.
	kGuild_Leave_SiegeProgress,	// 공성전 진행중에는 탈퇴할 수 없습니다.
};

public enum Guild_Manager_InfoModify_Result
{	
	kGuild_Manager_InfoModify_Result_Success = 0,
	kGuild_Manager_InfoModify_Result_NotGuildManager,	// 길드 관리자만 진행할 수 있음
};

public enum Guild_Manager_MarkModify_Result
{
	kGuild_Manager_MarkModify_Result_Success = 0,
	kGuild_Manager_MarkModify_Result_NotGuildManager, // 길드 관리자만 진행할 수 있음
	kGuild_Manager_MarkModify_Result_NotEnoughCost,
};


public enum GuildAttendanceType
{
	kGuildAttendanceType_WaitAttendance = 0,	// 출석 대기 상태
	kGuildAttendanceType_Attendance,			// 출석함
	kGuildAttendanceType_GuildJoinFirstDay,		// 첫날은 출석 불가
};

public enum Guild_Attendance_Result
{
	kGuild_Attendance_Result_Success = 0,
	kGuild_Attendance_Result_AlreadyToday,	// 이미 출석함
	kGuild_Attendance_Result_FirstDay,		// 첫날이라 출석할 수 없음
};

public enum Guild_AttendanceGetReward_Result
{
	kGuild_AttendanceGetReward_Result_Success = 0,
	kGuild_AttendanceGetReward_Result_Fail,			// 지금은 받을 수 없다.
};

public enum Guild_Master_Destroy_Result
{
	kGuild_Master_Destroy_Result_Success = 0,
	kGuild_Master_Destroy_Result_NotGuildMaster,	// 길드 마스터가 아닌데 요청함. 
	kGuild_Master_Destroy_Result_NotGuildMember,	// 길드 멤버가 아닌데 요청함.
	kGuild_Master_Destroy_Result_GuildBossTime,	// 길드 보스가 진행중임.
	kGuild_Master_Destroy_Result_GuildRaidOpen,	// 길드를 해산할 수 없습니다. 길드 레이드가 열려있습니다.
	kGuild_Master_Destroy_Result_GuildDiaNotZero,			// 길드를 해산할 수 없습니다. 길드 다이아를 모두 정산해야 해산할 수 있습니다.
	kGuild_Master_Destroy_Result_GuildItemExchangeNotZero,	// 길드를 해산할 수 없습니다. 길드 거래소에 등록된 아이템이 존재합니다.
	kGuild_Master_Destroy_Result_ParticipateInaSiegeAssaultGuild, // 길드 해산 불가, 공성 참여중
	kGuild_Master_Destroy_Result_ExistMember,				// 길드원이 존재하는 경우
	kGuild_Master_Destroy_Result_GuildDestroyInterval,		// 길드창설 후 24시간이 지나지 않았을 경우
};

public enum Guild_Manager_GuildLevelUp_Result
{
	kGuild_Manager_GuildLevelUp_Result_Success = 0,
	kGuild_Manager_GuildLevelUp_Result_NotFoundGuild,	// 길드가 조회되지 않음
	kGuild_Manager_GuildLevelUp_Result_NotGuildManager,	// 길드 관리자만 레벨업 할 수 있음
	kGuild_Manager_GuildLevelUp_Result_NotEnoughGold,	// 골드가 부족함
	kGuild_Manager_GuildLevelUp_Result_MaxLevel,		// 더이상 레벨업할 수 없음
};

public enum Guild_ChangeMessageResult
{
	kGuild_ChangeMessage_Success = 0,
	kGuild_ChangeMessage_NotGuildMember,	// 길드원이 아닌 상태임
};

public enum Guild_Manager_MemberBan_Result
{
	kGuild_Manager_MemberBan_Result_Success = 0,
	kGuild_Manager_MemberBan_Result_NotGuildMaster, // 길드장이 아니네요.
	kGuild_Manager_MemberBan_Result_NotGuildMember, // 길드원이 아닙니다.
	kGuild_Manager_MemberBan_Result_NotFoundGuild,  // 길드장이 소속된 길드가 없다.
	kGuild_Manager_MemberBan_Result_NotNormalMember,  // 일반 길드원만 추방할 수 있다.
	kGuild_Manager_MemberBan_Result_GuildBossTime,  // 길드 보스가 진행중이다.
	kGuild_Manager_MemberBan_Result_ParticipateInaSiegeAssaultGuild, // (처리됨)공성전에 참여중인 길드 (가입불가 및 토스트메시지 처리를 위함)
	kGuild_Manager_MemberBan_Result_GuildItemExchangeNotZero,		// 거래소에 아이템이 등록되어있다.
};

public enum Guild_Manager_ChangeMemberType_Result
{
	kGuild_Manager_ChangeMemberType_Result_NormalMember = 0,	// 일반 길드원으로 변경
	kGuild_Manager_ChangeMemberType_Result_SubMaster,			// 부길드 장으로 변경
	kGuild_Manager_ChangeMemberType_Result_MaxSubMaster,		// 부길드장은 최대 5인
	kGuild_Manager_ChangeMemberType_Result_NotMaster,			// 길드장만 변경 할 수 있다
	kGuild_Manager_ChangeMemberType_Result_NotFoundGuild,		// 존재하지 않는 길드
	kGuild_Manager_ChangeMemberType_Result_NotGuildMember,	// 우리 길드 맴버가 아님
	kGuild_Manager_ChangeMemberType_Result_NotGuildManager,			// 길드 관리자만 변경할 수 있습니다.
	kGuild_Manager_ChangeMemberType_Result_CantChangeManager,		// 길드 관리자의 길드원 등급은 마스터만 변경할 수 있습니다.
	kGuild_Manager_ChangeMemberType_Result_CantChangeMaster,			// 길드장의 등급은 변경할 수 없습니다.
	kGuild_Manager_ChangeMemberType_Result_SubMasterSetOnlyMaster,	// 부 길드장은 길드장만 지정할 수 있습니다.
	kGuild_Manager_ChangeMemberType_Result_ProbationMember,	// 견습 길드원으로 변경
	kGuild_Manager_ChangeMemberType_Result_ParticipateInaSiegeAssaultGuild, // (처리됨)공성전에 참여중인 길드 (가입불가 및 토스트메시지 처리를 위함)
};

public enum Guild_Master_ChangeMaster_Result
{
	kGuild_Master_ChangeMaster_Result_Success = 0,		// 길드장으로 변경
	kGuild_Master_ChangeMaster_Result_NotMaster,			// 길드장만 변경 할 수 있다
	kGuild_Master_ChangeMaster_Result_NotFoundGuild,		// 존재하지 않는 길드
	kGuild_Master_ChangeMaster_Result_NotGuildMember,	// 우리 길드 맴버가 아님
	kGuild_Master_ChangeMaster_Result_ParticipateInaSiegeAssaultGuild, // (처리됨)공성전에 참여중인 길드 (가입불가 및 토스트메시지 처리를 위함)
};

public enum Warehouse_ItemIn_Result
{
	kWarehouse_ItemIn_Result_Success = 0,		// 보관 성공
	kWarehouse_ItemIn_Result_WarehouseFull,		// 창고에 빈공간이 부족합니다.
	kWarehouse_ItemIn_Result_ItemIsEquipped,	// 장착중인 아이템입니다.
	kWarehouse_ItemIn_Result_InvalidAccess,		// 잘못된 접근
	kWarehouse_ItemIn_Result_LowCount,			// 보유한 아이템 카운트가 작을경우
};

public enum Warehouse_ItemOut_Result
{
	kWarehouse_ItemOut_Result_Success = 0,		// 찾기 성공
	kWarehouse_ItemOut_Result_InventoryFull,	// 가방에 빈공간이 부족합니다.
	kWarehouse_ItemOut_Result_NotEnoughGold,	// 아아템 찾기에 필요한 골드가 부족합니다.
	kWarehouse_ItemOut_Result_InvalidAccess,	// 잘못된 접근
};

public enum Warehouse_Expand_Result
{
	kWarehouse_Expand_Result_Success = 0,		// 확장 완료
	kWarehouse_Expand_Result_IsMax,				// 확장할 수 없습니다. 확장 최대치 입니다.
	kWarehouse_Expand_Result_NotEnoughRuby,		// 확장에 필요한 루비가 부족합니다.
	kWarehouse_Expand_Result_InvalidAccess,		// 잘못된 접근
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class WarehouseInOutItemInfo
{
	public Int64	_itemUID;
	public Int64	_itemCount;
	public Int64	_targetUID;
};

public enum ShopItemType
{
	kShop_ItmeTypeNone = 0,
	kShop_Dia,							// 캐시 재화 충전
	kShop_Item,							// 아이템 (패키지 포함)
	kShop_PetRandomBox,					// 펫 뽑기
	kShop_CostumeRandomBox,				// 코스튬 뽑기
	kShop_QuestProgress_Pet,			// 퀘스트용 펫뽑기
	kShop_QuestProgress_Costume,		// 퀘스트용 변신 뽑기
	kShop_CharacterSlotOpen,			// 캐릭터 슬롯 확장
	kShop_Guild,						// 길드 전용 상품
	kShop_NickNameChange,				// 닉네임 변경권 아이템
	kShop_EventPass,					// 패스 상품
};

public enum ShopItemPayType
{
	ePayType_None	= 0,
	ePayType_Item,				// 아이템(다이아, 골드, 인벤토리에 있는 아이템 등)
	ePayType_Mileage,			// 마일리지
	ePayType_Cash,				// 결제 구매
};

public enum ShopPackageType
{
	kNonePackage = 0,
	kNormalPackage,		// 일반
	kGuildPackage,		// 길드 패키지
};

public enum ShopBuyLimitType
{
	kShopBuyLimitType_Invalid = -1, // 잘못된 값
	kShopBuyLimitType_None = 0,		// 제한 없음
	kShopBuyLimitType_CountLimit,	// 초기화 없는 횟수 제한
	kShopBuyLimitType_Daily,		// 일일 횟수 제한
	kShopBuyLimitType_Monthly,		// 월간 횟수 제한
	kShopBuyLimitType_Weekly,		// 주간 횟수 제한
	kShopBuyLimitType_Character,	// 캐릭터 구매 제한
};

public enum ItemCreateLimitType
{
	kItemCreateLimitType_None = 0,		// 제한 없음
	kItemCreateLimitType_Server,		// 서버 제작 수량 제한
	kItemCreateLimitType_Account,		// 계정 제작 수량 제한
	kItemCreateLimitType_Level,			// 제작 레벨 제한
};

public enum Shop_BuyItem_Result
{
	Shop_BuyItem_Result_Success = 0,
	Shop_BuyItem_Result_NotEnoughCostItem,				// 구매에 필요한 재화가 부족합니다.
	Shop_BuyItem_Result_FullItemInven,					// 가방에 빈공간이 부족합니다.
	Shop_BuyItem_Result_AlreadyBuyPackage,				// 이미 구매한 패키지 상품입니다.
	Shop_BuyItem_Result_NotBuyAnymore,					// 더이상 구매할 수 없다.
	Shop_BuyItem_Result_NotEnoughMileage,				// 마일리지가 부족합니다.
	Shop_BuyItem_Result_TimeOver,				// 판매 기간 초과
	Shop_BuyItem_Result_LowLevel,				// 레벨이 낮아서 구매할 수 없습니다.
	Shop_BuyItem_Result_OverLevel,				// 레벨이 높아서 구매할 수 없습니다.

	Shop_BuyItem_Result_QuestMismatch,			// 해당 퀘스트를 진행중일때만 구매할 수 있습니다.

	Shop_BuyItem_Result_MaxCharacterSlot,		// 캐릭터 슬롯을 더이상 확장할 수 없습니다.
	Shop_BuyItem_Result_OnlyGuildMemberBuy,	// 소속 길드가 있어야 구매할 수 있습니다.

	Shop_BuyItem_Result_DBError,					// 상점 정보 갱신 오류
	Shop_BuyItem_Result_NotFoundTableID,			// RandomBoxTable에 뽑을 데이터가 없는 경우

	Shop_BuyItem_Result_CantBuy_NotUseableClass,	// 구매할 수 없는 클래스
	Shop_BuyItem_Result__ItemCountMiss,				// 1미만 100 초과

	Shop_BuyItem_Result_PetTempInvenIsFull,			// 펫 확정인벤 300개 초과
	Shop_BuyItem_Result_TCTempInvenIsFull,			// 변신 확정인벤 300개 초과

	Shop_BuyItem_Result_NotBuyPrevItem,				// 이 전 아이템을 구매하지 않은 경우

	Shop_BuyItem_Result_OfferUnactivated,           // 한정상품 비활성 상태
};

public enum Shop_BuyItem_BulkOrder_Result
{
	Shop_BuyItem_BulkOrder_Result_Success = 0,
	Shop_BuyItem_BulkOrder_Result_Fail,				// 구매 불가능한 상품이 포함되어있는 경우
	Shop_BuyItem_BulkOrder_Result_IsNull,			// 선택한 상품이 없는 경우
	Shop_BuyItem_BulkOrder_Result_NotEnoughCost,	// 재화가 없는 경우
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class ShopBuyCountInfo
{
	public Int32 shopItemID;		// ShopItemTable ID
	public Int32 buyCount;			// 구매한 횟수
}

public enum Item_Create_Result
{
	kItem_Create_Result_Success = 0,			// 성공
	kItem_Create_Result_Fail,					// 실패
	kItem_Create_Result_MaterialItemLock,		// 재료 아이템이 잠금 상태이다.
	kItem_Create_Result_NotEnoughMaterialItem,	// 재료가 부족하다.
	kItem_Create_Result_InventoryFull,			// 가방에 빈공간이 부족하다.
	kItem_Create_Result_CreateTimeOver,			// 제작 시간이 만료되었다.
	kItem_Create_Result_CreateCountOver,		// 더이상 제작할 수 없습니다.
	kItem_Create_Result_LevelLow,				// 레벨이 낮아서 제작할 수 없습니다.
	kItem_Create_Result_LevelHigh,				// 레벨이 높아서 제작할 수 없습니다.
	kItem_Create_Result_InvalidAccess,			// 잘못된 접근
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class ItemCollectionInfo
{
	public Int32 tableID;
	public byte collectionData;
}

public enum Item_Collection_Regist_Result
{
	kItem_Collection_Regist_Result_Success = 0,		// 등록 완료
	kItem_Collection_Regist_Result_AlreadyRegist,	// 이미 등록된 컬렉션이다.
	kItem_Collection_Regist_Result_ItemIsLock,		// 잠금 상태 아이템이다.
	kItem_Collection_Regist_Result_ItemIsEquipped,	// 장착중인 아이템이다.
	kItem_Collection_Regist_Result_LowLevel,		// 등록 가능한 레벨이 아니다
	kItem_Collection_Regist_Result_InvalidAccess,	// 잘못 된 접근
};

public enum Item_Collection_GetReward_Reulst
{
	Item_Collection_GetReward_Reulst_Success = 0,
	Item_Collection_GetReward_Reulst_LowCount,		// 받을 수 있는 보상이 없습니다.
	Item_Collection_GetReward_Reulst_InvalidAccess,	// DB or Table err
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class ExchangeItemInfo
{
	public Int64   _uid;				// 거래소UID
	public Int64	_sellerUID;			// 판매자 캐릭터UID
	public Int32	_tableID;			// 아이템 TID
	public Int32	_itemCount;			// 수량
	public Int32	_price;				// 판매 가격
	public Int64	_sellEndTime;		// 판매 종료 시간
	public byte	_attributeType;			// 아이템 속성
	public byte	_attributeEnchant;		// 속성 강화 수치
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;			// 추가 옵션 타입
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;		// 추가 옵션 값
	public Int32	_exchange_limitCount;
};

public enum Item_Exchange_Enum
{
	kItemExchange_ItemCount = 9999,
	kItemExchange_SellRegistCount = 30,
	kItemExchange_MinPrice = 10,
	kItemExchange_MaxPrice = 9999999,
	kItemExchange_SellRegistFeeRate = 20,
	kItemExchange_SellBaseFeePercent = 2,
	kItemExchange_ListSize = 200,
	kItemExchange_SettlementListSize = 200,
};

public enum Item_Exchange_SettlementType
{
	kItem_Exchange_SettlementType_Buy = 0,		// 구매 내역
	kItem_Exchange_SettlementType_Sell,			// 판매 내역
};

public enum Item_Exchange_SellRegist_Result
{
	kItem_Exchange_SellRegist_Result_Success = 0,
	kItem_Exchange_SellRegist_Result_NotFoundItem,		// 존재하지 않는 아이템
	kItem_Exchange_SellRegist_Result_CantExchangeItem,	// 거래소 등록 불가 아이템
	kItem_Exchange_SellRegist_Result_NotEnoughTax,		// 거래 수수료 부족
	kItem_Exchange_SellRegist_Result_ItemCountMiss,		// 아이템 수량이 1보다 작거나 최대 수량보다 더 많은 수량으로 판매하려고 할때
	kItem_Exchange_SellRegist_Result_MaxRegist,			// 더이상 등록할 수 없습니다. 수량 제한.
	kItem_Exchange_SellRegist_Result_PriceError,		// 판매 금액 오류 100보다 작거나 Max보다 큼
	kItem_Exchange_SellRegist_Result_ItemIsLock,		// 아이템이 잠금 상태다
	kItem_Exchange_SellRegist_Result_ItemIsEquipped,	// 아이템이 장착 중이다.
	kItem_Exchange_SellRegist_Result_InvalidAccess,		// 비정상 적인 접근
	kItem_Exchange_SellRegist_Result_Off,				// 거래소 점검 중
	kItem_Exchange_SellRegist_Result_GuestAccount,		// 게스트 계정은 이용 불가합니다.
	kItem_Exchange_SellRegist_Result_MaxRegistCount,	// 해당 상품을 더이상 거래소에 등록할 수 없습니다.
	kItem_Exchange_SellRegist_Result_Over_LimitCount,	// 거래 가능 횟수가 없습니다.
};

public enum Item_Exchange_Buy_Result
{
	kItem_Exchange_Buy_Result_Success = 0,
	kItem_Exchange_Buy_Result_NotEnoughDia, // 보석이 부족하다.
	kItem_Exchange_Buy_Result_AlreadySell,	 // 이미 판매된 아이템이다.
	kItem_Exchange_Buy_Result_SellTimeOver,	 // 판매 시간이 만료되었다.
	kItem_Exchange_Buy_Result_MyItem,		 // 본인이 등록한 아이템은 구매할 수 없습니다.
	kItem_Exchange_Buy_Result_Off,		 // 거래소 점검 중
	kItem_Exchange_Buy_Result_LowLevel,		 // 캐릭터 레벨이 낮아서 구매할 수 없습니다.
	kItem_Exchange_Buy_Result_AleadyCancle,	// 판매자가 아이템을 취소한 경우
	kItem_Exchange_Buy_Result_InvenFull,	// 인벤토리가 가득 찬 경우
};

public enum Item_Exchange_SellRegistCancel_Result
{
	kItem_Exchange_SellRegistCancel_Result_Success = 0,
	kItem_Exchange_SellRegistCancel_Result_AlreadySell,	// 판매됐거나 이미 취소한 아이템 입니다.
	kItem_Exchange_SellRegistCancel_Result_Off,	// 거래소 점검 중
	kItem_Exchange_SellRegistCancel_Result_InvenFull,	// 인벤토리가 가득 찬 경우
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class DeathPenaltyExpInfo
{
	public Int64	_uid;
	public Int64	_exp;
	public Int32	_deadLevel;
	public Int64	_deleteTime;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class DeathPenaltyItemInfo
{
	public Int64	_uid;
	public Int32	_itemTableID;
	public byte		_attributeType;
	public byte		_attributeEnchant;
	public Int64	_deleteTime;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;
	public Int32	_exchange_limitCount;
};

public enum DeathRestoreType
{
	kDeathRecoveryType_Free = 0,
	kDeathRecoveryType_Gold,
	kDeathRecoveryType_Dia,
	kDeathRecoveryType_Item,
};

public enum Death_Penalty_RestoreExp_Result
{
	kDeath_Penalty_RestoreExp_Result_Success = 0,
	kDeath_Penalty_RestoreExp_NotFound,
	kDeath_Penalty_RestoreExp_TimeOver,
	kDeath_Penalty_RestoreExp_FreeCountZero,
	kDeath_Penalty_RestoreExp_NotEnoughGold,
	kDeath_Penalty_RestoreExp_NotEnoughDia,
	kDeath_Penalty_RestoreExp_NotEnoughItem,
	kDeath_Penalty_RestoreExp_InvalidAccess,
};

public enum Death_Penalty_RestoreItem_Result
{
	kDeath_Penalty_RestoreItem_Result_Success = 0,
	kDeath_Penalty_RestoreItem_Result_NotFound,
	kDeath_Penalty_RestoreItem_Result_TimeOver,
	kDeath_Penalty_RestoreItem_Result_InventoryFull,
	kDeath_Penalty_RestoreItem_Result_NotEnoughGold,
	kDeath_Penalty_RestoreItem_Result_NotEnoughDia,
	kDeath_Penalty_RestoreItem_Result_InvalidAccess,
};

public enum Teleport_Worldmap_Result
{
	kTeleport_Worldmap_Result_Success = 0,
	kTeleport_Worldmap_Result_NotEnoughCost,	// 재화가 부족합니다.
	kTeleport_Worldmap_Result_CantAction,		// 이동할 수 없는 상태 입니다.
	kTeleport_Worldmap_Result_InvalidAccess,
	kTeleport_Worldmap_Result_LowwerLevel,		// 레벨이 낮아서 입장할 수 없습니다.
	kTeleport_Worldmap_Result_UpperLevel,		// 레벨이 높아서 입장할 수 없습니다.
};

public enum Teleport_FindNPC_Result
{
	kTeleport_FindNPC_Result_Success = 0,
	kTeleport_FindNPC_Result_NotTown,			// 마을이 아님
	kTeleport_FindNPC_Result_InvalidAccess,		// 테이블 에러
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class DropItemInfo
{
	public Int64	_uid;
	public Int32	_itemTableID;
	public Int64	_itemCount;
	public Int32	_dropNPCTID;
	public Int64	_dropChrUID;
	public Int32	_deleteTime;
	public Int32	_x;
	public Int32	_z;
	public Int32	_chunkIdx;
	public Int64	_chrUID;
	public Int64	_deleteSec;			//클라이언트용 삭제까지 남은 시간
};


public enum Coupon_Use_Result
{
	kCoupon_Use_Result_Success = 0,
	kCoupon_Use_Result_UseTimeOver,					// 유효 기간이 만료되었다.
	kCoupon_Use_Result_MissCouponCode,				// 잘못된 쿠폰 코드.
	kCoupon_Use_Result_AlreadyUseSameCouponType,	// 같은 종류 쿠폰은 이미 사용했다.
	kCoupon_Use_Result_AlreadyUseCoupon,			// 이미 사용된 쿠폰이다.
	kCoupon_Use_Result_CantUseServer,				// 해당 서버에서는 사용할 수 없다.

};

public enum CouponTypeEnum
{
	kCouponType_None = 0,
	kCouponType_MultiCode,
	kCouponType_OneCode,
};

public enum ItemInven_Expand_Result
{
	kItemInven_Expand_Result_Success = 0,		// 확장 완료
	kItemInven_Expand_Result_IsMax,				// 확장할 수 없습니다. 확장 최대치 입니다.
	kItemInven_Expand_Result_NotEnoughCost,		// 확장에 필요한 재화가 부족합니다.
	kItemInven_Expand_Result_InvalidAccess,		// 잘못된 접근
};

public enum NPC_Talk_Teleport_Result
{
	kNPC_Talk_Teleport_Result_Success = 0,
	kNPC_Talk_Teleport_Result_DistanceOver,			// NPC와의 거리가 너무 멀다.
	kNPC_Talk_Teleport_Result_NotEnoughGold,		// 골드가 부족하다.
	kNPC_Talk_Teleport_Result_CantTeleportState,	// 이동할 수 없는 상태다(죽었거나, 스턴상태)
	kNPC_Talk_Teleport_Result_UnderLevel,			// 레벨이 낮아서 입장할 수 없다.
	kNPC_Talk_Teleport_Result_OverLevel,			// 레벨이 높아서 입장할 수 없다.
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class QuestInfo
{
	public Int32	_questID;
	public byte		_questState;
	public Int32	_progressCount;
};

public enum QuestState
{
	kQuestState_AcceptWait = 0,	// 수락 대기
	kQuestState_Progress,		// 진행 중
	kQuestState_Complete,		// 완료됨 (보상 수령 가능 상태)
	kQuestState_GotReward,		// 다음 퀘스트가 없을 때 보상 수령함 상태
};

public enum QuestActionType
{
	kQuestActionType_Accept = 0,
	kQuestActionType_GetReward,
};

public enum QuestAction_Result
{
	kQuestAction_Result_Accept_Success = 0,
	kQuestAction_Result_AlreadyAccept,
	kQuestAction_Result_GetReward_Success,
	kQuestAction_Result_GetReward_Fail_NotComplete,
	kQuestAction_Result_Accept_Fail_LowLevel,
};

public enum Quest_Teleport_Result
{
	kQuest_Teleport_Result_Success = 0,
	kQuest_Teleport_Result_NotFoundQuestTeleport,	// 진행중인 퀘스트에 이동가능한 텔레포트가 없습니다.
	kQuest_Teleport_Result_NotEnoughGold,			// 골드가 부족하다.
	kQuest_Teleport_Result_CantTeleportState,		// 이동할 수 없는 상태다(죽었거나, 스턴상태)
	kQuest_Teleport_Result_UnderLevel,				// 레벨이 낮아서 입장할 수 없다.
	kQuest_Teleport_Result_OverLevel,				// 레벨이 높아서 입장할 수 없다.
};

public enum MissionClearType
{
	kMissionClearType_None = 0,
	kMissionClearType_MonsterKill,			// 몬스터 처치 -1일경우 아무몬스터, ID있는경우 지정 몬스터
	kMissionClearType_MainShopItemBuy,		// 확인필요.
	kMissionClearType_EnchantWeapon,		// 무기 강화
	kMissionClearType_EnchantArmor,			// 방어구 강화
	kMissionClearType_PetSynthesis,			// 펫 합성
	kMissionClearType_PlayerKill,			// PK
	kMissionClearType_LevelUp,				// 레벨 달성

	kMissionClearType_ItemUse,				// 아이템 타입 UseType의 아이템 사용 (ActionTableID_1~3은 아이템 ID)
	kMissionClearType_ItemPickUp_Grade,		// 드랍 아이템 습득(ActionTableID_1~3은 등급)
	kMissionClearType_EnchantGem,			// 젬스톤 강화(ActionTableID_1~3은 -1) 성공 여부 상관없음
	kMissionClearType_EnchantGem_Success,	// 젬스톤 강화(ActionTableID_1~3은 -1) 성공 시 카운트
	kMissionClearType_EnchantGem_Fail,		// 젬스톤 강화(ActionTableID_1~3은 -1) 실패 시 카운트
	kMissionClearType_ReproductionGem,		// 젬스톤 재부여(ActionTableID_1~3은 -1)
	kMissionClearType_MonsterKill_Type,		// 몬스터 사냥 수 몬스터 타입으로 구분. (ActionTableID_1~3은 NPCTable/NPCSubType)
	kMissionClearType_Attendance_Guild,    // 길드 출석, 하루 1번만 카운트(ActionTableID_1~3은 -1)

	kMissionClearType_NPCTalk,				// NPC대화
	kMissionClearType_ItemEquip,			// 아이템 장착
	kMissionClearType_Teleport,				// 텔레포트
	kMissionClearType_Progress,				// 클라이언트에서 퀘스트 진행 시 사용(UI클릭 등 클라 이벤트)

	kMissionClearType_SellItem,				// 아이템 판매 카운트(판매 NPC에게 판매)(종류, 가격등은 따지지 않음)
	kMissionClearType_ItemDecomposition,	// 아이템 분해 횟수 카운트
	kMissionClearType_GuildDonation,		// 길드 기부
	kMissionClearType_GuildRaidAttendence,	// 길드 레이드 참여
	kMissionClearType_TransformSynthesis,	// 변신 합성
	kMissionClearType_CreateItem,			// 아이템 제작 (성공, 실패 무관)
	kMissionClearType_ItemExchangeBuy,		// 거래소 아이템 구매 횟수
	kMissionClearType_SubDailyQuestClear,	// 일간 서브 퀘스트 클리어할 때 카운트(MissionType = 'kMissionType_Daily') 완료될 때 마다 카운트
	kMissionClearType_SubWeeklyQuestClear,	// 주간 서브 퀘스트 클리어할 때 카운트 (MissionType = 'kMissionType_Weekly') 완료될 때 마다 카운트
	kMissionClearType_Quest_ItemPickUp,		// 아이템 드랍 획득 (TID)
	kMissionClearType_ItemEnchant,			// 아이템 강화 전체
	kMissionClearType_EnchantAccessory,		// 장신구 강화

	kMissionClearType_ShopRandomBoxFree_MC,	// 마법인형뽑기 무료
	kMissionClearType_ShopRandomBoxFree_TC,	// 변신뽑기 무료

	kMissionClearType_Achievement_MainQuestClearStep,		// 메인 퀘스트 클리어 업적

	kMissionClearType_SubDailyGuildQuestClear,	// 길드 일간 서브 퀘스트 클리어할 때 카운트(MissionType = 'kMissionType_Daily') 완료될 때 마다 카운트
	kMissionClearType_SubWeeklyGuildQuestClear,	// 길드 주간 서브 퀘스트 클리어할 때 카운트 (MissionType = 'kMissionType_Weekly') 완료될 때 마다 카운트

	kMissionClearType_DiceRoundClear,	// 다이스 완주 퀘스트
	kMissionClearType_BingoLineClear,	// 빙고 클리어 퀘스트

	kMissionClearType_MonsterKill_World,	// 특정 지역 몬스터 킬
	kMissionClearType_Dissolution,			// 용해 카운트

	kMissionClearType_ItemEnchantSuccess,		// 강화 성공 -> grade
	kMissionClearType_ItemEnchantFail,			// 강화 실패 -> grade
	kMissionClearType_GraceGrowthStep,			// 가호 달성 -> GroupID
	kMissionClearType_PVP_Mocking,				// PVP 조롱 -> X
	kMissionClearType_PVP_Chasing,				// PVP 추적 -> X
	kMissionClearType_Ranking_Level,			// 토탈랭킹 달성 -> 토탈랭킹 - 재 협의 필요(미접속 유저 해당업적 갱신은 어떻게?)
	kMissionClearType_Ranking_Guild,			// 길드랭킹 달성 -> 길드랭킹 - 재 협의 필요
	kMissionClearType_Dungeon_Time,				// 던전시간 소모 -> 리셋TID
	kMissionClearType_PlayerKill_Lawful,		// 빨간닉 유저 처치(PVP_Zone) -> X
	kMissionClearType_GoldUse,					// 재화 소모 -> X
	kMissionClearType_DiaUse,					// 다이아 소모 -> X
	kMissionClearType_Offline_Time,				// 오프라인 시간 소모 -> X
	kMissionClearType_Restore,					// 아이템 및 경험치 복구 -> X
	kMissionClearType_GuildCreate,				// 길드 창설 -> X
	kMissionClearType_GuildTasks,				// 길드임무 완수 -> 현상수배 개인완수 확인 필요
	kMissionClearType_SiegeVictory,				// 공성전 승리 -> X - 재협의 필요(한번이라도 참가한 유저만? 아니면 길드원 전체?)
	kMissionClearType_SiegeJoin,				// 공성전 참여 -> X - 재협의 필요(``)
	kMissionClearType_SkillEnchant,				// 스킬강화 시도 -> grade
	kMissionClearType_CharacterDead,			// 사망 패널티 -> X
	kMissionClearType_ExchangeSell,				// 거래소 정산 -> X - 길드포함?
	kMissionClearType_Achievement_ClearStep,	// 업적 클리어 -> TID
};

public enum MissionType
{
	kMissionType_Daily = 0,		// 일간 미션
	kMissionType_Weekly,		// 주간 미션
	kMissionType_Monthly,		// 월간 미션
	kMissionType_GuildDaily,	// 길드 일간 미션
	kMissionType_GuildWeekly,	// 길드 주간 미션
	kMissionType_GuildMonthly,	// 길드 월간 미션
	kMissionType_NoReset,		// 초기화되지 않는 미션
	kMissionType_Urgent,		// 미션스크롤 사용 미션
	kMissionType_Title,			// 칭호 미션
	kMissionType_Panel,	// 패널 미션
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class MissionInfo
{
	public Int32	_tableID;
	public Int32	_actionCount;
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isGetReward;
	public Int32 _remainingTime;
};

public enum Mission_GetReward_Result
{
	kMission_GetReward_Result_Success = 0,
	kMission_GetReward_Result_AlreadyGet,		// 이미 수령했다.
	kMission_GetReward_Result_NotComplete,		// 완료하지 않았다.
	kMission_GetReward_Result_NotFound,			// 존재하지않는 미션이다
	kMission_GetReward_Result_NotGuildMember,	// 길드 퀘스트는 길드에 소속된 경우에만 보상을 수령할 수 있습니다.
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class AchievementInfo
{
	public Int32	_tableID;
	public Int32	_groupID;
	public Int32	_actionCount;
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isGetReward;
};

public enum Achievement_GetReward_Result
{
	kAchievement_GetReward_Result_Success = 0,
	kAchievement_GetReward_Result_AlreadyGet,		// 이미 수령했다.
	kAchievement_GetReward_Result_NotComplete,		// 완료하지 않았다.
	kAchievement_GetReward_Result_NotFound,			// 존재하지않는 미션이다
};

public enum NPC_Talk_Dont_Result
{
	Npc_Talk_Dont_Result_UnderWarehouseUseLevel = 0,
};

public enum DamageType
{
	kDamageType_Melee = 0,
	kDamageType_Range,
	kDamageType_Magic,
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class SellItemInfo
{
	public Int64 	_itemUID;
	public Int64 	_sellItemCount;
};


public enum Item_Sell_Result
{
	kItem_Sell_Result_Success = 0,
	kItem_Sell_Result_NotFoundItem,			// 판매할 아이템을 찾을 수 없습니다.
	kItem_Sell_Result_SellCountError,		// 판매 수량이 잘못됐습니다.
	kItem_Sell_Result_NPC_LongDistance,		// NPC 와의 거리거 너무 멉니다.
	kItem_Sell_Result_NotSellShop,			// 매입 상점이 아닙니다.
	kItem_Sell_Result_IsLock,				// 잠겨있는 아이템
	kItem_Sell_Result_EquipItem,				// 장착중인 아이템
	kItem_Sell_Result_NotSellItem,			// 판매 불가능한 아이템
};

public enum ItemGetTypeEnum
{
	kItemGetTypeEnum_None = 0,				// 기본 타입
	kItemGetTypeEnum_Filed,					// 필드 드랍 획득
	kItemGetTypeEnum_ItemCreate,			// 아이템 제작
	kItemGetTypeEnum_Box,					// 상자
	kItemGetTypeEnum_GemRankUp,				// 젬스톤 승급
	kItemGetTypeEnum_PetCard,				// 펫카드
	kItemGetTypeEnum_TransformCard,			// 변신카드
	kItemGetTypeEnum_PetBlessSynthesis,		 // 펫 축복 합성 <- 추가
	kItemGetTypeEnum_TransformCardBlessSynthesis, // 변신 축복 합성 <- 추가
};

public enum WorldMap_DropItem_Get_Result
{
	kWorldMap_DropItem_Get_Result_Success = 0,
	kWorldMap_DropItem_Get_Result_LongDistance,			// 획득할 수 없는 거리
	kWorldMap_DropItem_Get_Result_NoAcquisitionRights,	// 획득 권한 없음
	kWorldMap_DropItem_Get_Result_WaitDelay,			// 딜레이 시간
	kWorldMap_DropItem_Get_Result_ItemInvenFull,		// 아이템을 획득 할 수 없습니다. 가방에 획득 가능한 공간이 없습니다.
	kWorldMap_DropItem_Get_Result_WeightOver,

	kWorldMap_DropItem_Get_Result_NotGuildMaster,		// 길드장만 획득할 수 있습니다.
	kWorldMap_DropItem_Get_Result_SiegeTimeOver,		// 공성전 진행중에만 획득할 수 있습니다.
	kWorldMap_DropItem_Get_Result_NotSiegeEntry,		// 공성전에 참여중인 길드의 길드장만 획득할 수 있습니다.
	kWorldMap_DropItem_Get_Result_NotOffenceGuild,		// 공격 길드의 길드장만 획득할 수 있습니다.
	kWorldMap_DropItem_Get_Result_InvalidAccess,		// 잘못된 접근 (공성 존이 아닌곳)

	kWorldMap_DropItem_Get_Result_NotFoundItem,			// 아이템이 없는 경우
	kWorldMap_DropItem_Get_Result_StealthCharacter,		// 투명화 상태에서는 획득할 수 없음.
};

public enum PvpResultType
{	
	kPvpResultType_Kill = 0,	// 처치
	kPvpResultType_Dead,		// 사망
	kPvpResultType_All,			// 전체
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class PVPHistoryInfo
{
	public Int64    _uid;			// 히스토리 uid
	public Int64 	_pvpTime;		// pvp 시간
	public byte 	_pvpResult;		// pvp 결과 PvpResultType
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth+1)]
	public string	_targetNickname;	// 대상의 닉네임( 결과에 따라서 공격자나 처치 대상의 닉네임)
	public Int32	_guildMark;		// 대상의 길드 마크
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName+1)]
	public string	_guildName;		// 대상의 길드명
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)CharacterInfoEnum.eMaxCharacterDeathDropItem)]
	public Int32[]	_dropItemTID;	// 사망으로 드랍된 아이템 TID
	public Int32	_additionalFeaturesSeconds;	// 조롱하기 또는 위치찾기를 진행할 수 있는 잔여시간(초)
	public Int64	_lossexp;		// 손실 경험치
};

public enum Guild_Enemy_Regist_Result
{
	kGuild_Enemy_Regist_Result_Success = 0,		// 등록 완료
	kGuild_Enemy_Regist_Result_NotGuildManager,	// 길드 관리자가 아니다
	kGuild_Enemy_Regist_Result_NotFoundGuild,	// 해당 길드를 찾을 수 없다
	kGuild_Enemy_Regist_Result_Max_EnemyGuild,	// 등록된 적대 길드가 최대치 이다
	kGuild_Enemy_Regist_Result_AllianceGuild,	// 동맹 관계의 길드이다.
	kGuild_Enemy_Regist_Result_AlreadyEnemyGuild,	// 이미 적대길드 이다.
	kGuild_Enemy_Regist_Result_BelongGuild,		// 현재 소속된 길드입니다.
	kGuild_Enemy_Regist_Result_RequestedAllianceGuild,	// 동맹을 요청한 길드입니다.
};

public enum Guild_Enemy_Delete_Result
{
	kGuild_Enemy_Delete_Result_Success = 0,		// 삭제 완료
	kGuild_Enemy_Delete_Result_NotGuildManager,	// 길드 관리자가 아니다
	kGuild_Enemy_Delete_Result_NotFoundGuild,	// 적대 길드 목록에서 찾을 수 없다
	kGuild_Enemy_Delete_Result_ReleaseEnemyInterval, //등록 후 72시간이 지나지 않았을 경우
};

public enum Guild_Alliance_Request_Result
{
	kGuild_Alliance_Request_Result_Success = 0,		// 요청 완료
	kGuild_Alliance_Request_Result_NotGuildManager,	// 길드 관리자가 아니다
	kGuild_Alliance_Request_Result_NotFoundGuild,	// 존재하지 않는 길드
	kGuild_Alliance_Request_Result_AlreadyAlliance,	// 이미 동맹 관계이다
	kGuild_Alliance_Request_Result_EnemyGuild,		// 적대 관계의 길드이다.
	kGuild_Alliance_Request_Result_SendMax,			// 요청을 더이상 보낼 수 없다.
	kGuild_Alliance_Request_Result_RecvMax,			// 상대방이 더이상 요청을 받을 수 없다
	kGuild_Alliance_Request_Result_AlreadyRequest,	// 이미 요청한 길드다.
	kGuild_Alliance_Request_Result_BelongGuild,		// 현재 소속된 길드입니다.
};

public enum Guild_Alliance_RequestCancel_Result
{
	kGuild_Alliance_RequestCancel_Result_Success = 0,		// 취소 완료
	kGuild_Alliance_RequestCancel_Result_NotGuildManager,	// 길드 관리자가 아니다
	kGuild_Alliance_RequestCancel_Result_NotFoundRequest,	// 찾을 수 없는 요청
};

public enum Guild_Alliance_RequestReject_Result
{
	kGuild_Alliance_RequestReject_Result_Success = 0,		// 취소 완료
	kGuild_Alliance_RequestReject_Result_NotGuildManager,	// 길드 관리자가 아니다
	kGuild_Alliance_RequestReject_Result_NotFoundRequest,	// 찾을수 없는 요청
};

public enum Guild_Alliance_RequestAccept_Result
{
	kGuild_Alliance_RequestAccept_Result_Success = 0,			// 동맹 완
	kGuild_Alliance_RequestAccept_Result_NotGuildManager,		// 길드 관리자가 아니다
	kGuild_Alliance_RequestAccept_Result_NotFoundRequest,		// 찾을수 없는 요청
	kGuild_Alliance_RequestAccept_Result_MaxAlliance,			// 더이상 동맹을 맺을 수 없다.
	kGuild_Alliance_RequestAccept_Result_MaxAllianceTargetGuild,// 더이상 동맹을 맺을 수 없다.
	kGuild_Alliance_RequestAccept_Result_AreadyAlliance,		// 이미 동맹이다.
	kGuild_Alliance_RequestAccept_Result_EnemyGuild,			// 적대 길드입니다.
};

public enum Guild_Alliance_Cancel_Result
{
	kGuild_Alliance_Cancel_Result_Success = 0,			// 동맹 파기 완료.
	kGuild_Alliance_Cancel_Result_NotGuildManager,		// 매니저가 아니다.
	kGuild_Alliance_Cancel_Result_NotAllianceGuild,		// 동맹 관계가 아닙니다.
	kGuild_Alliance_Cancel_Result_NotFoundGuild,		// 존재하지 않는 길드입니다.

};

public enum Use_TransformScroll_Result
{
	kUse_TransformScroll_Result_Success = 0,
	kUse_TransformScroll_Result_NotEnoughScroll,	// 변신 주문서가 부족하다
	kUse_TransformScroll_Result_LowLevel,			// 레벨이 낮다
	kUse_TransformScroll_Result_NotFoundTransform,	// 보유하지 않는 변신이다.
	kUse_TransformScroll_Result_OverUseItemLevel,	// 아이템 사용가능 레벨보다 높다.
	kUse_TransformScroll_Result_LowUseItemLevel,		// 아이템 사용가능 레벨보다 낮다.
	kUse_TransformScroll_Result_DebuffPenalty,		// 디버프로 인해 사용할 수 없습니다.
};

public enum QuickSlotType
{
	kQuickSlotType_Empty = 0,
	kQuickSlotType_ItemUID,
	kQuickSlotType_ItemTable,
	kQuickSlotType_SkillTable,
};

public enum WorldBoss_Teleport_Result
{
	kWorldBoss_Teleport_Result_Success = 0,
	kWorldBoss_Teleport_Result_NoEntryTime,		// 입장 불가 시간
	kWorldBoss_Teleport_Result_NotEnoughGold,	// 골드 부족
	kWorldBoss_Teleport_Result_LowLevel,		// 입장 레벨이 낮음
	kWorldBoss_Teleport_Result_CantEnterState,	// 입장 불가 상태
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class QuickSlotInfo
{
	public byte		_slotIdx;
	public byte		_slotType;
	public Int64	_slotValue;
	public byte		_slotState;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class RankChrInfo
{
	public Int32   _rank;		// 순위
	public Int32	_prevRank;	// 갱신 전 순위
	public Int64	_chrUID;
	public Int16	_chrTID;	// 캐릭터 테이블ID 클래스 ID
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth+1)]
	public string	_nickname;	// 닉네임
	public Int64	_guildUID;	// 길드UID
	public Int32	_guildMark;	// 길드 마크
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName+1)]
	public string	_guildName;	// 길드명
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class CharacterRankInfo
{
	public Int32	_totalRank;			// 전체 랭킹
	public Int32	_classRank;			// 클래스 랭킹
	public Int32	_prevTotalRank;		// 이전 전체 랭킹
	public Int32	_prevClassRank;		// 이전 클래스 랭킹
};

public enum RankingEnum
{
	kRanking_Max = 100,
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class CharacterGuildInfo
{
	public Int64	_guildUID;
	public Int32	_guildMark;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName+1)]
	public string	_guildName;
};

public enum Character_Delete_Result
{
	kCharacter_Delete_Result_Success = 0,		// 삭제 진행
	kCharacter_Delete_Result_LowLevel,			// 레벨이 낮다
	kCharacter_Delete_Result_DeleteWaitState,	// 이미 삭제 대기중이다
	kCharacter_Delete_Result_InvalidAccess,		// 잘못된 접근
	kCharacter_Delete_Result_ItemExchangeNotZero,		// 거래소에 아이템/정산내역이 있는경우
	kCharacter_Delete_Result_JoinedGuild,		// 길드에 가입되어 있는 경우
	kCharacter_Delete_Result_IsMail,			// 우편함에 메일이 남아있는 경우
};

public enum Character_DeleteCancel_Result
{
	kCharacter_DeleteCancel_Result_Success = 0,		// 삭제 취소 완료
	kCharacter_DeleteCancel_Result_InvalidAccess,	// 잘못된 접근
};

public enum Connect_ChattingServer_Result
{
	kConnect_ChattingServer_Result_Success = 0,		// 접속 성공
	kConnect_ChattingServer_Result_InvalidAccess,		// 잘못된 접근
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class PartyMemberInfo
{
	public Int64	_chrUID;		// 캐릭터UID
	public Int32	_chrTID;		// charactertable ID
	public Int16	_chrLevel;		// 캐릭터 레벨
	public Int32	_hp;			// 현재 체력
	public Int32	_maxHp;			// 최대 체력
	public Int32	_mp;			// 현재 마나
	public Int32	_maxMp;			// 최대 마나
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth+1)]
	public string	_nickname;		// 닉네임
	public byte	_num;				// 파티내 번호
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class PetSynthesisInfo
{
	public Int32 _petTID;		// 펫TID
	public Int64 _count;		// 수량
};

public enum PartyMemberType
{
	kPartyMember_Normal	= 0,
	kPartyMember_Owner	= 1,
};
public enum Party_Create_Result
{
	kParty_Create_Result_Success = 0,
	kParty_Create_Result_AlreadyParty,	// 이미 파티에 소속되어있음
};

public enum Party_Invite_Result
{
	kParty_Invite_Result_Success = 0 ,			// 초대했습니다.
	kParty_Invite_Result_NotFoundCharacter,		// 초대할 사람이 로그인이 아닙니다.
	kParty_Invite_Result_InviteAccept,			// 초대를 수락했다
	kParty_Invite_Result_NotPartyMaster,		// 파티장이 아니다
	kParty_Invite_Result_FullParty,				// 더이상 초대할 수 없다
	kParty_Invite_Result_EmptyNickname,			// 닉네임을 입력해라
	kParty_Invite_Result_Logout,				// 로그아웃중이다
	kParty_Invite_Result_OtherPartyMember,		// 다른 파티에 참여중이다.
	kParty_Invite_Result_RejectingInvite,		// 초대 거부 중입니다.
	kParty_Invite_Result_Invite_Get_Block,		// 차단당했을 경우.
	kParty_Invite_Result_Invite_Blocked,		// 차단했을 경우.
	kParty_Invite_Result_SameParty,				// 동일한 파티
	kParty_Invite_Result_Register_EnemyGuild,	// 적대길드로 등록한 경우
	kParty_Invite_Result_Registered_EnemyGuild,	// 적대길드로 등록된 경우
};

public enum Party_AnswerInvite_Result
{
	kParty_AnswerInvite_Result_Accept = 0,			// 수락했습니다.
	kParty_AnswerInvite_Result_Reject,				// 거절 했습니다.
	kParty_AnswerInvite_Result_NotFoundParty,		// 존재하지 않는 파티입니다.
	kParty_AnswerInvite_Result_FullParty,			// 파티인원이 최대치 입니다. 참여할 수 없습니다.
	kParty_AnswerInvite_Result_OtherPartyMember,	// 이미 파티에 참여중입니다.
	kParty_AnswerInvite_Result_Blocking,			// 파티장을 차단했을 경우(메시지 미노출)

};

public enum ChangeNickname_Result
{
	kChangeNickname_Result_Succes = 0,
	kChangeNickname_AlreadyMakeNickName,	// 존재하는 닉네임 입니다.
	kChangeNickname_BadNickName,			// 생성할 수 없는 닉네임
	kChangeNickname_SlangNickName,			// 비속어가 포함되어 있는 닉네임
	kChangeNickname_NicknameLengthError,	// 닉네임 길이가 잘못됨
	kChangeNickname_NotEnoughChangeNickname, // 닉네임 변환권이 없습니다.
	kChangeNickname_IsBattle,				// 전투 중 일경우
	kChangeNickname_InvalidAccess,			 // 잘못된 접근입니다.
};

public enum SpecialAttendanceResult
{
	kSpecialAttendanceResult_Receive = 0,		// 아이템을 받았다.
	kSpecialAttendanceResult_Received,			// 아이템을 이미 받았다.
	kSpecialAttendanceResult_EventTimeOver,		// 이벤트 기간이 아니다
	kSpecialAttendanceResult_InvalidAccess,		// 잘못된 접근
};

public enum Item_GemStoneEnchant_Result
{
	kItem_GemStoneEnchant_Result_Success = 0,	// 성공
	kItem_GemStoneEnchant_Result_Fail,			// 실패
	kItem_GemStoneEnchant_Result_GemStoneLock,	// 잠겨있는 아이템이다
	kItem_GemStoneEnchant_Result_ScrollLock,	// 잠금 상태의 장비는 강화를 진행할 수 없습니다. 잠금 상태를 해제하고 진행해주세요.
	kItem_GemStoneEnchant_Result_DontEnchant,	// 더이상 강화할 수 없습니다.
	kItem_GemStoneEnchant_Result_CantEnchantNotFoundEquipItem,	// 강화를 진행할 수 없습니다. 존재하지 않는 아이템 입니다.
	kItem_GemStoneEnchant_Result_DontHaveCostItem, // 강화에 필요한 비용이 부족합니다.
};

public enum Item_GemStoneReproduction_Result
{
	kItem_GemStoneReproduction_Result_Success = 0,							// 성공
	kItem_GemStoneReproduction_Result_GemStoneLock,							// 잠금 상태의 젬스톤입니다.
	kItem_GemStoneReproduction_Result_ScrollLock,							// 잠금 상태의 주문서 입니다.
	kItem_GemStoneReproduction_Result_DontReproduction,						// 재부여를 할 수 없습니다.
	kItem_GemStoneReproduction_Result_CantReproductionNotFoundEquipItem,	// 재부여할 수 없는 젬스톤입니다.
	kItem_GemStoneReproduction_Result_DontHaveCostItem,						// 재부여에 필요한 비용이 부족합니다.
	kItem_GemStoneReproduction_Result_AllLock,								// 옵션 잠금은 최소 1개이상은 해제되어야 합니다. (추가)
};


public enum Item_GemStoneRankUp_Result
{
	kItem_GemStoneRankUp_Result_Success = 0,
	kItem_GemStoneRankUp_Result_NotFoundItem,	// 존재하지 않는 아이템입니다.
	kItem_GemStoneRankUp_Result_NotEnoughCost,	// 승급 비용이 부족합니다.
	kItem_GemStoneRankUp_Result_CantRankUp,		// 더 이상 승급할 수 없습니다.
	kItem_GemStoneRankUp_Result_GemStoneLock,	// 젬스톤이 잠겨있다.
	kItem_GemStoneRankUp_Result_SrcGemStoneLock,	// 재료 젬스톤이 잠겨있다.
};

public enum Summon_Pet_Result
{
	kSummon_Pet_Result_Success = 0,				// 펫 소환
	kSummon_Pet_Result_NotEnoughScroll,			// 소환 스크롤 부족
	kSummon_Pet_Result_OverUseItemLevel,		// 잘못된 접근
	kSummon_Pet_Result_LowUserItemLevel,		// 잘못된 접근
	kSummon_Pet_Result_InvalidAccess,			// 잘못된 접근
	kSummon_Pet_Result_DebuffPenalty,			// 디버프로 인해 사용할 수 없습니다.
};

public enum Pet_Temp_Change_Result
{
	kPet_Temp_Change_Success = 0,			// 다시 뽑기 완료
	kPet_Temp_Change_NotEnoughCostItem,		// 다시 뽑기 재료가 부족하다
	kPet_Temp_Change_TimeOver,				// 다시 뽑기 기간 만료
	kPet_Temp_Change_RetryCountOver,		// 다시 뽑기 횟수 초과
	kPet_Temp_Change_CantGrade,				// 다시 뽑기를 진행할 수 없는 등급
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class PetCollectionInfo
{
	public Int32 tableID;				// PetCollectionTable ID
	public byte collectionData;			// 등록된 컬렉션 정보
}

public enum Pet_GetSynthesisFailReward_Result
{
	kPet_GetSynthesisFailReward_Result_Success = 0,		// 진행 성공
	kPet_GetSynthesisFailReward_Result_LowCount,		// 실패 횟수 부족
};

public enum PetDeCompositionEnum
{
	kPetDeCompositionEnum_SrcSlot = 8,
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack = 1)]
public class Item_Enchant_Multiple_Data
{
	public byte _result;
	public ItemInfo _itemInfo;
}

public enum Party_Ban_Result 
{
	kParty_Ban_Result_Success = 0,
	kParty_Ban_Result_NotMaster,		// 파티장만 추방할 수 있다.
	kParty_Ban_Result_NotMember,		// 추방 대상이 파티원이 아닙니다.
};

public enum Event_Restore_Pet_Result
{
	kEvent_Restore_Pet_Result_Success = 0,
	kEvent_Restore_Pet_Result_DontHaveTicket = 1,
};

public enum Event_Restore_TC_Result
{
	kEvent_Restore_TC_Result_Success = 0,
	kEvent_Restore_TC_Result_DontHaveTicket = 1,
};


public enum Event_Restore_Item_Result
{
	kEvent_Restore_Item_Result_Success = 0,
	kEvent_Restore_Item_Result_DontHaveTicket,		// 복구 티켓 없음
	kEvent_Restore_Item_Result_InvalidRestoreItem,	// 잘못된 복구대상(이미 복구된 아이템이거나, 복구 타입이 다름)
};

public enum BossRaid_GetMissionReward_Result
{
	kBossRaid_GetMissionReward_Result_Success = 0,		// 보상이 우편함으로 지급되었습니다.
	kBossRaid_GetMissionReward_Result_AlreadyGet,		// 이미 받은 보상입니다.
	kBossRaid_GetMissionReward_Result_PrevRewardGet,		// 이전 보상을 먼저 수령해주세요.
	kBossRaid_GetMissionReward_Result_LowMissionCount,	// 보상받을 참여횟수가 안됨
};

public enum BossRaid_MissionReset_Result
{
	kBossRaid_MissionReset_Result_Success = 0,
	kBossRaid_MissionReset_Result_NotEnoughCostItem,	// 리셋 비용 부족
	kBossRaid_MissionReset_Result_NotAllGetReward,		// 모든 보상을 다 받아야지만 리셋 가능
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildItemExchangeInfo
{
	public Int64	_uid;				// 거래소아이템UID
	public Int64	_sellerChrUID;		// 판매자 캐릭터UID
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_registerNickname;	// 판매자 닉네임
	public Int32	_tableID;			// ItemTable ID
	public Int32	_itemCount;			// 수량
	public Int32	_price;				// 가격
	public Int64	_sellEndTime;		// 판매 종료 시간
	public byte	_attributeType;			// 속성 타입
	public byte	_attributeEnchant;		// 속성 강화 수치
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;			// 추가 옵션 타입
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;		// 추가 옵션 값
	public Int32	_exchange_limitCount;	//거래 제한 횟수
};

public enum GuildItem_Exchange_Enum
{
	kGuildItemExchange_ItemCount = 9999,
	kGuildItemExchange_SellRegistCount = 30,
	kGuildItemExchange_MinPrice = 10,
	kGuildItemExchange_MaxPrice = 9999999,
	kGuildItemExchange_SellBaseFeePercent = 10,
	kGuildItemExchange_MaxItemExchangeHistorySize = 30,
};

public enum GuildItem_Exchange_SellRegist_Result
{
	kGuildItem_Exchange_SellRegist_Result_Success = 0,
	kGuildItem_Exchange_SellRegist_Result_NotFoundItem,		// 존재하지 않는 아이템
	kGuildItem_Exchange_SellRegist_Result_CantExchangeItem,	// 거래소 등록 불가 아이템
	kGuildItem_Exchange_SellRegist_Result_ItemCountMiss,	// 아이템 수량이 1보다 작거나 최대 수량보다 더 많은 수량으로 판매하려고 할때
	kGuildItem_Exchange_SellRegist_Result_MaxRegist,		// 더이상 등록할 수 없습니다. 수량 제한.
	kGuildItem_Exchange_SellRegist_Result_PriceError,		// 판매 금액 오류 100보다 작거나 Max보다 큼
	kGuildItem_Exchange_SellRegist_Result_ItemIsLock,		// 아이템이 잠금 상태다
	kGuildItem_Exchange_SellRegist_Result_ItemIsEquipped,	// 아이템이 장착 중이다.
	kGuildItem_Exchange_SellRegist_Result_NotGuildMember,	// 소속된 길드가 없다
	kGuildItem_Exchange_SellRegist_Result_InvalidAccess,	// 비정상 적인 접근
	kGuildItem_Exchange_SellRegist_Result_NotEnoughRegistFee,	// 등록 수수료가 부족합니다.
	kGuildItem_Exchange_SellRegist_Result_LowLevel,			// 캐릭터 레벨이 낮아서 등록할 수 없습니다.
	kGuildItem_Exchange_SellRegist_Result_GuestAccount,		// 게스트 계정은 이용 불가합니다.
	kGuildItem_Exchange_SellRegist_Result_LowGuildLevel,	// 길드 레벨이 낮아서 구입할 수 없습니다.
	kGuildItem_Exchange_SellRegist_Result_Over_LimitCount,	//거래 가능 횟수가 없습니다.
};

public enum GuildItem_Exchange_Buy_Result
{
	kGuildItem_Exchange_Buy_Result_Success = 0,
	kGuildItem_Exchange_Buy_Result_NotEnoughDia,	// 다이아가 부족하다.
	kGuildItem_Exchange_Buy_Result_AlreadySell,		// 이미 판매된 아이템이다.
	kGuildItem_Exchange_Buy_Result_SellTimeOver,	// 판매 시간이 만료되었다.
	kGuildItem_Exchange_Buy_Result_MyItem,			// 본인이 등록한 아이템은 구매할 수 없습니다.
	kGuildItem_Exchange_Buy_Result_NotGuildMember,	// 소속된 길드가 없다.
	kGuildItem_Exchange_Buy_Result_LowLevel,		// 캐릭터 레벨이 낮아서 구입할 수 없습니다.
	kGuildItem_Exchange_Buy_Result_LowGuildLevel,	// 길드 레벨이 낮아서 구입할 수 없습니다.
	kGuildItem_Exchange_Buy_Result_AleadyCancle,	// 판매 취소된 아이템 입니다.
	kGuildItem_Exchange_Buy_Result_InvenFull,		// 인벤토리가 가득 참
};

public enum GuildItem_Exchange_SellRegistCancel_Result
{
	kGuildItem_Exchange_SellRegistCancel_Result_Success = 0,
	kGuildItem_Exchange_SellRegistCancel_Result_AlreadySell,		// 판매됐거나 이미 취소한 아이템 입니다.
	kGuildItem_Exchange_SellRegistCancel_Result_NotGuildMember,		// 소속된 길드가 없다.
	kGuildItem_Exchange_SellRegistCancel_Result_NotGuildManager,	// 길드 관리자가 아닌 경우 본인이 등록한 아이템만 취소할 수 있습니다.
	kGuildItem_Exchange_SellRegistCancel_Result_InvenFull,			// 인벤이 가득 찬 경우
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildItemExchangeHistoryInfo
{
	public Int64	_uid;
	public Int64	_exchangeTime;		// 판매된 시간
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_registerNickname;	// 판매자
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_buyerNickname;		// 구매자
	public byte		_actionType;		// GuildItemExchangeHistoryActionType
	public Int32	_registPrice;		// 판매 가격
	public Int32	_settlementPrice;	// 정산 가격
	public Int32	_tableID;			// ItemTableTID
	public Int32	_itemCount;			// 수량
	public byte	_attributeType;
	public byte	_attributeEnchant;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildDiaDistributionInfo
{
	public Int64	_chrUID;					// 캐릭터UID
	public Int64	_distributionDiaCount;		// 분배 다이아 카운트
};

public enum Guild_Dia_Distribution_Result
{
	kGuild_Dia_Distribution_Result_Success = 0,
	kGuild_Dia_Distribution_Result_NotGuildManager,			// 길드 관리자가 아님
	kGuild_Dia_Distribution_Result_TotalDiaOver,			// 분배 총액이 길드 소지 다이아 보다 높음
	kGuild_Dia_Distribution_Result_NotGuildMemberIncluded,	// 분배 대상에 길드원이 아닌 사람이 포함되어있음.
	kGuild_Dia_Distribution_Result_InvalidDiaCount,			// 분배 다이아가 0보다 작거나 같을때
	kGuild_Dia_Distribution_Result_LowGuildLevel,			// 이용 가능한 길드 레벨이 아닐 때
};

public enum GuildItemExchangeHistoryActionType
{
	kGuildItemExchangeHistoryActionType_Cancel = 0, // 취소
	kGuildItemExchangeHistoryActionType_Buy,	// 구매
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildDiaDistributionHistoryIdxInfo
{
	public Int64	_distributionIdx;		// 분배 진행 인덱스(분배 단위로 계속 증가되는 값)
	public Int64	_distributionTime;		// 분배 시간
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_managerNIckname;		// 분배를 진행한 캐릭터 닉네임
	public Int64	_distributionTotalDiaCount;	// 전체 분배된 수량
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildDiaDistributionHistoryInfo
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_nickname;					// 분배 받은 캐릭터 닉네임
	public Int64	_distributionDiaCount;		// 분배된 수량
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildRaidInfo
{
	public Int32	_raidTID;
	public byte		_raidState;
	public Int64	_raidCloseTimeSeconds;		// 0인 경우 닫힘 상태.
};

public enum GuildRaid_Open_Result
{
	kGuildRaid_Open_Result_Success = 0,			// 성공 시 GuildRaid_Info 요청
	kGuildRaid_Open_Result_NotGuildMember,		// 소속된 길드가 없습니다.
	kGuildRaid_Open_Result_NotGuildManager,		// 길드 관리자만 진행할 수 있습니다.
	kGuildRaid_Open_Result_NotEnoughKeyCount,	// 길드 레이드 열쇠가 부족합니다.
	kGuildRaid_Open_Result_CantOpenTime,		// 길드 레이드 오픈 제한 시간입니다.
	kGuildRaid_Open_Result_LowGuildLevel,		// 길드 레벨이 충족되지 않습니다.
	kGuildRaid_Open_Result_AlreadyOpen,			// 이미 길드 레이드가 진행 중입니다.
	kGuildRaid_Open_Result_AlreadyClear,		// 이미 클리어한 레이드 입니다.
	kGuildRaid_Open_Result_InvalidAccess,		// 진행할 수 없습니다. 잘못된 접근입니다. 길드가 조회되지 않거나 길드 레이드 번호가 잘못된경우.
};

public enum GuildRaid_Join_Result
{
	kGuildRaid_Join_Result_Success = 0,				//입장 성공
	kGuildRaid_Join_Result_RaidClose,				//길드 레이드 시작되지 않았습니다.
	kGuildRaid_Join_Result_AlreadyClear,			//이미 클리어한 레이드 입니다.
	kGuildRaid_Join_Result_AlreadyPlayOtherGuild,	//해당 레이드를 다른 길드에서 참여했습니다.
	kGuildRaid_Join_Result_NotGuildMember,			//길드에 소속되어 있지 않습니다.
	kGuildRaid_Join_Result_InvalidAccess,			// 잘못된 접근

};

public enum GuildRaidStateEnum
{
	kGuildRaidStateEnum_Close = 0, 
	kGuildRaidStateEnum_Open,		// 진행중
	kGuildRaidStateEnum_Clear,		// 클리어
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class UnSettlementItemInfo
{
	public Int64    _uid;					// 정산건에 대한 UID
	public Int32	_tableID;				// 아이템TID
	public Int32	_itemCount;				// 수량
	public Int32	_price;					// 판매 가격
	public Int32	_settlementPrice;		// 정산 가격
	public Int64	_sellEndTime;			// 판매 시간
	public Int32	_recvDia;				// 정산 받은 금액 (미정산인 경우 0)
	public byte		_attributeType;
	public byte		_attributeEnchant;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;
	public byte	_taxRate;					// 판매 시점 적용된 세율
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class Item_Exchange_SettlementInfo
{
	public Int32	_tableID;		// 아이템TID
	public Int32	_itemCount;		// 수량
	public Int32	_price;			// 판매 가격
	public Int32	_settlementDia;	// 정산 가격
	public Int64	_sellTime;		// 판매 시간
	public byte		_attributeType;		// 아이템 속성
	public byte		_attributeEnchant;	// 아이템 속성 강화
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int16[]	_option;		// 추가 옵션
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
	public Int32[]	_option_Value;
	public byte	_taxRate;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ChatBlackListInfo
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)(UserMemberInfo.eMaxNickNameLenth + 1))]
	public string	_nickName;
};

public enum Chat_BlackList_Regist_Result
{
	kChat_BlackList_Regist_Result_Success = 0,
	kChat_BlackList_Regist_Result_NotFoundCharacter, // 해당하는 닉네임의 캐릭터를 찾을 수 없습니다.
	kChat_BlackList_Regist_Result_BlackListFull,	 // 더이상 등록할 수 없습니다.
	kChat_BlackList_Regist_Result_AlreadyRegist,	 // 이미 등록되어있다.
};

public enum Chat_BlackList_UnRegist_Result
{
	kChat_BlackList_UnRegist_Result_Success = 0,
	kChat_BlackList_UnRegist_Result_NotFoundListCharacter,	// 차단 목록에 없는 닉네임 입니다.
};

public enum PVP_History_Ridicule_Result
{
	kPVP_History_Ridicule_Result_Success = 0,			// 성공
	kPVP_History_Ridicule_Result_AlreadyUse,			// 이미 사용했다.
	kPVP_History_Ridicule_Result_TimeOver,				// 사용 가능 시간 만료
	kPVP_History_Ridicule_Result_NotEnoughCostItem,     // 재화 부족
	kPVP_History_Ridicule_Result_NotFoundHistory,	    // 본인의 기록이 아니거나 잘못된 uid 전송시
	kPVP_History_Ridicule_Result_MismatchType,		    // 사망기록인데 진행 요청 시
	kPVP_History_Ridicule_Result_NotFoundMockingID,		// 조롱테이블에서 ID를 찾지 못하는 경우
};

public enum PVP_History_LocationCheck_Result
{
	kPVP_History_LocationCheck_Result_Success = 0,	// 성공
	kPVP_History_LocationCheck_AlreadyUse,			// 이미 사용했다.
	kPVP_History_LocationCheck_TimeOver,			// 사용 가능 시간 만료
	kPVP_History_LocationCheck_NotEnoughCostItem,	// 재화 부족
	kPVP_History_LocationCheck_NotFoundHistory,	    // 본인의 기록이 아니거나 잘못된 uid 전송시
	kPVP_History_LocationCheck_AttackerLogout,	    // 공격자가 로그아웃 상태 입니다.
	kPVP_History_LocationCheck_MismatchType,	    // 처치 기록인데 위치확인 요청 시
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class TransformCardInfo
{
	public Int32	_tableID;		// 테이블 ID
	public Int64	_itemCount;		// 보유 수량 (클라이언트에서 보여질때는 수량에 -1로 표기해야함)
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isLock;		// 잠금 설정
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class TransformCardTempInfo
{
	public Int64	_uid;				// 카드 고유 번호 (임시보관함은 동일 TID가 분리 되기때문에 UID필요)
	public Int32	_tableID;			// 테이블ID
	public byte		_retryCount;		// 다시뽑기 잔여  카운트
	public Int64	_retryEndTime;		// 다시뽑기 가능한 시간(unixtime) 서버 사용
	public Int64	_retryEndSeconds;	// 다시뽑기 가능한 남은 시간(초) 클라이언트 사용
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class TransformCardGachaInfo
{
	public Int32	_tableID;		// 뽑기 결과 테이블 ID
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isBlessSynthesis;	// 축복 합성 여부
};

public enum TransformCardGetType
{
	kTransformCardGetType_UseItem = 0,	// 뽑기권 사용
	kTransformCardGetType_Shop,			// 상점 뽑기
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class TransformCardCollectionInfo
{
	public Int32 tableID;			// 컬렉션테이블 TID
	public byte collectionData;		// 컬렉션 정보(비트)
}


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class TCSynthesisInfo
{
	public Int32	_tableID;	// 변신카드 TID
	public Int64	_count;		// 수량
};

public enum TransformCard_Synthesis_Enum
{
	kTransformCard_Synthesis_Enum_SrcSetCount = 11,

	kTransformCard_Synthesis_Enum_StartGrade = 1,
	kTransformCard_Synthesis_Enum_EndGrade = 5,

	kTransformCard_Synthesis_Enum_SrcItemCount = 4,
	kTransformCard_Synthesis_Enum_SpecialGrade = 5,
	kTransformCard_Synthesis_Enum_SpecialGradeSrcItemCount = 2,
};

public enum TransformCard_BlessSynthesis_Enum
{
	kTransformCard_BlessSynthesis_Enum_SrcSetCount = 11,

	kTransformCard_BlessSynthesis_Enum_StartGrade = 4,
	kTransformCard_BlessSynthesis_Enum_EndGrade = 6,

	kTransformCard_BlessSynthesis_Enum_SrcItemCount = 3,
};

public enum TransformCardTemp_Change_Result
{
	kTransformCardTemp_Change_Result_Success = 0,		// 다시 뽑기 완료
	kTransformCardTemp_Change_Result_NotEnoughCostItem,	// 다시 뽑기 재료가 부족하다
	kTransformCardTemp_Change_Result_TimeOver,			// 다시 뽑기 기간 만료
	kTransformCardTemp_Change_Result_RetryCountOver,	// 다시 뽑기 횟수 초과
	kTransformCardTemp_Change_Result_CantGrade,			// 다시 뽑기를 진행할 수 없는 등급
};

public enum TransformCard_GetSynthesisFailReward_Result
{
	kTransformCard_GetSynthesisFailReward_Result_Success = 0,		// 진행 성공
	kTransformCard_GetSynthesisFailReward_Result_LowCount,			// 실패 횟수 부족
};

public enum TransformCardDeCompositionEnum
{
	kTransformCardDeCompositionEnum_SrcSlot = 8,
};

public enum RetryContentsType
{
	kRetryContentsType_None = 0,
	kRetryContentsType_PetCard = 1,
	kRetryContentsType_TransformCard = 2,
	kRetryContentsType_Max,
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ShopAutoBuyInfo
{
	public Int32	_npcShopTID;
	public Int32	_autoBuyCount;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class WarehouseAutoKeepInfo
{
	public Int32	_itemTID;
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isKeep;
};

public enum PartyItemGetTypeEnum
{
	kPartyItemGetType_Normal = 0,		// 미 설정(일반)
	kPartyItemGetType_Random,			// 무작위
	kPartyItemGetType_Sequential,		// 번호 순
};

public enum Party_Change_ItemGetType_Result
{
	kParty_Change_ItemGetType_Result_Success = 0,
	kParty_Change_ItemGetType_Result_NotPartyMaster,	// 파티장만 변경가능
};

public enum Party_Change_PartyMaster_Result
{
	kParty_Change_PartyMaster_Result_Success = 0,
	kParty_Change_PartyMaster_Result_NotPartyMaster,	// 파티장만 변경가능
	kParty_Change_PartyMaster_Result_NotPartyMember,	// 파티원이 아님.
};

public enum Mission_Immediately_Clear_Result
{
	kMission_Immediately_Clear_Result_Success = 0,			// 성공
	kMission_Immediately_Clear_Result_NotEnoughCostItem,	// 진행에 필요한 아이템 부족
	kMission_Immediately_Clear_Result_AlreadyClear,			// 이미 클리어 상태이거나, 보상받은 상태
	kMission_Immediately_Clear_Result_InvalidAccess,		//잘못된 접근(없는 mission등)
};

public enum ScenarioQuest_Teleport_Result
{
	kScenarioQuest_Teleport_Result_Success = 0,
	kScenarioQuest_Teleport_Result_NotFoundQuestTeleport,	// 진행중인 퀘스트에 이동가능한 텔레포트가 없습니다.
	kScenarioQuest_Teleport_Result_NotEnoughGold,			// 골드가 부족하다.
	kScenarioQuest_Teleport_Result_CantTeleportState,		// 이동할 수 없는 상태다(죽었거나, 스턴상태)
	kScenarioQuest_Teleport_Result_UnderLevel,				// 레벨이 낮아서 입장할 수 없다.
	kScenarioQuest_Teleport_Result_OverLevel,				// 레벨이 높아서 입장할 수 없다.
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildInviteChrInfo
{
	public Int64	_chrUID;
	public Int32	_characterTableID;
	public Int64	_actionTime;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_chrNickname;
};

public enum Guild_Invite_Result
{
	kGuild_Invite_Result_Success = 0,		// 길드 초대 성공
	kGuild_Invite_Result_NotGuildManager,	// 길드 관리자만 초대할 수 있습니다.
	kGuild_Invite_Result_GuildMember,		// 초대 할 사람이 소속된 길드가 있습니다.
	kGuild_Invite_Result_NotFoundCharacter,	// 존재하지 않는 닉네임 입니다.
	kGuild_Invite_Result_InviteCountMax,	// 더이상 초대할 수 없습니다. 초대 인원 Max
	kGuild_Invite_Result_AlreadyInvite,		// 이미 초대한 캐릭터 입니다.
	kGuild_Invite_Result_InviteRecvMax,		// 해당 캐릭터는 더 이상 초대를 받을 수 없습니다.
	kGuild_Invite_Result_LowLevel,			// 초대할 캐릭터의 레벨이 낮아서 초대할 수 없습니다.
	kGuild_Invite_Result_GuildJoinInterval,	 // 해당 캐릭터는 초대할 수 없습니다. 탈퇴 또는 길드 해산 후 24시간이 지나지 않았습니다.
};

public enum Guild_Invite_Accept_Result
{
	kGuild_Invite_Accept_Result_Success = 0,	// 길드 초대 수락 성공
	kGuild_Invite_Accept_Result_GuildMember,	// 길드에 소속되어있어서 수락할 수 없습니다.
	kGuild_Invite_Accept_Result_NotFoundGuild,	// 초대한 길드를 찾을 수 없습니다. 해산된 길드입니다.
	kGuild_Invite_Accept_Result_GuildMemberMax,	// 가입할 수 없습니다. 해당 길드의 길드 인원이 최대치입니다.
	kGuild_Invite_Accept_Result_NotFoundInvite,	// 취소된 초대 입니다.
	kGuild_Invite_Accept_Result_GuildJoinInterval,	 // 초대를 수락할 수 없습니다. 탈퇴 또는 길드 해산 후 24시간이 지나지 않았습니다.
	kGuild_Invite_Accept_ParticipateInaSiegeAssaultGuild, // (처리됨)공성전에 참여중인 길드 (가입불가 및 토스트메시지 처리를 위함)
};

public enum Guild_Invite_Cancel_Result
{
	kGuild_Invite_Cancel_Result_Success = 0,		// 길드 초대 취소 성공
	kGuild_Invite_Cancel_Result_NotGuildManager,	// 길드 관리자만 취소할 수 있습니다.
};

public enum C2S_Guild_WeeklyGetReward_Result
{
	kC2S_Guild_WeeklyGetReward_Result_Success = 0,
	kC2S_Guild_WeeklyGetReward_Result_Fail,			// 수령할 보상이 없습니다. (보상이 지급되지 않은 상태거나, 이미 수령한 상태)
	kC2S_Guild_WeeklyGetReward_Result_LowCount,		// 보상 수령 가능한 상태이나 공헌도에 해당하는 보상이 없을때. 최저 보상 기준 미달.
};

public enum Change_GuildName_Result
{
	kChange_GuildName_Result_Success = 0,				// 성공
	kChange_GuildName_Result_NotEnoughChangeTicket,		// 변경권없음
	kChange_GuildName_Result_NotGuildManager,			// 길드 매니저만 변경할 수 있음
	kChange_GuildName_Result_AlreadSameGuildName,		// 동일한 길드명이 존재함
	kChange_GuildName_Result_GuildNameLongger,			// 길드명이 길다
	kChange_GuildName_Result_GuildNameEmpty,			// 입력한 길드명이 없다.
	kChange_GuildName_Result_BadChar,					// 허용되지 않는 문자 포함
	kChange_GuildName_Result_ParticipateInaSiegeAssaultGuild, //(처리됨) 공성전에 참여중인 길드 (가입불가 및 토스트메시지 처리를 위함)
};

public enum GuildDonationType
{
	kGuildDonationType_None = 0,		// 타입 없음
	kGuildDonationType_Gold,			// 퍼셀
	kGuildDonationType_Dia,				// 다이아
};

public enum Guild_Donation_Result
{
	kGuild_Donation_Result_Success = 0,
	kGuild_Donation_Result_NotEnoughCostItem,		// 재화 부족
	kGuild_Donation_Result_NotGuildMember,			// 길드에 소속되어있지 않음
	kGuild_Donation_Result_DonationCountMax,		// 더이상 진행할 수 없음(하루 기부 횟수 초과)
	kGuild_Donation_Result_DonationCountMaxGuild,		// 길드의 하루 기부 횟수 초과.
	kGuild_Donation_Result_InventoryFull,			// 가방이 가득차서 기부할 수 없습니다. 보상 수령 공간 부족.
	kGuild_Donation_Result_DonationInterval,		// 가입당일 날 기부를 하려는 경우
};



[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildBuffShopInfo
{
	public Int32 _tid;				// GuildBuffShopTable ID
	public Int32 _buyCount;			// 구매 횟수
	public Int64 _endTime;			// 버프 종료 시간 (초)
}

public enum Guild_BuffShop_Buy_Result
{
	kGuild_BuffShop_Buy_Result_Success = 0,
	kGuild_BuffShop_Buy_Result_NotEnoughGuildPoint,		// 길드 포인트 부족
	kGuild_BuffShop_Buy_Result_NotGuildManager,			// 관리자만 구매할 수 있습니다.
	kGuild_BuffShop_Buy_Result_BuyCountMax,				// 더이상 구매할 수 없습니다. 구매 횟수 최대.
	kGuild_BuffShop_Buy_Result_LowGuildLevel,			// 길드 레벨이 낮습니다.
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildHouseInfo
{
	public Int32 _tid;				// GuildBuffShopTable ID
	public Int64 _endTime;			// 아지트 대여 종료 시간
}

public enum Guild_House_Buy_Result
{
	kGuild_House_Buy_Result_Success = 0,
	kGuild_House_Buy_Result_NotGuildManager,		// 길드 관리자만 구매 or 연장 할 수 있습니다.
	kGuild_House_Buy_Result_NotEnoughCostItem,		// 비용이 부족합니다.
	kGuild_House_Buy_Result_LowGuildLevel,			// 길드 레벨이 낮아서 구매 or 연장 할 수 없습니다.
	kGuild_House_Buy_Result_RentalTimeMax,			// 대여 기간이 최대치입니다. 더이상 연장할 수 없습니다.
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class SettingInfo
{
	public byte	_partyInviteAgreeType;		// PartyInviteAgreeType 참고
	[MarshalAs(UnmanagedType.U1)]
	public bool	_isBroadcastAnonymous;		// 가챠, 강화 서버 전체 알림 익명 true = 익명
}

public enum PartyInviteAgreeType
{
	kPartyInviteAgreeType_AllAgree = 0,				// 모두 수락
	kPartyInviteAgreeType_GuildAndFriendsAgree,		// 길드, 친구 수락
	kPartyInviteAgreeType_AllReject,				// 모두 거절
};

public enum Item_Exchange_Open_Result
{
	kItem_Exchange_Open_Result_Success = 0,		// 오픈
	kItem_Exchange_Open_Result_LowLevel,			// 레벨이 낮아서 거래소를 이용할 수 없습니다.
	kItem_Exchange_Open_Result_Off,				// 거래소 점검 중입니다.
};

public enum GuildHistoryType
{
	kGuildHistoryType_PackageBuy = 0,		// 패키지 구매
	kGuildHistoryType_Donation_Gold,		// 기부 골드
	kGuildHistoryType_Donation_Dia,			// 기부 다이아
	kGuildHistoryType_Donation_ALL,			// 전체 내역
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildHistoryInfo
{
	public byte		_memberType;
	public byte		_actionType;
	public Int32	_cost;
	public Int64	_actionTime;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_chrNickname;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class DungeonTimeInfo
{
	public Int32	_tid;
	public Int32	_endTimeSeconds;
	public byte		_chargeCount;
};

public enum Dungeon_Teleport_Result
{
	kDungeon_Teleport_Result_Success = 0,	// 입장. 성공 시 해당 Result발송하지 않고 Teleport패킷이 발송됩니다.
	kDungeon_Teleport_Result_LowLevel,		// 캐릭터 레벨이 낮다.
	kDungeon_Teleport_Result_OverLevel,		// 캐릭터 레벨이 높다.
	kDungeon_Teleport_Result_NotOpen,		// 테이블 설정으로 입장 불가한 던전
	kDungeon_Teleport_Result_TimeOver,		// 이용 가능 시간이 초과.
	kDungeon_Teleport_Result_NotOpenTime,	// 입장 가능한 시간이 아닙니다.
	kDungeon_Teleport_Result_NotEnoughCost,	// 입장 비용이 부족합니다.
	kDungeon_Teleport_Result_CantAction,	// 캐릭터가 이동할 수 없는 상태 입니다. (스턴이나 상태이상)
	kDungeon_Teleport_Result_NotSiegeOwnerGuild, // 성 길드멤버가 아닙니다.
	kDungeon_Teleport_Result_SiegeTime,		// 공성전 진행중에는 입장할 수 없습니다.
};

public enum ResetCycleType
{
	kResetCycleType_None = 0,	// 초기화 안됨
	kResetCycleType_Daily,		// 매일 0시
	kResetCycleType_Weekly,		// 매주 일요일 0시
	kResetCycleType_Monthly,	// 매월 1일 0시
	kResetCycleType_Siege,		// 성던전
};

public enum ProgressType
{
	kProgressType_Account = 0,	// 계정 단위 진행
	kProgressType_Character,	// 캐릭터 단위 진행
};


public enum Item_Delete_Result
{
	kItem_Delete_Result_Success = 0,
	kItem_Delete_Result_IsLock,			// 잠금 상태의 아이템 입니다.
	kItem_Delete_Result_CantDelete,		// 삭제 불가한 아이템 입니다.
	kItem_Delete_Result_EquipItem,		// 장착중인 아이템 입니다.
	kItem_Delete_Result_NotFoundItem,	// 삭제할 아이템을 가방에서 찾을 수 없습니다. (이상한 값이나, 잘못보낸경우)
	kItem_Delete_Result_ItemCountError,	// 아이템 수량 입력 오류 1보다 작거나 소지 수량보다 큰경우
};

public enum ItemList_Delete_Result
{
	kItemList_Delete_Result_Success = 0,
	kItemList_Delete_Result_IsLock,			// 잠금 상태의 아이템 입니다.
	kItemList_Delete_Result_CantDelete,		// 삭제 불가한 아이템 입니다.
	kItemList_Delete_Result_EquipItem,		// 장착중인 아이템 입니다.
	kItemList_Delete_Result_NotFoundItem,	// 삭제할 아이템을 가방에서 찾을 수 없습니다. (이상한 값이나, 잘못보낸경우)
	kItemList_Delete_Result_ItemCountError,	// 아이템 수량 입력 오류 1보다 작거나 소지 수량보다 큰경우
};

public enum InGameEventType
{
	InGameEventType_None = 0,
	InGameEventType_SpecialAttendance,    // 특별 출석부  (Max 15days) 보상 세팅된 수량으로 가변적으로 사용
	InGameEventType_Level,					// 레벨
	InGameEventType_PlayTime,					// 인게임 플레이 타임
	InGameEventType_ContinuousAttendance,     // 연속 출석
	InGameEventType_Banner,					// 배너
	InGameEventType_FirstPayMent,				// 첫결제 이벤트 출석
	InGameEventType_Bingo,					// 빙고 이벤트
	InGameEventType_Dice,					// 주사위 이벤트
	InGameEventType_WelcomebackAttendance,	// 복귀 유저 출석 이벤트
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class EventProgressInfo
{
	public Int32	_tid;
	public Int32 	_actionCount;
	public Int32	_stackCount;
	public Int32	_getRewardIdx_Normal;	// 이미 받은 보상 Idx
	public Int32	_getRewardIdx_Special;	// 이미 받은 보상 Idx
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isOpenSpecialPass;		// 스페셜 패스 오픈 상태 (true면 오픈된 상태)
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class BingoEventProgressInfo
{
	public Int32	_tid;
	public Int32 	_bingoNumberData;		// 숫자 판 정보(비트값 0~24비트까지 사용 획득한 경우 해당비트는 1)
	public Int32 	_actionCount;			// 완성한 줄
	public Int32	_getRewardIdx_Normal;	// rewardIdx (지급된 보상 Idx)
	public Byte		_dailyResetCount;		// 일일 초기화 카운트
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class NPCRecoveryInfo
{
	public Int32	_tid;
	public byte		_costType;
	public Int32 	_count;
	public Int64 	_innDelaySec;
};

public enum DiceEventEnum
{
	kDiceEventEnum_MaxDice = 6,
	kDiceEventEnum_MaxActionCount = 16,
};

public enum DiceEvent_Info_Result
{
	kDiceEvent_Info_Result_Success = 0,
	kDiceEvent_Info_Result_CloseEvent,	// 진행 중인 이벤트 없음
};

public enum DiceEvent_RewardGet_Result
{
	kDiceEvent_RewardGet_Result_Success = 0,	// 아이템 수령 성공.
	kDiceEvent_RewardGet_EventTimeOver,			// 이벤트 기간이 아니다.
	kDiceEvent_RewardGet_CloseEvent,			// 진행 중인 이벤트 없음
	kDiceEvent_RewardGet_InvalidAccess,			// DB 오류나 잘못된 접근
	kDiceEvent_RewardGet_NotEnoughCostItem,		// 주사위 부족
};

public enum BingoEnum
{
	kBingoEnum_MaxBingoNumber = 25,
};

public enum BingoEvent_Info_Result
{
	kBingoEvent_Info_Result_Success = 0,
	kBingoEvent_Info_Result_CloseEvent,	// 진행 중인 이벤트 없음
};

public enum BingoEvent_Action_Result
{
	kBingoEvent_Action_Result_Success = 0,	// 아이템 수령 성공.
	kBingoEvent_Action_EventTimeOver,		// 이벤트 기간이 아니다.
	kBingoEvent_Action_CloseEvent,			// 진행 중인 이벤트 없음
	kBingoEvent_Action_InvalidAccess,		// DB 오류나 잘못된 접근
	kBingoEvent_Action_NotEnoughCostItem,	// 빙고 액션 cost부족
	kBingoEvent_Action_AllComplete,			// 최대 보상까지 모두 수령한 상태라 진행 불가
};

public enum BingoEvent_Reset
{
	kBingoEvent_Reset_Success = 0,	// 리셋 진행됨
	kBingoEvent_Reset_NotEnoughResetCount,		// 리셋 카운트 부족
	kBingoEvent_Reset_CantResetNotProgress,		// 리셋을 진행할 수 없음. 초기값 상태.
	kBingoEvent_Reset_CloseEvent,		// 리셋을 진행할 수 없음. 초기값 상태.
};

public enum SpecialAttendance_Info_Result
{
	kSpecialAttendance_Info_Result_Success = 0,
	kSpecialAttendance_Info_Result_CloseEvent,	// 진행 중인 이벤트 없음
};

public enum SpecialAttendance_RewardGet_Result
{
	kSpecialAttendance_RewardGet_Result_Success = 0,	// 아이템 수령 성공.
	kSpecialAttendance_RewardGet_Received,				// 아이템을 이미 받았다.
	kSpecialAttendance_RewardGet_EventTimeOver,			// 이벤트 기간이 아니다.
	kSpecialAttendance_RewardGet_DontRecvPrevReward,	// 이전 보상을 먼저 수령해주세요.
	kSpecialAttendance_RewardGet_DontHavePass,			// 패스권이 없다.(패스 보상 요청했으나 패스가 없을때)
	kSpecialAttendance_RewardGet_CloseEvent,			// 진행 중인 이벤트 없음
	kSpecialAttendance_RewardGet_InvalidAccess,			// DB 오류나 잘못된 접근
	kSpecialAttendance_RewardGet_NotFoundReward,		// 더이상 받을수 있는 보상이 없다.
	kSpecialAttendance_RewardGet_LowActionCount,		// 해당 보상 기준의 출석이 되지 않았다
};

public enum Event_Level_Info_Result
{
	kEvent_Level_Info_Result_Success = 0,
	kEvent_Level_Info_Result_CloseEvent,	// 진행 중인 이벤트 없음
};

public enum Event_Level_RewardGet_Result
{
	kEvent_Level_RewardGet_Result_Success = 0,			// 아이템 수령 성공.
	kEvent_Level_RewardGet_Result_Received,				// 아이템을 이미 받았다.
	kEvent_Level_RewardGet_Result_EventTimeOver,		// 이벤트 기간이 아니다.
	kEvent_Level_RewardGet_Result_DontRecvPrevReward,	// 이전 보상을 먼저 수령해주세요.
	kEvent_Level_RewardGet_Result_DontHavePass,			// 패스권이 없다.(패스 보상 요청했으나 패스가 없을때)
	kEvent_Level_RewardGet_Result_CloseEvent,			// 진행 중인 이벤트 없음
	kEvent_Level_RewardGet_Result_InvalidAccess,		// DB 오류나 잘못된 접근
	kEvent_Level_RewardGet_Result_NotFoundReward,		// 더이상 받을수 있는 보상이 없다.
	kEvent_Level_RewardGet_Result_LowActionCount,		// 해당 보상 기준의 레벨에 도달하지 못했다.
};

public enum Event_PlayTime_Info_Result
{
	kEvent_PlayTime_Info_Result_Success = 0,
	kEvent_PlayTime_Info_Result_CloseEvent,	// 진행 중인 이벤트 없음
};

public enum Event_PlayTime_RewardGet_Result
{
	kEvent_PlayTime_RewardGet_Result_Success = 0,		// 아이템 수령 성공.
	kEvent_PlayTime_RewardGet_Result_Received,			// 아이템을 이미 받았다.
	kEvent_PlayTime_RewardGet_Result_EventTimeOver,		// 이벤트 기간이 아니다.
	kEvent_PlayTime_RewardGet_Result_DontRecvPrevReward,// 이전 보상을 먼저 수령해주세요.
	kEvent_PlayTime_RewardGet_Result_DontHavePass,		// 패스권이 없다.(패스 보상 요청했으나 패스가 없을때)
	kEvent_PlayTime_RewardGet_Result_CloseEvent,		// 진행 중인 이벤트 없음
	kEvent_PlayTime_RewardGet_Result_InvalidAccess,		// DB 오류나 잘못된 접근
	kEvent_PlayTime_RewardGet_Result_NotFoundReward,	// 더이상 받을수 있는 보상이 없다.
	kEvent_PlayTime_RewardGet_Result_LowActionCount,	// 해당 보상 기준의 플레이 시간에 도달하지 못했다.
};


public enum Event_ContinuousAttendance_Result
{
	kEvent_ContinuousAttendance_Success = 0,
	kEvent_ContinuousAttendance_CloseEvent,	// 진행 중인 이벤트 없음
};

public enum Event_ContinuousAttendance_RewardGet_Result
{
	kEvent_ContinuousAttendance_RewardGet_Result_Success = 0,		// 아이템 수령 성공.
	kEvent_ContinuousAttendance_RewardGet_Result_Received,			// 아이템을 이미 받았다.
	kEvent_ContinuousAttendance_RewardGet_Result_EventTimeOver,		// 이벤트 기간이 아니다.
	kEvent_ContinuousAttendance_RewardGet_Result_DontRecvPrevReward,// 이전 보상을 먼저 수령해주세요.
	kEvent_ContinuousAttendance_RewardGet_Result_DontHavePass,		// 패스권이 없다.(패스 보상 요청했으나 패스가 없을때)
	kEvent_ContinuousAttendance_RewardGet_Result_CloseEvent,		// 진행 중인 이벤트 없음
	kEvent_ContinuousAttendance_RewardGet_Result_InvalidAccess,		// DB 오류나 잘못된 접근
	kEvent_ContinuousAttendance_RewardGet_Result_NotFoundReward,	// 더이상 받을수 있는 보상이 없다.
	kEvent_ContinuousAttendance_RewardGet_Result_LowActionCount,	// 해당 보상 기준의 플레이 시간에 도달하지 못했다.
};

public enum Event_OpenSpecialPass_Result
{
	kEvent_OpenSpecialPass_Result_Success = 0,			// 스페셜 패스 오픈 성공
	kEvent_OpenSpecialPass_Result_NotSpecialPassEvent,	// 스페셜 패스가 존재하지 않는 이벤트
	kEvent_OpenSpecialPass_Result_NotEnoughCostItem,	// 오픈에 필요한 아이템이나 재화 부족
	kEvent_OpenSpecialPass_Result_CloseEvent,			// 종료됐거나 존재하지 않는 이벤트
	kEvent_OpenSpecialPass_Result_AlreadyOpen,			// 이미 스페셜패스 오픈되어있는 이벤트
};

public enum Chat_AllianceGuild_Result
{
	kChat_AllianceGuild_Result_Success = 0,			// 성공, 자신의 길드와 발송할 길드에게 전부 Send됨. (아래 실패의 경우 전송한 사람에게만 전송됨)
	kChat_AllianceGuild_Result_DontSendSelf,		// 자신의 길드에게 보낼 수 없다.
	kChat_AllianceGuild_Result_NotFoundGuild,		// 존재하지 않는 길드
	kChat_AllianceGuild_Result_NotAllianceGuild,	// 동맹길드가 아니어서 보낼 수 없다.
	kChat_AllianceGuild_Result_NotGuildMember,		// 소속된 길드가 없다.
};


public enum ItemChangeLogType
{
	kItemChangeLogType_None = 0,
	kItemChangeLogType_DeathPanelty,				// 사망 패널티 소실						
	kItemChangeLogType_SkillCost,					// 스킬 사용 재화						
	kItemChangeLogType_ItemCollection,				// 아이템 컬렉션 등록					
	kItemChangeLogType_WorldMapTeleport,			// 월드맵 텔레포트 비용					
	kItemChangeLogType_NPCTalkTeleport,				// NPC대화 텔레포트 비용				
	kItemChangeLogType_QuestTeleport,				// 퀘스트 텔레포트 비용					
	kItemChangeLogType_RaidTeleport,				// 레이드 텔레포트 비용					
	kItemChangeLogType_NPCShopSell,					// NPC상점에 판매						
	kItemChangeLogType_NPCShopSellReward,			// NPC상점 판매 보상					
	kItemChangeLogType_SiegeTeleport,				// 공성 텔레포트 비용					
	kItemChangeLogType_ItemExchangeRegist,			// 거래소 등록							
	kItemChangeLogType_ItemExchangeRegistCancel,	// 거래소 취소							
	kItemChangeLogType_ItemExchangeRegistCost,		// 거래소 등록 비용						
	kItemChangeLogType_ItemExchangeBuyCost,			// 거래소 구입 비용						
	kItemChangeLogType_ItemExchangeSettlementGet,	// 거래소 정산 획득						
	kItemChangeLogType_PVPRidicule,					// PVP 조롱하기 비용					
	kItemChangeLogType_PVPLocationCheck,			// PVP 위치찾기							
	kItemChangeLogType_BossRaidMissionReset,		// 보스 미션 리셋 비용					
	kItemChangeLogType_RecvMailBox,					// 메일함 수령							
	kItemChangeLogType_EventPassUnlockCost,			// 스페셜 패스 오픈 비용				
	kItemChangeLogType_DiceEventCost,				// 주사위 이벤트 비용					
	kItemChangeLogType_BingoEventCost,				// 빙고 이벤트 비용						
	kItemChangeLogType_ItemUse,						// 아이템 사용							
	kItemChangeLogType_NPCShopBuyCost,				// NPC상점 구매 비용					
	kItemChangeLogType_NPCShopBuy,					// NPC상점 구매 획득					
	kItemChangeLogType_MainShopBuyCost,				// 메인상점 구매 비용					
	kItemChangeLogType_DeathPaneltyItemRestore,		// 사망패널티아이템 복구 획득			
	kItemChangeLogType_DeathPaneltyItemRestoreCost,	// 사망패널티아이템 복구 비용			
	kItemChangeLogType_DropItemGet,					// 드롭아이템 획득						
	kItemChangeLogType_QuestRewardGet,				// 퀘스트 보상획득						
	kItemChangeLogType_MissionRewardGet,			// 미션 보상획득						
	kItemChangeLogType_AchievementRewardGet,		// 업적 보상획득						
	kItemChangeLogType_MissionImmediatelyCost,		// 미션 즉시 달성 비용					
	kItemChangeLogType_ScenarioQuestRewardGet,		// 시나리오 퀘스트 보상획득				
	kItemChangeLogType_DungeonTeleportCost,			// 던전 텔레포트 비용					
	kItemChangeLogType_GraceLevelUpCost,			// 가호 레벨업 비용						
	kItemChangeLogType_NicknameChangeTicket,		// 닉네임 변경권 사용					
	kItemChangeLogType_GuildCreateCost,				// 길드 생성 비용						
	kItemChangeLogType_GuildAttendanceReward,		// 길드 출석 보상						
	kItemChangeLogType_GuildExchangeRegist,			// 길드 거래소 등록						
	kItemChangeLogType_GuildExchangeRegistCost,		// 길드 거래소 등록 비용				
	kItemChangeLogType_GuildExchangeRegistCancel,	// 길드 거래소 취소						
	kItemChangeLogType_GuildExchangeBuyCost,		// 길드 거래소 구입 비용				
	kItemChangeLogType_GuildWeeklyReward,			// 길드 주간 보상						
	kItemChangeLogType_GuildDonationCost,			// 길드 기부 비용						
	kItemChangeLogType_GuildDonationReward,			// 길드 기부 보상						
	kItemChangeLogType_GuildHouseOpenCost,			// 길드 하우스 오픈 비용				
	kItemChangeLogType_GuildHouseExtensionCost,		// 길드 하우스 연장 비용				
	kItemChangeLogType_GuildMarkChangeCost,			// 길드 마크 변경 비용					
	kItemChangeLogType_ItemDissolutionCost,			// 용해 비용							
	kItemChangeLogType_ItemDissolutionItem,			// 용해 아이템							
	kItemChangeLogType_ItemDissolution,				// 용해 획득							
	kItemChangeLogType_WarehouseIn,					// 창고 보관							
	kItemChangeLogType_WarehouseOut,				// 창고 보관							
	kItemChangeLogType_WarehouseOutCost,			// 창고 찾기 비용						
	kItemChangeLogType_WarehouseExpandCost,			// 창고 확장 비용						
	kItemChangeLogType_ItemCreateMaterial,			// 아이템 제작 재료						
	kItemChangeLogType_ItemCreateSuccess,			// 아이템 제작 획득						
	kItemChangeLogType_ItemInvenExpandCost,			// 인벤 확장 비용						
	kItemChangeLogType_GemStoneEnchantCost,			// 매직스톤 강화 비용					
	kItemChangeLogType_GemStoneReproductionCost,	// 매직스톤 재부여 비용					
	kItemChangeLogType_GemStoneRankUpCost,			// 매직스톤 승급 재료					
	kItemChangeLogType_PetTempChangeCost,			// 소환수 교환 비용						
	kItemChangeLogType_ItemEnchantFail,				// 아이템강화실패소실					
	kItemChangeLogType_ItemEchantCost,				// 아이템 강화 스크롤 사용				
	kItemChangeLogType_ItemAttributeChangeCost,		// 아이템 속성 변경 비용				
	kItemChangeLogType_ItemEventRestoreCost,		// 아이템 이벤트 복구 티켓				
	kItemChangeLogType_PetEventRestoreCost,			// 소환수 이벤트 다시뽑기 티켓			
	kItemChangeLogType_TCEventRestoreCost,			// 코스튬 이벤트 다시뽑기 티켓			
	kItemChangeLogType_TCTempChangeCost,			// 코스튬 변경 비용						
	kItemChangeLogType_RandomBoxUseGet,				// 랜덤박스 사용 획득					
	kItemChangeLogType_ItemBoxUseGet,				// 아이템상자 사용 획득					
	kItemChangeLogType_UserActionItemDelete,		// 아이템 사용자 삭제					
	kItemChangeLogType_SkillPromotionCost,			// 스킬 강화 비용						
	kItemChangeLogType_StatResetCost,				// 스텟초기화 비용						
	kItemChangeLogType_DeathPaneltyExpRestoreCost,	// 사망 패널티 경험치 복구
	kItemChangeLogType_NPCRecoveryCost,				// NPC회복 비용
	kItemChangeLogType_OfflineModeReward,			// 오프라인 모드 보상
	kItemChangeLogType_PresetExpendCost,			// 프리셋 확장 비용
	kItemChangeLogType_GuildExchangeBuy,			// 길드거래소 구매
	kItemChangeLogType_MainShopBuy,					// 메인상점 구매
	kItemChangeLogType_GuildStrongholdOpenCost,		// 길드 요새 구매 비용
	kItemChangeLogType_GuildStrongholdChangeNameCost,// 길드 요새 이름 변경 비용
	kItemChangeLogType_Tasks_Contribute_Reward,		// 길드임무 주간공헌도 보상
	kItemChangeLogType_Tasks_Donation_GetTicket,	// 길드임무 룰렛 티켓 획득
	kItemChangeLogType_Tasks_Donation_RewardCost,	// 길드임무 룰렛 아이템 소모
	kItemChangeLogType_Tasks_Donation_Reward,		// 길드임무 룰렛 아이템 획득
	kItemChangeLogType_Tasks_Wanted_Reward,			// 길드임무 현상수배 보상 획득
	kItemChangeLogType_Tasks_Wanted_Complete,		// 길드임무 현상수배 임무완수 보상 획득
	kItemChangeLogType_NPCGuildShopBuy,				// 길드 보급소 구매
	kItemChangeLogType_NPCGuildShopBuyCost,			// 길드 보급소 비용
	kItemChangeLogType_NPCGuildGeneralBuy,			// 길드 잡화상점 구매
	kItemChangeLogType_NPCGuildGeneralBuyCost,		// 길드 잡화상점 소모
	kItemChangeLogType_MissionTeleport,             // 미션 텔레포트 비용
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class EventTimeInfo
{
	public Int32	_tid;
	public Int64 	_startTime;
	public Int64 	_endTime;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class SiegeInfo
{
	public Int32	_tid;			// SiegeTableID
	public Int64 	_guildUID;		// 현재 성 주인 guildUID
	public Int64 	_roundIdx;		// 현재 공성 진행 Round
	public float 	_curTax;		// 현재 누적된 세금 (공성 종료 후 0으로 변경되고 승리 길드로 다이아 지급됨)
	public float 	_prevTax;		// 공성 종료 시 지급된 세금
	public Int32 	_cwc;			// 연속 공성 승리 카운트
	public Int64 	_prevGuildUID;	// 공성 시작 시 성 주인 guildUID 보관
	public Int32 	_prevCWC;		// 공성 시작 시 성 주인 guild 연속 승리 카운트 보관
	[MarshalAs(UnmanagedType.U1)]
	public bool 	_isCanTaxChange;	// 세금 변경 가능 여부 true일때만 세금 변경할 수 있음. 공성 종료 후 true, 변경 후 false
	public Int64 	_siegeStartTimeSeconds;	// 공성 시작까지 남은 시간  (아래 종료까지 남은 시간이 -1일때만 유효)
	public Int64 	_siegeEndTimeSeconds;	// 공성 종료까지 남은 시간	(공성 시작되지 않으면 -1, 공성 진행중일때만 유효)
	public byte		_exchangeTax;		// 해당 성의 거래소 세율
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName + 1)]
	public string	_guildName;			// 길드명
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public string	_guildMasterNickname; // 길드 마스터 닉네임
	public Int32 	_guildMark;		// 길드 마크
	public Int64	_changeTaxEndTimeSeconds;	// 성 세율 변경 가능 종료까지 남은 시간, -1인 경우 유효하지 않음.  공성전이 종료되는 시점에 세팅됨.
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class SiegeEntryGuildInfo
{
	public Int64 	_guildUID;		//guildUID
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName + 1)]
	public string	_guildName;			// 길드명
	public Int32 	_guildMark;		// 길드 마크
};

public enum Siege_Entry_JoinRequest_Result
{
	kSiege_Entry_JoinRequest_Result_Success = 0,
	kSiege_Entry_JoinRequest_Result_AlreadyEntryGuild,		// 이미 공성을 신청한 길드입니다.
	kSiege_Entry_JoinRequest_Result_NotEnoughGuildPoint,	// 길드 포인트가 부족합니다.
	kSiege_Entry_JoinRequest_Result_NotGuildManager,		// 길드 관리자만 신청할 수 있음.
	kSiege_Entry_JoinRequest_Result_MaxEntry,				// 공성 신청 길드 Max일때 신청 불가
	kSiege_Entry_JoinRequest_Result_OverTime,				// 신청 시간이 아닌경우.
	kSiege_Entry_JoinRequest_Result_LowGuildLevel,			// 길드 레벨이 선포 레벨 보다 낮을때.
	kSiege_Entry_JoinRequest_Result_OtherSiegeOwnerGuild,  // 다른 성을 소유하고 있는 길드 입니다.
	kSiege_Entry_JoinRequest_Result_OtherSiegeEntryJoined, // 다른 성에 공성 참여한 길드 입니다.
};

public enum Siege_Entry_JoinCancel_Result
{
	kSiege_Entry_JoinCancel_Result_Success = 0,
	kSiege_Entry_JoinCancel_Result_NotFoundEntryGuild,	// 공성전에 신청하지 않은 길드
	kSiege_Entry_JoinCancel_Result_NotGuildManager,		// 길드 관리자만 취소할 수 있음.
	kSiege_Entry_JoinCancel_Result_OverTime,			// 신청 시간이 아닌경우.
	kSiege_Entry_JoinCancel_Result_SiegeOwnerGuild,		// 성을 소유하고 있는 길드 입니다.
};

public enum SIege_Teleport_Result
{
	kSIege_Teleport_Result_Success = 0,
	kSIege_Teleport_Result_NotEntryGuild,			// 공성에 참여중인 길드만 공성존으로 텔레포트할 수 있습니다.
	kSIege_Teleport_Result_NotAttackerTeleportTime,	// 아직 입장할 수 없습니다. (공격측 입장 시간 아닐때)
	kSIege_Teleport_Result_NotDefenderTeleportTime,	// 아직 입장할 수 없습니다. (수비측 입장 시간 아닐때)
	kSIege_Teleport_Result_NotEnoughCostItem,		// 입장 비용이 부족합니다.
	kSIege_Teleport_Result_CantAction,				// 이동 불가 상태, 스턴 등
};


public enum Event_FirstPayment_Info_Result
{
	kEvent_FirstPayment_Info_Success = 0,
	kEvent_FirstPayment_Info_CloseEvent,	// 진행 중인 이벤트 없음
};

public enum Event_FirstPayment_RewardGet_Result
{
	kEvent_FirstPayment_RewardGet_Result_Success = 0,	// 아이템 수령 성공.
	kEvent_FirstPayment_RewardGet_Result_Received,				// 아이템을 이미 받았다.
	kEvent_FirstPayment_RewardGet_Result_EventTimeOver,			// 이벤트 기간이 아니다.
	kEvent_FirstPayment_RewardGet_Result_DontRecvPrevReward,	// 이전 보상을 먼저 수령해주세요.
	kEvent_FirstPayment_RewardGet_Result_DontHavePass,			// 패스권이 없다.(패스 보상 요청했으나 패스가 없을때)
	kEvent_FirstPayment_RewardGet_Result_CloseEvent,			// 진행 중인 이벤트 없음
	kEvent_FirstPayment_RewardGet_Result_InvalidAccess,			// DB 오류나 잘못된 접근
	kEvent_FirstPayment_RewardGet_Result_NotFoundReward,		// 더이상 받을수 있는 보상이 없다.
	kEvent_FirstPayment_RewardGet_Result_LowActionCount,		// 해당 보상 기준의 출석이 되지 않았다
	kEvent_FirstPayment_RewardGet_Result_NotPaymentAccount,		// 무과금 계정
};

public enum Siege_ChangeExchangeTaxRate_Result
{
	kSiege_ChangeExchangeTaxRate_Result_Success = 0,
	kSiege_ChangeExchangeTaxRate_Result_SiegeProgress,  // 공성전 진행 중에는 변경할 수 없음
	kSiege_ChangeExchangeTaxRate_Result_NotSiegeOwner,  // 성 소유자가 아님. 성 소유 길드의 길드장만 가능
	kSiege_ChangeExchangeTaxRate_Result_ChangeCountOver,  // 이번 회차에서는 세율을 이미 변경했습니다.
	kSiege_ChangeExchangeTaxRate_Result_SameTaxRate,  // 동일한 세율로 변경할 수 없음
	kSiege_ChangeExchangeTaxRate_Result_TimeOver,	 // 변경 가능한 시간이 아닙니다.
	kSiege_ChangeExchangeTaxRate_Result_InvalidTaxRate, // 설정할 수 없는 세율입니다.
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class SiegeTaxPaidInfo
{
	public Int64	_roundIdx;		// 회차 정보
	public Int64	_paidTime;		// 지급 일시
	public Int64	_paidTaxDia;	// 지급된 다이아
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)GuildEnum.kMaxGuildName + 1)]
	public string	_guildName;			// 길드명
	public byte	_exchangeTax;		// 성 세율
};

public enum SiegeNotiType
{
	kSiegeNotiType_Start = 0,				// 공성 시작
	kSiegeNotiType_End,						// 공성 종료
	kSiegeNotiType_ChangeSIegeGuild,		// 공성 진행 중 성 소유 변경
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GraceInfo
{
	public Int32	_groupID;
	public Int32	_graceTID;
};

public enum Grace_LevelUp_Result
{
	kGrace_LevelUp_Result_Suceess = 0,					// 성공
	kGrace_LevelUp_Result_Fail,							// 실패
	kGrace_LevelUp_Result_NotEnoughMaterialItemCount,	// 레벨업 재료 부족
	kGrace_LevelUp_Result_MaxLevel,						// 더이상 레벨업 할 수 없음. (마지막 레벨)
	kGrace_LevelUp_Result_InvalidAccess,				// 존재하지 않는 그룹이거나 데이터
	kGrace_LevelUp_Result_MaterialItemMissmatch,		// 테이블에 없는 재료 아이템 TID를 보낸경우
	kGrace_LevelUp_Result_NotEnoughCostItem,			// 비용이 부족할때
	kGrace_LevelUp_Result_NotFoundRate,					// 테이블에 존재하지 않는 확률일떄
	kGrace_LevelUp_Result_NotUnLock,					// 잠금 해제 조건이 만족하지 않을 때
};

public enum Item_Dissolution_Result
{
	kItem_Dissolution_Result_Success = 0,
	kItem_Dissolution_Result_IncludeLockItem,		// 잠금 상태의 아이템이 포함된 경우
	kItem_Dissolution_Result_IncludeEquipItem,		// 장착중인 아이템이 포함된 경우
	kItem_Dissolution_Result_IncludeNotFoundItem,	// 없는 아이템이 포함된 경우
	kItem_Dissolution_Result_IncludeCantItem,	// 용해할 수 없는 아이템이 포함된 경우
	kItem_Dissolution_Result_IncludeSameUID,		// 중복된 UID가 리스트에 있는 경우
	kItem_Dissolution_Result_NotEnoughGold,		// 용해에 필요한 골드가 부족합니다.
};

public enum Skill_Enchant_Result
{
	kSkill_Enchant_Result_Success = 0,				// 성공
	kSkill_Enchant_Result_Fail,						// 실패
	kSkill_Enchant_Result_MaterialItemLock,			// 재료 아이템이 잠금 상태이다.
	kSkill_Enchant_Result_NotEnoughMaterialItem,	// 재료가 부족하다.
	kSkill_Enchant_Result_MaxEnchant,				// 더이상 강화할 수 없다.
	kSkill_Enchant_Result_NotEnoughCostPercell,		// 퍼셀 비용 부족
	kSkill_Enchant_Result_InvalidAccess,			// 잘못된 접근
};

public enum AccountSwitchResult
{
	kAccountSwitchResult_Success = 0,	// 계정 전환 성공
	kAccountSwitchResult_NotFound,		// 전환할 계정 없음
	kAccountSwitchResult_NoCount,		// 전환 카운트 부족.
	kAccountSwitchResult_InvalidAccess, // 잘못된 접근 (로그인 전에 호출)
};

public enum Event_WelcombackAttendance_Info_Result
{
	kEvent_WelcombackAttendance_Info_Result_Success = 0,
	kEvent_WelcombackAttendance_Info_Result_CloseEvent,	// 진행 중인 이벤트 없음
	kEvent_WelcombackAttendance_Info_Result_NotReturnee, // 복귀 유저가 아님
};

public enum Event_WelcombackAttendance_RewardGet_Result
{
	kEvent_WelcombackAttendance_RewardGet_Result_Success = 0,		// 아이템 수령 성공.
	kEvent_WelcombackAttendance_RewardGet_Result_Received,			// 아이템을 이미 받았다.
	kEvent_WelcombackAttendance_RewardGet_Result_EventTimeOver,		// 이벤트 기간이 아니다.
	kEvent_WelcombackAttendance_RewardGet_Result_DontRecvPrevReward,// 이전 보상을 먼저 수령해주세요.
	kEvent_WelcombackAttendance_RewardGet_Result_DontHavePass,		// 패스권이 없다.(패스 보상 요청했으나 패스가 없을때)
	kEvent_WelcombackAttendance_RewardGet_Result_CloseEvent,		// 진행 중인 이벤트 없음
	kEvent_WelcombackAttendance_RewardGet_Result_InvalidAccess,		// DB 오류나 잘못된 접근
	kEvent_WelcombackAttendance_RewardGet_Result_NotFoundReward,	// 더이상 받을수 있는 보상이 없다.
	kEvent_WelcombackAttendance_RewardGet_Result_LowActionCount,	// 해당 보상 기준의 출석 카운트에 도달하지 못했다.
	kEvent_WelcombackAttendance_RewardGet_Result_NotReturnee,	// 복귀 유저가 아님
};

public enum Event_RewardGetAll_Result
{
	kEvent_RewardGetAll_Result_Success = 0,		// 아이템 수령 성공.
	kEvent_RewardGetAll_Result_Received,			// 아이템을 이미 받았다.
	kEvent_RewardGetAll_Result_EventTimeOver,		// 이벤트 기간이 아니다.
	kEvent_RewardGetAll_Result_CloseEvent,			// 진행 중인 이벤트 없음
	kEvent_RewardGetAll_Result_InvalidAccess,		// DB 오류나 잘못된 접근
	kEvent_RewardGetAll_Result_NotFoundReward,		// 더이상 받을수 있는 보상이 없다.
	kEvent_RewardGetAll_Result_NotPaymentAccount,	// 결제를 하지 않은 유저
	kEvent_RewardGetAll_Result_NotReturnee,	// 복귀 유저가 아님
};


public enum Shop_BuyItemCheck_Result
{
	Shop_BuyItemCheck_Result_Success = 0,

	Shop_BuyItemCheck_Result_AlreadyBuyPackage,				// 이미 구매한 패키지 상품입니다.
	Shop_BuyItemCheck_Result_NotBuyAnymore,					// 더이상 구매할 수 없다.
	Shop_BuyItemCheck_Result_TimeOver,				// 판매 기간 초과
	Shop_BuyItemCheck_Result_LowLevel,				// 레벨이 낮아서 구매할 수 없습니다.
	Shop_BuyItemCheck_Result_OverLevel,				// 레벨이 높아서 구매할 수 없습니다.

	Shop_BuyItemCheck_Result_MaxCharacterSlot,		// 캐릭터 슬롯을 더이상 확장할 수 없습니다.
	Shop_BuyItemCheck_Result_OnlyGuildMemberBuy,	// 소속 길드가 있어야 구매할 수 있습니다.

	Shop_BuyItemCheck_Result_DBError,					// DB에러

	Shop_BuyItemCheck_Result_CantBuy_NickNameChange,	// 서버통합으로 인해 캐릭터 닉네임변경권이 있는 경우
	Shop_BuyItemCheck_Result_CpuntOver,

	Shop_BuyItemCheck_CantUse_NotUseableClass,			// 구매 불가능 클래스
	Shop_BuyItemCheck_Result_ItemCountMiss,				// 1미만 100초과
	
	Shop_BuyItemCheck_Result_OfferUnactivated,           // 한정상품 비활성 상태
	Shop_BuyItemCheck_Result_InvalidOfferTID,

	Shop_BuyItemCheck_Result_PassCannotBuy,					// 보상을 전부 수령하지 못하는 경우
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ItemExchangeSellGroupInfo
{
	public Int32	_itemTID;		// 대표아이템TID
	public Int32	_registCount;	// 등록된 수량
	public float	_minPrice;		// 1개당 최소 가격
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class EnemyCharacterInfo
{
	public Int64 chrUID;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = (int)UserMemberInfo.eMaxNickNameLenth + 1)]
	public  string nickname;
}


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ItemCountInfo
{
	public Int64 itemUID;	// 수량이 변경된 아이템 UID
	public Int64 itemCount;	// 변경된 아이템 수량
}


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ResetEndTimeInfo
{
	public Int64	_daily;
	public Int64	_weekly;
	public Int64	_monthly;
};

public enum ChangeNickname_ItemUseCheck_Result
{
	kChangeNickname_ItemUseCheck_Result_Succes = 0,
	kChangeNickname_ItemUseCheck_Result_NotEnough,		 // 닉네임 변경권이 없습니다.
	kChangeNickname_ItemUseCheck_Result_CantUse_NickNameChange, // 해당 캐릭터는 이미 "닉네임 변경권"이 적용되어 있으니 확인해 주세요.
	kChangeNickname_ItemUseCheck_Result_IsBattle,		// 전투중 인 경우
	kChangeNickname_ItemUseCheck_Result_InvalidAccess,						 // 잘못된 접근입니다.
};

public enum CantAttack_Result
{
	kCantAttack_WeightOver_Result = 0,           // 무게초과
	kCantAttack_UnEquipWeapon_Result,            // 무기 미 장착
	kCantAttack_NotFoundTaget_Result,            // 타겟을 찾을 수 없는 경우
	kCantAttack_HidePenalty_Result,              // 은신패널티인 경우
	kCantAttack_RangeOver_Result,                // 사정거리가 안되는 경우
	kCantAccack_SiegeAlliances_Result,			 // 공성전 맵에서 연맹 혹은 같은 길드를 공격하는 경우
};

public enum NPC_Recovery_Result
{
	kNPC_Recovery_Result_Succes = 0,
	kNPC_Recovery_Result_Full,
	kNPC_Recovery_Result_NotEnoughCost,
	kNPC_Recovery_Result_IsDelay,
	kNPC_Recovery_Result_InvalidAccess,
};

public enum ItemCollectionType
{
	kItemCollection_Normal = 0,
	kItemCollection_Event
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ItemGetInfo
{
	public Int32	_tableid;
	public Int64	_itemcount;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class LimitCreateCountInfo
{
	public Int32   _tableid;
	public Int32   _createcount;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ItemTimeInfo
{
	public Int32	_tableid;
	public Int64	_startTime;
	public Int64	_endTime;
};

public enum Logout_Result
{
	kLogout_Result_Success = 0,		// 재시작 처리
	kLogout_Result_IsBattleState,	// 전투 상태에서는 재시작할 수 없습니다. {0}초 후 재시작할 수 있습니다.
};

public enum ItemInven_MoveSlot_Result {
	kItemInven_MoveSlot_Result_Success = 0,
	kItemInven_MoveSlot_Result_TryAgainLater, // 잠시 후 다시 이용해주세요.
};

public enum ItemInven_Alignment_Result {
	kItemInven_Alignment_Result_Success = 0,
	kItemInven_Alignment_Result_TryAgainLater, // 잠시 후 다시 이용해주세요.
};

public enum AddBuff_Reulst {
	kAddBuff_Reulst_Success = 0,
	kAddBuff_Reulst_ApplySameTypeBuff,
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ChangeItemInfo
{
	public Int64 _itemUID;
	public Int64 _itemCount;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class RewardItemInfo
{
	public Int32 _itemTID;
	public Int64 _itemCount;
};

public enum OSType {
	kOSType_None = 0,
	kOSType_AOS,
	kOSType_IOS,
	kOSType_WEB,
	kOSType_WINDOWS,
};

public enum CharacterState
{
	kCharacterState_Idle = 0,
	kCharacterState_MultiShoot,
	kCharacterState_Dead,
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class BossRaidInfo
{
	public Int32	_bossRaidTID;
	public Int32	_missionCount;
	public byte		_missionRewardGetIdx;
	public Int64	_startSec;
};

public enum AccountWithdraw_Result
{
	kAccountWithdraw_Result_Success = 0,
	kAccountWithdraw_Result_IsGuild,		// 길드가 존재하는 경우
	kAccountWithdraw_Result_IsExchange,	// 거래소에 판매등록 물품 혹은 정산받지 않은 물품이 있는경우
	kAccountWithdraw_Result_InvalidID,		// ID가 잘못된 경우
	kAccountWithdraw_Result_InvalidDB,		// DB에러
};

public enum Pet_Synthesis_Result
{
	kPet_Synthesis_Result_Success = 0,
	kPet_Synthesis_Result_PetTempInvenIsFull,	// 펫 확정인벤이 300개 이상인 경우
};

public enum TransformCard_Synthesis_Result
{
	kTransformCard_Synthesis_Result_Success = 0,
	kTransformCard_Synthesis_Result_TCTempInvenIsFull,	// 변신 확정 인벤이 300개 이상인 경우
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ExchangeItemPriceInfo
{
	public float lastPrice;
	public float maxPrice;
	public float minPrice;
};

public enum OfflineModeState
{
	eOfflineState_Wait = 0,
	eOfflineState_Progress,
	eOfflineState_Complete,
};

public enum Party_Member_ItemGet_Result
{
	tParty_ItemGet_Success = 0,
	tParty_ItemGet_WeightOver,			// 무게 100 초과
	tParty_ItemGet_InvenFull,			// 인벤 가득참
	tParty_ItemGet_LongDistance,		// 거리가 멈
	tParty_ItemGet_NotPartyMember,		// 파티원이 아닌경우(현재사용X)
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class PresetSlotInfo
{
	public Int16 _equipSlot;
	public Int64 _itemUID;
};

public enum Preset_Info_Result
{
	kPreset_Info_Result_Success = 0,
	kPreset_Info_Result_InvalidAccess,
};

public enum Preset_Change_Result
{
	kPreset_Change_Result_Success = 0,
	kPreset_DelayError,						// 교체 쿨타임이 남음
	kPreset_Change_Result_InvalidAccess,
};

public enum Preset_Copy_Result
{
	Preset_Copy_Result_Success = 0,
	kPreset_SpaceError,						// 오픈되지 않은 프리셋
	kPreset_Copy_DelayError,						// 쿨타임 남음(교체와 쿨타임 공유)
	Preset_Copy_Result_InvalidAccess,
};

public enum Preset_Expand_Result
{
	Preset_Expand_Result_Success = 0,
	kPreset_NotEnoughCost,					// 재화가 모자람
	Preset_Expand_Result_InvalidAccess,
};

public enum Preset_Reset_Result
{
	kPreset_Reset_Result_Success = 0,
	kPreset_Reset_Result_InvalidAccess,
};

public enum Preset_NameChange_Result
{
	kPreset_NameChange_Result_Success = 0,
	kPreset_LengthError,				// 1글자 미만 혹은 10글자 초과하는경우
	kPreset_DuplicateName,				// 현재 프리셋과 동일한 이름인 경우
	kPreset_NameChange_Result_InvalidAccess,
};


public enum OfflineMode_Info_Result
{
	kOffline_Info_Result_Success = 0,
	kOffline_Info_Result_IsNotReward,			// 보상을 수령받지 않았을 경우
	kOffline_Info_Result_LowLevel,			// 레벨제한에 걸려있을 경우
	kOffline_Info_Result_InvalidAccess,
};

public enum OfflineMode_Start_Result
{
	kOfflineMode_Start_Result_Success = 0,
	kOfflineMode_Start_Result_IsNotSafeZone,	// 안전구역이 아닌경우
	kOfflineMode_Start_Result_LowState,			// 진행 조건에 충족되지 않았을경우
	kOfflineMode_Start_Result_NotEnoughTime,	// 시간이 없음
	kOfflineMode_Start_Result_InvalidAccess,
};

public enum OfflineMode_Stop_Result
{
	kOfflineMode_Stop_Result_Success = 0,
	kOfflineMode_Stop_Result_InvalidAccess,
};

public enum OfflineMode_Reward_Result
{
	kOfflineMode_Reward_Result_Success = 0,
	kOfflineMode_Reward_Result_IsNotComplete,	// 받을 수 있는 보상이 없음
	kOfflineMode_reward_Result_IsNotReward,	// 다른캐릭터에 보상이 진행되고 있을 경우
	kOfflineMode_Reward_Result_InvalidAccess,
};

public enum AccountWithdraw_Regist_Result
{
	kAccountWithdraw_Regist_Result_Success = 0,
	kAccountWithdraw_Regist_Result_Fail,
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class FirebaseLogData
{
	public Int32 login_Count;
	public Int32 best_Lv;
	public Int32 best_Quest;
	public Int32 total_purchase_amount;
	public Int32 total_purchase_count;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class FieldBossGenInfo
{
	public Int32 npcTID;
	public Int32 remainingGenTime;
};

public enum Unregist_Result
{
	kUnregist_Result_Success = 0,
	kUnregist_Result_Fail,
};

public enum LimitedOfferShopActiveType
{
	eActiveType_Invalid,
	eActiveType_AchievingLevel,
	eActiveType_ItemShortage,
	eActiveType_ItemShortageCount,
	eActiveType_ItemUse,
	eActiveType_ItemUseCount,
	eActiveType_QuestClear,
	eActiveType_PvpDefeat,
	eActiveType_MonsterDefeat,
	eActiveType_Enchant_Type,
	eActiveType_Enchant_Count,
	eActiveType_Enchant_Grade,
	eActiveType_GetStone_Type,
	eActiveType_GetStone_Count,
	eActiveType_GetStone_Grade,
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class ActivatedLimitedOfferShopItem
{
	public Int32 tableID;
	public Int32 remainingTime;
};

public enum Item_Enchant_Collection_Result
{
	kItem_Enchant_Collection_Result_Success,			//강화 성공
	kItem_Enchant_Collection_Result_Fail,				//강화 실패
	kItem_Enchant_Collection_Result_NotSame,			//컬렉션 재료가 아님
	kItem_Enchant_Collection_Result_OverEncant,			//이미 강화를 넘어 선 경우
	kItem_Enchant_Collection_Result_NotCostItem,		//강화 아이템이 아닌 경우
	kItem_Enchant_Collection_Result_NotEnoughCostItem,	//강화 아이템이 부족한 경우
	kItem_Enchant_Collection_Result_NotEnoughGold,		//골드가 부족한 경우
	kItem_Enchant_Collection_Result_IsLock,				//잠겨있는 경우
	kItem_Enchant_Collection_Result_DontEnchant,		//enchantGroup가 안맞을 경우
	kItem_Enchant_Collection_Result_IsEquip,			//장착중인 경우
	kItem_Enchant_Collection_Result_InvalidAccess,		//알수없는 오류
};

public enum StealthPenaltie_Result
{
	kStealthPenaltie_Result_EquipAndNotHide = 0,	// 장착했지만 패널티 디버프로 투명화 되지 않음.
};

public enum Stronghold_Buy_Result
{
	kStronghold_Buy_Result_Success,            // 성공
	kStronghold_Buy_Result_InvalidName,        // 잘못된 요새 이름
	kStronghold_Buy_Result_PermissionDenied,   // 권한 없음. 길드 마스터가 아니거나, 길드원이 아님.
	kStronghold_Buy_Result_NotEnoughCostItem,  // 요새 구매 비용 부족
	kStronghold_Buy_Result_GuildLevelTooLow,   // 최소 요구 길드 레벨 불충족
	kStronghold_Buy_Result_AlreadyHas,         // 이미 보유중
};

public enum Stronghold_ChangeName_Result
{
	kStronghold_ChangeName_Success,            // 성공
	kStronghold_ChangeName_InvalidName,        // 잘못된 요새 이름
	kStronghold_ChangeName_PermissionDenied,   // 권한 없음. 길드 마스터가 아님.
	kStronghold_ChangeName_NotEnoughCostItem,  // 요새 이름 변경 비용 부족
	kStronghold_ChangeName_NotOwned,           // 요새 없음
};

public enum TitleType
{
	kPrefix = 0,		// 접두
	kSuffix,			// 접미
};

public enum PanelType
{
	kBuyDia = 0,		// 다이아 구매
	kMission,			// 미션 획득
	kProduct,			// 상품 구매
};

public enum TasksType
{
	InGameTasksType_None = 0,
	InGameTasksType_Contribute,
	InGameTasksType_DonationSpinner,
	InGameTasksType_MonsterWanted,
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class TitleMissionInfo
{
	public Int32 tableID;
	public Int32 actionCount;
	[MarshalAs(UnmanagedType.U1)]
	public bool isReward;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class PanelMissionInfo
{
	public Int32 tableID;
	public Int32 actionCount;
	[MarshalAs(UnmanagedType.U1)]
	public bool isReward;
};

public enum Tasks_Info_Result
{
	kTasks_Info_Result_Success = 0,
	kTasks_Info_Result_NotOwned,		//요새가 없음
	kTasks_Info_Result_NotTasks,		//임무가 없음
	kTasks_Info_Result_InvalidDB,		//DB에러
};

public enum Tasks_Contribute_Info_Result
{
	kTasks_Contribute_Result_Success = 0,
	kTasks_Contribute_Result_InvalidDB,			//DB에러
	kTasks_Contribute_Result_NotTasks,			//진행할 수 있는 임무가 없음
};

public enum Tasks_Contribute_Reward_Result
{
	kTasks_Contribute_Reward_Result_Success = 0,
	kTasks_Contribute_Reward_Result_InvalidDB,		//DB에러
	kTasks_Contribute_Reward_Result_NotTasks,		//진행할 수 있는 임무가 없음
	kTasks_Contribute_Reward_Result_LowCount,		//더 이상 받을 수 있는 보상이 없음
	kTasks_Contribute_Reward_Result_InvenFull,		//인벤토리가 가득참
};

public enum Tasks_Donaiton_Info_Result
{
	kTasks_Donaiton_Info_Result_Success = 0,
	kTasks_Donaiton_Info_Result_InvalidDB,			//DB에러
	kTasks_Donaiton_Info_Result_NotTasks,			//진행할 수 있는 임무가 없음
};

public enum Tasks_Donaiton_GetTicket_Result
{
	kTasks_Donaiton_GetTicket_Result_Success = 0,
	kTasks_Donaiton_GetTicket_Result_InvalidDB,		//DB에러
	kTasks_Donaiton_GetTicket_Result_NotTasks,		//진행할 수 있는 임무가 없음
	kTasks_Donaiton_GetTicket_Result_LowCount,		//더 이상 받을 수 있는 티켓이 없음
	kTasks_Donaiton_GetTicket_Result_InvenFull,		//인벤토리가 가득참
};

public enum Tasks_Donaiton_Reward_Result
{
	kTasks_Donaiton_Reward_Result_Success = 0,
	kTasks_Donaiton_Reward_Result_InvalidDB,		//DB에러
	kTasks_Donaiton_Reward_Result_NotTasks,			//진행할 수 있는 임무가 없음
	kTasks_Donaiton_Reward_Result_NoTicket,			//티켓이 없음
	kTasks_Donaiton_Reward_Result_InvenFull,		//인벤토리가 가득참
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class TasksInfo
{
	public Int32	_tid;
	public Int64 	_startTime;
	public Int64	_endTime;
	[MarshalAs(UnmanagedType.U1)]
	public bool		_isReddot;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class WantedMissionInfo
{
	public Int32 _selectedTargetTID;
	public Int32 _actionCount;
	public Int16 _rewardIdx;
	[MarshalAs(UnmanagedType.U1)]
	public bool _isComplete;
};


public enum Tasks_Wanted_Info_Result
{
	kTasks_Wanted_Info_Result_Success = 0,
	kTasks_Wanted_Info_Result_InvalidDB,		//DB에러
	kTasks_Wanted_Info_Result_NotTasks,			//진행할 수 있는 임무가 없음
};

public enum Tasks_Wanted_Pick_Result
{
	kTasks_Wanted_Pick_Result_Success = 0,
	kTasks_Wanted_Pick_Result_InvalidDB,		//DB에러
	kTasks_Wanted_Pick_Result_NotTasks,			//진행할 수 있는 임무가 없음
	kTasks_Wanted_Pick_Result_NotFoundNPC,		//NPC를 찾을 수 없음
	kTasks_Wanted_Pick_Result_SelectedNPC,		//이미 선택한NPC가 있음
	kTasks_Wanted_Pick_Result_NotWantedTarget,		//현상수배 갱신중
};

public enum Tasks_Wanted_Complete_Result
{
	kTasks_Wanted_Complete_Result_Success = 0,
	kTasks_Wanted_Complete_Result_InvalidDB,	//DB에러
	kTasks_Wanted_Complete_Result_NotTasks,		//진행할 수 있는 임무가 없음
	kTasks_Wanted_Complete_Result_IsComplete,	//이미 임무를 완수함
	kTasks_Wanted_Complete_Result_NotSelectNPC,	//NPC를 선택하지 않음
	kTasks_Wanted_Complete_Result_LowActionCount,	//목표를 달성하지 못함
};

public enum Tasks_Wanted_Reward_Result
{
	kTasks_Wanted_Reward_Result_Success = 0,
	kTasks_Wanted_Reward_Result_InvalidDB,		//DB에러
	kTasks_Wanted_Reward_Result_NotTasks,		//진행할 수 있는 임무가 없음
	kTasks_Wanted_Reward_Result_LowCount,		//더 이상 받을 수 있는 보상이 없음
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildDonationChrInfo
{
	public Int64 chrUID;
	public Int16 count;
};


[Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
public class GuildWantedChrInfo
{
	public Int64 chrUID;
	[MarshalAs(UnmanagedType.U1)]
	public bool isComplete;
};

public enum Title_Change_Result
{
	kTitle_Change_Result_Success = 0,
	kTitle_Change_Result_InvalidDB,
	kTitle_Change_Result_NotUnlockTitle,		// 언락되지 않은 타이틀 장착 시도
	kTitle_Change_Result_InvalidTID,			// 잘못된 타입의 장착TID가 들어옴
	kTitle_Change_Result_LowLv,					// 레벨이 모자름
	kTitle_Change_Result_DelayError,			// 쿨타임 중
};

public enum Title_Unlock_Result
{
	kTitle_Unlock_Result_Success = 0,
	kTitle_Unlock_Result_InvalidDB,
	kTitle_Unlock_Result_NotEnoughCount,		// 미션카운트가 목표에 달성하지 못한 경우
	kTitle_Unlock_Result_IsReward,				// 이미 보상을 받았을 경우
};

public enum TitlePanel_Unlock_Result
{
	kTitlePanel_Unlock_Result_Success = 0,
	kTitlePanel_Unlock_Result_InvalidDB,
	kTitlePanel_Unlock_Result_NotEnoughCount,		// 미션카운트가 목표에 달성하지 못한 경우
	kTitlePanel_Unlock_Result_NotEnoughCostItem,	// 재화가 없는 경우
	kTitlePanel_Unlock_Result_IsUnlock,			// 이미 언락이 되어있는 경우
	kTitlePanel_Unlock_Result_NotPanelType,		// 구매하지 못하는 패널 타입이 들어온 경우
};

public enum Title_Achieve_Reward_Result
{
	kTitle_Achieve_Reward_Success = 0,
	kTitle_Achieve_Reward_InvalidDB,
	kTitle_Achieve_Reward_LowCount,					// 더 이상 보상을 받을 수 없는 경우
};

public enum TitleColor_Unlock_Result
{
	kTitleColor_Unlock_Result_Success = 0,
	kTitleColor_Unlock_Result_InvalidDB,
	kTitleColor_Unlock_Result_NotEnoughCostItem,	// 재화가 없는 경우
	kTitleColor_Unlock_Result_IsUnlock,				// 이미 보유한 색상인 경우
};

public enum Mission_Teleport_Result
{
	kMission_Teleport_Result_Success = 0,          // 성공
	kMission_Teleport_Result_NotEnoughCostItem,    // 재화 부족
	kMission_Teleport_Result_LowLevel,             // 레벨 낮음
	kMission_Teleport_Result_OverLevel,            // 레벨 높음
	kMission_Teleport_Result_CantTeleportState,    // 텔레포트 불가 상태(스턴, 사망 등)
	kMission_Teleport_Result_NotFoundMission,      // 미션을 찾을 수 없음
	kMission_Teleport_Result_NotFoundTeleportInfo, // 텔레포트 정보를 찾을 수 없음
};

}



