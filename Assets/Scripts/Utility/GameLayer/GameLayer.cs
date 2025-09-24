namespace YunSun
{
	using UnityEngine;

	public class GameLayer
	{
		private const string PLAYER_LAYER_TXT = "MainPlayer";
		private const string OTHERPLAYER_LAYER_TXT = "OtherPlayer";
		private const string MONSTER_LAYER_TXT = "Monster";
		private const string BOSS_LAYER_TXT = "BOSS";
		private const string NPC_LAYER_TXT = "NPC";
		private const string PET_LAYER_TXT = "Pet";
		private const string DROP_LAYER_TXT = "DropItem";
		private const string TARGETMARK_LAYER_TXT = "TargetMark";
		private const string WALLOBJECT_LAYER_TXT = "WallObject";
		private const string BACKGROUND_LAYER_TXT = "BackGround";
		private const string SUMMON_LAYER_TXT = "Summon";
		private const string SUMMON_CHARACTER_LAYER_TXT = "SummonCharacter";
		private const string SUMMON_PET_LAYER_TXT = "SummonPet";
		private const string DEFAULT_TXT = "Default";

		static public int DefualtLayer = LayerMask.NameToLayer( DEFAULT_TXT );
		static public int BackGroundLayer = LayerMask.NameToLayer( BACKGROUND_LAYER_TXT );
		static public int MainPlayerLayer = LayerMask.NameToLayer( PLAYER_LAYER_TXT );
		static public int OtherPlayerLayer = LayerMask.NameToLayer( OTHERPLAYER_LAYER_TXT );
		static public int NPCLayer = LayerMask.NameToLayer( NPC_LAYER_TXT );
		static public int BossLayer = LayerMask.NameToLayer( BOSS_LAYER_TXT );
		static public int MonsterLayer = LayerMask.NameToLayer( MONSTER_LAYER_TXT );
		static public int DropItemLayer = LayerMask.NameToLayer( DROP_LAYER_TXT );
		static public int TargetMarkLayer = LayerMask.NameToLayer( TARGETMARK_LAYER_TXT );
		static public int SummonLayer = LayerMask.NameToLayer( SUMMON_LAYER_TXT );
		static public int SummonCharacterLayer = LayerMask.NameToLayer( SUMMON_CHARACTER_LAYER_TXT );
		static public int SummonPetLayer = LayerMask.NameToLayer( SUMMON_PET_LAYER_TXT );

		static public int BackGroundCheckLayer
			= (1 << LayerMask.NameToLayer( BACKGROUND_LAYER_TXT ))
			;
		static public int CharacterCheckLayer
			= (1 << LayerMask.NameToLayer( OTHERPLAYER_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( MONSTER_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( BOSS_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( NPC_LAYER_TXT ))
			;
		static public int TouchCheckLayer
			= (1 << LayerMask.NameToLayer( BACKGROUND_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( OTHERPLAYER_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( MONSTER_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( BOSS_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( NPC_LAYER_TXT ))
			+ (1 << LayerMask.NameToLayer( DROP_LAYER_TXT ))
			;
		static public int DropCheckLayer
			= (1 << LayerMask.NameToLayer( DROP_LAYER_TXT ))
			;
	}
}
