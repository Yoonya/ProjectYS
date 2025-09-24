namespace YunSun.UI
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;
	using TMPro;
	using Doozy.Runtime.UIManager.Components;
	using Doozy.Runtime.UIManager.Animators;
	using Doozy.Runtime.Reactor.Animators;

	static public partial class Util
	{
		//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		static private float _CoolDown_ = 0.13f;  //!< #115017
		static private float _CoolDownTimer_ = 0; //!< #113660
		//!< ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		static private bool CheckUnCoolDown()
		{
			if( _CoolDownTimer_ > Time.realtimeSinceStartup )
				return false;

			_CoolDownTimer_ = Time.realtimeSinceStartup + _CoolDown_;
			return true;
		}

#if _MOVE_TO_GameUtil_
		static public bool IsActive( this GameObject obj )
			=> obj == null ? false : obj.activeSelf;
		static public bool IsActive( this Component obj )
			=> obj == null ? false : IsActive( obj.gameObject );
		static public bool IsActiveInHierarchy( this GameObject obj )
			=> obj == null ? false : obj.activeInHierarchy;
		static public bool IsActiveInHierarchy( this Component obj )
			=> obj == null ? false : IsActiveInHierarchy( obj.gameObject );

		static public void SetActiveEx( this GameObject obj, bool value )
		{
			if( obj != null )
				obj.SetActive( value );
		}
		static public void SetActiveEx( this Component obj, bool value )
		{
			if( obj != null &&
				obj.gameObject != null )
				obj.gameObject.SetActive( value );
		}

		static public void SetParent( this GameObject obj, Transform parent )
		{
			if( obj != null &&
				obj.transform != null )
				obj.transform.SetParent( parent );
		}
		static public void SetParent( this GameObject obj, GameObject parent )
			=> SetParent( obj, parent == null ? null : parent.transform );
		static public void SetParent( this Component obj, Transform parent )
		{
			if( obj != null &&
				obj.transform != null )
				obj.transform.SetParent( parent );
		}
		static public void SetParent( this Component obj, GameObject parent )
			=> SetParent( obj, parent == null ? null : parent.transform );

		static public void SetEnabled( this Behaviour obj, bool value )
		{
			if( obj != null )
				obj.enabled = value;
		}
#endif

		static public void SetActiveOnOff( this Component obj, bool value )
		{
			if( obj != null )
			{
				foreach( Transform child in obj.transform )
				{
					if( child.name.Equals( "On" ) )
						child.gameObject.SetActive( value );
					else if( child.name.Equals( "Off" ) )
						child.gameObject.SetActive( !value );
				}
			}
		}

		static public void SetInteractable( this Selectable obj, bool value )
		{
			if( obj != null )
				obj.interactable = value;
		}
		static public void SetInteractableAll( this GameObject obj, bool value )
		{
			if( obj != null )
			{
				var gcs = obj.GetComponentsInChildren<Selectable>( true );
				if( gcs == null || gcs.Length <= 0 )
					return;

				foreach( var gc in gcs )
				{
					gc.interactable = value;
				}
			}
		}
		static public void SetInteractableAll( this Component obj, bool value )
		{
			if( obj != null )
				obj.gameObject.SetInteractableAll( value );
		}

		static public void SetColor( this Graphic obj, Color color )
		{
			if( obj != null )
				obj.color = color;
		}
		static public void SetColor( this GameObject obj, Color color )
		{
			if( obj != null )
			{
				var its = obj.GetComponentsInChildren<Graphic>( true );
				if( its != null )
				{
					foreach( var it in its )
					{
						it.color = color;
					}
				}
			}
		}
		static public void SetColor( this Component obj, Color color )
		{
			if( obj != null )
				obj.gameObject.SetColor( color );
		}
		static public void SetColor( this UIButton btn, Color color )
		{
			if( btn != null )
			{
				var imgs = btn.GetComponentsInChildren<Image>( true );
				if( imgs != null )
				{
					foreach( var img in imgs )
					{
						img.color = color;
					}
				}
				var anims = btn.GetComponentsInChildren<UISelectableColorAnimator>();
				if( anims != null )
				{
					foreach( var anim in anims )
					{
						anim.SetStartColor( color );
					}
				}
			}
		}
		static public void SetColor( this UIButton btn, bool isEnabled )
		{
			if( btn != null )
			{
				SetColor( btn, isEnabled ? btn.colors.normalColor : btn.colors.disabledColor );
			}
		}

		static public void SetAlpha( this CanvasGroup obj, float alpha )
		{
			if( obj != null )
				obj.alpha = Mathf.Clamp01( alpha );
		}

		static public void SetTextEx( this TMP_Text obj, string text )
		{
			if( obj != null )
				obj.SetText( text );
		}
		static public void SetTextEx( this TMP_Text obj, string text, params object[] args )
		{
			if( obj != null )
				obj.SetText( string.Format( text, args ) );
		}
		/*
		static public void SetTextKey( this TMP_Text obj, string key )
		{
			if( obj != null )
				obj.SetText( InGameMessageTable.GetText( key ) );
		}
		static public void SetTextKey( this TMP_Text obj, string key, params object[] args )
		{
			if( obj != null )
				obj.SetText( InGameMessageTable.GetText( key, args ) );
		}
		static public void SetTextKeyEx( this TMP_Text obj, string key )
		{
			if( obj != null )
			{
				if( string.IsNullOrWhiteSpace( key ) )
					obj.gameObject.SetActive( false );
				else
				{
					obj.gameObject.SetActive( true );
					obj.SetText( InGameMessageTable.GetText( key ) );
				}
			}
		}
		static public void SetTextKeyEx( this TMP_Text obj, string key, params object[] args )
		{
			if( obj != null )
			{
				if( string.IsNullOrWhiteSpace( key ) )
					obj.gameObject.SetActive( false );
				else
				{
					obj.gameObject.SetActive( true );
					obj.SetText( InGameMessageTable.GetText( key, args ) );
				}
			}
		}
		*/

		static public void SetTextEx( this GameObject obj, string text )
		{
			if( obj != null )
			{
				var its = obj.GetComponentsInChildren<TMP_Text>( true );
				if( its != null )
				{
					foreach( var it in its )
					{
						it.SetText( text );
					}
				}
			}
		}
		/*
		static public void SetTextKeyEx( this GameObject obj, string key )
		{
			if( obj != null )
			{
				var its = obj.GetComponentsInChildren<TMP_Text>( true );
				if( its != null )
				{
					foreach( var it in its )
					{
						it.SetTextKey( key );
					}
				}
			}
		}
		static public void SetTextKey( this GameObject obj, string key )
		{
			if( obj != null )
				obj.SetTextEx( InGameMessageTable.GetText( key ) );
		}
		static public void SetTextKey( this GameObject obj, string key, params object[] args )
		{
			if( obj != null )
				obj.SetTextEx( InGameMessageTable.GetText( key, args ) );
		}
		*/

		static public void SetTextEx( this UIToggle obj, string text )
			=> obj?.gameObject.SetTextEx( text );
		//static public void SetTextKey( this UIToggle obj, string key )
		//		=> obj?.gameObject.SetTextKey( key );

		static public void SetTextEx( this UIButton obj, string text )
			=> obj?.gameObject.SetTextEx( text );
		//static public void SetTextKey( this UIButton obj, string key )
		//	=> obj?.gameObject.SetTextKey( key );
		//static public void SetTextKey( this UIButton obj, string key, params object[] args )
		//	=> obj?.gameObject.SetTextKey( key, args );

		static public void SetTextColor( this GameObject obj, Color col )
		{
			if( obj != null )
			{
				var texts = obj.GetComponentsInChildren<TMP_Text>( true );
				if( texts != null )
				{
					foreach( var text in texts )
					{
						text.color = col;
					}
				}
			}
		}
		static public void SetTextColor( this UIToggle obj, Color col )
			=> obj?.gameObject.SetTextColor( col );
		static public void SetTextColor( this UIButton obj, Color col )
			=> obj?.gameObject.SetTextColor( col );
		static public void SetTextStyle( this UIButton obj, TextStyle style )
		{
			if( obj != null )
			{
				var texts = obj.GetComponentsInChildren<TMP_Text>( true );
				if( texts != null )
				{
					foreach( var text in texts )
					{
						text.SetTextStyle( style );
					}
				}
			}
		}

		static public void SetImages( this GameObject obj, string goName, Sprite sprite )
		{
			if( obj != null )
			{
				var images = obj.GetComponentsInChildren<Image>( true );
				if( images.Length > 0 )
				{
					foreach( var image in images )
					{
						if( image.name.Equals( goName ) )
							image.sprite = sprite;
					}
				}
			}
		}
		static public void SetImages( this Component obj, string goName, Sprite sprite )
		{
			if( obj != null )
				obj.gameObject.SetImages( goName, sprite );
		}

		static public void SetCooldown( this UISelectable obj, float cooldown, bool force )
		{
			if( obj == null )
				return;
			if( obj.Cooldown > 0 && !force )
				return;

			obj.Cooldown = cooldown;
		}

		static public void InitClickEvent( this Button obj, UnityAction action, bool skipCoolDown = false )
		{
			if( obj != null )
			{
				if( skipCoolDown )
					obj.onClick.AddListener( action );
				else
					obj.onClick.AddListener( () => { if( CheckUnCoolDown() ) action?.Invoke(); } );
			}
		}
		static public void SetClickEvent( this Button obj, UnityAction action, bool skipCoolDown = false )
		{
			if( obj != null )
			{
				obj.onClick.RemoveAllListeners();
				if( skipCoolDown )
					obj.onClick.AddListener( action );
				else
					obj.onClick.AddListener( () => { if( CheckUnCoolDown() ) action?.Invoke(); } );
			}
		}

		static public void InitClickEvent( this UIButton obj, UnityAction action, bool skipCoolDown = false )
		{
			if( obj != null )
			{
				if( skipCoolDown )
					obj.onClickEvent.AddListener( action );
				else
					obj.onClickEvent.AddListener( () => { if( CheckUnCoolDown() ) action?.Invoke(); } );
				obj.SetCooldown( _CoolDown_, force: false );
			}
		}
		static public void SetClickEvent( this UIButton obj, UnityAction action, bool skipCoolDown = false )
		{
			if( obj != null )
			{
				obj.onClickEvent.RemoveAllListeners();
				if( skipCoolDown )
					obj.onClickEvent.AddListener( action );
				else
					obj.onClickEvent.AddListener( () => { if( CheckUnCoolDown() ) action?.Invoke(); } );
				obj.SetCooldown( _CoolDown_, force: false );
			}
		}

		static public void InitToggle( this UIToggle obj, Action<bool> onChanged )
		{
			if( obj != null )
			{
				obj.OnValueChangedCallback.AddListener( val => onChanged?.Invoke( val ) );
				obj.SetCooldown( _CoolDown_, force: false );
			}
		}
		static public void SetToggle( this UIToggle obj, bool value, bool triggerValueChanged = true )
		{
			if( obj != null )
				obj.SetIsOn( value, true, triggerValueChanged );
		}
		static public void SetToggleEvent( this UIToggle obj, Action<bool> onChanged )
		{
			if( obj != null )
			{
				obj.OnValueChangedCallback.RemoveAllListeners();
				obj.OnValueChangedCallback.AddListener( val => onChanged?.Invoke( val ) );
			}
		}

		static public void SetFinishedEvent( this UIAnimator obj, UnityAction onFinished )
		{
			if( obj != null && obj.animation != null )
			{
				obj.animation.OnFinishCallback.RemoveAllListeners();
				obj.animation.OnFinishCallback.AddListener( onFinished );
			}
		}
		static public void ResetFinishedEvent( this UIAnimator obj )
		{
			if( obj != null && obj.animation != null )
			{
				obj.animation.OnFinishCallback.RemoveAllListeners();
			}
		}
	/*
		static public void SetRedDotUI( this GameObject obj, params ActiveUI.Flag[] flags )
			=> ActiveUI.SetFlags( obj, flags );
		static public void SetRedDotUI( this Component obj, params ActiveUI.Flag[] flags )
			=> ActiveUI.SetFlags( obj, flags );
		static public void SetRedDotUI( this GameObject obj, int value, Func<int, bool> isActiveFunc )
			=> ActiveUI.SetFunc( obj, value, isActiveFunc );
		static public void SetRedDotUI( this Component obj, int value, Func<int, bool> isActiveFunc )
			=> ActiveUI.SetFunc( obj, value, isActiveFunc );
		static public void SetRedDotUI( this UIToggleGroup obj, int index, params ActiveUI.Flag[] flags )
		{
			if( obj != null && 0 < obj.toggles.Count )
				ActiveUI.SetFlags( obj.toggles[index], flags );
		}
		static public void SetRedDotUI( this UIToggleGroup obj, int index, Func<int, bool> isActiveFunc )
		{
			if( obj != null && 0 < obj.toggles.Count )
				ActiveUI.SetFunc( obj.toggles[index], index, isActiveFunc );
		}
	*/
		static public void InitTabGroup( this UIToggleGroup obj, Action<int> onSelEvent, int tabCount )
		{
			if( obj != null /*&& 0 < obj.toggles.Count*/ )
			{
				var toggles = obj.toggles;
				{
					if( toggles.Count < 1 )
					{
						for( int i = 0; i < obj.transform.childCount; ++i )
						{
							var it = obj.transform.GetChild( i ).gameObject.GetComponent<UIToggle>();
							if( it != null )
								toggles.Add( it );
						}
						if( toggles.Count < 1 )
							return;
					}
				}

				obj.mode = UIToggleGroup.ControlMode.OneToggleOnEnforced;
				obj.FirstToggle = toggles[0];
				obj.SetCooldown( _CoolDown_, force: false );

				for( int i = 0; i < toggles.Count; ++i )
				{
					var ui = toggles[i];
					{
						ui.SetActiveEx( i < tabCount );
						if( tabCount <= i )
							continue;
					}
					ui.isOn = false;

					var Event = ui.OnValueChangedCallback;
					switch( i )
					{
						case 0: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 0 ); } ); break;
						case 1: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 1 ); } ); break;
						case 2: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 2 ); } ); break;
						case 3: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 3 ); } ); break;
						case 4: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 4 ); } ); break;
						case 5: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 5 ); } ); break;
						case 6: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 6 ); } ); break;
						case 7: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 7 ); } ); break;
						case 8: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 8 ); } ); break;
						case 9: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 9 ); } ); break;
						case 10: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 10 ); } ); break;
						case 11: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 11 ); } ); break;
						case 12: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 12 ); } ); break;
						case 13: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 13 ); } ); break;
						case 14: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 14 ); } ); break;
						case 15: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 15 ); } ); break;
						case 16: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 16 ); } ); break;
						case 17: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 17 ); } ); break;
						case 18: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 18 ); } ); break;
						case 19: Event.AddListener( ( val ) => { if( val ) onSelEvent?.Invoke( 19 ); } ); break;
					}
				}
			}
		}
		static public void SetTabNames( this UIToggleGroup obj, params string[] args )
		{
			if( obj != null && 0 < obj.toggles.Count )
			{
				for( int i = 0; i < obj.toggles.Count; ++i )
				{
					if( args.Length <= i )
						break;

					obj.toggles[i].SetTextEx( args[i] );
				}
			}
		}
		/*
		static public void SetTabNameKeys( this UIToggleGroup obj, params string[] args )
		{
			if( obj != null && 0 < obj.toggles.Count )
			{
				for( int i = 0; i < obj.toggles.Count; ++i )
				{
					if( args.Length <= i )
						break;

					obj.toggles[i].SetTextKey( args[i] );
				}
			}
		}
		*/
		static public void SetTabName( this UIToggleGroup obj, int index, string text )
		{
			if( obj != null && index < obj.toggles.Count )
				obj.toggles[index].SetTextEx( text );
		}
		/*
		static public void SetTabNameKey( this UIToggleGroup obj, int index, string key )
		{
			if( obj != null && index < obj.toggles.Count )
				obj.toggles[index].SetTextKey( key );
		}
		*/
		static public void SetSelectTab( this UIToggleGroup obj, int index, bool triggerValueChanged = true )
		{
			if( obj != null )
			{
				if( 0 <= index && index < obj.toggles.Count )
					obj.toggles[index].SetIsOn( true, true, triggerValueChanged );
				else
					obj.SetAllTogglesOff( true, triggerValueChanged );
			}
		}
		static public void SetActiveTab( this UIToggleGroup obj, int index, bool value )
		{
			if( obj != null && index < obj.toggles.Count )
				obj.toggles[index].SetActiveEx( value );
		}
		static public void SetActiveTabAll( this UIToggleGroup obj, bool value )
		{
			if( obj != null && 0 < obj.toggles.Count )
				obj.toggles.ForEach( it => it.SetActiveEx( value ) );
		}

		static public void InitToggleGroup( this UIToggleGroup obj, Action<int, bool> onChgEvent, int toggleCount, UIToggleGroup.ControlMode mode )
		{
			if( obj != null /*&& 0 < obj.toggles.Count*/ )
			{
				var toggles = obj.toggles;
				{
					if( toggles.Count < 1 )
					{
						for( int i = 0; i < obj.transform.childCount; ++i )
						{
							var it = obj.transform.GetChild( i ).gameObject.GetComponent<UIToggle>();
							if( it != null )
								toggles.Add( it );
						}
						if( toggles.Count < 1 )
							return;
					}
				}

				obj.mode = mode;
				obj.FirstToggle = toggles[0];
				obj.SetCooldown( _CoolDown_, force: false );

				for( int i = 0; i < toggles.Count; ++i )
				{
					var ui = toggles[i];
					{
						ui.SetActiveEx( i < toggleCount );
						if( toggleCount <= i )
							continue;
					}
					ui.isOn = false;

					var Event = ui.OnValueChangedCallback;
					switch( i )
					{
						case 0: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 0, val ); } ); break;
						case 1: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 1, val ); } ); break;
						case 2: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 2, val ); } ); break;
						case 3: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 3, val ); } ); break;
						case 4: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 4, val ); } ); break;
						case 5: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 5, val ); } ); break;
						case 6: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 6, val ); } ); break;
						case 7: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 7, val ); } ); break;
						case 8: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 8, val ); } ); break;
						case 9: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 9, val ); } ); break;
						case 10: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 10, val ); } ); break;
						case 11: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 11, val ); } ); break;
						case 12: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 12, val ); } ); break;
						case 13: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 13, val ); } ); break;
						case 14: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 14, val ); } ); break;
						case 15: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 15, val ); } ); break;
						case 16: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 16, val ); } ); break;
						case 17: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 17, val ); } ); break;
						case 18: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 18, val ); } ); break;
						case 19: Event.AddListener( ( val ) => { onChgEvent?.Invoke( 19, val ); } ); break;
					}
				}
			}
		}
		static public void SetToggleName( this UIToggleGroup obj, int index, string text )
			=> SetTabName( obj, index, text );
		static public void SetToggleNames( this UIToggleGroup obj, params string[] args )
			=> SetTabNames( obj, args );
		//static public void SetToggleNameKey( this UIToggleGroup obj, int index, string key )
		//	=> SetTabNameKey( obj, index, key );
		static public void SetToggleValue( this UIToggleGroup obj, int index, bool isOn, bool triggerValueChanged = true )
		{
			if( obj != null )
			{
				if( 0 <= index && index < obj.toggles.Count )
					obj.toggles[index].SetIsOn( isOn, true, triggerValueChanged );
				else
					obj.SetAllTogglesOff( true, false );
			}
		}
		static public bool GetOnOffValue( this UIToggleGroup obj, int index )
		{
			if( obj != null && index < obj.toggles.Count )
				return obj.toggles[index].isOn;
			return false;
		}
		static public void SetActiveToggle( this UIToggleGroup obj, int index, bool value )
			=> SetActiveTab( obj, index, value );
		static public void SetActiveToggleAll( this UIToggleGroup obj, bool value )
			=> SetActiveTabAll( obj, value );

		static public void InitInputField( this TMP_InputField obj, UnityAction<string> onSubmit, UnityAction<string> onEndEdit = null, UnityAction<string> onValueChanged = null )
		{
			if( obj != null )
			{
				obj.onSubmit.AddListener( onSubmit );
				if( onEndEdit != null )
					obj.onEndEdit.AddListener( onEndEdit );
				if( onValueChanged != null )
					obj.onValueChanged.AddListener( onValueChanged );
			}
		}
		static public void SetTextEx( this TMP_InputField obj, string text )
		{
			if( obj != null )
				obj.text = text;
		}
		static public void SetTextAtPlaceHolder( this TMP_InputField obj, string text )
		{
			if( obj != null )
			{
				var txt_placeholder = obj.placeholder?.GetComponent<TMP_Text>() ?? null;
				if( txt_placeholder != null )
					txt_placeholder.text = text;
			}
		}
		/*
		static public void SetTextKeyAtPlaceHolder( this TMP_InputField obj, string key )
			=> SetTextAtPlaceHolder( obj, InGameMessageTable.GetText( key ) );
		static public void SetCharacterLimit( this TMP_InputField obj, int value )
		{
			if( obj != null )
				obj.characterLimit = Mathf.Max( 0, value );
		}
		static public void SetCharacterLimit( this TMP_InputField obj, string globalTableKey, int defValue = 0 )
			=> SetCharacterLimit( obj, GlobalTable.GetValue( globalTableKey, defValue ) );
		static public void SetTextClearedRichTextTags( this TMP_InputField obj, string text )
		{
			if( obj != null )
				obj.SetTextEx( ClearRichTextTags( text ) );
		}
	    */
		static public void InitSliderEvent( this Slider obj, UnityAction<float> onChange )
		{
			if( obj != null )
				obj.onValueChanged.AddListener( onChange );
		}
		static public void SetSliderRange( this Slider obj, float minValue, float maxValue )
		{
			if( obj != null )
			{
				obj.minValue = minValue;
				obj.maxValue = maxValue;
			}
		}
		static public void SetSliderValue( this Slider obj, float value )
		{
			if( obj != null )
				obj.value = value;
		}

		static public void CopyRectTransform( this RectTransform rect, RectTransform copy, bool isRebuild = true )
		{
			rect.anchorMin = copy.anchorMin;
			rect.anchorMax = copy.anchorMax;
			rect.anchoredPosition = copy.anchoredPosition;
			rect.sizeDelta = copy.sizeDelta;
			rect.pivot = copy.pivot;
			rect.rotation = copy.rotation;
			rect.localScale = copy.localScale;

			if( isRebuild )
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate( rect );
			}
		}
		public static T FindDeepChild<T>( this Transform parent, string name, bool ignoreCase = true
			, bool ignoreSpaces = true ) where T : UnityEngine.Object
		{
			var targetName = ignoreSpaces ? name.Replace( " ", "" ) : name;
			targetName = ignoreCase ? targetName.ToLower() : targetName;

			foreach( Transform child in parent )
			{
				var childName = ignoreSpaces ? child.name.Replace( " ", "" ) : child.name;
				childName = ignoreCase ? childName.ToLower() : childName;

				if( childName == targetName )
				{
					var component = child.GetComponent<T>();
					if( component != null )
						return component;
				}

				var result = FindDeepChild<T>( child, name, ignoreCase, ignoreSpaces );
				if( result != null )
					return result;
			}

			return null;
		}
	}
}
